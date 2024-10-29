using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class movePlayer : MonoBehaviour
{
    public Camera camera;
    public GameObject defaultBulletPrefab;
    public GameObject rocketBulletPrefab;
    public GameObject flameBulletPrefab;
    public float bulletSpeed = 10f;
    private int bulletCount = 0;

    public TextMeshProUGUI bulletCountText;

    public TextMeshProUGUI PlayerBulletCountText;

    public int bulletLimit = 10;

    public Transform obj;

    private Transform gun;

    public GameObject handgun;
    public GameObject rocketGun;
    public GameObject fireGun;

    public GameObject losingText;

    private float force = 5f;
    public float handgunForce = 5f;
    public float rocketForce = 10f;
    public float fireForce = 2f;



    Vector2 playerPos;
    Vector2 mousePos;
    float mousePosY, mousePosX;

    private Rigidbody2D rb;
    private Vector3 direction;

    private bool gameOver = false;

    private float offsetDistance = 0f;

    private int inHandGun = 0;
    private int rocketBullets = 0;
    private int rocketBulletsLimit = 0;
    private int flameBullets = 0;
    private int flameBulletsLimit = 0;

    public int specialGunCollected = 0;
    public int specialGunUsed = 0;
    public int questionBoxTouched = 0;

    public PauseMenuController pauseMenuController;


    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        // set bullet collision gameOver to false
        BulletCollision.setGameOver(false);
        ExplosionDamage.setGameOver(false);

        rb = GetComponent<Rigidbody2D>();
        // make bullet count text invisible by setting it's alpha to 0
        bulletCountText.color = new Color(bulletCountText.color.r, bulletCountText.color.g, bulletCountText.color.b, 0);

        bulletCountText.text = "Bullets: " + (bulletLimit - bulletCount) + "/" + bulletLimit;
        PlayerBulletCountText.text = "" + (bulletLimit - bulletCount);

        handgun.SetActive(true);
        rocketGun.SetActive(false);
        fireGun.SetActive(false);

        gun = handgun.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Game Over mechanics
        if (gameOver)
        {
            return;
        }
        if (BulletCollision.isGameOver())
        {
            gameOver = true;
        }
        if (ExitDoor.isGameOver())
        {
            gameOver = true;
        }
        if (ExplosionDamage.isGameOver())
        {
            gameOver = true;
        }

        if (bulletCount >= bulletLimit && rb.velocity.magnitude < 0.00001f && rocketBullets <= 0 && flameBullets <= 0)
        {
            Debug.Log("Game Over as you have no bullets left");
            gameOver = true;
            losingText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Game Over\nNo bullets left!";
            losingText.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(400, losingText.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y);
            losingText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            Instantiate(losingText, new Vector3(0, 800, 0), Quaternion.identity);
            PauseMenuController pauseMenuController = FindObjectOfType<PauseMenuController>();
            pauseMenuController.ShowGamePauseMenuDelay();
            LevelController levelController = FindObjectOfType<LevelController>();
            levelController.increNoBullet();
            levelController.increNumOfTries();
            pauseMenuController.EndGame();
        }

        // If paused, do not allow shooting
        if (pauseMenuController != null && pauseMenuController.IsPaused())
        {
            return;
        }

        // Shooting Mechanics
        if (Input.GetMouseButtonDown(0))
        {
            if (bulletCount < bulletLimit || flameBullets > 0 || rocketBullets > 0)
            {
                playerPos = obj.transform.position;//gets player position
                mousePos = Input.mousePosition;//gets mouse postion
                mousePos = camera.ScreenToWorldPoint(mousePos);
                mousePosX = mousePos.x - playerPos.x;//gets the distance between object and mouse position for x
                mousePosY = mousePos.y - playerPos.y;//gets the distance between object and mouse position for y  if you want this.
                float dirX = mousePosX / (Mathf.Abs(mousePosX) + Mathf.Abs(mousePosY));
                float dirY = mousePosY / (Mathf.Abs(mousePosX) + Mathf.Abs(mousePosY));

                Vector2 accelerationForce = new Vector2(-dirX, -dirY) * force;
                ShootBullet(inHandGun, accelerationForce);
            }

        }
        switch (inHandGun) {
            case 0:
                bulletCountText.text = "Bullets: " + (bulletLimit - bulletCount) + "/" + bulletLimit;
                PlayerBulletCountText.text = "" + (bulletLimit - bulletCount);
                break;
            case 1:
                bulletCountText.text = "Rocket Bullets: " + (rocketBullets) + "/" + rocketBulletsLimit;
                PlayerBulletCountText.text = "" + (rocketBullets);
                break;
            case 2:
                bulletCountText.text = "Flame Bullets: " + (flameBullets) + "/" + flameBulletsLimit;
                PlayerBulletCountText.text = "" + (flameBullets);
                break;
            default:
                break;
        }
        
        // Switching guns
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inHandGun = 0;
            force = handgunForce;
            handgun.SetActive(true);
            rocketGun.SetActive(false);
            fireGun.SetActive(false);
            gun = handgun.transform;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inHandGun = 1;
            force = rocketForce;
            handgun.SetActive(false);
            rocketGun.SetActive(true);
            fireGun.SetActive(false);
            gun = rocketGun.transform;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inHandGun = 2;
            force = fireForce;
            handgun.SetActive(false);
            rocketGun.SetActive(false);
            fireGun.SetActive(true);
            gun = fireGun.transform;
        }
    }

    void ShootBullet(int bullettype = 0, Vector2 accelerationForce = new Vector2())
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set z to 0 since we're in 2D

        // Get the player's position (where the bullet will spawn)
        Vector3 playerPosition = transform.position;

        // Calculate the direction
        Vector2 direction = (mousePosition - playerPosition).normalized;
        Vector3 direction3 = new Vector3(direction.x, direction.y, 0);

        Vector3 bulletSpawnPosition = playerPosition + direction3 * offsetDistance;
        bool canShoot = true;

        if (bullettype == 0)
        {
            if (bulletCount >= bulletLimit)
            {
                canShoot = false;
            }
            else if (bulletCount < bulletLimit)
            {
                bulletCount++;
            }
            shootBulletofType(canShoot, defaultBulletPrefab, bulletSpawnPosition, direction, accelerationForce);
        }
        else if (bullettype == 1)
        {
            if (rocketBullets <= 0)
            {
                canShoot = false;
            }
            else
            {
                specialGunUsed++;
                rocketBullets--;
            }
            shootBulletofType(canShoot, rocketBulletPrefab, bulletSpawnPosition, direction, accelerationForce);
        }
        else if (bullettype == 2)
        {
            if (flameBullets <= 0)
            {
                canShoot = false;
            }
            else
            {
                specialGunUsed++;
                flameBullets--;
            }
            shootBulletofType(canShoot, flameBulletPrefab, bulletSpawnPosition, direction, accelerationForce);
        }
    }

    void shootBulletofType(bool canShoot, GameObject prefab, Vector3 bulletSpawnPosition, Vector2 direction, Vector2 accelerationForce)
    {
        if (!canShoot)
        {
            Debug.Log("Cannot shoot");
            return;
        }

        GameObject bullet = Instantiate(prefab, bulletSpawnPosition, Quaternion.identity);

        Rigidbody2D rb_bullet = bullet.GetComponent<Rigidbody2D>();
        if (rb_bullet != null)
        {
            rb_bullet.velocity = direction * bulletSpeed;
            rb.AddForce(accelerationForce, ForceMode2D.Impulse);
        }

        Debug.Log("Bullet count: " + bulletCount);

        Destroy(bullet, 2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "GunPowerup")
        {
            Debug.Log("Gun powerup, " + other.gameObject.name + " collected");
            specialGunCollected++;
            if (other.name == "RocketPowerup")
            {
                rocketBullets++;
                rocketBulletsLimit++;
                Debug.Log("Rocket bullets: " + rocketBullets);
            }
            else if (other.name == "FlamethrowerPowerup")
            {
                flameBullets++;
                flameBulletsLimit++;
                Debug.Log("Flame bullets: " + flameBullets);
            }
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "lava")
        {
            Debug.Log("Game Over");
            if (!gameOver)
            {
                gameOver = true;
                losingText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Game Over!\nYou died by Lava!";
                losingText.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(400, losingText.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y);
                losingText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                Instantiate(losingText, new Vector3(0, 0, 0), Quaternion.identity);
                LevelController levelController = FindObjectOfType<LevelController>();
                levelController.increTouchLava();
                levelController.increNumOfTries();                
                PauseMenuController pauseMenuController = FindObjectOfType<PauseMenuController>();
                pauseMenuController.ShowGamePauseMenuDelay();
                pauseMenuController.EndGame();
            }
        }
    }

    public int getBulletUsed()
    {
        return bulletCount;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
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

    void setPlayerBulletCountText(int bulletCount)
    {
        PlayerBulletCountText.text = "" + bulletCount;
        if (bulletCount <= 5)
        {
            PlayerBulletCountText.fontSize = 46;
        }
        else
        {
            PlayerBulletCountText.fontSize = 36;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Call the update function in LavaAlphaController
        LavaAlphaController.UpdateLavaAlpha();       
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

        // If paused, do not allow shooting
        PauseMenuController pauseMenuController = FindObjectOfType<PauseMenuController>();       
        if (pauseMenuController != null && pauseMenuController.IsPaused())
        {
            return;
        }

        GunPopUp gunPopUp = GetComponent<GunPopUp>();
        if (gunPopUp != null && gunPopUp.IsPaused())
        {
            return;
        }

        if (bulletCount >= bulletLimit && rb.velocity.magnitude < 0.00001f && rocketBullets <= 0 && flameBullets <= 0)
        {
            Debug.Log("Game Over as you have no bullets left");
            gameOver = true;
            pauseMenuController.ShowGamePauseMenu(false, "Game Over\nNo bullets left!");
            LevelController levelController = FindObjectOfType<LevelController>();
            levelController.increNoBullet();
            levelController.increNumOfTries();
            pauseMenuController.EndGame();
        }

        AutoSwitchGunIfNecessary();

        // Shooting Mechanics
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
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
                LevelController levelController = FindObjectOfType<LevelController>();
                Vector2 playerLocation = camera.ScreenToWorldPoint(playerPos);
                levelController.recordPlayerLocation(playerLocation);
                AutoSwitchGunIfNecessary();
            }

        }
        switch (inHandGun)
        {
            case 0:
                bulletCountText.text = "Bullets: " + (bulletLimit - bulletCount) + "/" + bulletLimit;
                setPlayerBulletCountText(bulletLimit - bulletCount);
                break;
            case 1:
                bulletCountText.text = "Rocket Bullets: " + (rocketBullets) + "/" + rocketBulletsLimit;
                setPlayerBulletCountText(rocketBullets);
                break;
            case 2:
                bulletCountText.text = "Flame Bullets: " + (flameBullets) + "/" + flameBulletsLimit;
                setPlayerBulletCountText(flameBullets);
                break;
            default:
                break;
        }

        // Switching guns
        if (Input.GetKeyDown(KeyCode.Alpha1) && bulletCount < bulletLimit)
        {
            inHandGun = 0;
            force = handgunForce;
            handgun.SetActive(true);
            rocketGun.SetActive(false);
            fireGun.SetActive(false);
            gun = handgun.transform;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && rocketBullets > 0)
        {
            inHandGun = 1;
            force = rocketForce;
            handgun.SetActive(false);
            rocketGun.SetActive(true);
            fireGun.SetActive(false);
            gun = rocketGun.transform;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && flameBullets > 0)
        {
            inHandGun = 2;
            force = fireForce;
            handgun.SetActive(false);
            rocketGun.SetActive(false);
            fireGun.SetActive(true);
            gun = fireGun.transform;
        }
    }

    bool CanShootCurrentGun()
    {
        return (inHandGun == 0 && bulletCount < bulletLimit) ||
               (inHandGun == 1 && rocketBullets > 0) ||
               (inHandGun == 2 && flameBullets > 0);
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
            if (other.name.StartsWith("Rocket"))
            {
                rocketBullets += other.gameObject.GetComponent<SpecialBulletControl>().bulletNum;
                rocketBulletsLimit += other.gameObject.GetComponent<SpecialBulletControl>().bulletNum;
                Debug.Log("Rocket bullets: " + rocketBullets);
            }
            else if (other.name.StartsWith("Flame"))
            {
                // get variable other bulletNum from SpecialBulletControl
                flameBullets += other.gameObject.GetComponent<SpecialBulletControl>().bulletNum;
                flameBulletsLimit += other.gameObject.GetComponent<SpecialBulletControl>().bulletNum;
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
                LevelController levelController = FindObjectOfType<LevelController>();
                levelController.increTouchLava();
                levelController.increNumOfTries();
                PauseMenuController pauseMenuController = FindObjectOfType<PauseMenuController>();
                pauseMenuController.ShowGamePauseMenu(false, "Game Over!\nYou died by Lava!");
                pauseMenuController.EndGame();
            }
        }
    }

    void AutoSwitchGunIfNecessary()
    {
        if (inHandGun == 0 && bulletCount >= bulletLimit) // Handgun out of bullets
        {
            if (rocketBullets > 0)
            {
                SwitchToRocketGun();
            }
            else if (flameBullets > 0)
            {
                SwitchToFlameGun();
            }
        }
        else if (inHandGun == 1 && rocketBullets <= 0) // Rocket gun out of bullets
        {
            if (bulletCount < bulletLimit)
            {
                SwitchToHandgun();
            }
            else if (flameBullets > 0)
            {
                SwitchToFlameGun();
            }
        }
        else if (inHandGun == 2 && flameBullets <= 0) // Flame gun out of bullets
        {
            if (bulletCount < bulletLimit)
            {
                SwitchToHandgun();
            }
            else if (rocketBullets > 0)
            {
                SwitchToRocketGun();
            }
        }
    }

    public void SwitchToHandgun()
    {
        inHandGun = 0;
        force = handgunForce;
        handgun.SetActive(true);
        rocketGun.SetActive(false);
        fireGun.SetActive(false);
        gun = handgun.transform;
        Debug.Log("Switched to Handgun");
    }

    public void SwitchToRocketGun()
    {
        inHandGun = 1;
        force = rocketForce;
        handgun.SetActive(false);
        rocketGun.SetActive(true);
        fireGun.SetActive(false);
        gun = rocketGun.transform;
        Debug.Log("Switched to Rocket Gun");
    }

    public void SwitchToFlameGun()
    {
        inHandGun = 2;
        force = fireForce;
        handgun.SetActive(false);
        rocketGun.SetActive(false);
        fireGun.SetActive(true);
        gun = fireGun.transform;
        Debug.Log("Switched to Flame Gun");
    }

    public int getBulletUsed()
    {
        return bulletCount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject reticle;
    [SerializeField] GameObject reticlecenter;
    //bool spacepressed = false;
    Rigidbody2D rb;
    Vector2 playerpos;
    Vector2 reticlepos;
    Vector2 direction;
    [SerializeField] float speed = 10f;
    [SerializeField] float regGrav = 1.0f;
    [SerializeField] float wallGrav = 0.5f;
    [SerializeField] int numJumps = 1;
    int currentJumps;
    bool onWall = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentJumps = numJumps;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("space") && currentJumps > 0)
        {
            playerpos = (Vector2)transform.position;
            reticlepos = (Vector2)reticle.transform.position;
            direction = reticlepos - playerpos;
            if (currentJumps < numJumps || onWall)
            {
                rb.velocity = Vector3.zero;
            }
            
            rb.AddForce(direction.normalized * speed);
            currentJumps--;
        }
        if (Input.GetKeyUp("r"))
        {
            string currentscene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentscene);
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            currentJumps = numJumps;
            onWall = true;
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = wallGrav;
            }
        }
    }
   

    private void OnCollisionStay2D(Collision2D col)
    {
        
        if (col.gameObject.CompareTag("Floor"))
        {
       
            currentJumps = numJumps;
        }
        
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            rb.gravityScale = regGrav;
            onWall = false;
        }
        if (col.gameObject.CompareTag("Floor"))
        {
            currentJumps = numJumps -1 ;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxControl : MonoBehaviour
{
    public bool isTrap;
    public GameObject player;
    public int extraBullet;
    public GameObject[] hiddenNPCs;
    
    // Start is called before the first frame update
    void Start()
    {
        if (isTrap)
        {
            foreach (GameObject hiddenNPC in hiddenNPCs)
            {
                hiddenNPC.SetActive(false);
            }
        }
    }

    void showHiddenNPC()
    {
        // show hidden NPC
        foreach (GameObject hiddenNPC in hiddenNPCs)
        {
            hiddenNPC.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit the question box");
            if (!isTrap) {
                player.GetComponent<movePlayer>().bulletLimit += extraBullet;
            }else {
                showHiddenNPC();
            }

            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    // Start is called before the first frame update
    static bool gameFail = false;
    public GameObject losingText;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static bool isGameOver()
    {
        return gameFail;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            Instantiate(losingText, new Vector3(0, 800, 0), Quaternion.identity);
            gameFail = true;
        }
    }
}

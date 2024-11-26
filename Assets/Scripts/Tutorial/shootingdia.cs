using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class shootingdia : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject gate2;

    private List<string> begindialog = new List<string> {
        "Use Mouse left to shoot; \n1,2 to switch weapon",
        "R to reload"
    };

    private int index = 0;
    private float timer = 0f;
    public float interval = 2f;

    void Start()
    {
        text.text = begindialog[index];
        gate2.SetActive(true);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0f;
            index++;

            if (index < begindialog.Count)
            {
                text.text = begindialog[index];
            }
            else
            {
                Debug.Log("End of shoot dialog.");
                Destroy(text.gameObject);
            }
        }
    }
}

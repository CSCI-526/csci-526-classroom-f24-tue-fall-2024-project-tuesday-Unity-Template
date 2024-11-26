using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class leveltext : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject gate3;

    private List<string> begindialog = new List<string> {
        "Zombies has different infection levels"
    };

    private int index = 0;
    private float timer = 0f;
    public float interval = 2f;

    void Start()
    {
        text.text = begindialog[index];
        gate3.SetActive(true);
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
                Debug.Log("End of cure dialog.");
                Destroy(text.gameObject);
            }
        }
    }
}

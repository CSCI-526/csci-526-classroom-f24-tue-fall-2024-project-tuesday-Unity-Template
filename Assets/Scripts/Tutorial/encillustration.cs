using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class encillustration : MonoBehaviour
{
    public TextMeshProUGUI Board;
    public int ind = 0;
    public int count = 0;
    public int killed = 0;
    private bool show = false;

    void changback()
    {
        ind = 0;
        show = false;
    }
    // Start is called before the first frame update
    void showBoard(int ind)
    {
        if(ind == 0)
        {
            Board.text = "";
        }
        else if(ind == 1)
        {
            Board.text = "<color=red>-$50</color>";
            Invoke("changback", 2.0f);
        }
        else if(ind == 2)
        {
            Board.text = "<color=#00FF00>+$100</color>";
            Invoke("changback", 2.0f);
        }
        else
        {
            Debug.Log("error");
        }
    }

    private void Start()
    {
        count = curedCount.Instance.count;
        killed = curedCount.Instance.killed;
    }
    // Update is called once per frame
    void Update()
    {
        int ccount = curedCount.Instance.count;
        int ckilled = curedCount.Instance.killed;
        int id = 0;
        if(ccount > count)
        {
            id = 2;
            count = ccount;
            show = true;
            showBoard(id);
        }
        if(ckilled > killed)
        {
            id = 1;
            killed = ckilled;
            show = true;
            showBoard(id);
        }
        if (show)
        {
            Invoke("changback", 2.0f);
        }
        else {
            showBoard(0);
        }
    }
}

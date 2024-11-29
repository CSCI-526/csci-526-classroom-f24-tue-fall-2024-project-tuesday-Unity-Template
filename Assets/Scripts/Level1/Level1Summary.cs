using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level1Summary : MonoBehaviour
{
    public TextMeshProUGUI sum;

    // Update is called once per frame
    void Update()
    {
        sum.text = $"{"Killed: " + curedCount.Instance.killed + "\nCured: " + curedCount.Instance.count + "\nTotal: " + (curedCount.Instance.killed * -50 + curedCount.Instance.count*100)}";
    }

    public void calculation()
    {
        int sum = (curedCount.Instance.killed * -50 + curedCount.Instance.count * 100);
        if(sum >= 0)
        {
            PlayerResource.Instance.inc_money(sum);
        }
        else
        {
            sum *= -1;
            PlayerResource.Instance.Dec_money(sum, 1);
        }
        
    }
}

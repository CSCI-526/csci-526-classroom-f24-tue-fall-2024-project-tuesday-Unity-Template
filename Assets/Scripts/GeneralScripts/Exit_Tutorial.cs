using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit_Tutorial : MonoBehaviour
{
    public GameObject leaveUI;
    bool can_trigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && can_trigger && curedCount.Instance.level_mission <= curedCount.Instance.count)
        {
            leaveUI.GetComponent<RangeUIControal_tutorial>().showWinUI();
            can_trigger = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            can_trigger = true;
        }
    }
}

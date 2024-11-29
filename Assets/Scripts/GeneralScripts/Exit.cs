using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject leaveUI;
    bool can_trigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && can_trigger && curedCount.Instance.level_mission <= curedCount.Instance.count)
        {
            leaveUI.GetComponent<rangeUIControl>().showWinUI();
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

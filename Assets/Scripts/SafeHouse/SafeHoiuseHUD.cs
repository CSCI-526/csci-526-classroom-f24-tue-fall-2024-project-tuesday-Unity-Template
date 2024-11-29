using TMPro;
using UnityEngine;

public class SafeHoiuseHUD : MonoBehaviour
{

    public TextMeshProUGUI Money;

    private void Update()
    {
        Money.text = $"{"$ " + PlayerResource.Instance.GetPlayermonney() + "\nPistol Ammo: " + PlayerResource.Instance.GetPlayerPis_ammo() + "\nRifel Ammo: " + PlayerResource.Instance.GetPlayerRif_ammo() + "\nAntidote: " + PlayerResource.Instance.GetPlayeranti()}";

    }
}

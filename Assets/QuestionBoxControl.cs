using System.Collections;
using UnityEngine;
using TMPro;

public class QuestionBoxControl : MonoBehaviour
{
    public bool isTrap;
    public GameObject player;
    public int extraBullet;
    public GameObject[] hiddenNPCs;
    public RectTransform textRectTransform;
    public PopUpTextManager popUpTextManager;
    public float textYOffset = 0.0f; // Offset above the box
    public float textXOffset = 0.0f;

    void SetPopUpText(string message)
    {
        Vector3 worldPosition = transform.position;
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        textRectTransform.position = screenPosition + new Vector2(textXOffset, textYOffset);
        popUpTextManager.ShowText(message);
    }

    void Start()
    {
        textRectTransform.gameObject.SetActive(false);
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
            player.GetComponent<movePlayer>().questionBoxTouched += 1;
            if (!isTrap) {
                player.GetComponent<movePlayer>().bulletLimit += extraBullet;
                SetPopUpText("+" + extraBullet + " bullets!");
            }
            else
            {
                showHiddenNPC();
                SetPopUpText("Oops! A trap!");
            }

            gameObject.SetActive(false); // Hide the question box
        }
    }

}
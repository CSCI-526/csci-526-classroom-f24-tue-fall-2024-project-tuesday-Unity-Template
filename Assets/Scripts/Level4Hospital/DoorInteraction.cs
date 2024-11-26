using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorInteraction : MonoBehaviour
{
    public Transform player;       
    public float interactionDistance = 5f;
    public float rotationAngle = 90f;      
    public float rotationSpeed = 2f;      
    public TMP_Text interactionPrompt;          


    private bool isRotated = false;        
    private Quaternion targetRotation;     
    private bool hasPromptShown = false;      

    void Start()
    {
        targetRotation = transform.rotation;

       /* if (interactionPrompt != null)
        {
            interactionPrompt.gameObject.SetActive(false);
        }*/
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        /*if (interactionPrompt != null && distanceToPlayer <= interactionDistance && !hasPromptShown)
        {
            StartCoroutine(ShowPromptOnce());
            hasPromptShown = true;


        }*/
     

        if (distanceToPlayer <= interactionDistance && Input.GetKeyDown(KeyCode.F))
        {
            isRotated = !isRotated;

            targetRotation = isRotated ? Quaternion.Euler(transform.eulerAngles + new Vector3(0, rotationAngle, 0)) : Quaternion.Euler(transform.eulerAngles - new Vector3(0, rotationAngle, 0));
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
   /* IEnumerator ShowPromptOnce()
    {
        interactionPrompt.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);

        interactionPrompt.gameObject.SetActive(false);
    }*/
}

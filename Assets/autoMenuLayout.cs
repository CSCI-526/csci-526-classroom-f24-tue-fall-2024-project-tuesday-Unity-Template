using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class autoMenuLayout : MonoBehaviour
{
    float horizontalPaddingPercentage = 0.25F;
    float verticalPaddingPercentage = 0.1F;
    float verticalSpacingPercentage = 0.1F;
    float fontSizePercentage = 0.05F;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateMenuLayout();
    }


    private void updateMenuLayout()
    {
        
        RectTransform parentRect = GetComponent<RectTransform>();
 
        VerticalLayoutGroup layoutGroup = GetComponent<VerticalLayoutGroup>();

        if (layoutGroup != null)
        {
            layoutGroup.childControlHeight = true;
            layoutGroup.childControlWidth = true;
            layoutGroup.childForceExpandHeight = true;
            layoutGroup.childForceExpandWidth = true;
            layoutGroup.padding.left = (int)Mathf.Floor(horizontalPaddingPercentage * parentRect.rect.width);
            layoutGroup.padding.right = (int)Mathf.Floor(horizontalPaddingPercentage * parentRect.rect.width);
            layoutGroup.padding.top = (int)Mathf.Floor(verticalPaddingPercentage * parentRect.rect.height);
            layoutGroup.padding.bottom = (int)Mathf.Floor(verticalPaddingPercentage * parentRect.rect.height);
            layoutGroup.spacing = (int)Mathf.Floor(verticalSpacingPercentage * parentRect.rect.height);
            
            // Traverse each child and adjust font size
            foreach (Transform child in transform)
            {
                TraverseAndAdjustFontSize(child, parentRect.rect.width);
            }

            // Force layout update
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.GetComponent<RectTransform>());
        }


    }

    void TraverseAndAdjustFontSize(Transform obj, float parentWidth)
    {
        
        TextMeshProUGUI tmpComponent = obj.GetComponentInChildren<TextMeshProUGUI>();
        if (tmpComponent != null)
        {
            tmpComponent.fontSize = parentWidth * fontSizePercentage;
        }
        // Recursively check child objects for text
        foreach (Transform child in obj)
        {
            TraverseAndAdjustFontSize(child, parentWidth);
        }
    }
}

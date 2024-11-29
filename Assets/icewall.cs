using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icewall : MonoBehaviour
{
    private int hitCount = 0; 
    public int requiredHits = 5; 

    private SpriteRenderer spriteRenderer;

    // Initialize and get the SpriteRenderer component
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void onCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Bullet (Clone)")
        {
            RegisterHit(); 
        }
    }

    public void RegisterHit(int hitNum = 1)
    {
        hitCount += hitNum; 
        Debug.Log("Ice Wall hit count: " + hitCount);
        // Update transparency based on the number of hits
        UpdateTransparency();
        Debug.Log("Ice Wall hit count: " + hitCount);
        if (hitCount >= requiredHits)
        {
            Destroy(gameObject);
            Debug.Log("Ice Wall destroyed after 5 hits.");
        }
    }

    private void UpdateTransparency()
    {
        if (spriteRenderer != null)
        {
            // Calculate new opacity (transparency decreases with each hit)
            float newOpacity = Mathf.Clamp(1.0f - (hitCount / (float)requiredHits), 0f, 1f);
            
            // Get current color of the sprite and update its alpha value
            Color color = spriteRenderer.color;
            color.a = newOpacity;
            spriteRenderer.color = color;

            Debug.Log("Ice Wall transparency updated: " + newOpacity);
        }
    }
}

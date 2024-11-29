using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetscript : MonoBehaviour
{
    private Renderer objectRender;

    private void Start()
    {
        objectRender = GetComponent<Renderer>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value);
            objectRender.material.color = randomColor;
            Destroy(collision.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destory_after_delay : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        DestroyAfterDelay();
    }

    public void DestroyAfterDelay()
    {
        Invoke("DestroyGameObject", 3f);
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}

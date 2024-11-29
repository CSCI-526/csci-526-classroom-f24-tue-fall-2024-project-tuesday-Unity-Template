using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light flashlight;          // 将手电筒的光源拖放到此变量中
    private bool isFlashlightOn = false; // 用于存储手电筒的当前状态
    public float maxDistance = 40f;      // 手电筒照射的最大距离

    void Start()
    {
        // 初始时关闭手电筒
        flashlight.enabled = isFlashlightOn;
    }

    void Update()
    {
        // 监听鼠标右键 (默认是鼠标右键)
        if (Input.GetMouseButtonDown(1)) // 1表示鼠标右键
        {
            // 切换手电筒的开关状态
            isFlashlightOn = !isFlashlightOn;
            flashlight.enabled = isFlashlightOn;
        }

        // 当手电筒开启时，进行射线检测
        if (isFlashlightOn)
        {
            CheckForZombies();
        }
        else
        {
            // 如果手电筒关闭，则将所有僵尸的 isIlluminated 设为 false
            ResetZombieIllumination();
        }
    }

    // 检查射线是否照射到僵尸
    void CheckForZombies()
    {
        RaycastHit hit;
        Ray ray = new Ray(flashlight.transform.position, flashlight.transform.forward);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            Zombie zombie = hit.collider.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.SetIlluminated(true);
            }
        }
        else
        {
            // 如果射线没有照到僵尸，重置所有僵尸的照射状态
            ResetZombieIllumination();
        }
    }

    // 重置所有僵尸的照射状态
    void ResetZombieIllumination()
    {
        Zombie[] zombies = FindObjectsOfType<Zombie>();
        foreach (Zombie zombie in zombies)
        {
            zombie.SetIlluminated(false);
        }
    }
}

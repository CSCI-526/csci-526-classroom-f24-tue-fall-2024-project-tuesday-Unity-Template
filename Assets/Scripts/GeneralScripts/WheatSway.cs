using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    public float swayAmount = 10f;  // 摆动幅度（角度）
    public float swaySpeed = 1f;   // 摆动速度
    public float swayOffset = 0f;  // 每根稻草的偏移值，用于让摆动看起来不统一

    private Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.localRotation; // 保存初始旋转
        swayOffset = Random.Range(0f, 100f);        // 随机偏移，避免所有稻草同步摆动
    }

    void Update()
    {
        // 使用 Perlin Noise 生成平滑的摆动角度
        float swayAngle = Mathf.PerlinNoise(Time.time * swaySpeed + swayOffset, 0f) * swayAmount - (swayAmount / 2);
        transform.localRotation = originalRotation * Quaternion.Euler(0, 0, swayAngle);
    }
}

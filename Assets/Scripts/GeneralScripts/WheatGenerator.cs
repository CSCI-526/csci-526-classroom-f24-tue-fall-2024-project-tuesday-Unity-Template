using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatFieldGenerator : MonoBehaviour
{
    public GameObject wheatPrefab;  // 稻草的预制件
    public int numberOfClusters = 5; // 稻草簇的数量
    public int minWheatPerCluster = 5; // 每个簇最少生成稻草数量
    public int maxWheatPerCluster = 20; // 每个簇最多生成稻草数量
    public float clusterRadius = 5f; // 每个簇的半径范围
    public float checkRadius = 1f;  // 检测范围半径，避免重叠障碍物
    public Vector2 fieldSize = new Vector2(50, 50); // 地图大小（宽度和深度）

    void Start()
    {
        GenerateClusters();
    }

    void GenerateClusters()
    {
        for (int i = 0; i < numberOfClusters; i++)
        {
            // 随机选择簇的中心点
            Vector3 clusterCenter = new Vector3(
                Random.Range(-fieldSize.x / 2, fieldSize.x / 2),
                0f,
                Random.Range(-fieldSize.y / 2, fieldSize.y / 2)
            );

            // 确保簇中心点不在障碍物上
            if (IsPositionBlocked(clusterCenter))
                continue;

            // 在该簇生成稻草
            int wheatCount = Random.Range(minWheatPerCluster, maxWheatPerCluster);
            GenerateWheatInCluster(clusterCenter, wheatCount);
        }
    }

    void GenerateWheatInCluster(Vector3 clusterCenter, int wheatCount)
    {
        for (int i = 0; i < wheatCount; i++)
        {
            // 簇内随机生成稻草位置
            Vector3 position = clusterCenter + new Vector3(
                Random.Range(-clusterRadius, clusterRadius),
                0f,
                Random.Range(-clusterRadius, clusterRadius)
            );

            // 检查该位置是否被障碍物占据
            if (!IsPositionBlocked(position))
            {
                Instantiate(wheatPrefab, position, Quaternion.Euler(0, Random.Range(0, 360), 0));
            }
        }
    }

    bool IsPositionBlocked(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, checkRadius);
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Obstacle")) // 检查是否有 Obstacle 标签的物体
            {
                return true;
            }
        }
        return false;
    }
}

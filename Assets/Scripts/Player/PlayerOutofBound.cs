using UnityEngine;

public class PlayerOutOfBounds : MonoBehaviour
{
    public GameObject gameOverUI; // 拖入 GameOver UI
    public float maxZCoordinate = 24f; // 超过此 Z 坐标时触发 GameOver

    void Update()
    {
        if (transform.position.z > maxZCoordinate)
        {
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true); // 显示 GameOver UI
        }

        // 这里可以添加暂停游戏等其他逻辑
    }
}

using UnityEngine;
using UnityEngine.AI;

public class NavmeshControldoor : MonoBehaviour
{
    public NavMeshObstacle doorObstacle; // 引用门上的 NavMeshObstacle

    public void OpenDoor()
    {
        // 当门打开时，禁用 NavMesh 障碍物，允许僵尸通过
        if (doorObstacle != null)
        {
            doorObstacle.enabled = false; // 禁用雕刻，让僵尸通过
        }

        // 添加门的动画或其他打开门的逻辑
    }

    public void CloseDoor()
    {
        // 当门关闭时，启用 NavMesh 障碍物
        if (doorObstacle != null)
        {
            doorObstacle.enabled = true; // 重新启用雕刻，阻止僵尸通过
        }

        // 添加门关闭的逻辑
    }
}

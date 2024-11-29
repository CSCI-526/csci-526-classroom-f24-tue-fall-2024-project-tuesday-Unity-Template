/*using UnityEngine;

public class WallAttraction : MonoBehaviour
{
    public float maxAttractionForce = 200f; // 最大吸引力
    public float minAttractionDistance = 220f; // 吸引距离内的最小吸引距离
    public Transform wallCenter; // 墙中心
    private bool isAttractionActive = false; // 是否激活吸引行为


    void OnTriggerStay(Collider other)
    {
        // 检查是否是Player对象
        if (isAttractionActive && other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();

            if (controller != null)
            {
                Vector3 direction = (wallCenter.position - other.transform.position).normalized;
                float distance = Vector3.Distance(wallCenter.position, other.transform.position);

                // 修改吸引力计算公式，使吸引力随距离减小，但不会直接为0
                float attractionStrength = 200 * maxAttractionForce / (1 + Mathf.Pow(distance / minAttractionDistance, 2));

                // 应用吸引力
                controller.Move(direction * attractionStrength * Time.deltaTime);
            }
        }
    }

    // 激活吸引行为的方法
    public void ActivateAttraction()
    {
        isAttractionActive = true;
    }
}
*/


using UnityEngine;

public class WallAttraction : MonoBehaviour
{
    private float maxAttractionForce = 12f; // 最大吸引力
    private float minAttractionDistance = 1f; // 吸引距离内的最小吸引距离
    public Transform wallCenter; // 墙中心
    private bool isAttractionActive = false; // 是否激活吸引行为
    private float baseAttractionForce; // 用于存储初始的吸引力大小
    private  float attractionReductionPerCure = 0.3f; // 每治愈一个减少的吸引力
    private float forceRevise = 30;


    void Start()
    {
        baseAttractionForce = maxAttractionForce; // 初始化时保存最大吸引力
    }

    void OnTriggerStay(Collider other)
    {
        // 检查是否是Player对象
        if (isAttractionActive && other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();

            if (controller != null)
            {
                Vector3 direction = (wallCenter.position - other.transform.position).normalized;
                float distance = Vector3.Distance(wallCenter.position, other.transform.position);

                // 计算当前吸引力，根据 curedCount.Instance.count 减少吸引力
                float reductionAmount = curedCount.Instance.count * attractionReductionPerCure;
                float currentAttractionForce = Mathf.Max(baseAttractionForce - reductionAmount, 0); // 确保吸引力不低于0

                // 吸引力计算公式，使吸引力随距离减小
                //float attractionStrength = currentAttractionForce / (1 + Mathf.Pow(distance / minAttractionDistance, 2));
                float attractionStrength = forceRevise * (currentAttractionForce / (1 + Mathf.Pow(distance / minAttractionDistance, 1.2f))); // 调整指数

                // 输出调试信息
                Debug.Log("Current Attraction Force: " + attractionStrength + ", Distance: " + distance);

                // 应用吸引力
                controller.Move(direction * attractionStrength * Time.deltaTime);
            }
        }
    }

    // 激活吸引行为的方法
    public void ActivateAttraction()
    {
        isAttractionActive = true;
    }
}

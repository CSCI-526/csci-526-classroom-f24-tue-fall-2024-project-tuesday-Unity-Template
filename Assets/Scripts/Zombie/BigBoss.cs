/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BigBoss : MonoBehaviour
{
    // Health control
    private float health = 160;
    private float Maxhealth = 160;
    public GameObject healthbar;
    public Slider slider;

    // Attack control
    public int attackDamage = 10;       // 攻击造成的伤害
    private float attackInterval = 2f;   // 攻击间隔
    private float attackRange = 1f;    // 攻击距离，只有在该范围内才会开始攻击

    private float attackTimer;          // 计时器，用于控制攻击间隔
    private NavMeshAgent navMeshAgent;  // 用于控制移动
    private Transform target;           // 攻击目标
    private bool isDead = false; // 标志位，表示BigBoss是否已经死亡


    void Start()
    {
        // 初始化健康值
        health = Maxhealth;
        slider.value = calHealth();
        attackTimer = attackInterval; // 初始化攻击计时器

        // 获取 NavMeshAgent 组件
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Check if BigBoss is on a valid NavMesh
        if (!navMeshAgent.isOnNavMesh)
        {
            Debug.LogError("BigBoss is not on a NavMesh! Please ensure it is positioned correctly.");
            return;
        }

        // 找到带有 "Player" 标签的目标
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
            if (navMeshAgent != null)
            {
                navMeshAgent.SetDestination(target.position);
            }
        }
        else
        {
            Debug.LogWarning("No Player object found with the 'Player' tag.");
        }
    }

    void Update()
    {
        // 如果BigBoss已死亡，则停止后续逻辑
        if (isDead) return;
        HealthCheck();

        // 如果指定了目标，检查是否已经靠近目标
        if (target != null)
        {
            // 检查距离，如果在攻击范围内则停止移动并攻击
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget <= attackRange)
            {
                navMeshAgent.isStopped = true;  // 停止移动
                Attack();                       // 开始攻击
            }
            else
            {
                navMeshAgent.isStopped = false; // 继续移动
                navMeshAgent.SetDestination(target.position); // 不断更新目标位置   
            }
        }

        attackTimer += Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private float calHealth()
    {
        return health / Maxhealth;
    }

    private void HealthCheck()
    {
        if (health <= 0 && !isDead)
        {
            isDead = true; // 标记为已死亡，阻止后续Update调用
            curedCount.Instance.killed++;
            Destroy(gameObject);
        }
        if (health > Maxhealth)
        {
            health = Maxhealth;
        }
        slider.value = calHealth();
    }

    private void Attack()
    {
        // 确保攻击间隔
        if (attackTimer >= attackInterval)
        {
            // 假设player有playerState脚本，直接调用其 TakeDamage 方法
            playerState player = target.GetComponent<playerState>();
            if (player != null)
            {
                player.TakeDamage(attackDamage);
                Debug.Log("Attacking Player: " + player.health);
            }
            else
            {
                Debug.LogWarning("No playerState component found on Player target.");
            }

            // 重置攻击计时器
            attackTimer = 0f;
        }
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BigBoss : MonoBehaviour
{
    private float health = 160;
    private float Maxhealth = 160;
    public GameObject healthbar;
    public Slider slider;

    private int attackDamage = 20;
    private float attackInterval = 2f;
    private float attackRange = 1f;

    private float attackTimer;
    private NavMeshAgent navMeshAgent;
    private Transform target;
    private bool isDead = false; // 标志位，表示BigBoss是否已经死亡

    private Renderer bossRenderer; // Renderer组件，用于控制颜色


    void Start()
    {
        health = Maxhealth;
        slider.value = calHealth();
        attackTimer = attackInterval;

        navMeshAgent = GetComponent<NavMeshAgent>();

        // 获取Renderer组件
        bossRenderer = GetComponent<Renderer>();

        if (!navMeshAgent.isOnNavMesh)
        {
            Debug.LogError("BigBoss is not on a NavMesh! Please ensure it is positioned correctly.");
            return;
        }

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
            navMeshAgent.SetDestination(target.position);
        }
        else
        {
            Debug.LogWarning("No Player object found with the 'Player' tag.");
        }
    }

    void Update()
    {
        // 如果BigBoss已死亡，则停止后续逻辑
        if (isDead) return;

        // 调用颜色变化方法
        ChangeColorOverTime();

        HealthCheck();

        if (target != null && navMeshAgent.isOnNavMesh) // 添加navMeshAgent.isOnNavMesh检查
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= attackRange)
            {
                if (!navMeshAgent.isStopped)
                {
                    navMeshAgent.isStopped = true;
                    Attack();
                }
            }
            else
            {
                if (navMeshAgent.isStopped)
                {
                    navMeshAgent.isStopped = false;
                }
                navMeshAgent.SetDestination(target.position);
            }
        }

        attackTimer += Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private float calHealth()
    {
        return health / Maxhealth;
    }

    private void HealthCheck()
    {
        if (health <= 0 && !isDead)
        {
            isDead = true; // 标记为已死亡，阻止后续Update调用
            curedCount.Instance.killed++;

            // 停止所有NavMeshAgent操作，避免错误
            if (navMeshAgent != null && navMeshAgent.isOnNavMesh)
            {
                navMeshAgent.isStopped = true;
            }

            Destroy(gameObject); // 销毁对象
        }

        if (health > Maxhealth)
        {
            health = Maxhealth;
        }
        slider.value = calHealth();
    }

    private void Attack()
    {
        if (attackTimer >= attackInterval)
        {
            playerState player = target.GetComponent<playerState>();
            if (player != null)
            {
                player.TakeDamage(attackDamage);
                Debug.Log("Attacking Player: " + player.health);
            }
            else
            {
                Debug.LogWarning("No playerState component found on Player target.");
            }

            attackTimer = 0f;
        }
    }
    private void ChangeColorOverTime()
    {
        // 动态变化的色相值 H
        float hue = Mathf.PingPong(Time.time * 0.1f, 1); // 0.1 控制变化速度
        float saturation = 1f; // 饱和度为 1（全彩色）
        float value = 1f; // 亮度为 1（完全亮）

        // 根据 HSV 值生成 RGB 颜色
        Color dynamicColor = Color.HSVToRGB(hue, saturation, value);

        // 将生成的颜色赋值给材质的颜色
        bossRenderer.material.color = dynamicColor;
    }

}

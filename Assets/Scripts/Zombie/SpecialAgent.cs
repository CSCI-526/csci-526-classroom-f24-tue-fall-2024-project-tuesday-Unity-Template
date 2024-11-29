
/*原有的
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class SpecialAgent : MonoBehaviour
{
    // Health control
    public float health;
    public float Maxhealth;
    public GameObject healthbar;
    public Slider slider;

    // Attack control
    public Transform target;            // 指定的攻击目标
    public int attackDamage = 10;       // 攻击造成的伤害
    public float attackInterval = 2f;   // 攻击间隔
    public float attackRange = 2f;      // 攻击距离，只有在该范围内才会开始攻击

    private float attackTimer;          // 计时器，用于控制攻击间隔
    private NavMeshAgent navMeshAgent;  // 用于控制移动

    void Start()
    {
        // 初始化健康值
        health = Maxhealth;
        slider.value = calHealth();
        attackTimer = attackInterval; // 初始化攻击计时器

        // 获取 NavMeshAgent 组件
        navMeshAgent = GetComponent<NavMeshAgent>();

        // 设置目标位置
        if (target != null && navMeshAgent != null)
        {
            navMeshAgent.SetDestination(target.position);
        }
    }

    void Update()
    {
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
        if (health <= 0)
        {
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
            // 假设墙有 WallHealth 脚本，直接调用其 TakeDamage 方法
            WallHealth wallHealth = target.GetComponent<WallHealth>();
            if (wallHealth != null)
            {
                wallHealth.TakeDamage(attackDamage);
                Debug.Log("Attacking Wall: " + wallHealth.health);
            }
            else
            {
                Debug.LogWarning("No WallHealth component found on target.");
            }

            // 重置攻击计时器
            attackTimer = 0f;
        }
    }
}
*/

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SpecialAgent : MonoBehaviour
{
    // Health control
    public float health;
    public float Maxhealth;
    public GameObject healthbar;
    public Slider slider;

    // Attack control
    public Transform target;
    public int attackDamage = 10;
    public float attackInterval = 2f;
    public float attackRange = 2f;

    private float attackTimer;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        health = Maxhealth;
        slider.value = calHealth();
        attackTimer = attackInterval;

        navMeshAgent = GetComponent<NavMeshAgent>();
        if (target != null && navMeshAgent != null)
        {
            navMeshAgent.SetDestination(target.position);
        }
    }

    void Update()
    {
        HealthCheck();
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget <= attackRange)
            {
                navMeshAgent.isStopped = true;
                Attack();
            }
            else
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(target.position);
            }
        }
        attackTimer += Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        slider.value = calHealth();
    }

    private float calHealth()
    {
        return health / Maxhealth;
    }

    private void HealthCheck()
    {
        if (health <= 0)
        {
            DestroyAgent(); // Destroy the agent when health is zero
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
            WallHealth wallHealth = target.GetComponent<WallHealth>();
            if (wallHealth != null)
            {
                wallHealth.TakeDamage(attackDamage);
                Debug.Log("attack danage:" +  attackDamage);
            }
            attackTimer = 0f;
        }
    }

    // Method to destroy the agent
    public void DestroyAgent()
    {
        if (healthbar != null)
        {
            Destroy(healthbar); // Destroy health bar UI
        }
        Destroy(gameObject); // Destroy the agent
    }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SpecialAgent : MonoBehaviour
{
    public float health;
    public float Maxhealth;
    public GameObject healthbar;
    public Slider slider;

    public Transform target;
    public int attackDamage = 10;
    public float attackInterval = 2f;
    public float attackRange = 2f;

    private float attackTimer;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        health = Maxhealth;
        slider.value = calHealth();
        attackTimer = attackInterval;

        navMeshAgent = GetComponent<NavMeshAgent>();

        if (target != null && navMeshAgent != null)
        {
            navMeshAgent.SetDestination(target.position);
        }
        else
        {
            Debug.LogWarning("SpecialAgent: Target or NavMeshAgent is not set correctly.");
        }
    }

    void Update()
    {
        HealthCheck();

        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= attackRange)
            {
                navMeshAgent.isStopped = true;
                Attack();
            }
            else
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(target.position);
            }
        }

        attackTimer += Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        slider.value = calHealth();
    }

    private float calHealth()
    {
        return health / Maxhealth;
    }

    private void HealthCheck()
    {
        if (health <= 0)
        {
            DestroyAgent();
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
            WallHealth wallHealth = target.GetComponent<WallHealth>();
            if (wallHealth != null)
            {
                wallHealth.TakeDamage(attackDamage);
            }
            else
            {
                Debug.LogWarning("No WallHealth component found on target.");
            }

            attackTimer = 0f;
        }
    }

    public void DestroyAgent()
    {
        if (healthbar != null)
        {
            Destroy(healthbar);
        }
        Destroy(gameObject);
    }
}


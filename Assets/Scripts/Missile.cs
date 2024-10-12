using UnityEngine;

public class Missile : Bullet
{
    [Header("Missile Attributes")]
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float acceleration = 1f;  // ���ٶ�

    private float currentSpeed;

    private void Start()
    {
        // ��ʼ����ǰ�ٶ�Ϊ�����ĳ�ʼ�ٶ�
        currentSpeed = bulletSpeed;

        // ���Ŀ����ڣ���ʼ��ʱ�ͳ���Ŀ��
        if (target != null)
        {
            SetInitialRotation();
        }
    }

    private void FixedUpdate()
    {
        if (!target) return;

        // ���㳯��Ŀ��ķ���
        Vector2 direction = (target.position - transform.position).normalized;

        // ���㵱ǰ�ǶȺ�Ŀ�귽��֮��ĽǶ�
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float currentAngle = rb.rotation;

        // ����ת�������ﵽƽ��ת��Ч��
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, angle, rotationSpeed * Time.fixedDeltaTime);
        rb.rotation = newAngle;

        // �����ٶ�
        currentSpeed += acceleration * Time.fixedDeltaTime;

        // �����µķ��������ٶ�
        rb.velocity = rb.transform.right * currentSpeed;
    }

    private void SetInitialRotation()
    {
        // ���㵼����ʼλ�ú�Ŀ��֮��ķ���
        Vector2 direction = (target.position - transform.position).normalized;

        // ���㳯��Ŀ��ĳ�ʼ�Ƕ�
        float initialAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ���� Rigidbody2D �� Transform �ĳ�ʼ��ת�Ƕ�
        rb.rotation = initialAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, initialAngle);
    }
}

using UnityEngine;

public class IceRing : MonoBehaviour
{
    public float fadeSpeed = 1f; // ����͸���ȼ������ٶ�
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // ��ȡ��ǰ��ɫ
        Color color = spriteRenderer.color;

        // ������С alpha ֵ
        color.a -= fadeSpeed * Time.deltaTime;

        // ȷ�� alpha ֵ������� 0
        color.a = Mathf.Clamp01(color.a);

        // Ӧ����ɫ
        spriteRenderer.color = color;

        // ����ȫ͸��ʱ���ٶ���
        if (color.a <= 0f)
        {
            Destroy(gameObject);
        }
    }
}

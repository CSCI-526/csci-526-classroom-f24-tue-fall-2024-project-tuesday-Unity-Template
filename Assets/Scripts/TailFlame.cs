using UnityEngine;

public class TailFlame: MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private SpriteRenderer spriteRenderer; // SpriteRenderer ���
    [SerializeField] private Sprite[] flameSprites; // ��Ż���ͼƬ������

    [Header("Attribute")]
    [SerializeField] private float frameDuration = 0.1f; // ÿ֡����ʱ��

    private int currentFrame = 0; // ��ǰ֡����
    private float timer = 0f; // ��ʱ��

    private void Update()
    {
        // ���¼�ʱ��
        timer += Time.deltaTime;

        // �����ʱ�������˵�ǰ֡����ʱ��
        if (timer >= frameDuration)
        {
            // ���ü�ʱ��
            timer = 0f;

            // �л�����һ������ͼƬ
            currentFrame = (currentFrame + 1) % flameSprites.Length;
            spriteRenderer.sprite = flameSprites[currentFrame];
        }
    }
}

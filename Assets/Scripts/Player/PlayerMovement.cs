using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    
    //hp
    public float reducedSpeed = 6f;
    public float slowDownRate = 3f;   // 减速速率（每秒减少的速度值）
    public float speedUpRate = 3f;    // 加速速率（每秒增加的速度值）


    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;
    bool isMoving;

    private Vector3 lastPosition = new Vector3(0f,0f,0f);
    private CharacterController characterController;


    private float currentSpeed;       // 当前速度
    private HashSet<Collider> activeSlowZones = new HashSet<Collider>(); // 当前触发的稻草集合

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentSpeed = speed; // 初始化速度为正常速度
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * x + transform.forward * z;

        characterController.Move(moveDirection * currentSpeed * Time.deltaTime);

        //if(Input.GetButton("Jump") && isGrounded)
        //{
           // velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);
        //}

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        if(lastPosition != gameObject.transform.position && isGrounded == true)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        lastPosition = gameObject.transform.position;

        // 动态调整速度
        if (activeSlowZones.Count > 0 && currentSpeed > reducedSpeed)
        {
            currentSpeed -= slowDownRate * Time.deltaTime; // 逐步减速
            currentSpeed = Mathf.Max(currentSpeed, reducedSpeed); // 确保不会低于最低速度
        }
        else if (activeSlowZones.Count == 0 && currentSpeed < speed)
        {
            currentSpeed += speedUpRate * Time.deltaTime; // 逐步恢复速度
            currentSpeed = Mathf.Min(currentSpeed, speed); // 确保不会超过正常速度
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SlowZone"))
        {
            activeSlowZones.Add(other); // 将触发器加入集合
            Debug.Log($"Entered SlowZone: Active Zones = {activeSlowZones.Count}");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SlowZone"))
        {
            activeSlowZones.Remove(other); // 从集合中移除触发器
            Debug.Log($"Exited SlowZone: Active Zones = {activeSlowZones.Count}");
        }
    }

}

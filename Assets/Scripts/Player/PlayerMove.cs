using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5.0f;
    public float rotSpeed = 5.0f;
    public float jumbPower = 5.0f;
    public float gravity = 10.0f;
    private Vector3 velocity;

    [Header("Direction")]
    public Vector3 dir = Vector3.zero;
    private CharacterController cc;

    [Header("Camera")]
    [SerializeField] private Camera camera;
    public Quaternion cameraDir;
    public float smoothness = 10f;

    [Header("GroundCheck")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGround = true;

    PlayerAnim pa;


    private void Awake()
    {
        pa = GetComponent<PlayerAnim>();
        cc = GetComponent<CharacterController>();
    }

    public void Move()
    {
        if (cc == null) return;

        // 입력 처리
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.z = Input.GetAxisRaw("Vertical");
        cameraDir = Quaternion.Euler(0.0f, camera.transform.eulerAngles.y, 0.0f);

        Vector3 curVec = (cameraDir * dir).normalized;
        Vector3 move = curVec * speed;
        move.y = velocity.y;

        cc.Move(curVec * speed * Time.deltaTime);
        pa.UpdateSpeed(cc.velocity, speed);
    }

    public void Jump()
    {
        if (isGround && velocity.y < 0.0f)
        {
            velocity.y = -1.0f;
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumbPower;

            if(pa != null)
            {
                StartCoroutine(pa.PulseBool("isJumping"));
            }
        }
    }

    // 지면 체크
    public void CheckGround()
    {
        isGround = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // 카메라 조작
    public void RotateCamera()
    {
        Vector3 playerRotate = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
    }
}

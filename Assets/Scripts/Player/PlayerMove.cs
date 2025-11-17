using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotSpeed = 5.0f;
    public float jumbPower = 5.0f;
    public float smoothness = 10f;

    public Vector3 dir = Vector3.zero;
    private Rigidbody rb;
    [SerializeField] private Camera camera;

    private bool isGround = true;

    PlayerAnim pa;


    private void Start()
    {
        pa = GetComponent<PlayerAnim>();
        rb = GetComponent<Rigidbody>();
    }

    public void Move()
    {
        // 입력 처리
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.z = Input.GetAxisRaw("Vertical");
        dir.Normalize();

        if (dir != Vector3.zero)
        {
            // 캐릭터 회전
            transform.forward = Vector3.Lerp(transform.forward, dir, rotSpeed * Time.deltaTime);
            pa.SetMoveAnim(true);       // 달리기 모션
        }
        else pa.SetMoveAnim(false);     // 걷는 모션
        
        // 움직임
        rb.MovePosition(gameObject.transform.position + dir * speed * Time.deltaTime);
    }
    public void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.AddForce(Vector3.up * jumbPower, ForceMode.Impulse);
            pa.SetJumpAnim(true);
        }
        if (transform.position.y < 0.05f && rb.velocity.y < 0)
            pa.SetJumpAnim(false);
    }
    public void CheckGround()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.down, out hit, 0.15f))
        {
            if(hit.transform.tag != null)
            {
                isGround = true;
                return;
            }
        }
        isGround = false;
    }

    public void RotateCamera()
    {
        Vector3 playerRotate = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
    }
}

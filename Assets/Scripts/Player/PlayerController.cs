using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMove PM;

    float horizontal;
    float vertical;

    private void Awake()
    {
        Initialize();
    }
    private void Initialize()
    {
        PM = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        PM.Jump();
        PM.CheckGround();
    }

    private void FixedUpdate()
    {
        PM.Move();
    }

    private void LateUpdate()
    {
        PM.RotateCamera();
    }
}

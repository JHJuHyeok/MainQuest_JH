using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetMoveAnim(bool isMove)
    {
        anim.SetBool("isMove", isMove);
    }

    public void SetJumpAnim(bool isJumping)
    {
        anim.SetBool("isJumping", isJumping);
    }
}

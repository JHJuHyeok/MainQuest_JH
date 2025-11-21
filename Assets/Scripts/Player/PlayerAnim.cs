using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void UpdateSpeed(Vector3 vec, float speed)
    {
        Vector2 vec2 = new Vector2(vec.x, vec.z);
        float moveSpeed = vec2.magnitude / speed;

        if (moveSpeed < 0f)
            moveSpeed = 0f;
        if (moveSpeed > 1.0f)
            moveSpeed = 1f;

        anim.SetFloat("speed", moveSpeed);
    }

    //파라미터갱신 코루틴
    public IEnumerator PulseBool(string name)
    {
        anim.SetBool(name, true);
        yield return null;
        anim.SetBool(name, false);
    }


}

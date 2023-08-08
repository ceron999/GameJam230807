using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    [SerializeField]
    float jumpSpeed;
    [SerializeField]
    Rigidbody2D rigid;
    [SerializeField]
    Animator anim;

    void Start()
    {
        StartCoroutine(JumpCoroutine());
    }

    IEnumerator JumpCoroutine()
    {
        while (true)
        {
            Collider2D getCol = Physics2D.OverlapCircle(this.transform.position, 8);
            if (getCol.tag == "Plane")
            {
                anim.SetBool("isJump", true);
                Vector3 targetVector = getCol.transform.position - this.transform.position;
                rigid.velocity = targetVector.normalized * jumpSpeed;
                targetVector = Vector3.down;
                yield return new WaitForSeconds(2);

                rigid.velocity = targetVector.normalized * jumpSpeed;

                yield return new WaitForSeconds(2);
                break;

            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}

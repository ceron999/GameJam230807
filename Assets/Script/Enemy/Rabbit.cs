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
    [SerializeField]
    GameObject plane;

    void Start()
    {
        plane = GameObject.Find("PaperAirPlaneImage");
        StartCoroutine(JumpCoroutine());
    }

    IEnumerator JumpCoroutine()
    {
        Vector3 dist = plane.transform.position - this.transform.position;
        while (true)
        {
            dist = plane.transform.position - this.transform.position;

            if (dist.magnitude <= 15f)
            {
                Debug.Log("Rabbit");
                anim.SetBool("isJump", true);
                Vector3 targetVector = plane.transform.position - this.transform.position + new Vector3(2,0,0);
                rigid.velocity = targetVector.normalized * jumpSpeed;
                targetVector = Vector3.down;
                yield return new WaitForSeconds(2);

                rigid.velocity = targetVector.normalized * jumpSpeed;

                yield return new WaitForSeconds(2);
                break;

            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}

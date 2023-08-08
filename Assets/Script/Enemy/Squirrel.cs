using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : MonoBehaviour
{
    [SerializeField]
    GameObject proj;
    [SerializeField]
    Rigidbody2D projRigid;

    [SerializeField]
    float shootSpeed = 10;
    void Start()
    {
        StartCoroutine(ShootCoroutine());
    }

    IEnumerator ShootCoroutine()
    {
        while(true)
        {
            Collider2D getCol = Physics2D.OverlapCircle(this.transform.position, 3);
            if(getCol.tag =="Plane")
            {
                proj.SetActive(true);
                Vector3 targetVector = getCol.transform.position - this.transform.position;
                targetVector.x += 1.5f;
                projRigid.velocity = targetVector.normalized * shootSpeed;
                break;

            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}

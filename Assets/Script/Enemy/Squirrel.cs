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
    GameObject plane;

    [SerializeField]
    float shootSpeed = 10;
    void Start()
    {
        plane = GameObject.Find("PaperAirPlaneImage");
        StartCoroutine(ShootCoroutine());
    }

    IEnumerator ShootCoroutine()
    {
        Vector3 dist = plane.transform.position - this.transform.position;
        while(true)
        {
            dist = plane.transform.position - this.transform.position;
            if (dist.magnitude <= 15f)
            {
                proj.SetActive(true);
                Vector3 targetVector = plane.transform.position - this.transform.position;
                targetVector.x += 1.5f;
                projRigid.velocity = targetVector.normalized * shootSpeed;
                break;

            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}

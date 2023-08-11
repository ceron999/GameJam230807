using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    Sprite[] fishArr;
    [SerializeField]
    SpriteRenderer nowSpriteRenderer;
    [SerializeField]
    Rigidbody2D nowRigid;
    [SerializeField]
    GameObject plane;
    [SerializeField]
    float jumpSpeed;

    private void Start()
    {
        plane = GameObject.Find("PaperAirPlaneImage");
        nowSpriteRenderer = this.GetComponent<SpriteRenderer>();
        nowRigid = this.GetComponent<Rigidbody2D>();
        int index = (int)Random.Range(0f, 1.9f);
        nowSpriteRenderer.sprite = fishArr[index]; StartCoroutine(JumpCoroutine());
        Vector3 pos = this.transform.localPosition;
        pos.y = -7;
        this.transform.localPosition = pos;
    }

    IEnumerator JumpCoroutine()
    {
        Vector3 dist = plane.transform.position - this.transform.position;
        while (true)
        {
            dist = plane.transform.position - this.transform.position;

            if (dist.magnitude <= 15f)
            {
                Debug.Log("fish");
                Vector3 targetVector = Vector3.up;
                nowRigid.velocity = targetVector.normalized * jumpSpeed;
                break;

            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
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
    float jumpSpeed;

    private void Start()
    {
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
        while (true)
        {
            Collider2D getCol = Physics2D.OverlapCircle(this.transform.position, 8);
            if (getCol.tag == "Plane")
            {
                Vector3 targetVector = Vector3.up;
                nowRigid.velocity = targetVector.normalized * jumpSpeed;
                break;

            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
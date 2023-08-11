using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slide : MonoBehaviour
{
    float nowAlpha = 0;

    [SerializeField]
    float fadeSpeed;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Button endingBtn;

    private void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        StartCoroutine(StartFadeCoroutine());
    }

    IEnumerator StartFadeCoroutine()
    {
        Color nowColor = spriteRenderer.color;
        while (nowAlpha<=1)
        {
            nowAlpha += Time.deltaTime * fadeSpeed;
            nowColor.a = nowAlpha;
            spriteRenderer.color = nowColor;
            yield return null;
        }
        endingBtn.interactable = true;
    }
}

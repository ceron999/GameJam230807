using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    float nowAlpha = 0;

    [SerializeField]
    float fadeSpeed;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Button TutorialBtn;

    public bool istutorialEnd = false;

    private void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        StartCoroutine(StartFadeCoroutine());
    }

    IEnumerator StartFadeCoroutine()
    {
        Color nowColor = spriteRenderer.color;
        while (nowAlpha <= 1)
        {
            nowAlpha += Time.deltaTime * fadeSpeed;
            nowColor.a = nowAlpha;
            spriteRenderer.color = nowColor;
            yield return null;
        }

        yield return new WaitUntil(() => istutorialEnd);

        while (nowAlpha >= 0)
        {
            nowAlpha -= Time.deltaTime * fadeSpeed;
            nowColor.a = nowAlpha;
            spriteRenderer.color = nowColor;
            yield return null;
        }
        TutorialBtn.interactable = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialBtn : MonoBehaviour
{
    [SerializeField]
    GameObject fadeBtn;
    [SerializeField]
    GameObject sceneMoveBtn;
    [SerializeField]
    Tutorial tutorialScript;


    public void OnClickFadeBtn()
    {
        fadeBtn.SetActive(false);
        sceneMoveBtn.SetActive(true);
        tutorialScript.istutorialEnd = true;
    }

    public void OnClickEndingBtn()
    {
        SceneManager.LoadScene("UpgradeScene");
    }
}

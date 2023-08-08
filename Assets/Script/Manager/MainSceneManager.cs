using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    private void Start()
    {
        if (audioSource == null)
            audioSource = GameObject.Find("MainBGM").GetComponent<AudioSource>();
    }

    public void OnClickStartBtn()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void OnClickLoadBtn()
    {
        SceneManager.LoadScene("UpgradeScene");
    }

    public void OnClickQuitBtn()
    {
        Application.Quit();
    }

    void StartBtnCLickSound()
    {
        audioSource.PlayOneShot(SoundManager.instance.StartEffectSound(1));
        Debug.Log(SoundManager.instance.StartEffectSound(1).name);
        Debug.Log(Time.timeScale);
    }
}

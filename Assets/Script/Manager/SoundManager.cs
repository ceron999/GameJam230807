using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField]
    AudioClip[] effectSoundArr;
    [SerializeField]
    AudioClip mainSound;

    public AudioClip StartEffectSound(int index)
    {
        Debug.Log(effectSoundArr[index]);
        return effectSoundArr[index];
    }
}

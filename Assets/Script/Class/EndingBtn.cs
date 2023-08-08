using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingBtn : MonoBehaviour
{
    public void OnClickEndingBtn()
    {
        SaveDataClass saveData = new SaveDataClass();
        JsonManager.SaveJson<SaveDataClass>(saveData, "UserData");
        Debug.Log("CreateSaveData");
        SceneManager.LoadScene("MainScene");
    }
}

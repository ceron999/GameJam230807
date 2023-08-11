using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public SaveDataClass saveData;
    public UpgradeWrapper upgradeWrapper;
    public UpgradeWrapper upgradeWrapper2;
    public Sprite[] paperPlaneSpriteArr;

    public int planeImgIndex = 1;
    void Awake()
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

    private void Start()
    {
        saveData = JsonManager.LoadSaveData();

        if (saveData == null)
        {
            saveData = new SaveDataClass();
            JsonManager.SaveJson<SaveDataClass>(saveData, "UserData");
            Debug.Log("CreateSaveData");
        }
        else
        {
            Debug.Log("LoadSaveData");
        }

        SetUpgradeWrapper();
    }

    public void ResetData()
    {
        saveData = new SaveDataClass();
        JsonManager.SaveJson<SaveDataClass>(saveData, "UserData");
        Debug.Log("ResetSaveData");
    }

    void SetUpgradeWrapper()
    {
        upgradeWrapper.manaEfficiencyWrapper = JsonManager.ResourceDataLoad<ManaEfficiencyWrapper>("ManaEfficiencyWrapper");
        upgradeWrapper.moveSpeedWrapper = JsonManager.ResourceDataLoad<MoveSpeedWrapper>("MoveSpeedWrapper");
        upgradeWrapper.slowDownWrapper = JsonManager.ResourceDataLoad<SlowDownWrapper>("SlowDownWrapper");
    }
}

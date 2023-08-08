using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum UpgradeName
{
    None, ManaEfficiency, MoveSpeed, SlowDown
}

public class UpgradeSceneManager : MonoBehaviour
{
    [SerializeField]
    GameObject manaPopUpParent;
    [SerializeField]
    GameObject movePopUpParent;
    [SerializeField]
    GameObject slowPopUpParent;
    [SerializeField]
    Button manaUpgradeBtn;
    [SerializeField]
    Button moveUpgradeBtn;
    [SerializeField]
    Button slowUpgradeBtn;

    [SerializeField]
    TextMeshProUGUI moneyText;
    [SerializeField]
    TextMeshProUGUI manaStepText;
    [SerializeField]
    TextMeshProUGUI moveStepText;
    [SerializeField]
    TextMeshProUGUI slowStepText;

    //강화 알림
    [SerializeField]
    TextMeshProUGUI[] upgradeInfoTextArr;

    //사운드
    [SerializeField]
    AudioSource audioSource;


    private void Start()
    {
        moneyText.text = GameManager.instance.saveData.money.ToString();
        manaStepText.text = "마나 효율: " + GameManager.instance.saveData.manaEfficiencyUpgrade * 100 + "%";
        moveStepText.text = "이동 속도: " + GameManager.instance.saveData.moveSpeedUpgrade * 100 + "%";
        slowStepText.text = "둔화 저항: " + GameManager.instance.saveData.slowDownUpgrafe.ToString() + "%";
    }

    //강화파트
    public void Upgrade(int getIndex)
    {
        StartBtnCLickSound();
        UpgradeName getUpgradeName = (UpgradeName)getIndex;
        switch(getUpgradeName)
        {
            case UpgradeName.None:
                break;
            case UpgradeName.ManaEfficiency:
                UpgradeManaEfficiency();
                break;
            case UpgradeName.MoveSpeed:
                UpgradeMoveSpeed();
                break;
            case UpgradeName.SlowDown:
                UpgradeSlowDown();
                break;
        }
    }

    void UpgradeManaEfficiency()
    {
        int nowUpgradeStep = GameManager.instance.saveData.manaEfficiencyUpgrade;
        int nowMoney = GameManager.instance.saveData.money;

        if (nowUpgradeStep < 5)
        {
            int needMoney = GameManager.instance.upgradeWrapper.manaEfficiencyWrapper.manaEfficiencyArr[nowUpgradeStep + 1].upgradeMoney;
            if (needMoney < nowMoney)
            {
                GameManager.instance.saveData.manaEfficiencyUpgrade++;
                GameManager.instance.saveData.money -= needMoney;
                moneyText.text = GameManager.instance.saveData.money.ToString();
                manaStepText.text = "Mana: " + GameManager.instance.saveData.manaEfficiencyUpgrade.ToString();
                Debug.Log(GameManager.instance.saveData.manaEfficiencyUpgrade);
                JsonManager.SaveJson<SaveDataClass>(GameManager.instance.saveData, "UserData");
            }
        }
    }
    void UpgradeMoveSpeed()
    {
        int nowUpgradeStep = GameManager.instance.saveData.moveSpeedUpgrade;
        int nowMoney = GameManager.instance.saveData.money;

        if (nowUpgradeStep < 5)
        {
            int needMoney = GameManager.instance.upgradeWrapper.moveSpeedWrapper.moveSpeedArr[nowUpgradeStep + 1].upgradeMoney;
            if (needMoney < nowMoney)
            {
                GameManager.instance.saveData.moveSpeedUpgrade++;
                GameManager.instance.saveData.money -= needMoney;
                moneyText.text = GameManager.instance.saveData.money.ToString();
                moveStepText.text = "Move: " + GameManager.instance.saveData.moveSpeedUpgrade.ToString();
                Debug.Log(GameManager.instance.saveData.moveSpeedUpgrade);
                JsonManager.SaveJson<SaveDataClass>(GameManager.instance.saveData, "UserData");
            }
        }
    }
    void UpgradeSlowDown()
    {
        int nowUpgradeStep = GameManager.instance.saveData.slowDownUpgrafe;
        int nowMoney = GameManager.instance.saveData.money;

        if (nowUpgradeStep < 5)
        {
            int needMoney = GameManager.instance.upgradeWrapper.slowDownWrapper.slowDownArr[nowUpgradeStep + 1].upgradeMoney;
            if (needMoney < nowMoney)
            {
                GameManager.instance.saveData.slowDownUpgrafe++;
                GameManager.instance.saveData.money -= needMoney;
                moneyText.text = GameManager.instance.saveData.money.ToString();
                slowStepText.text = "Slow: " + GameManager.instance.saveData.slowDownUpgrafe.ToString();
                Debug.Log(GameManager.instance.saveData.slowDownUpgrafe);
                JsonManager.SaveJson<SaveDataClass>(GameManager.instance.saveData, "UserData");
            }
        }
    }

    //이미지 파트
    public void OnClickPlaneBtn1()
    {
        //1번 이미지 사용
        StartBtnCLickSound();
        GameManager.instance.planeImgIndex = 1;
    }
    public void OnClickPlaneBtn2()
    {
        //2번 이미지 사용
        StartBtnCLickSound();
        GameManager.instance.planeImgIndex = 2;
    }
    public void OnClickPlaneBtn3()
    {
        //3번 이미지 사용
        StartBtnCLickSound();
        GameManager.instance.planeImgIndex = 3;
    }

    //팝업 파트
    public void OnClickManaUpgradeBtn()
    {
        StartBtnCLickSound();
        manaPopUpParent.SetActive(true);
        int nowUpgradeStep = GameManager.instance.saveData.manaEfficiencyUpgrade;
        int nowMoney = GameManager.instance.saveData.money;
        if (nowUpgradeStep < 5)
        {
            if (GameManager.instance.upgradeWrapper.manaEfficiencyWrapper.manaEfficiencyArr[nowUpgradeStep + 1].upgradeMoney > nowMoney)
            {
                manaUpgradeBtn.enabled = false;
            }
            else
                manaUpgradeBtn.enabled = true;

            upgradeInfoTextArr[0].text = "마나 효율 : "
                + GameManager.instance.upgradeWrapper.manaEfficiencyWrapper.manaEfficiencyArr[nowUpgradeStep + 1].upgrafeRate * 100 + "%";
            upgradeInfoTextArr[1].text = GameManager.instance.upgradeWrapper.manaEfficiencyWrapper.manaEfficiencyArr[nowUpgradeStep + 1].upgradeMoney
                + "골드 소모";
        }
        else
        {
            upgradeInfoTextArr[0].text = "더 이상 강화할 수 없습니다.";
            upgradeInfoTextArr[1].gameObject.SetActive(false);
        }
    }

    public void OnClickMoveUpgradeBtn()
    {
        StartBtnCLickSound();
        movePopUpParent.SetActive(true);
        int nowUpgradeStep = GameManager.instance.saveData.moveSpeedUpgrade;
        int nowMoney = GameManager.instance.saveData.money;
        if (nowUpgradeStep < 5)
        {
            if (GameManager.instance.upgradeWrapper.moveSpeedWrapper.moveSpeedArr[nowUpgradeStep + 1].upgradeMoney > nowMoney)
            {
                moveUpgradeBtn.enabled = false;
            }
            else
                moveUpgradeBtn.enabled = true;

            upgradeInfoTextArr[2].text = "이동 속도 : "
                + GameManager.instance.upgradeWrapper.moveSpeedWrapper.moveSpeedArr[nowUpgradeStep + 1].upgrafeRate * 100 + "%";
            upgradeInfoTextArr[3].text = GameManager.instance.upgradeWrapper.moveSpeedWrapper.moveSpeedArr[nowUpgradeStep + 1].upgradeMoney
                + "골드 소모";
        }
        else
        {
            upgradeInfoTextArr[2].text = "더 이상 강화할 수 없습니다.";
            upgradeInfoTextArr[3].gameObject.SetActive(false);
        }
    }

    public void OnClickSlowUpgradeBtn()
    {
        StartBtnCLickSound();
        slowPopUpParent.SetActive(true);
        int nowUpgradeStep = GameManager.instance.saveData.slowDownUpgrafe;
        int nowMoney = GameManager.instance.saveData.money;
        if (nowUpgradeStep < 5)
        {
            if (GameManager.instance.upgradeWrapper.slowDownWrapper.slowDownArr[nowUpgradeStep + 1].upgradeMoney > nowMoney)
            {
                slowUpgradeBtn.enabled = false;
            }
            else
                slowUpgradeBtn.enabled = true;

            upgradeInfoTextArr[4].text = "둔화 저항 : "
                + GameManager.instance.upgradeWrapper.slowDownWrapper.slowDownArr[nowUpgradeStep + 1].upgrafeRate * 100 + "%";
            upgradeInfoTextArr[5].text = GameManager.instance.upgradeWrapper.slowDownWrapper.slowDownArr[nowUpgradeStep + 1].upgradeMoney
                + "골드 소모";
        }
        else
        {
            upgradeInfoTextArr[4].text = "더 이상 강화할 수 없습니다.";
            upgradeInfoTextArr[5].gameObject.SetActive(false);
        }
    }

    public void OnClickManaUpgradeExitBtn()
    {
        StartBtnCLickSound();
        manaPopUpParent.SetActive(false);
    }

    public void OnClickMoveUpgradeExitBtn()
    {
        StartBtnCLickSound();
        movePopUpParent.SetActive(false);
    }

    public void OnClickSlowUpgradeExitBtn()
    {
        StartBtnCLickSound();
        slowPopUpParent.SetActive(false);
    }

    public void OnClickStartBtn()
    {
        StartBtnCLickSound();
        JsonManager.SaveJson<SaveDataClass>(GameManager.instance.saveData, "UserData");
        SceneManager.LoadScene("FlyScene");
    }

    void StartBtnCLickSound()
    {
        audioSource.PlayOneShot(SoundManager.instance.StartEffectSound(1));
    }
}

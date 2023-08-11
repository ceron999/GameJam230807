using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FlySceneManager : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer paperPlaneRenderer;

    [SerializeField]
    GameObject[] enemyPrefabArr;
    [SerializeField]
    GameObject soapBubble;
    [SerializeField]
    Transform enemyParent;
    [SerializeField]
    GameObject paperPlaneParent;
    [SerializeField]
    GameObject paperPlaneImage;
    [SerializeField]
    PaperAirPlane paperPlaneScript;
    [SerializeField]
    GameObject fixCameraPos;

    [SerializeField]
    Button boosterBtn;
    [SerializeField]
    Button angleUpBtn;
    [SerializeField]
    Button angleDownBtn;

    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    TextMeshProUGUI speedText;
    [SerializeField]
    TextMeshProUGUI PointText;

    //부스터 파트
    [SerializeField]
    Image boosterBar;
    [SerializeField]
    float boosterGauge = 100;
    [SerializeField]
    float boosterConsumption = 0.001f;

    //각도 조절 버튼
    [SerializeField]
    GameObject angleBtn;
    bool isAngleBtnActve = false;
    bool isMoveAngleUp = false;
    bool isMoveAngleDown = false;
    Vector3 nowAngleBtnPos;

    [SerializeField]
    float angle = 0;
    [SerializeField]
    float angleSpeed;
    bool isAngleUp = true;
    [SerializeField]
    bool isBoosterOn = false;
    public bool isGameEnd = false;
    public bool isClear = false;

    //데이터
    int nowManaIndex = 0;
    float nowMana;

    //끝나고 이동, 재화 데이터 표시
    [SerializeField]
    GameObject endPopUpParent;
    [SerializeField]
    TextMeshProUGUI moveDistanceText;
    [SerializeField]
    TextMeshProUGUI earnMoneyText;
    float moveDistance = 0;
    float earnMoney = 0;

    //사운드
    [SerializeField]
    AudioSource audioSource;
    private void Start()
    {
        nowManaIndex = GameManager.instance.saveData.manaEfficiencyUpgrade;
        nowMana = GameManager.instance.upgradeWrapper.manaEfficiencyWrapper.manaEfficiencyArr[nowManaIndex].upgrafeRate;
        boosterConsumption /= nowMana;

        paperPlaneRenderer = paperPlaneImage.GetComponent<SpriteRenderer>();
        Debug.Log(GameManager.instance.planeImgIndex - 1);
        paperPlaneRenderer.sprite = GameManager.instance.paperPlaneSpriteArr[GameManager.instance.planeImgIndex - 1];


        //약 1초의 시간 이후 각도를 설정해 날린다.
        nowAngleBtnPos = angleBtn.transform.localPosition;
        ShootPlane();
        SetEnemys();
    }

    private void Update()
    {
        SetFixCameraPos();
        if (!isGameEnd)
        {
            UseBooster();
            speedText.text = ((int)paperPlaneScript.ReturnVelocity().magnitude).ToString();
            float dist = paperPlaneImage.transform.localPosition.x;
            moveDistance = dist * 3;
            PointText.text = ((int)moveDistance).ToString();
        }
        else
        {

        }
    }

    void SetEnemys()
    {
        int randomIndex = 0;
        Vector3 spawnPos = Vector3.zero;
        spawnPos.y -= 4.5f;
        for(int i = 0; i< 59; i++)
        {
            //17~40은 물임
            if (i >= 17 && i < 41)
            {
                randomIndex = (int)Random.Range(5, 6.9f);
                spawnPos.x = 6 * (i + 1);
                GameObject enemy = Instantiate(enemyPrefabArr[randomIndex], enemyParent);
                enemy.transform.SetParent(enemyParent);
                enemy.transform.localPosition = spawnPos;
            }
            else
            {
                randomIndex = (int)Random.Range(1, 6f);
                spawnPos.x = 6 * (i + 1);

                if (randomIndex == 2)
                    spawnPos.y = -2.56f;
                else if (randomIndex == 5)
                    spawnPos.y = -2.56f;

                GameObject enemy = Instantiate(enemyPrefabArr[randomIndex], enemyParent);
                enemy.transform.SetParent(enemyParent);
                enemy.transform.localPosition = spawnPos;

                spawnPos.y = -4.5f;
            }
            if (i % 5 == 0)
            {
                float randomYPos = Random.Range(2, 4);
                spawnPos.y = randomYPos;
                GameObject bubble = Instantiate(soapBubble, enemyParent);
                bubble.transform.SetParent(enemyParent);
                bubble.transform.localPosition = spawnPos;

                spawnPos.y = -4.5f;
            }
        }
    }

    void ShootPlane()
    {
        StartCoroutine(ShootPlaneCoroutine());
    }

    IEnumerator ShootPlaneCoroutine()
    {
        yield return new WaitForSeconds(1f);

        StartCoroutine(SetShootAngle());
    }

    

    IEnumerator SetShootAngle()
    {
        while(true)
        {
            if (!Input.GetMouseButtonDown(0))
            {
                //각도조절
                if(isAngleUp)
                {
                    angle += Time.deltaTime * angleSpeed;
                    if (angle > 90)
                    {
                        angle = 90;
                        isAngleUp = false;
                    }
                }
                else
                {
                    angle -= Time.deltaTime * angleSpeed;
                    if (angle < 0)
                    {
                        angle = 0;
                        isAngleUp = true;
                    }
                }
                paperPlaneScript.SetPlaneAngle(angle);
                yield return null;
            }

            else if(Input.GetMouseButtonDown(0))
            {
                //슛
                Debug.Log("Shoot");
                boosterBtn.interactable = true;
                Vector3 startVector = Angle2Vector();
                paperPlaneScript.SetPlaneVelocity(startVector);
                mainCamera.transform.SetParent(fixCameraPos.transform);
                StartCoroutine(FindEndGameCoroutine());
                StartEffectSound(8);
                break;
            }
        }
    }

    Vector3 Angle2Vector()
    {
        float xPos = Mathf.Cos(angle * Mathf.Deg2Rad);
        float yPos = Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector3 direction = new Vector3(xPos, yPos, 0);

        return direction;
    }

    void SetFixCameraPos()
    {
        fixCameraPos.transform.localPosition = paperPlaneImage.transform.localPosition;
    }

    public void OnClickBoosterBtnDown()
    {
        if(boosterGauge>0)
        {
            isBoosterOn = true;
        }
        else
        {
            isBoosterOn = false;
        }
    }

    public void OnClickBoosterBtnUp()
    {
        isBoosterOn = false;
    }

    void UseBooster()
    {
        if (isBoosterOn)
        {
            
            if (boosterGauge > 0)
            {
                if (!paperPlaneScript.IsMaxSpeed())
                {
                    StartEffectSound(0);
                    boosterGauge -= boosterConsumption;
                    boosterBar.fillAmount = boosterGauge / 100;
                    paperPlaneScript.UseBooster();
                }
            }
            else if (boosterGauge < 0)
            {
                StartEffectSound(4);
                boosterGauge = 0;
            }
        }
    }

    //위아래로 움직일 수 있는 버튼 사용
    public void SetAngleBtnUp()
    {
        isMoveAngleUp = true;
        isMoveAngleDown = false;
        paperPlaneScript.SetAngleBools(isMoveAngleUp, isMoveAngleDown);
        paperPlaneScript.SetMoveAngleUp(isMoveAngleUp);
    }

    public void SetAngleBtnUpOff()
    {
        isMoveAngleUp = false;
        paperPlaneScript.SetAngleBools(isMoveAngleUp, isMoveAngleDown);
    }

    public void SetAngleBtnDown()
    {
        isMoveAngleUp = false;
        isMoveAngleDown = true;
        paperPlaneScript.SetAngleBools(isMoveAngleUp, isMoveAngleDown);
        paperPlaneScript.SetMoveAngleDown(isMoveAngleDown);
    }

    public void SetAngleBtnDownOff()
    {
        isMoveAngleDown = false;
        paperPlaneScript.SetAngleBools(isMoveAngleUp, isMoveAngleDown);
    }


    IEnumerator FindEndGameCoroutine()
    {
        yield return new WaitForSeconds(1);
        while(!isGameEnd)
        {
            EndGame();

            
            yield return null;
        }
        paperPlaneScript.SetIsGameEnd();
        boosterBtn.enabled = false;
        angleUpBtn.enabled = false;
        angleDownBtn.enabled = false;
        Debug.Log("gameENd");

        if(!isClear)
            SetEndPopUp();
    }

    void SetEndPopUp()
    {
        float dist = paperPlaneImage.transform.localPosition.x;
        moveDistance = dist * 3;
        moveDistanceText.text = ((int)moveDistance).ToString();
        earnMoney = moveDistance * 5;
        earnMoney =  (int)earnMoney / 100;
        earnMoney *= 100;
        earnMoneyText.text = ((int)earnMoney).ToString();
        StartCoroutine(SetEndPopUpCoroutine());
    }

    IEnumerator SetEndPopUpCoroutine()
    {
        yield return new WaitForSeconds(2f);
        endPopUpParent.SetActive(true);
    }

    void EndGame()
    {
        Vector2 endVector = Vector2.zero;
        if (paperPlaneScript.ReturnVelocity().x <= 0.4f)
        {
            Debug.Log("속도0");
            speedText.text = 0.ToString();
            isGameEnd = true;
            paperPlaneScript.SetPlaneFix(true);
        }
        else if (paperPlaneScript.ReturnIsGroundTouch())
        {
            Debug.Log("땅바닥");
            isGameEnd = true;
            paperPlaneScript.SetPlaneFix(true);
        }
    }

    public void OnClickShopBtn()
    {
        GameManager.instance.saveData.money += (int)earnMoney;
        JsonManager.SaveJson<SaveDataClass>(GameManager.instance.saveData, "UserData");
        SceneManager.LoadScene("UpgradeScene");
    }

    public void OnClickStartBtn()
    {
        GameManager.instance.saveData.money += (int)earnMoney;
        JsonManager.SaveJson<SaveDataClass>(GameManager.instance.saveData, "UserData");
        SceneManager.LoadScene("FlyScene");
    }

    void StartEffectSound(int getIndex)
    {
        audioSource.PlayOneShot(SoundManager.instance.StartEffectSound(getIndex));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaperAirPlane : MonoBehaviour
{
    [SerializeField]
    Transform planeTransform;
    [SerializeField]
    Rigidbody2D planeRigid;

    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float planeStartSpeed;
    [SerializeField]
    float boosterSpeed;
    [SerializeField] float nowAngle;

    Vector3 nowDir;
    bool isSlow = false;

    Vector2 angleVector = Vector2.zero;
    bool isAngleUp = false;
    bool isAngleDown = false;

    [SerializeField]bool isGameEnd = false;
    [SerializeField] bool isGroundTouch = false;

    //업글 데이터
    int nowSlowDownIndex = 0;
    int nowMoveSpeedIndex = 0;
    float nowSlowDown;
    float nowMoveSpeed;

    //엔딩
    [SerializeField]
    GameObject wizard;
    [SerializeField]
    FlySceneManager flySceneManager;

    void Start()
    {
        nowSlowDownIndex = GameManager.instance.saveData.slowDownUpgrafe;
        nowSlowDown = GameManager.instance.upgradeWrapper.slowDownWrapper.slowDownArr[nowSlowDownIndex].upgrafeRate;
        nowMoveSpeedIndex = GameManager.instance.saveData.moveSpeedUpgrade;
        nowMoveSpeed = GameManager.instance.upgradeWrapper.moveSpeedWrapper.moveSpeedArr[nowMoveSpeedIndex].upgrafeRate;
        maxSpeed *= nowMoveSpeed;
        planeStartSpeed *= nowMoveSpeed;
    }

    private void Update()
    {
        if (!isGameEnd)
            WatchMoveDir();
        else
            SetPlaneFix(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isGameEnd)
        {
            if (collision.tag == "SlowArea")
                SetPlaneSlow();
            if (collision.tag == "Enemy")
            {
                CollideEnemy();
            }
            if (collision.tag == "Under")
            {
                isGroundTouch = true;
            }
            if(collision.tag =="SoapBubble")
            {
                Vector2 upVector = planeRigid.velocity.normalized * 5;
                planeRigid.velocity += upVector;
                Destroy(collision.gameObject);
            }
            if(collision.tag == "End")
            {
                SaveDataClass newData = new SaveDataClass();
                JsonManager.SaveJson<SaveDataClass>(newData, "UserData");
                Debug.Log("CLear");
                flySceneManager.isClear = true;
                flySceneManager.isGameEnd = true;

                planeRigid.gravityScale = 0;
                nowDir = wizard.transform.position - planeTransform.localPosition;
                planeRigid.velocity = nowDir.normalized * 5;
                Invoke("MoveEndScene", 5);
            }
        }
    }

    void MoveEndScene()
    {
        SceneManager.LoadScene("EndingScene");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "SlowArea")
            SetPlaneSlowExit();
    }

    public void SetPlaneFix(bool isEnd)
    {
        if (isEnd)
        {
            isGameEnd = true;
            planeRigid.velocity = Vector2.zero;
            planeTransform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        }
    }

    void CollideEnemy()
    {
        Debug.Log("Enemy collide");
        Vector2 slowVector = new Vector3(3f * (1 - nowSlowDown), 0, 0);
        planeRigid.velocity -= slowVector;
    }

    void WatchMoveDir()
    {
        nowDir = planeRigid.velocity;

        nowAngle = Mathf.Atan2(nowDir.y, nowDir.x) * Mathf.Rad2Deg;
        planeTransform.rotation = Quaternion.AngleAxis(nowAngle, Vector3.forward);
    }

    public void SetPlaneAngle(float getAngle)
    {
        planeTransform.rotation = Quaternion.AngleAxis(getAngle, Vector3.forward);
    }

    public void SetPlaneVelocity(Vector3 getVec)
    {
        Debug.Log(getVec);
        planeRigid.velocity = getVec.normalized * planeStartSpeed;
        planeRigid.gravityScale = 0.5f;
    }

    public bool IsMaxSpeed()
    {
        if (planeRigid.velocity.magnitude >= maxSpeed)
            return true;
        else
            return false;
    }

    public void UseBooster()
    {
        Debug.Log(planeRigid.velocity.magnitude);
        //nowDir = planeRigid.velocity.normalized;
        nowDir = new Vector3(1, 0, 0);
        if(planeRigid.velocity.magnitude <= maxSpeed)
            planeRigid.AddForce(nowDir * boosterSpeed, ForceMode2D.Impulse);
    }

    void SetPlaneSlow()
    {
        isSlow = true;
        StartCoroutine(SetPlaneSlowCoroutine());
    }

    void SetPlaneSlowExit()
    {
        isSlow = false;
    }

    IEnumerator SetPlaneSlowCoroutine()
    {
        Vector2 slowVector = new Vector3(0.1f, 0, 0);
        if(isSlow)
        {
            yield return new WaitForSeconds(0.5f);
            //planeRigid.velocity *= 0.9f;
            planeRigid.velocity -= slowVector;
        }
    }

    public void SetAngleBools(bool isUp, bool isDown)
    {
        isAngleUp = isUp;
        isAngleDown = isDown;
    }

    public void SetMoveAngleUp(bool getBool)
    {
        isAngleUp = getBool;
        if (nowAngle < 89)
        {
            StartCoroutine(SetMoveAngleUpCoroutine());
            Debug.Log("up");
        }
    }

    IEnumerator SetMoveAngleUpCoroutine()
    {
        while(isAngleUp)
        {
            if (!isAngleUp)
                break;
            if(planeRigid.velocity.x > 10)
                angleVector.x = -0.5f;
            else
                angleVector.x = 0;
            angleVector.y = 1;
            planeRigid.AddForce(angleVector, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void SetMoveAngleDown(bool getBool)
    {
        isAngleDown = getBool;
        if (nowAngle > 1)
        {
            StartCoroutine(SetMoveAngleDownCoroutine());
            Debug.Log("down");
        }
    }

    IEnumerator SetMoveAngleDownCoroutine()
    {
        while (isAngleDown)
        {
            if (!isAngleDown)
                break;
            angleVector.y = -1;
            angleVector.x = -0.1f;
            planeRigid.AddForce(angleVector, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public Vector3 ReturnVelocity()
    {
        return planeRigid.velocity;
    }

    public bool ReturnIsGroundTouch()
    {
        return isGroundTouch;
    }

    public void SetIsGameEnd()
    {
        isGameEnd = true;
    }
}

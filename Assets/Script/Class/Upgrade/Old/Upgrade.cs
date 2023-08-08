using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Upgrade
{
    public int upgradeStep;             //업글 단계
    public float upgradePercentage;     //업글 확률
    public float startVelocity;         //시작 속도
    public float boosterDuration;       //부스터 지속 시간
    public float boosterAccel;          //부스터 가속
    public float deceleration;          //감속 정도
}

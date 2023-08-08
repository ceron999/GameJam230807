using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoosterAccelUpgrade
{
    public int upgradeStep;             //업글 단계
    public float upgradePercentage;     //업글 확률
    public float boosterAccel;          //부스터 가속
}

public class BoosterAccelUpgradeWrapper
{
    public BoosterAccelUpgrade[] boosterAccelUpgradeArr;
}
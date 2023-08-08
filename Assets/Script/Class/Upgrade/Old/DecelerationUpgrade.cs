using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DecelerationUpgrade
{
    public int upgradeStep;             //업글 단계
    public float upgradePercentage;     //업글 확률
    public float deceleration;          //감속 정도
}

public class DecelerationUpgradeWrapper
{
    public DecelerationUpgrade[] decelerationUpgradeUpgradeArr;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoosterDurationUpgrade
{
    public int upgradeStep;             //업글 단계
    public float boosterDuration;       //부스터 지속 시간
}

public class BoosterDurationUpgradeWrapper
{
    public BoosterDurationUpgrade[] boosterDurationUpgradeArr;
}

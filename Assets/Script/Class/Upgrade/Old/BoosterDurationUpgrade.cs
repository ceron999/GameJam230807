using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoosterDurationUpgrade
{
    public int upgradeStep;             //���� �ܰ�
    public float boosterDuration;       //�ν��� ���� �ð�
}

public class BoosterDurationUpgradeWrapper
{
    public BoosterDurationUpgrade[] boosterDurationUpgradeArr;
}

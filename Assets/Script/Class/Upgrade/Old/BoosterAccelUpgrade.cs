using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoosterAccelUpgrade
{
    public int upgradeStep;             //���� �ܰ�
    public float upgradePercentage;     //���� Ȯ��
    public float boosterAccel;          //�ν��� ����
}

public class BoosterAccelUpgradeWrapper
{
    public BoosterAccelUpgrade[] boosterAccelUpgradeArr;
}
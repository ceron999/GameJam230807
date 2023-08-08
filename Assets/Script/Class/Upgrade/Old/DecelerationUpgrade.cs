using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DecelerationUpgrade
{
    public int upgradeStep;             //���� �ܰ�
    public float upgradePercentage;     //���� Ȯ��
    public float deceleration;          //���� ����
}

public class DecelerationUpgradeWrapper
{
    public DecelerationUpgrade[] decelerationUpgradeUpgradeArr;
}
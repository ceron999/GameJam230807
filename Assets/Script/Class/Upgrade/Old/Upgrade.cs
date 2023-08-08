using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Upgrade
{
    public int upgradeStep;             //���� �ܰ�
    public float upgradePercentage;     //���� Ȯ��
    public float startVelocity;         //���� �ӵ�
    public float boosterDuration;       //�ν��� ���� �ð�
    public float boosterAccel;          //�ν��� ����
    public float deceleration;          //���� ����
}

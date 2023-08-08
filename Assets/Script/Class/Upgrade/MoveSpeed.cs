using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveSpeed
{
    public int upgradeStep;
    public float upgrafeRate;
    public int upgradeMoney;
}

[System.Serializable]
public class MoveSpeedWrapper
{
    public MoveSpeed[] moveSpeedArr;
}

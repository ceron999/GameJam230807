using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlowDown
{
    public int upgradeStep;
    public float upgrafeRate;
    public int upgradeMoney;
}

[System.Serializable]
public class SlowDownWrapper
{
    public SlowDown[] slowDownArr;
}

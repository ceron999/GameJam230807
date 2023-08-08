using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ManaEfficiency
{
    public int upgradeStep;
    public float upgrafeRate;
    public int upgradeMoney;
}

[System.Serializable]
public class ManaEfficiencyWrapper
{
    public ManaEfficiency[] manaEfficiencyArr;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveDataClass
{
    public int money;
    public int manaEfficiencyUpgrade;
    public int moveSpeedUpgrade;
    public int slowDownUpgrafe;
    public float maxPoint;
    
    public SaveDataClass()
    {
        money = 0;
        manaEfficiencyUpgrade = 0;
        moveSpeedUpgrade = 0;
        slowDownUpgrafe = 0;
        maxPoint = 0;
    }
}

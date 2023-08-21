using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

#region PlayerBaseData
[System.Serializable]
public struct PlayerBaseData
{
    public int baseHp;
    public float baseAttackCoolTime;
    public float baseAttackSpeed;
    public float baseMoveSpeed;
    public float baseJumpPower;
    public int doubleShotCount;
    public float doubleShotCoolTime;
    public float circleDiffuseCoolTime;
}
#endregion
#region WaveData
#region PlayerSaveData
[System.Serializable]
public struct PlayerSaveData
{
    public List<int> upgradeValues;
    public int bestWaveCount;
    public int dregCount;
    public int perfectDregCount;
    public List<int> destroyCounts;
    public bool isAdRemove;
    public bool isDoubleDropRate;
}
#endregion
[System.Serializable]
public struct MeteorHpRate
{
    public int minRate;
    public int maxRate;
}
[System.Serializable]
public struct WaveInfo
{
    public int waveNumber;
    public int spawnCount;
    public float spawnTime;
    public List<int> meteorCount;
    public List<MeteorHpRate> meteorHpRate;
}
[System.Serializable]
public struct WaveData
{
    public List<WaveInfo> waveInfo;
    public int waveTerm;
    public List<float> meteorMoveSpeed;
    public List<int> meteorDamage;
    public List<int> dropRate;
    public List<int> dropDregs;
    public List<int> dropPerfectDregs;
    public List<int> dropHearts;
    public int baseMeteorHp;
}
#endregion
#region UpgradeStatData
[System.Serializable]
public struct UpdaradeStatInfo
{
    public string statNameK;
    public string statNameE;
    public int defaultValue;
    public int maxUpgradeValue;
    public List<float> upgradeValue;
    public List<int> upgradeCost;
}
[System.Serializable]
public struct UpgradeStatData
{
    public List<UpdaradeStatInfo> upgradeStatInfo;
}
#endregion
#region ShopInfo
[System.Serializable]
public struct MeteorList
{
    public int count;
    public int cost;
}
[System.Serializable]
public struct ShopInfo
{
    public List<MeteorList> meteorList;
    public int adRemoveCost;
}
#endregion
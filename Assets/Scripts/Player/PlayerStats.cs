using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EnumClass;

public class PlayerStats : MonoBehaviour
{
    #region Sgt
    private static PlayerStats instance;
    public static PlayerStats GetInstance() => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    #region Event
    public event EventManager.SingleInt showPlayerHp;
    public event EventManager.SingleInt showDregs;
    public event EventManager.SingleInt showPerfectDregs;
    #endregion

    [Header("BaseStats")]
    [SerializeField] private int maxHp;
    [SerializeField] private int curHp;
    [SerializeField] private float attackCoolTime;
    [SerializeField] private float attakcSpeed;
    public float AttackSpeed { get { return attakcSpeed; } }
    [SerializeField] private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }
    [SerializeField] private float jumpPower;
    public float JumpPower { get { return jumpPower; } }
    

    [Header("UpgradeStats")]
    [SerializeField] private int power;
    public int Power { get { return power; } }
    [SerializeField] private int criticalLaserChance;
    public int CriticalLaserChance { get { return criticalLaserChance; } }
    [SerializeField] private int unbeatableChance;
    [SerializeField] private float circleDiffuseCoolTime;
    public float CircleDiffuseCoolTime { get { return circleDiffuseCoolTime; } }
    [SerializeField] private float magnetRange;
    public float MagnetRange { get {  return magnetRange; } }

    [Header("Etc")]
    [SerializeField] private int doubleShotCount;
    public int DoubleShotCount { get { return doubleShotCount; } }
    [SerializeField] private float doubleShotCoolTime;
    public float DoubleShotCoolTime { get { return doubleShotCoolTime; } }
    [SerializeField] private bool isDie = false;
    [SerializeField] private bool isTakeDamage = false;

    [Header("SaveStats")]
    [SerializeField] private int dregsCount;
    [SerializeField] private int perfectDregsCount;
    [SerializeField] private List<int> destroyCounts = new List<int>();

    private void OnEnable()
    {
        SetData();
        ConnectedUI();
    }

    private void ConnectedUI()
    {
        InGameUiManager.GetInstance().playerHpHolder.SetHpEvnet(maxHp);
        InGameUiManager.GetInstance().SetUpgradeUI(DataManager.GetInstance().playerSaveData.GetData().upgradeValues);
    }

    private void SetData()
    {
        PlayerBaseData baseData = DataManager.GetInstance().playerBaseData.GetData();

        maxHp = baseData.baseHp;
        curHp = maxHp;
        jumpPower = baseData.baseJumpPower;
        attackCoolTime = baseData.baseAttackCoolTime;
        attakcSpeed = baseData.baseAttackSpeed;
        doubleShotCount = baseData.doubleShotCount;
        doubleShotCoolTime = baseData.doubleShotCoolTime;

        PlayerSaveData saveData = DataManager.GetInstance().playerSaveData.GetData();
        UpgradeStatData upgradeStatData = DataManager.GetInstance().updaradeStatData.GetData();

        power =
            upgradeStatData.upgradeStatInfo[(int)EnumClass.Upgrade.Power].defaultValue +
            (int)upgradeStatData.upgradeStatInfo[(int)EnumClass.Upgrade.Power].upgradeValue[saveData.upgradeValues[(int)EnumClass.Upgrade.Power]];
        criticalLaserChance =
            upgradeStatData.upgradeStatInfo[(int)EnumClass.Upgrade.Critical_Laser_Chance].defaultValue +
            (int)upgradeStatData.upgradeStatInfo[(int)EnumClass.Upgrade.Critical_Laser_Chance].upgradeValue[saveData.upgradeValues[(int)EnumClass.Upgrade.Critical_Laser_Chance]];
        unbeatableChance =
            upgradeStatData.upgradeStatInfo[(int)EnumClass.Upgrade.Unbeatable_Chance].defaultValue +
            (int)upgradeStatData.upgradeStatInfo[(int)EnumClass.Upgrade.Unbeatable_Chance].upgradeValue[saveData.upgradeValues[(int)EnumClass.Upgrade.Unbeatable_Chance]];
        moveSpeed = 
            baseData.baseMoveSpeed + upgradeStatData.upgradeStatInfo[(int)EnumClass.Upgrade.Move_Speed].defaultValue +
            upgradeStatData.upgradeStatInfo[(int)EnumClass.Upgrade.Move_Speed].upgradeValue[saveData.upgradeValues[(int)EnumClass.Upgrade.Move_Speed]];
        circleDiffuseCoolTime =
            baseData.circleDiffuseCoolTime - (upgradeStatData.upgradeStatInfo[(int)EnumClass.Upgrade.Circle_Diffuse_Time_Decrease].defaultValue +
            upgradeStatData.upgradeStatInfo[(int)EnumClass.Upgrade.Circle_Diffuse_Time_Decrease].upgradeValue[saveData.upgradeValues[(int)EnumClass.Upgrade.Circle_Diffuse_Time_Decrease]]);
        magnetRange =
            upgradeStatData.upgradeStatInfo[(int)EnumClass.Upgrade.Magnet_Range].defaultValue +
            upgradeStatData.upgradeStatInfo[(int)EnumClass.Upgrade.Magnet_Range].upgradeValue[saveData.upgradeValues[(int)EnumClass.Upgrade.Magnet_Range]];

        destroyCounts = new List<int>();
        for (EnumClass.MeteorSize type = EnumClass.MeteorSize.Small; type < EnumClass.MeteorSize.End; type++)
        {
            destroyCounts.Add(0);
        }
    }

    public void SaveData()
    {
        PlayerSaveData saveData = DataManager.GetInstance().playerSaveData.GetData();
        saveData.bestWaveCount
            = saveData.bestWaveCount < (WaveManager.GetInstance().CurWaveNumber - 1) 
            ? (WaveManager.GetInstance().CurWaveNumber - 1) : saveData.bestWaveCount;
        saveData.perfectDregCount += perfectDregsCount;
        saveData.dregCount += dregsCount;

        if(saveData.destroyCounts.Count == 0 )
        {
            saveData.destroyCounts = new List<int>(Enumerable.Repeat(DEFAULT_UPGRADE_VALUE, (int)MeteorSize.End).ToArray());
        }

        for (EnumClass.MeteorSize type = EnumClass.MeteorSize.Small; type < EnumClass.MeteorSize.End; type++)
        {
            saveData.destroyCounts[(int)type] += destroyCounts[(int)type];
        }


        DataManager.GetInstance().UpdatePlayerSaveData(saveData);
    }

    public void SaveAdsReward()
    {
        PlayerSaveData saveData = DataManager.GetInstance().playerSaveData.GetData();
        saveData.dregCount += dregsCount;
        InGameUiManager.GetInstance().UpdateDregsCoutnTextForAdsReward();
        DataManager.GetInstance().UpdatePlayerSaveData(saveData);
    }

    public void AddDregs(int amount)
    {
        dregsCount += amount;
        showDregs.Invoke(dregsCount);
    }

    public void AddPerfectDregs(int amount)
    {
        perfectDregsCount += amount;
        showPerfectDregs.Invoke(perfectDregsCount);
    }

    public void AddHp(int amount)
    {
        curHp += amount;
        if(curHp > maxHp) curHp = maxHp;
        showPlayerHp.Invoke(curHp < 0 ? 0 : curHp);
    }

    public void AddDestroyCount(EnumClass.MeteorSize type, int count)
    {
        destroyCounts[(int)type]++;
    }

    public int GetDregs()
    {
        return dregsCount;
    }
    
    public int GetPerfectDregs()
    {
        return perfectDregsCount;
    }

    public int GetDestroyCount(EnumClass.MeteorSize type)
    {
        return destroyCounts[(int)type];
    }

    public void TakeDamage(int damage)
    {
        if (!isDie && !isTakeDamage)
        {
            int ran = UnityEngine.Random.Range(0, 100);

            if(ran < unbeatableChance)
            {
                PlayerController.GetInstance().OnShield();
            }
            else
            {
                isTakeDamage = true;
                curHp -= damage;
                showPlayerHp.Invoke(curHp < 0 ? 0 : curHp);
                SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.PLAYER_DAMAGED);
                StartCoroutine(CoTakeDamage());
                if (curHp <= 0)
                {
                    curHp = 0;
                    Die();
                }
            }

        }
    }

    IEnumerator CoTakeDamage()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
        isTakeDamage = false;
    }

    public void TakeDamageAction()
    {
        isTakeDamage = false;
        GetComponent<PlayerController>().IsMove(true);
    }

    private void Die()
    {
        isDie = true;
        SaveData();
        GetComponent<PlayerAnimation>().PlayGameover();
    }

    public void DieAction()
    {
        InGameUiManager.GetInstance().OnGameOverPannel(false);
    }
}

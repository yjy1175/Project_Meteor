using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatController : MonoBehaviour
{
    [SerializeField] private List<GageHolderUI> gageHolderUIs = new List<GageHolderUI>();
    [SerializeField] private List<CreateTextUI> createTextUIs = new List<CreateTextUI>();
    [SerializeField] private CreateTextUI perfectDregsUI;
    [SerializeField] private int perfectDregsCount = 0;
    [SerializeField] private int changePerfectDregsCount = 0;
    [SerializeField] private int dregsCount = 0;
    [SerializeField] private List<int> dummyUpradeValue;
    private UpgradeStatData upgradeData;

    [SerializeField] private GameObject dregChangePannel;
    [SerializeField] private Text dregsCountText;
    [SerializeField] private Text perfectDregsCountText;

    public void LoadUpgradeValue()
    {
        PlayerSaveData dummyData = DataManager.GetInstance().playerSaveData.GetData();
        perfectDregsCount = dummyData.perfectDregCount;
        dregsCount = dummyData.dregCount;
        dummyUpradeValue = new List<int>(dummyData.upgradeValues);
        upgradeData = DataManager.GetInstance().updaradeStatData.GetData();

        RefreshUI();
    }

    public void RefreshUI()
    {
        for (int i = 0; i < (int)EnumClass.Upgrade.End; i++)
        {
            gageHolderUIs[i].ResetGage();
            gageHolderUIs[i].SetGage(dummyUpradeValue[i] + 1);
            createTextUIs[i].SetNumberLock(dummyUpradeValue[i] + 1, EnumClass.LEVEL_NUM_LENGHT);
        }

        perfectDregsUI.SetNumberLock(perfectDregsCount, EnumClass.DREGS_NUM_LENGTH);
        dregsCountText.text = dregsCount.ToString();
        perfectDregsCountText.text = changePerfectDregsCount.ToString();
    }

    public void PlusPerfectDregs()
    {
        if(dregsCount >= 200)
        {
            dregsCount -= 200;
            changePerfectDregsCount += 1;

            dregsCountText.text = dregsCount.ToString();
            perfectDregsCountText.text = changePerfectDregsCount.ToString();

            SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.BUTTON);
        }
        else
        {
            return;
        }
    }

    public void MinusPerfectDregs()
    {
        if(changePerfectDregsCount > 0)
        {
            dregsCount += 200;
            changePerfectDregsCount -= 1;

            dregsCountText.text = dregsCount.ToString();
            perfectDregsCountText.text = changePerfectDregsCount.ToString();

            SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.BUTTON);
        }
        else
        {
            return;
        }
    }

    public void MaxPerfectDregs()
    {
        if (dregsCount >= 200)
        {
            changePerfectDregsCount = 0;
            dregsCount = DataManager.GetInstance().playerSaveData.GetData().dregCount;

            changePerfectDregsCount += dregsCount / 200;
            dregsCount -= 200 * changePerfectDregsCount;
            
            dregsCountText.text = dregsCount.ToString();
            perfectDregsCountText.text = changePerfectDregsCount.ToString();

            SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.BUTTON);
        }
        else
        {
            return;
        }
    }

    public void ResetUpgradeValue(int neededCount)
    {
        if(perfectDregsCount >= neededCount)
        {
            int resetPerfectDregs = -neededCount;

            for(EnumClass.Upgrade i = EnumClass.Upgrade.Power; i < EnumClass.Upgrade.End; i++)
            {
                for(int value = dummyUpradeValue[(int)i]; value > 0; value--)
                {
                    resetPerfectDregs += upgradeData.upgradeStatInfo[(int)i].upgradeCost[value];
                }
                dummyUpradeValue[(int)i] = 0;
            }

            perfectDregsCount += resetPerfectDregs;

            SaveUpgradeValue();

            SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.BUTTON);
        }
        else
        {
            return;
        }
    }

    public void SaveUpgradeValue()
    {
        PlayerSaveData dummyData = DataManager.GetInstance().playerSaveData.GetData();
        dummyData.upgradeValues = dummyUpradeValue;
        dummyData.perfectDregCount = perfectDregsCount + changePerfectDregsCount;
        dummyData.dregCount = dregsCount;
        changePerfectDregsCount = 0;
        DataManager.GetInstance().UpdatePlayerSaveData(dummyData);
        LoadUpgradeValue();
    }

    public void PlusUpgradeDummyValue(int idx)
    {
        if (upgradeData.upgradeStatInfo[idx].upgradeValue[dummyUpradeValue[idx]] == upgradeData.upgradeStatInfo[idx].maxUpgradeValue)
        {
            return;
        }

        if (perfectDregsCount >= upgradeData.upgradeStatInfo[idx].upgradeCost[dummyUpradeValue[idx] + 1])
        {
            perfectDregsCount -= upgradeData.upgradeStatInfo[idx].upgradeCost[dummyUpradeValue[idx] + 1];

            dummyUpradeValue[idx]++;

            RefreshUI();

            SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.BUTTON);
        }
    }

    public void MinusUpgradeDummyValue(int idx)
    {
        PlayerSaveData originData = DataManager.GetInstance().playerSaveData.GetData();

        if (dummyUpradeValue[idx] > originData.upgradeValues[idx])
        {
            perfectDregsCount += upgradeData.upgradeStatInfo[idx].upgradeCost[dummyUpradeValue[idx]];
            dummyUpradeValue[idx]--;

            RefreshUI();

            SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.BUTTON);
        }
        else
        {
            return;
        }
    }

    public void ToggleDregChangePannel(bool on)
    {
        dregChangePannel.SetActive(on);
        SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.BUTTON);
    }
}

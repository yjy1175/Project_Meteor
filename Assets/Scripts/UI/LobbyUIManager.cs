using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    #region Sgt
    private static LobbyUIManager instance;
    public static LobbyUIManager GetInstance() => instance;

    private void Awake()
    {
        instance = null; 

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

    [SerializeField] private CreateTextUI dregsCount;
    [SerializeField] private CreateTextUI perfectDregsCount;
    [SerializeField] private List<GageHolderUI> upgradeValueHolders = new List<GageHolderUI>();
    [SerializeField] private List<CreateTextUI> upgradeNumbers = new List<CreateTextUI>();
    [SerializeField] private Text bestWaveNumText;

    [SerializeField] private GameObject helpPannel;
    [SerializeField] private GameObject upgradePannel;
    [SerializeField] private GameObject shopPannel;

    [SerializeField] private GameObject inGameUI;

    [SerializeField] private Slider allVolumeScale;
    [SerializeField] private Slider bgmVolumeScale;
    [SerializeField] private Slider effectVolumeScale;

    [SerializeField] private Toggle allMute;
    [SerializeField] private Toggle bgmMute;
    [SerializeField] private Toggle effectVolumeMute;

    private void Start()
    {
        DataManager.GetInstance().showDregs += ShowDregsCount;
        DataManager.GetInstance().showPerfectDregs += ShowPerfectDregsCount;
        DataManager.GetInstance().showUpgradeLists += SetUpgradeUI;


        allVolumeScale.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.SetAllVolumeScale(value);
            SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.BUTTON);
        });

        bgmVolumeScale.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.SetBgmVolumeScale(value);
            SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.BUTTON);
        });

        effectVolumeScale.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.SetEffectVolumeScale(value);
            SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.BUTTON);
        });

        allMute.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.SetMuteAll(value);
            SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.BUTTON);
        });

        bgmMute.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.SetMuteBgm(value);
            SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.BUTTON);
        });

        effectVolumeMute.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.SetMuteEffect(value);
            SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.BUTTON);
        });
    }

    public void GameStart()
    {
        gameObject.SetActive(false);
        inGameUI.SetActive(true);
        GameManager.GetInstance().GameStart();
    }

    public void ShowBestWave(int wave)
    {
        bestWaveNumText.text = "Best Wave : " + wave.ToString();
    }

    private void ShowDregsCount(int amount)
    {
        dregsCount.SetNumberLock(amount, EnumClass.DREGS_NUM_LENGTH);
    }

    private void ShowPerfectDregsCount(int amount)
    {
        perfectDregsCount.SetNumberLock(amount, EnumClass.DREGS_NUM_LENGTH);
    }

    private void SetUpgradeUI(List<int> upgradeValues)
    {
        for (int i = 0; i < (int)EnumClass.Upgrade.End; i++)
        {
            upgradeValueHolders[i].ResetGage();
            upgradeValueHolders[i].SetGage(upgradeValues[i] + 1);
            upgradeNumbers[i].SetNumberLock(upgradeValues[i] + 1, EnumClass.LEVEL_NUM_LENGHT);
        }
    }

    public void ToggleHelpPannel(bool on)
    {
        helpPannel.SetActive(on);
        SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.BUTTON);
    }

    public void ToggleUpgradePannel(bool on)
    {
        if(on)
            upgradePannel.GetComponent<StatController>().LoadUpgradeValue();
        upgradePannel.SetActive(on);
        SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.BUTTON);
    }

    public void ToggleShopPannel(bool on)
    {
        if (DataManager.GetInstance().playerSaveData.GetData().isAdRemove)
        {
            shopPannel.transform.GetChild(1).GetComponent<Button>().interactable = false;
            shopPannel.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        }

        if (DataManager.GetInstance().playerSaveData.GetData().isDoubleDropRate)
        {
            shopPannel.transform.GetChild(2).GetComponent<Button>().interactable = false;
            shopPannel.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
        }

        shopPannel.SetActive(on);
        SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.BUTTON);
    }
}

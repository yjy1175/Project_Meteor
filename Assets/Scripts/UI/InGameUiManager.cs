using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUiManager : MonoBehaviour
{
    #region Sgt
    private static InGameUiManager instance;
    public static InGameUiManager GetInstance() => instance;

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
    [SerializeField] private CreateTextUI waveNum;
    [SerializeField] private CreateTextUI dregsCount;
    [SerializeField] private CreateTextUI perfectDregsCount;
    [SerializeField] private GameObject playerHp;
    [SerializeField] private List<GageHolderUI> upgradeValueHolders = new List<GageHolderUI>();
    [SerializeField] private List<CreateTextUI> upgradeNumbers = new List<CreateTextUI>();

    public GageHolderUI playerHpHolder;
    public VirtualDPad curPed;

    [SerializeField] private GameObject leftPedPannel;
    [SerializeField] private GameObject rightPedPannel;

    [SerializeField] private GameObject pausePannel;
    [SerializeField] private Text pausePerfectDregCountText;
    [SerializeField] private Text pauseDregCountText;
    [SerializeField] private Text pauseWaveNumberText;
    [SerializeField] private List<Text> pauseDestroyCountText = new List<Text>();

    [SerializeField] private GameObject settingPannel;

    [SerializeField] private GameObject gameoverPannel;
    [SerializeField] private Image gameoverTitle;
    [SerializeField] private Sprite gameoverIamge;
    [SerializeField] private Sprite gameclearImage;
    [SerializeField] private Text perfectDregCountText;
    [SerializeField] private Text dregCountText;
    [SerializeField] private Text waveNumberText;

    [SerializeField] private WaveAlert waveAlertPannel;

    public void StartGame()
    {
        WaveManager.GetInstance().showWaveNumber += SetWaveNumber;
        PlayerMovement.GetInstance().updatePlayerHpPosition += UpdatePlayerHpPosition;
        PlayerStats.GetInstance().showDregs += ShowDregs;
        PlayerStats.GetInstance().showPerfectDregs += ShowPerfectDregs;

        curPed = leftPedPannel.transform.GetChild(0).GetComponent<VirtualDPad>();
    }

    private void SetWaveNumber(int waveNumber)
    {
        waveNum.SetNumberLock(waveNumber, EnumClass.WAVE_NUM_LENGTH);
        waveAlertPannel.SetWaveNum(waveNumber);
    }

    private void ShowDregs(int amount)
    {
        dregsCount.SetNumberLock(amount, EnumClass.DREGS_NUM_LENGTH);
    }

    private void ShowPerfectDregs(int amount)
    {
        perfectDregsCount.SetNumberLock(amount, EnumClass.DREGS_NUM_LENGTH);
    }

    private void UpdatePlayerHpPosition(Vector2 pos)
    {
        playerHp.GetComponent<RectTransform>().position = new Vector3(pos.x, playerHp.GetComponent<RectTransform>().position.y, playerHp.GetComponent<RectTransform>().position.z);
    }

    public void SetUpgradeUI(List<int> upgradeValues)
    {
        for(int i = 0; i < (int)EnumClass.Upgrade.End; i++)
        {
            upgradeValueHolders[i].SetGage(upgradeValues[i] + 1);
            upgradeNumbers[i].SetNumberLock(upgradeValues[i] + 1, EnumClass.LEVEL_NUM_LENGHT);
        }
    }

    public void OnGameOverPannel(bool clear)
    {
        Time.timeScale = 0;
        gameoverTitle.sprite = clear ? gameclearImage : gameoverIamge;
        perfectDregCountText.text = PlayerStats.GetInstance().GetPerfectDregs().ToString();
        dregCountText.text = PlayerStats.GetInstance().GetDregs().ToString();
        waveNumberText.text = (WaveManager.GetInstance().CurWaveNumber - 1).ToString();

        gameoverPannel.SetActive(true);
    }

    public void UpdateDregsCoutnTextForAdsReward()
    {
        dregCountText.text = PlayerStats.GetInstance().GetDregs().ToString() + "x2";
    }

    public void TogglePausePannel(bool on)
    {
        pauseWaveNumberText.text = WaveManager.GetInstance().CurWaveNumber.ToString();
        pausePerfectDregCountText.text = "x" + PlayerStats.GetInstance().GetPerfectDregs().ToString();
        pauseDregCountText.text = "x" + PlayerStats.GetInstance().GetDregs().ToString();

        for(EnumClass.MeteorSize type = EnumClass.MeteorSize.Small; type < EnumClass.MeteorSize.End; type++)
        {
            pauseDestroyCountText[(int)type].text = "x" + PlayerStats.GetInstance().GetDestroyCount(type).ToString();
        }

        pausePannel.SetActive(on);

        if (on)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.BUTTON);
    }

    public void ToggleSettingPannel(bool on)
    {
        settingPannel.SetActive(on);
        SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.BUTTON);
    }

    public void TogglePedPannel(bool isLeft)
    {
        leftPedPannel.SetActive(isLeft);
        rightPedPannel.SetActive(!isLeft);
        if (isLeft)
        {
            curPed = leftPedPannel.transform.GetChild(0).GetComponent<VirtualDPad>();
        }
        else
        {
            curPed = rightPedPannel.transform.GetChild(0).GetComponent<VirtualDPad>();
        }
        SoundManager.Instance.PlayOneShot(EnumClass.SOUND_EFFECT.BUTTON);
    }

    public void GoToMain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene((int)EnumClass.SceneNumber.Loading);

        WaveManager.isRestart = false;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene((int)EnumClass.SceneNumber.Loading);

        WaveManager.isRestart = true;
    }
}

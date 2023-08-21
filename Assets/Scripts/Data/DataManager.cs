using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static EnumClass;

public class DataManager : MonoBehaviour
{
    #region Sgt
    private static DataManager instance;
    public static DataManager GetInstance() => instance;

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
    #region Event
    public event EventManager.SingleInt showDregs;
    public event EventManager.SingleInt showPerfectDregs;
    public event EventManager.ListInt showUpgradeLists;
    #endregion
    private readonly string key = "g13INZ3XNHWUJTpwZkGyxc12ASVOgdv8";

    [Header(GAME_PATH)]
    public DataHolder<WaveData> waveData;
    public DataHolder<UpgradeStatData> updaradeStatData;
    public DataHolder<ShopInfo> shopInfo;
    public DataHolder<PlayerBaseData> playerBaseData;

    [Header(LOCAL_FILE)]
    public DataHolder<PlayerSaveData> playerSaveData;

    private void Start()
    {
        LoadGameData();
        LoadLocalData();

        if (WaveManager.isRestart)
            LobbyUIManager.GetInstance().GameStart();
    }

    private void LoadGameData()
    {
        waveData = new DataHolder<WaveData>(JsonParser.LoadGameData<WaveData>(Path.Combine(GAME_PATH, WAVE_DATA_FILE)));
        updaradeStatData = new DataHolder<UpgradeStatData>(JsonParser.LoadGameData<UpgradeStatData>(Path.Combine(GAME_PATH, UPGRADE_DATA_FILE)));
        shopInfo = new DataHolder<ShopInfo>(JsonParser.LoadGameData<ShopInfo>(Path.Combine(GAME_PATH, SHOP_DATA_FILE)));
        playerBaseData = new DataHolder<PlayerBaseData>(JsonParser.LoadGameData<PlayerBaseData>(Path.Combine(GAME_PATH, PLAYER_BASE_DATA_FILE)));
    }

    public void LoadLocalData()
    {
        string loadPath = Application.persistentDataPath;

        if (File.Exists(Path.Combine(loadPath, LOCAL_FILE + LOCAL_FILE_EXTENSION)))
        {
            try
            {
                playerSaveData = new DataHolder<PlayerSaveData>(JsonParser.LoadJsonFileAES<PlayerSaveData>(loadPath, LOCAL_FILE, key));
            }
            catch
            {
                File.Delete(Path.Combine(loadPath, LOCAL_FILE + LOCAL_FILE_EXTENSION));
                Debug.Log("Error : save file crash");
            }
        }
        else
        {
            int[] upgradeValues = Enumerable.Repeat(DEFAULT_UPGRADE_VALUE, updaradeStatData.GetData().upgradeStatInfo.Count).ToArray();
            int[] destroyValues = Enumerable.Repeat(DEFAULT_UPGRADE_VALUE, (int)MeteorSize.End).ToArray();

            playerSaveData = new DataHolder<PlayerSaveData>(new PlayerSaveData()
            {
                upgradeValues = new List<int>(upgradeValues),
                bestWaveCount = 0,
                dregCount = 0,
                perfectDregCount = 0,
                destroyCounts = new List<int>(destroyValues),
                isAdRemove = false,
                isDoubleDropRate = false,
            });

            JsonParser.CreateJsonFile(loadPath, LOCAL_FILE, AESParser.Encrypt256(JsonParser.ObjectToJson(playerSaveData.GetData()), key));
        }

        showDregs.Invoke(playerSaveData.GetData().dregCount);
        showPerfectDregs.Invoke(playerSaveData.GetData().perfectDregCount);
        showUpgradeLists.Invoke(playerSaveData.GetData().upgradeValues);
        LobbyUIManager.GetInstance().ShowBestWave(playerSaveData.GetData().bestWaveCount);
    }

    public void SaveLocalData()
    {
        string loadPath = Application.persistentDataPath;

        if(File.Exists(Path.Combine(loadPath, LOCAL_FILE + LOCAL_FILE_EXTENSION)))
        {
            File.Delete(Path.Combine(loadPath, LOCAL_FILE + LOCAL_FILE_EXTENSION));
        }

        if(playerSaveData != null)
        {
            JsonParser.CreateJsonFile(loadPath, LOCAL_FILE, AESParser.Encrypt256(JsonParser.ObjectToJson(playerSaveData.GetData()), key));
        }
        else
        {
            Debug.Log("Error : no local data");
        }
    }

    public void UpdatePlayerSaveData(PlayerSaveData saveData)
    {
        playerSaveData.SetData(saveData);

        showDregs.Invoke(playerSaveData.GetData().dregCount);
        showPerfectDregs.Invoke(playerSaveData.GetData().perfectDregCount);
        showUpgradeLists.Invoke(playerSaveData.GetData().upgradeValues);

        SaveLocalData();
    }
}

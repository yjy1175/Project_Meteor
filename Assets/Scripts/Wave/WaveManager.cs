using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    #region Sgt
    private static WaveManager instance;
    public static WaveManager GetInstance() => instance;

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
    public event EventManager.SingleInt showWaveNumber;
    #endregion
    [SerializeField] private int curWaveNumber = 0;
    public int CurWaveNumber { get { return curWaveNumber; } }
    [SerializeField] private WaveData waveData;
    [SerializeField] private WaveInfo curWaveInfo;
    [SerializeField] private List<int> curMeteorCounts;

    [SerializeField] private List<Transform> spawnPosition = new List<Transform>();
    [SerializeField] private List<GameObject> meteors = new List<GameObject>();

    public static bool isRestart = false;

    public void StartGame()
    {
        SoundManager.Instance.PlayInfinity(EnumClass.SOUND_BGM.INGAME);
        StartCoroutine(CoStartGame());
    }

    IEnumerator CoStartGame()
    {
        yield return new WaitForSeconds(3f);
        waveData = DataManager.GetInstance().waveData.GetData();
        SetWave();
    }

    private void SetWave()
    {
        if(curWaveNumber < waveData.waveInfo.Count)
        {
            curWaveInfo = waveData.waveInfo[curWaveNumber++];
            curMeteorCounts = new List<int>(curWaveInfo.meteorCount);
            showWaveNumber.Invoke(curWaveNumber);
            WaveStart();
        }
        else
        {
            PlayerStats.GetInstance().SaveData();
            InGameUiManager.GetInstance().OnGameOverPannel(true);
        }
    }

    private void WaveStart()
    {
        StartCoroutine(CoStartWave());
    }

    IEnumerator CoStartWave()
    {
        while (!IsWaveEnd())
        {
            for(int i = 0; i < curWaveInfo.spawnCount; i++)
            {
                int randomSize = Random.Range((int)EnumClass.MeteorSize.Small, (int)EnumClass.MeteorSize.End);
                while (curMeteorCounts[randomSize] < 1)
                {
                    randomSize = Random.Range((int)EnumClass.MeteorSize.Small, (int)EnumClass.MeteorSize.End);
                }             
                int randomPosition = Random.Range(0, spawnPosition.Count);
                Meteor newMeteor = Instantiate(meteors[randomSize], spawnPosition[randomPosition]).GetComponent<Meteor>();
                newMeteor.SetInfo(waveData.baseMeteorHp, curWaveInfo.meteorHpRate[randomSize], waveData.meteorMoveSpeed[randomSize], waveData.meteorDamage[randomSize]);
                curMeteorCounts[randomSize]--;
            }

            yield return new WaitForSeconds(curWaveInfo.spawnTime);
        }

        yield return new WaitForSeconds(waveData.waveTerm);
        SetWave();
    }

    private bool IsWaveEnd()
    {
        bool isEnd = true;

        for(int i = 0; i < curMeteorCounts.Count; i++)
        {
            if (curMeteorCounts[i] > 0)
            {
                isEnd = false;
                break;
            }
                
        }

        return isEnd;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Sgt
    private static GameManager instance;
    public static GameManager GetInstance() => instance;

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

    [SerializeField] private GameObject playerPrefab;

    private void Start()
    {
        SoundManager.Instance.PlayInfinity(EnumClass.SOUND_BGM.LOBBY);
    }

    public void GameStart()
    {
        Instantiate(playerPrefab);
        InGameUiManager.GetInstance().StartGame();
        WaveManager.GetInstance().StartGame();
    }
}

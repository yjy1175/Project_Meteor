using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemMessage : MonoBehaviour
{
    #region Sgt
    private static SystemMessage instance;
    public static SystemMessage GetInstance() => instance;

    [SerializeField] private Button exitButton;

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

    [SerializeField] private Text messageText;

    private void Start()
    {
        exitButton.onClick.AddListener(() =>
        {
            OffMessage();
        });
        gameObject.SetActive(false);
    }

    public void OnMessage(string message)
    {
        messageText.text = message;
        gameObject.SetActive(true);
    }

    private void OffMessage()
    {
        gameObject.SetActive(false);
    }
}

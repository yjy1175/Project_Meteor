using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public class GageHolderUI : MonoBehaviour
{
    [SerializeField] private GameObject gage;
    [SerializeField] private List<GameObject> gages = new List<GameObject>();

    public void SetHpEvnet(int baseValue)
    {
        PlayerStats.GetInstance().showPlayerHp += ChangeValue;

        for (int i = 0; i < baseValue; i++)
        {
            gages.Add(Instantiate(gage, transform));
        }
    }

    public void SetGage(int baseValue)
    {
        for (int i = 0; i < baseValue; i++)
        {
            gages.Add(Instantiate(gage, transform));
        }
    }

    public void ResetGage()
    {
        for(int i = 0; i< gages.Count; i++)
        {
            Destroy(gages[i]);
        }

        gages.Clear();
    }

    private void ChangeValue(int value)
    {
        for(int i = 0; i < gages.Count; i++)
        {
            gages[i].gameObject.SetActive(i < value ? true : false);
        }
    }
}

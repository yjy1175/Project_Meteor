using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataHolder<T>
{
    [SerializeField] private T data;

    public DataHolder(T data)
    {
        this.data = data;
    }

    public T GetData()
    {
        return data;
    }

    public void SetData(T data)
    {
        this.data= data;
    }
}

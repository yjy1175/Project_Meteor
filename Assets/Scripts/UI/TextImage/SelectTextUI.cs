using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTextUI : MonoBehaviour
{
    [SerializeField] private List<Sprite> numSprites = new List<Sprite>();

    public void SetNumber(int number)
    {
        GetComponent<Image>().sprite = numSprites[number];
    }
}

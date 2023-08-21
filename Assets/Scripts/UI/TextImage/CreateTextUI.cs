using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTextUI : MonoBehaviour
{
    [SerializeField] private GameObject numberUI;
    [SerializeField] private Transform target;

    [SerializeField] private List<SelectTextUI> numbers = new List<SelectTextUI>();

    public void SetNumber(int number, Transform target)
    {
        // 초기화를 할지 재사용을 할지??
        char[] numberS = number.ToString().ToCharArray();
        this.target = target;
        GetComponent<RectTransform>().position = target.position + Vector3.up * (0.5f + target.GetComponent<BoxCollider2D>().size.y);
        for (int i = 0; i < numberS.Length; i++)
        {
            SelectTextUI newUI = Instantiate(numberUI, transform).GetComponent<SelectTextUI>();
            newUI.SetNumber((int)char.GetNumericValue(numberS[i]));
            numbers.Add(newUI);
        }
    }

    public void SetNumberLock(int number, int length)
    {
        char[] numberS;
        if (number.ToString().Length > length)
        {
            number = 9;
            numberS = number.ToString().PadLeft(length, '9').ToCharArray();
        }
        else
        {
            numberS = number.ToString().PadLeft(length, '0').ToCharArray();
        }
        
        for (int i = 0; i < numberS.Length; i++)
        {
            transform.GetChild(i).GetComponent<SelectTextUI>().SetNumber((int)char.GetNumericValue(numberS[i]));
        }
    }

    public void UpdateNumber(int number)
    {
        char[] numberS = number.ToString().ToCharArray();
        int count = numberS.Length;
        for (int i = 0; i < numbers.Count; i++)
        {
            if(i < count)
            {
                numbers[i].SetNumber((int)char.GetNumericValue(numberS[i]));
            }
            else
            {
                numbers[i].gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if(target != null)
        {
            GetComponent<RectTransform>().position = target.position + Vector3.up * (1 + target.GetComponent<BoxCollider2D>().size.y);
        }
    }
}

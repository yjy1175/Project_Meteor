using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Next : MonoBehaviour
{
    [SerializeField] private GameObject fadePannel;

    void Start()
    {
        StartCoroutine(CoFadeInOut());
    }
    
    private void GoToNext()
    {
        SceneManager.LoadScene((int)EnumClass.SceneNumber.Main);
    }

    IEnumerator CoFadeInOut()
    {
        float fadeCount = 1f;

        while (fadeCount > 0)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            fadePannel.GetComponent<Image>().color = new Color(
                fadePannel.GetComponent<Image>().color.r, 
                fadePannel.GetComponent<Image>().color.g, 
                fadePannel.GetComponent<Image>().color.b, 
                fadeCount);
        }

        yield return new WaitForSeconds(1f);

        fadeCount = 0f;

        while (fadeCount < 1f)
        {
            fadeCount += 0.02f;
            yield return new WaitForSeconds(0.02f);
            fadePannel.GetComponent<Image>().color = new Color(
                fadePannel.GetComponent<Image>().color.r,
                fadePannel.GetComponent<Image>().color.g,
                fadePannel.GetComponent<Image>().color.b,
                fadeCount);
        }

        GoToNext();
    }
}

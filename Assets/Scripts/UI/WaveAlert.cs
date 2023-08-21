using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveAlert : MonoBehaviour
{
    [SerializeField] private Image waveIamge;
    [SerializeField] private CreateTextUI waveNum;
    [SerializeField] private List<Image> waveNumImages = new List<Image>();


    public void SetWaveNum(int wave)
    {
        waveNum.SetNumberLock(wave, 3);
        gameObject.SetActive(true);
        StartCoroutine(CoFadeOut());
    }

    IEnumerator CoFadeOut()
    {
        float fadeCount = 0f;

        while (fadeCount < 0.5f)
        {
            fadeCount += 0.02f;
            yield return new WaitForSeconds(0.02f);
            waveIamge.color = new Color(waveIamge.color.r, waveIamge.color.g, waveIamge.color.b, fadeCount);
            for (int i = 0; i < waveNumImages.Count; i++)
            {
                waveNumImages[i].color = new Color(waveNumImages[i].color.r, waveNumImages[i].color.g, waveNumImages[i].color.b, fadeCount);
            }
        }

        fadeCount = 0.5f;

        while(fadeCount > 0)
        {
            fadeCount -= 0.02f;
            yield return new WaitForSeconds(0.02f);
            waveIamge.color = new Color(waveIamge.color.r, waveIamge.color.g, waveIamge.color.b, fadeCount);
            for(int i = 0; i < waveNumImages.Count; i++)
            {
                waveNumImages[i].color = new Color(waveNumImages[i].color.r, waveNumImages[i].color.g, waveNumImages[i].color.b, fadeCount);
            }
        }

        gameObject.SetActive(false);
    }
}

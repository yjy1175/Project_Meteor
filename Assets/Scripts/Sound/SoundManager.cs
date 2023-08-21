using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumClass;

public class SoundManager : MonoBehaviour
{
    #region SingleTon
    private static SoundManager instance = null;
    public static SoundManager Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                return null;
            }
        }
    }
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

    public List<AudioClip> bgmLists = new List<AudioClip>();
    public List<AudioClip> effectLists = new List<AudioClip>();

    public AudioSource bgmAudio;
    public AudioSource effectAudio;

    [SerializeField] private float allVolumeScale;
    public float AllVolumeScale
    {
        get { return allVolumeScale; }
        set
        {
            allVolumeScale = value;
            SetBgmVolumeScale(bgmVolumeScale);
            SetEffectVolumeScale(effectVolumeScale);
        }
    }
    [SerializeField] private float bgmVolumeScale;
    public float BgmVolumeScale
    {
        get
        {
            return bgmVolumeScale;
        }
        set 
        { 
            bgmVolumeScale = value;
            bgmAudio.volume = value * AllVolumeScale;
        }
    }
    [SerializeField] private float effectVolumeScale;
    public float EffectVolumeScale
    {
        get
        {
            return effectVolumeScale;
        }
        set
        {
            effectVolumeScale = value;
            effectAudio.volume = value * AllVolumeScale;
        }
    }

    public void PlayOneShot(SOUND_EFFECT num)
    {
        effectAudio.PlayOneShot(effectLists[(int)num]);
    }

    public void PlayInfinity(SOUND_BGM num)
    {
        bgmAudio.Stop();

        if (!bgmAudio.isPlaying)
        {
            bgmAudio.clip = bgmLists[(int)num];
            bgmAudio.loop = true;
            bgmAudio.Play();
        }
    }

    public void SetAllVolumeScale(float scale)
    {
        AllVolumeScale = scale;
    }

    public void SetBgmVolumeScale(float scale)
    {
        BgmVolumeScale = scale;
    }

    public void SetEffectVolumeScale(float scale)
    {
        EffectVolumeScale = scale;
    }

    public void SetMuteAll(bool isMute)
    {
        bgmAudio.mute = isMute;
        effectAudio.mute = isMute;
    }

    public void SetMuteBgm(bool isMute)
    {
        bgmAudio.mute = isMute;
    }

    public void SetMuteEffect(bool isMute)
    {
        effectAudio.mute = isMute;
    }
}

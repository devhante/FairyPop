using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {
    public Image[] BackVolumeImage;
    public Sprite BlankVolume;
    public Sprite FillVolume;
    public Image[] EffectVolumeImage;
    public SoundMng SMG;
    int BackVolume;
    int EffectVolume;
    // Use this for initialization
    void Start() {
        BackVolume = 5;
        EffectVolume = 5;
    }
    
    // Update is called once per frame
    void Update() {
        switch (BackVolume)
        {
            case 0:
                SMG.Bgm.volume = 0;
                break;
            case 1:
                SMG.Bgm.volume = 0.25f;
                break;
            case 2:
                SMG.Bgm.volume = 0.5f;
                break;
            case 3:
                SMG.Bgm.volume = 0.75f;
                break;
            case 4:
                SMG.Bgm.volume = 1;
                break;
            default:
                break;
        }
        switch (EffectVolume)
        {
            case 0:
                SMG.ClickSound.volume = 0;
                break;
            case 1:
                SMG.ClickSound.volume = 0.25f;
                break;
            case 2:
                SMG.ClickSound.volume = 0.5f;
                break;
            case 3:
                SMG.ClickSound.volume = 0.75f;
                break;
            case 4:
                SMG.ClickSound.volume = 1;
                break;
            default:
                break;
        }
    }

    public void BackPlus()
    {
        SMG.PlayClickSound();
        switch (BackVolume)
        {
            case 0:
                BackVolume = 1;
                BackVolumeImage[0].sprite = FillVolume;
                break;
            case 1:
                BackVolume = 2;
                BackVolumeImage[1].sprite = FillVolume;
                break;
            case 2:
                BackVolume = 3;
                BackVolumeImage[2].sprite = FillVolume;
                break;
            case 3:
                BackVolume = 4;
                BackVolumeImage[3].sprite = FillVolume;
                break;
            case 4:
                BackVolume = 5;
                BackVolumeImage[4].sprite = FillVolume;
                break;
            default:
                break;
        }
    }
    public void BackMinus()
    {
        SMG.PlayClickSound();
        switch (BackVolume)
        {
            case 1:
                BackVolume = 0;
                BackVolumeImage[0].sprite = BlankVolume;
                break;
            case 2:
                BackVolume = 1;
                BackVolumeImage[1].sprite = BlankVolume;
                break;
            case 3:
                BackVolume = 2;
                BackVolumeImage[2].sprite = BlankVolume;
                break;
            case 4:
                BackVolume = 3;
                BackVolumeImage[3].sprite = BlankVolume;
                break;
            case 5:
                BackVolume = 4;
                BackVolumeImage[4].sprite = BlankVolume;
                break;
            default:
                break;
        }
    }
    public void EffectPlus()
    {
        SMG.PlayClickSound();
        switch (EffectVolume)
        {
            case 0:
                EffectVolume = 1;
                EffectVolumeImage[0].sprite = FillVolume;
                break;
            case 1:
                EffectVolume = 2;
                EffectVolumeImage[1].sprite = FillVolume;
                break;
            case 2:
                EffectVolume = 3;
                EffectVolumeImage[2].sprite = FillVolume;
                break;
            case 3:
                EffectVolume = 4;
                EffectVolumeImage[3].sprite = FillVolume;
                break;
            case 4:
                EffectVolume = 5;
                EffectVolumeImage[4].sprite = FillVolume;
                break;
            default:
                break;
        }
    }
    public void EffectMinus()
    {
        SMG.PlayClickSound();
        switch (EffectVolume)
        {
            case 1:
                EffectVolume = 0;
                EffectVolumeImage[0].sprite = BlankVolume;
                break;
            case 2:
                EffectVolume = 1;
                EffectVolumeImage[1].sprite = BlankVolume;
                break;
            case 3:
                EffectVolume = 2;
                EffectVolumeImage[2].sprite = BlankVolume;
                break;
            case 4:
                EffectVolume = 3;
                EffectVolumeImage[3].sprite = BlankVolume;
                break;
            case 5:
                EffectVolume = 4;
                EffectVolumeImage[4].sprite = BlankVolume;
                break;
            default:
                break;
        }
    }
}

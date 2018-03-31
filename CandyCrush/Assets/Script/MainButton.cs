using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainButton : MonoBehaviour {
    public Image OptionImage;
    public Image StageImage;
    public StageButtonCtrl SBC;
    public SoundMng SMG;
    private void Start()
    {
        //SBC = GameObject.Find("StageScene").GetComponent<StageButtonCtrl>();
    }
    public void OpenOption()
    {
        SMG.PlayClickSound();
        OptionImage.gameObject.SetActive(true);
    }
    
    public void CloseOption()
    {
        SMG.PlayClickSound();
        OptionImage.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        SMG.PlayClickSound();
        StageImage.gameObject.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            if (SBC.GameWindow[i].gameObject.activeSelf)
            {
                Debug.Log("Window");
                SBC.GameWindow[i].gameObject.SetActive(false);
            }
        }
    }
    public void CloseGame()
    {
        SMG.PlayClickSound();
        for (int i = 0; i < 3; i++)
            SBC.StagePaper[i].localPosition = new Vector3(0, 0, 0);
        StageImage.gameObject.SetActive(false);
    }
}

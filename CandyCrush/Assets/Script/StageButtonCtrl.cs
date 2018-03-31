using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageButtonCtrl : MonoBehaviour {
    public Button[] StageButton;
    public Image[] StageLockKey;
    public RectTransform[] StagePaper;
    public Image[] GameWindow;
    Vector3[] Target = { new Vector3(0, 318, 0), new Vector3(-170, -337, 0), new Vector3(180, -337, 0) };
    private Vector3[] buttonVelocity = { Vector3.zero, Vector3.zero, Vector3.zero };

    public SoundMng SMG;
    // Use this for initialization
    void Start () {
		if (GameManager.flag2 == true) UnLockBossStage(1);
		if (GameManager.flag3 == true) UnLockBossStage(2);
	}
	
	// Update is called once per frame
	void Update () {
        MoveStage();
    }

    private void MoveStage()
    {
        for(int i = 0; i < 3; i++)
            StagePaper[i].localPosition = Vector3.SmoothDamp(StagePaper[i].localPosition, Target[i], ref buttonVelocity[i], 0.5f);
    }

    public void UnLockBossStage(int i)
    {
        StageButton[i].interactable = true;
        StageLockKey[i].gameObject.SetActive(false);
    }

    public void OpenWindow(int i)
    {
        SMG.PlayClickSound();
        GameWindow[i].gameObject.SetActive(true);
    }

    public void CloseWindow(int i)
    {
        SMG.PlayClickSound();
        GameWindow[i].gameObject.SetActive(false);
    }

    public void StartGame(int i)
    {
        SMG.PlayClickSound();
        SceneManager.LoadScene(i);
    }
}

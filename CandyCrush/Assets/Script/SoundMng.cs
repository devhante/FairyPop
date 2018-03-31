using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMng : MonoBehaviour {

    public AudioSource ClickSound;
    public AudioSource Bgm;
    public AudioClip ClickClip;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void PlayClickSound()
    {
        ClickSound.PlayOneShot(ClickClip);
    }
}

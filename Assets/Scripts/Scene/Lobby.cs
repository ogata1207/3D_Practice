using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour {
    private SoundManager soundManager;
	// Use this for initialization
	void Start () {
        FindObjectOfType<Fade>().FadeIn();

        soundManager = FindObjectOfType<SoundManager>();
        soundManager.BGM_FadeIn(soundManager.lobbyBGM);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

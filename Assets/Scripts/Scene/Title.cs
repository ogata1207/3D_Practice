using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour {
    private SceneTransition sceneManager;
    private SoundManager soundManager;

    public bool isTouch; //複数回タッチを回避
	// Use this for initialization
	void Start () {
        sceneManager = FindObjectOfType<SceneTransition>();
        soundManager = FindObjectOfType<SoundManager>();

        soundManager.BGM_FadeIn(soundManager.titleBGM);
    }

    // Update is called once per frame
    void Update()
    {
        var touchState = TouchParam.GetInstance.touchState;
        if (!isTouch && touchState == TouchState.Tap)
        {
            isTouch = true;
            soundManager.BGM_FadeOut();
            sceneManager.ChangeScene("Lobby");
        }
	}
}

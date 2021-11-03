using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum SceneEnum
{
    Title = 1,
    Lobby,
}

public class SceneTransition : MonoBehaviour {
    private Fade fade;
    public SceneEnum nextScene;

    private bool isRunning = false;
	// Use this for initialization
	void Start () {
        fade = FindObjectOfType<Fade>();
        fade.FadeIn();
	}
	
	// Update is called once per frame
	void Update () {

    }

    public  void ChangeScene(string sceneName = null)
    {
        //
        StartCoroutine(ChangeCoroutine(sceneName));

    }

    IEnumerator ChangeCoroutine(string sceneName = null)
    {
        if (sceneName == null) sceneName = nextScene.ToString();
        isRunning = true;
        fade.FadeOut();
        yield return null;
        while(!fade.isFinish)
        {
            yield return null;
        }
        StartCoroutine(SceneLoader.LoadNextScene(sceneName));
        isRunning = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : MonoBehaviour {
    public string nextScene;
    public SceneTransition sceneManager;
    private SoundManager soundManager;

    [SerializeField]
    private Canvas canvas;
    void Start()
    {
        sceneManager = FindObjectOfType<SceneTransition>();
        soundManager = FindObjectOfType<SoundManager>();

    }

    void Update()
    {
        
    }
    public void ClickToSceneChange()
    {
        canvas.sortingOrder = 0;
        transform.parent.gameObject.SetActive(false);
        sceneManager.ChangeScene(nextScene);
        soundManager.BGM_FadeOut();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest001 : MonoBehaviour {
    private SoundManager soundManager;
    private ParticleManager particleManager;
    private TimeManager timeManager;
    private GameObject[] enemys;

    public SceneTransition sceneManager;
    private bool isChangeScene ;
    // Use this for initialization
    IEnumerator Start () {
        soundManager = FindObjectOfType<SoundManager>();
        particleManager = FindObjectOfType<ParticleManager>();
        sceneManager = FindObjectOfType<SceneTransition>();

        soundManager.BGM_FadeIn(soundManager.questBGM);
        FindObjectOfType<Fade>().FadeIn();

        yield return new WaitForSeconds(0.5f);

        enemys = GameObject.FindGameObjectsWithTag("Enemy");

        //timeManager.SetTimeScaleWhileTime(0.0f, 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(!isChangeScene)
        {
            enemys = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemys.Length <= 0)
            {
                isChangeScene = true;
                StartCoroutine(MoveScene("Lobby"));
            }
        }
	}

    IEnumerator MoveScene(string sceneName)
    {
        Transform cameraTransform = Camera.main.transform;
        particleManager.Play("StageClear", cameraTransform.position + cameraTransform.forward * 18.0f + new Vector3(0, 5, 0), cameraTransform.rotation);

        //遷移開始までの時間(sec)
        yield return new UnscaledWaitForTime(5);

        sceneManager.ChangeScene(sceneName);
        soundManager.BGM_FadeOut();


    }
}

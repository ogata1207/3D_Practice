using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    private float startTime;
    private float currentTime;
    private float stopTime;

    public float TimeScale
    {
        get { return Time.timeScale; }
        set
        {
            Time.timeScale = (value >0.0f) ? value : 0.0f;
        }
    }

    /// <summary>
    /// 一定時間タイムスケールを変更する
    /// </summary>
    /// <param name="scale">セットするスケール</param>
    /// <param name="time">使用する時間</param>
    public void SetTimeScaleWhileTime(float scale,float time)
    {
        startTime = Time.unscaledTime;

        TimeScale = scale;
        stopTime = time;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        currentTime = Time.unscaledTime;
        if(stopTime > 0.0f)
        {
            //経過時間
            var elapsedTime = currentTime - startTime;

            if(elapsedTime >= stopTime)
            {
                TimeScale = 1.0f;
                stopTime = 0.0f;
                startTime = 0.0f;
                Debug.Log("[終了] : TimeScale -> " + TimeScale);
            }
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            SetTimeScaleWhileTime(0.0f, 1.0f);
            Debug.Log("[開始] : TimeScale -> " + TimeScale);
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            TimeScale = 1 - TimeScale;
        }

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PuniconTest))]
public class punikon : MonoBehaviour {
    private TouchParam touchParam;
    private PuniconTest puniconMesh;

    // Use this for initialization
    void Awake () {
        puniconMesh = GetComponent<PuniconTest>();

        touchParam = TouchParam.GetInstance;
        touchParam.isTouch = false;
        touchParam.startTime = 0;

        Input.multiTouchEnabled = false;
	}

    private void Update()
    {

        {
            //touchCount:タッチした指の数
            if (Input.touchCount > 0)
            {
                //タッチ状態の取得
                var touch = Input.GetTouch(0);
                //画面へのタッチ状態
                touchParam.touchPhase = touch.phase;

                switch (touchParam.touchPhase)
                {
                    case TouchPhase.Began:

                        touchParam.startTime = Time.time;
                        touchParam.startPosition = touch.position;
                        touchParam.isTouch = true;
                        puniconMesh.Begin();
                        break;
                    case TouchPhase.Moved:

                        touchParam.touchTime = Time.time - touchParam.startTime;
                        touchParam.currentTouchPosition = touch.position;

                        touchParam.touchDirection = (touchParam.currentTouchPosition - touchParam.startPosition).normalized;
                        touchParam.touchLength = Vector2.Distance(touchParam.startPosition, touchParam.currentTouchPosition);

                        if (touchParam.touchLength < touchParam.flickLength)
                        {

                        }
                        if (touchParam.touchTime >= touchParam.flickTime)
                        {
                            touchParam.touchState = TouchState.Move;
                        }
                        puniconMesh.TrackingPunipuni();
                        break;
                    case TouchPhase.Stationary:
                        //TODO :白猫のスキルを使うときみたいなアレを作る
                        puniconMesh.TrackingPunipuni();
                        //chargeTime = Time.time - startTime;

                        break;
                    case TouchPhase.Ended:
                        touchParam.currentTouchPosition = touch.position;

                        touchParam.touchDirection = (touchParam.currentTouchPosition - touchParam.startPosition).normalized;
                        touchParam.touchLength = Vector2.Distance(touchParam.startPosition, touchParam.currentTouchPosition);

                        //タップかフリックかの判定
                        if (touchParam.touchTime < touchParam.flickTime)
                        {
                            touchParam.touchState = (touchParam.touchLength < touchParam.flickLength) ? TouchState.Tap : TouchState.Flick;
                        }
                        touchParam.isTouch = false;
                        puniconMesh.End();
                        break;
                }
            }
            else touchParam.Reset();
            //yield return null;

        }
    }
 
}

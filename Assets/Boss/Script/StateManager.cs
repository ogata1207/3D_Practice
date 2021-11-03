using System;
using UnityEngine;
public class StateManager
{
    Action<bool> nextState;
    Action<bool> currentState;
    
    public int currentPhase;        //攻撃パターンを管理するときに使う
    public float startTime;         //ステートに移った時点の時間
    public float elapsedTime;       //現在のステートの経過時間

    public void RequestState(Action<bool> next)
    {
        nextState = next;
    }

    public void Update()
    {
        if( nextState != null )
        {
            //現在のステートを破棄する
            currentState = null;

            //リクエストされたステートに移行
            currentState = nextState;
            currentState(true);
            currentPhase = 0;
            startTime = Time.time;

            //リクエストを破棄する
            nextState = null;
        }
        else
        {
            //更新

            elapsedTime = Time.time - startTime;
            currentState(false);
        }
    }
}

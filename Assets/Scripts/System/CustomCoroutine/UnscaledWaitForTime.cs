using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnscaledWaitForTime : CustomYieldInstruction {

    private float waitTime;
    private float startTime;
    private float currentTime;

    public override bool keepWaiting
    {
        get
        {
            currentTime = Time.unscaledTime;
            if( currentTime - startTime > waitTime )
            {
                return false;
            }
            return true;
        }
    }

    public UnscaledWaitForTime(float time)
    {

        waitTime = time;
        startTime = Time.unscaledTime;
    }
}

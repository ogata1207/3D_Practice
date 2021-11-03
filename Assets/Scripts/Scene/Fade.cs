using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {
    public float _FadeSpeed = 0.1f;
    public bool isPlaying = false;
    public bool isFinish = false;

    public Image panel;
    
    // Use this for initialization
	void Start () {

	}

    public void FadeOut(float fadeSpeed = 0)
    {
        if (fadeSpeed == 0) fadeSpeed = _FadeSpeed;
        if (!isPlaying) StartCoroutine(ExecuteFade(0.0f, 255.0f, _FadeSpeed));
    }

    public void FadeIn(float fadeSpeed = 0)
    {
        if (fadeSpeed == 0) fadeSpeed = _FadeSpeed;
        if (!isPlaying) StartCoroutine(ExecuteFade(255.0f, 0.0f, _FadeSpeed))
  ;    }

    IEnumerator ExecuteFade(float start, float end, float speed)
    {
        isPlaying = true;
        isFinish = false;
        var value = 0.0f;
        while(value < 1.1f)
        {
            var alpha = Mathf.Lerp(start, end, value);
            var color = panel.color;


            panel.color = new Color(color.r, color.g, color.b, alpha / 255.0f);

            value += speed;
            yield return new UnscaledWaitForTime(0.0f);
        }

        isPlaying = false;
        isFinish = true;
    }

}

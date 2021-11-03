using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {
    /****************************************/


    /****************************************/
    // Player

    public AudioSource bgmPlayer;
    public AudioSource sePlayer;

    /****************************************/
    // BGM

    public AudioClip titleBGM;
    public AudioClip lobbyBGM;
    public AudioClip questBGM;

    /****************************************/
    // SE

    public Dictionary<string, AudioClip> soundEffectList;
    public List<AudioClip> seList;



    /****************************************/

    [Range(0.01f, 1.0f)]
    public float _FadeInSpeed = 0.01f;

    [Range(0.01f, 1.0f)]
    public float _FadeOutSpeed = 0.01f;

    private Coroutine fadeCoroutine;
    /****************************************/

	// Use this for initialization
	void Start () {
        soundEffectList = new Dictionary<string, AudioClip>();
        foreach(var list in seList)
        {
            string key = list.name;
            soundEffectList.Add(key, list);
        }
	}

    public void BGM_FadeIn(AudioClip nextBGM)
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeIn(nextBGM));
    }

    public void BGM_FadeOut()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeOut());
    }


    IEnumerator FadeOut()
    {
        var value = bgmPlayer.volume;
        while(value >= 0)
        {
            bgmPlayer.volume = value;
            value -= _FadeOutSpeed;
            yield return null;
        }
        bgmPlayer.Stop();
    }

    IEnumerator FadeIn(AudioClip nextBGM)
    {
        var value = 0.0f;
        bgmPlayer.clip = nextBGM;
        bgmPlayer.volume = value;
        bgmPlayer.Play();

        while (value <= 1.0f)
        {
            bgmPlayer.volume = value;
            value += _FadeInSpeed;
            yield return null;
        }
    }

    //TODO:しっかりCrossFadeを勉強してから作り直す
    IEnumerator CrossFade(AudioClip nextBGM)
    {
        yield return StartCoroutine(FadeOut());
        yield return StartCoroutine(FadeIn(nextBGM));
    }

    public void PlayOneShotSoundEffect(string name_se)
    {
        sePlayer.PlayOneShot(soundEffectList[name_se]);
    }
}

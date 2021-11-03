using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCollider : MonoBehaviour {

    private bool isPlay;
	public GameObject _EffectPrefab;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Enemy")
        {
            if (!isPlay) StartCoroutine(GenerateEffect(other.gameObject));
        }
    }

    IEnumerator GenerateEffect(GameObject enemy)
    {
        isPlay = true;
        var effect = Instantiate(_EffectPrefab, transform);
        var startTime = Time.time;

        while(Time.time - startTime < 5.0f)
        {
            effect.transform.position += ( enemy.transform.position - transform.position ).normalized*0.01f;
            yield return null;

        }
        Destroy(effect);
        isPlay = false;
    }
}

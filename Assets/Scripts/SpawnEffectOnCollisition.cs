//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SpawnEffectOnCollisition : MonoBehaviour {

//    public GameObject _Prefab;
//    public CharacterStatus myStatus;
//    Coroutine isPlaying;

//    private void OnTriggerStay(Collider other)
//    {
//        if (other.gameObject.tag == "Enemy")
//        {

//            if (isPlaying == null)
//            {
//                var enemyStatus = other.gameObject.GetComponent<CharacterStatus>();
//                enemyStatus.Damage(myStatus.currentAttack);
//                isPlaying = StartCoroutine(PlayEffect(other.gameObject));
//            }
//        }
//    }

//    IEnumerator PlayEffect(GameObject obj)
//    {
//        var effect = Instantiate(_Prefab, obj.transform);

//        var startTime = Time.time;
//        while (Time.time - startTime < 0.6f) { yield return null; }
//        Destroy(effect.gameObject);
//        isPlaying = null;
//    }
//}

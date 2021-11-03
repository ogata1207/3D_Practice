using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

    public List<GameObject> particleList = new List<GameObject>();
    private List<ObjectPool> objectPool = new List<ObjectPool>();

    
    //HACK :
    private readonly int Slash = 0;
    private readonly int HitEffect = 1;
    private readonly int Explosion = 2;

	// Use this for initialization
	void Start () {

        //各エフェクトをプールする
        foreach (var particle in particleList)
        {
            objectPool.Add(new ObjectPool(particle, 10) );
        }

        //プールしたエフェクトを１つのオブジェクトの子に入れる
        GameObject EffectPool = new GameObject();
        //EffectPool.name = "EffectPool";
        PutInParent(EffectPool.transform);
	}
	
	// Update is called once per frame
	void Update () {

        //Activeがオフのエフェクトを再使用可能にする
        foreach(var list in objectPool)
        {
            list.ConfirmWhetherItIsInUse();
        }

	}

    public void Play(int number)
    {
        var pos = new Vector3(Random.Range(0, 10), 0, Random.Range(0, 10));
        var particle = objectPool[number].GetInstance(pos, transform.rotation);
        if (particle != null) particle.SetActive(true);
    }

    public void Play(string effectName, Vector3 pos, Quaternion rotation)
    {
        ObjectPool usePool;
        foreach(var effect in objectPool)
        {
            if(effect.effectName == effectName)
            {
                usePool = effect;
                var particle = usePool.GetInstance(pos, rotation);
                if (particle != null) particle.SetActive(true);
                break;
            }
        }      
    }


    public void PutInParent(Transform parent)
    {
        foreach(var objList in objectPool)
        {
            foreach(var pool in objList.pool)
            {
                pool.transform.parent = parent;
            }
        }
    }
}

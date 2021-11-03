using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public List<GameObject> pool = new List<GameObject>();
    public string effectName;
    public int maxCount;
    public bool[] isUsed;

    private List<ParticleSystem> particleSystem = new List<ParticleSystem>();

    public ObjectPool(GameObject originalObject, int num)
    {
        maxCount = num;
        isUsed = new bool[num];
        effectName = originalObject.name;

        for (int i = 0; i < num; i++)
        {
            isUsed[i] = false;
            var obj = GameObject.Instantiate(originalObject);
            obj.name = originalObject.name + i.ToString();
            obj.SetActive(false);
            particleSystem.Add(obj.GetComponent<ParticleSystem>());
            pool.Add(obj);
        }
    }

    public GameObject GetInstance(Vector3 position, Quaternion rotation)
    {
        for(int i = 0; i < maxCount; i++)
        {
            if(!isUsed[i])
            {
                isUsed[i] = true;
                pool[i].transform.position = position;
                pool[i].transform.rotation = rotation;
                return pool[i];
            }
        }

        return null;
    }
    
    //使用中かどうかを確認する  
    //(isUsed[i]がtrueでオブジェクト[i]がfalseなら使用済みなのでisUsed[i]をfalseにして再度使用可能にする)
    public void ConfirmWhetherItIsInUse()
    {
        for(int i=0; i<maxCount;i++)
        {
            if(isUsed[i])
            {
                isUsed[i] = (particleSystem[i].isPlaying) ? true : false;
                pool[i].SetActive(isUsed[i]);
            }
        }
    }
}

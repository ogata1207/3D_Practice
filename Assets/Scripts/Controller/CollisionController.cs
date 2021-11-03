using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController{

    private Dictionary<int, BoxCollider> CollisionList;
    public CollisionController() { CollisionList = new Dictionary<int, BoxCollider>(); }
    public int RegisterCollosion(BoxCollider col)
    {
        int id = CollisionList.Count + 1;
        CollisionList[id] = col;
        return id;
    }

    public void SetCollisionEnabled(int id, bool isEnabled)
    {
        CollisionList[id].enabled = isEnabled;
    }
}

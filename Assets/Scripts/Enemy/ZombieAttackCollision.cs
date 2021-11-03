using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackCollision : MonoBehaviour
{

    [System.NonSerialized]
    public CharacterStatus myStatus;
    public bool isAttack = false;
    private BoxCollider box;
    // Use this for initialization
    void Start()
    {
        box = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerStay(Collider other)
    {
        if (isAttack)
        {
            if (other.tag == "Player")
            {
                var playerStatus = other.GetComponent<CharacterStatus>();
                if (!playerStatus.isDelay)
                {
                    Camera.main.gameObject.GetComponent<CameraManager>().PlayShake(0.5f);
                    playerStatus.Damage(myStatus.currentAttack);
                    playerStatus.Delay();
                }
            }
        }
    }

    public void DisableCollision()
    {
        box.enabled = false;
    }

    public void EnableCollision()
    {
        box.enabled = true;
    }
}

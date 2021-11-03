using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour {

    static SoundManager soundManager;
    static private CharacterStatus playerStatus;
    private CharacterStatus myStatus;
    private ParticleManager particleManager;
    

    void Start()
    {
        if (soundManager == null) soundManager = FindObjectOfType<SoundManager>();
        if(playerStatus == null)playerStatus = GameObject.FindWithTag("Player").GetComponent<CharacterStatus>();
        myStatus = GetComponent<CharacterStatus>();
        particleManager = FindObjectOfType<ParticleManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerWeapon")
        {
            if (myStatus.isDelay) return;
            else
            {
                soundManager.PlayOneShotSoundEffect("PlayerHitAttack");
                particleManager.Play("HitEffect", transform.position, transform.rotation);
                myStatus.Damage(playerStatus.currentAttack);
                myStatus.Delay();
            }
            
            
        }
    }


}

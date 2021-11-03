using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour {

    //プレイヤーのアニメーションコントローラー
    static Player player;
    static CharacterStatus playerStatus;
    static SoundManager soundManager;
    private void Start()
    {
        if (soundManager == null) soundManager = FindObjectOfType<SoundManager>();
        var p = GameObject.FindWithTag("Player");
        if(player == null)player = p.GetComponent<Player>();
        if(playerStatus==null)playerStatus = player.GetComponent<CharacterStatus>();
    }


    void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            if(!playerStatus.isDelay)
            {
                soundManager.PlayOneShotSoundEffect("PlayerHitDamage");
                playerStatus.Damage(10);
                player.isHitDamage = true;
                playerStatus.Delay();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AttackState : StateMachineBehaviour {
    private bool Initialize;
    private int currentState;
    private PlayerAnimationController anim;
    private Player player;
    private CharacterStatus playerStatus;
    private SoundManager soundManager;
    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash, AnimatorControllerPlayable controller)
    {
        if (soundManager == null) soundManager = FindObjectOfType<SoundManager>();
        if (player == null) player = FindObjectOfType<Player>();

        currentState = 0;
        Initialize = false;
    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!Initialize)
        {
            currentState++;
            Initialize = true;
        }
        if (currentState == 2) soundManager.PlayOneShotSoundEffect("PlayerAttackVoice1");
        else soundManager.PlayOneShotSoundEffect("PlayerAttackVoice2");

        anim = FindObjectOfType<PlayerAnimationController>();
        anim.isChain = false;
        anim.isAttack = false;

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim.isAttack = (animator.GetInteger("State") == 5) ? false : true;
       
        if (TouchParam.GetInstance.touchState == TouchState.Tap)
        {
            anim.isChain = true;
        }
        if (player.isHitDamage) anim.ChangeAnimation(PlayerAnimationNumber.Damaged);
        //else if (anim.isChain == true) anim.ChangeAnimation(PlayerAnimationNumber.Attack);
        //else anim.ChangeAnimation(PlayerAnimationNumber.Idle);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetBool("AtkCombo")) anim.ChangeAnimation(PlayerAnimationNumber.Idle);
        Initialize = false;
        anim.isAttack = false;
    }
    

}

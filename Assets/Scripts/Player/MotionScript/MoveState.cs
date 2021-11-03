using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : StateMachineBehaviour {
    private Player player;
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null) player = FindObjectOfType<Player>();
        //最初と最後のフレームを除く、各フレーム単位で実行
        player.RunAction();
        if (TouchParam.GetInstance.touchState != TouchState.Move) FindObjectOfType<PlayerAnimationController>().ChangeAnimation(PlayerAnimationNumber.Idle);

    }
}

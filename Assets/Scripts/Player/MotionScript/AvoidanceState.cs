using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidanceState : StateMachineBehaviour
{
    public Player player;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //新しいステートに移り変わった時に実行
        if (player == null) player = FindObjectOfType<Player>();

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player.AvoidanceAction();
    }


}

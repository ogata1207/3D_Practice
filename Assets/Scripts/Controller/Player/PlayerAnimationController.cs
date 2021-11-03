using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimationNumber : int
{
    Idle      = 0,     //待機
    Run       = 1,     //移動
    Attack    = 2,     //攻撃
    Avoidance = 3,     //回避
    Dead      = 4,     //死亡
    Damaged   = 5,     //アクション(テスト)

    NONE      =-1

}
public class PlayerAnimationController : MonoBehaviour {

    #region 各アニメーションのハッシュ
    public readonly int IdleState       = Animator.StringToHash("Base Layer.Idle");
    public readonly int AdvoidanceState = Animator.StringToHash("Base Layer.Avoidance");
    public readonly int RunState        = Animator.StringToHash("Base Layer.Run");
    public readonly int AttackState   = Animator.StringToHash("Base Layer.Attack");
    public readonly int DeadState       = Animator.StringToHash("Base Layer.Dead");
    public readonly int DamagedState        = Animator.StringToHash("Base Layer.Damaged");
    #endregion

    private Animator anim;
    [System.NonSerialized]
    public int currentState;
    [System.NonSerialized]
    public bool isAttack;

    private bool isChainAttack;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }
    //ベースレイヤーの現在のステイトを返す
    public AnimatorStateInfo CurrentBaseState
    {
        get { return anim.GetCurrentAnimatorStateInfo(0); }
    }

    //ベースレイヤーの現在のステイトのハッシュを返す
    public int CurrentStateHash
    {
        get { return CurrentBaseState.fullPathHash; }
    }

    //現在再生されているモーションの正規化された時間を返す
    public float AnimNormalizedTime
    {
        get{ return anim.GetCurrentAnimatorStateInfo(0).normalizedTime; }
    }

    //モーションの再生速度
    public float PlaySpeed
    {
        get { return anim.speed; }
        set { anim.speed = value; }
    }

    //モーションが遷移途中かどうか
    //遷移中ならtrue
    public bool IsTransration
    {
        get { return anim.IsInTransition(0); }
    }

    //モーション変更
    public void ChangeAnimation(int animationNumber)
    {
        currentState = animationNumber;
        anim.SetInteger("State", animationNumber);
    }

    public void ChangeAnimation(PlayerAnimationNumber animationNumber)
    {
        ChangeAnimation((int)animationNumber);
    }

    //通常攻撃の連続攻撃
    public bool isChain
    {
        get { return isChainAttack; }
        set
        {
            isChainAttack = value;
            anim.SetBool("AtkCombo", isChainAttack);
        }
    }

    public void StopAnimation()
    {
        Debug.Log("Time:" + CurrentBaseState.normalizedTime);
        //anim.speed = 0.0f;
    }

    public void PlayAnimation()
    {
        anim.speed = 1.0f;
    }


}

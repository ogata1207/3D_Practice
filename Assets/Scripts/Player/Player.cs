using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {

    /* ステータス */
    public CharacterStatus myStatus;
    private PlayerAnimationController animController;
    public bool isHitDamage;

    /* 衝突判定 */
    public GameObject _MyWeapon;
    private CollisionController colController;
    private int weaponColliderID;

    // Use this for initialization
    void Start ()
    {
        myStatus = GetComponent<CharacterStatus>();
        animController = GetComponent<PlayerAnimationController>();

        colController = new CollisionController();
        var weaponCol = _MyWeapon.GetComponent<BoxCollider>();
        weaponColliderID = colController.RegisterCollosion(weaponCol);     //武器のBoxColliderを登録      
        StartCoroutine(CollisionUpdate());
	}
	IEnumerator CollisionUpdate()
    {
        while (true) { SetWeaponCollision(animController.isAttack); yield return null; }
    }
	// Update is called once per frame
	void Update () {

        //SetWeaponCollision(animController.isAttack);


        //モーション遷移
        PlayerAnimationNumber state = PlayerAnimationNumber.NONE;
        
        //HPが0の場合
        if(myStatus.isDead)
        {
            state = PlayerAnimationNumber.Dead;
        }
        //敵の攻撃に当たった時
        else if (isHitDamage)
        {

            state = PlayerAnimationNumber.Damaged;
            HitDamage();
        }
        //プレイヤーのタッチ操作
        else
        {
            switch (TouchParam.GetInstance.touchState)
            {
                case TouchState.Move:
                    LookAtDirection(TouchParam.GetInstance.touchDirection);
                    state = PlayerAnimationNumber.Run;
                    break;
                case TouchState.Flick:
                    state = PlayerAnimationNumber.Avoidance;
                    //フリックの方向に回転(ここ怪しい)
                    LookAtDirection(TouchParam.GetInstance.touchDirection);
                    break;
                case TouchState.Tap:
                    state = PlayerAnimationNumber.Attack;
                    //SetWeaponCollision(true);
                    break;
                default:
                    if (Input.GetKeyDown(KeyCode.A)) state = PlayerAnimationNumber.Attack;
                    //SetWeaponCollision(false);
                    break;
            }
        }
        if (animController.CurrentStateHash == animController.DamagedState) state = (animController.AnimNormalizedTime > 0.8f) ? PlayerAnimationNumber.Idle : PlayerAnimationNumber.NONE;
        //アニメーションの変更
        if (state != PlayerAnimationNumber.NONE)animController.ChangeAnimation(state);

        
    }
    void FixedUpdate()
    {


    }

    #region ActionMethod

    //移動アクション
    public void RunAction()
    {
        //LookAtDirection(TouchParam.GetInstance.touchDirection);
        transform.localPosition += transform.forward * myStatus._Speed;

    }

    

    //回避アクション
    public void AvoidanceAction()
    {
        if (animController.AnimNormalizedTime >= 0.65f) animController.ChangeAnimation(PlayerAnimationNumber.Idle);
        transform.localPosition += transform.forward * myStatus._Speed;
    }
    

    //攻撃を受けた時
    void HitDamage()
    {
        isHitDamage = false;
        animController.ChangeAnimation(PlayerAnimationNumber.Idle);
    }

    #endregion

    #region Method
    public void LookAtDirection(Vector2 flickVec)
    {
        //回転させる方向
        Vector3 direction = new Vector3(flickVec.x, 0, flickVec.y);
        direction += transform.position;
        transform.LookAt(direction);
    }
    
    public void SetWeaponCollision(bool isEnable)
    {
        //攻撃時に武器の判定をつける
        colController.SetCollisionEnabled(weaponColliderID, isEnable);
    }
    #endregion
}

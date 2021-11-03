using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyState : MonoBehaviour {

    [Header("待機時間(次の攻撃までの時間)")]
    public float _AttackInterval; 
    [Header("回転ビーム(全方向)の時間")]
    public float _CrossBeamDuration;   
    [Header("回転ビームの回転速度")]
    public float _CrossBeamRotateSpeed;
    [Header("2方向ビームの継続時間")]
    public float _TwoWayWayBeamDuration;

    private CharacterStatus myStatus;

    /* ビーム　*/
    [SerializeField, Range(0, 15)]
    public int beamSize = 15;        //RateOverTime
    private int beamRate;
    private float currentBeamStrengthValue;
    private float keepTime = 5.0f;
    private float startTime;
    public List<ParticleSystem> _BeamList;
    private Dictionary<int, ParticleSystem> beamList;
    private List<int> useBeamID;

    /*　ステート管理 */
    private StateManager stateManager;
    private int nextState_Random;

    /*  衝突判定  */
    public BoxCollider[] beamCollision;
    private CollisionController colController;
    private int[] collisionID;



    // Use this for initialization
    void Start () {

        myStatus = GetComponent<CharacterStatus>();

        //ステイト管理
        stateManager = new StateManager();
        stateManager.RequestState(IdleState);   //初期のステイト

        #region beamListを作成
        beamList = new Dictionary<int, ParticleSystem>();
		for(int i=0;i<_BeamList.Count; i++)
        {
            beamList[i] = _BeamList[i];
            var emmision = beamList[i].emission;
            emmision.rateOverTime = 0;
        }
        #endregion

        //ビームの管理用
        useBeamID = new List<int>();

        //衝突判定
        colController = new CollisionController();
        int collisionNum = beamCollision.Length;
        collisionID = new int[collisionNum];
        for(int i=0;i<collisionNum;i++)
        {
            //衝突判定用にコライダーを登録し、IDを保存する
            collisionID[i] = colController.RegisterCollosion(beamCollision[i]);
        }
    }
	
    //**********************************************************************************************************************
	// Update is called once per frame
	void FixedUpdate () {
        stateManager.Update();

        if (myStatus.isDead)
        {
            FindObjectOfType<ParticleManager>().Play("Explosion", transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
    }

    //**********************************************************************************************************************
    // Method
    //**********************************************************************************************************************
    void SetEmissionRate(int id,int rate)
    {
        var emission = beamList[id].emission;
        emission.rateOverTime = rate;
    }

    void StartEmission(int id)
    {
        var value = (int)Mathf.Lerp(0, beamSize, currentBeamStrengthValue);
        SetEmissionRate(id, value);
    }
    void StopEmission(int id)
    {
        var value = (int)Mathf.Lerp(beamSize, 0, currentBeamStrengthValue);
        SetEmissionRate(id, value);
    }

    //**********************************************************************************************************************
    // State Method
    //**********************************************************************************************************************
    void IdleState(bool initialize)
    {
        if (initialize)
        {
            //使用していたビームを解放する
            useBeamID.Clear();

            //次の攻撃までの時間をセット
            keepTime = _AttackInterval;

            //TODO:攻撃のパターンを作成する
            //次の攻撃をランダムで決める
            nextState_Random = UnityEngine.Random.Range(0, 3);

            currentBeamStrengthValue = 0;
           
        }
        else
        {
            //transform.Rotate(new Vector3(0, 0.2f * stateManager.elapsedTime, 0));

            if (stateManager.elapsedTime > keepTime)
            {
                switch (nextState_Random)
                {
                    case 0:
                        //攻撃の継続時間をセット
                        keepTime = _TwoWayWayBeamDuration;

                        //使用するビームを登録
                        useBeamID.Add(0);
                        useBeamID.Add(2);

                        //ビームの衝突判定をオンにする
                        colController.SetCollisionEnabled(collisionID[0], true);
                        colController.SetCollisionEnabled(collisionID[2], true);

                        //攻撃ステイトに遷移
                        stateManager.RequestState(StartState);

                        break;

                    case 1:
                        //攻撃の継続時間をセット
                        keepTime = _TwoWayWayBeamDuration;

                        //使用するビームを登録
                        useBeamID.Add(1);
                        useBeamID.Add(3);

                        //ビームの衝突判定をオンにする
                        colController.SetCollisionEnabled(collisionID[1], true);
                        colController.SetCollisionEnabled(collisionID[3], true);

                        //攻撃ステイトに遷移
                        stateManager.RequestState(StartState);

                        break;

                    case 2:
                        //攻撃の継続時間をセット
                        keepTime = _CrossBeamDuration;

                        //使用するビームを登録
                        useBeamID.Add(0);
                        useBeamID.Add(1);
                        useBeamID.Add(2);
                        useBeamID.Add(3);
                        useBeamID.Add(0);

                        //ビームの衝突判定をオンにする
                        colController.SetCollisionEnabled(collisionID[0], true);
                        colController.SetCollisionEnabled(collisionID[1], true);
                        colController.SetCollisionEnabled(collisionID[2], true);
                        colController.SetCollisionEnabled(collisionID[3], true);

                        //攻撃ステイトに遷移
                        stateManager.RequestState(CrossBeam);
                        break;

                    default:
                        stateManager.RequestState(IdleState);
                        break;

                }
            }
        }
    }
    void CrossBeam(bool initialize)
    {
        //攻撃の初期化
        if (initialize)
        {
            currentBeamStrengthValue = 0.0f;
        }
        else
        {
            //指定された時間が経過したら待機ステイトに遷移する
            if (stateManager.elapsedTime > keepTime)
            {
                stateManager.RequestState(StopState);
            }
            else
            {
                //回転させる
                transform.Rotate(new Vector3(0, 0.1f * stateManager.elapsedTime, 0));

                //徐々にビームの強さを強くしていく
                foreach (var id in useBeamID)
                {
                    StartEmission(id);
                }
                currentBeamStrengthValue += 0.1f;
            }
        }

    }

    void StartState(bool initialize)
    {
        //攻撃の初期化
        if (initialize)
        {
            currentBeamStrengthValue = 0;
        }
        else
        {
            //指定された時間が経過したら待機ステイトに遷移する
            if (stateManager.elapsedTime > keepTime)
            {
                stateManager.RequestState(StopState);
            }
            else
            {
                //徐々にビームの強さを強くしていく
                foreach (var id in useBeamID)
                {
                    StartEmission(id);
                }
                currentBeamStrengthValue += 0.1f;
            }
        }
    }

    void StopState(bool initialize)
    {
        //攻撃の初期化
        if (initialize)
        {
            currentBeamStrengthValue = 0;
        }
        else
        {
            if (currentBeamStrengthValue >1.1f)
            {
                SetAllBeamCollisionEnabled(false);
                stateManager.RequestState(IdleState);
            }
            else
            {
                foreach (var id in useBeamID)
                {
                    StopEmission(id);
                }
                currentBeamStrengthValue += 0.1f;
            }
        }
    }

    void SetAllBeamCollisionEnabled(bool isEnabled)
    {
        for(int i=0;i<beamCollision.Length;i++)
        {
            colController.SetCollisionEnabled(collisionID[i], isEnabled);
        }
    }


}

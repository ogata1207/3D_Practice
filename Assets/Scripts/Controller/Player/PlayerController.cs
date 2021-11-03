//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;
//using UnityEngine.Events;

//[RequireComponent(typeof(CharacterStatus))]
//public class PlayerController : MonoBehaviour
//{
//    public ParticleSystem ps;
//    public float _RotateSpeed = 1.0f;

//    public CharacterStatus myStatus;

//    private CameraManager cameraManager;
//    private PlayerAnimationController pAnim;

//    //武器の判定の有無を変更するため
//    public GameObject _WeaponObject;
//    private BoxCollider weaponCollider;

//    //攻撃していると時　true
//    public bool isAttack = false;

//    public float actionTime = 3.0f;
//    private float actionPlayTime = 0.0f;
//    private float actionStartTime = 0;

//    //TODO:パーティクルを整理する用のクラスを作る
//    public GameObject _FireBall, _MagicCircle, _FireWind;
//    private GameObject[] playEffect = new GameObject[3];
//    public GameObject _AtkCollider;
//    private Rigidbody rigidBody;

//    private event UnityAction action;


//    // Use this for initialization
//    void Start()
//    {
//        myStatus = GetComponent<CharacterStatus>();
//        cameraManager = FindObjectOfType<CameraManager>();
//        pAnim = GetComponent<PlayerAnimationController>();

//        weaponCollider = _WeaponObject.GetComponent<BoxCollider>();
//        weaponCollider.enabled = false;

//        //武器に自分のステータスを渡す
//        _WeaponObject.GetComponent<SpawnEffectOnCollisition>().myStatus = GetComponent<CharacterStatus>();

//        rigidBody = GetComponent<Rigidbody>();

//    }

//    void Update()
//    {
//        //アニメーション遷移
//        StateTransition();

//        if (Input.GetKeyDown(KeyCode.A)) pAnim.ChangeAnimation(PlayerAnimationNumber.Attack);
//        if (pAnim.currentState == (int)PlayerAnimationNumber.Idle) { weaponCollider.enabled = false; isAttack = false; }

//        if (Input.GetKeyDown(KeyCode.S))
//        {
//            actionStartTime = Time.time;
//            pAnim.ChangeAnimation(PlayerAnimationNumber.Damaged);
//        }


//    }

//    void FixedUpdate()
//    {

//        //アニメーションステイトが回避ステイトにいる時のみ実行
//        if (pAnim.CurrentStateHash == pAnim.AdvoidanceState)
//        {
//            Move(0.1f);
//        }

//        if (pAnim.CurrentStateHash == pAnim.AttackState) Attack01();

//        //アニメーションステイトが移動ステイトにいる時(遷移中含む)
//        if (pAnim.currentState == (int)PlayerAnimationNumber.Run)
//        {
//            ps.Play();
//            var direction = TouchParam.GetInstance.touchDirection;
//            //画面にタッチ(スワイプ)している方向へプレイヤーを向ける
//            LookAtDirection(direction);
//            Move(0.1f);
//        }
//        else ps.Stop();

//        //if (pAnim.CurrentStateHash == pAnim.Action01)
//        //{
//        //    ActionTest();
//        //}
//    }

//    //アニメーション遷移
//    //タッチの入力がない時はIdle状態にする
//    void StateTransition()
//    {
//        PlayerAnimationNumber animNumber = PlayerAnimationNumber.Idle;
//        //HPが0になった時死亡ステイトに遷移する
//        if (myStatus.isDead)
//        {
//            pAnim.ChangeAnimation(PlayerAnimationNumber.Dead);
//            if (TouchParam.GetInstance.touchState == TouchState.Tap)
//            {
//                SceneManager.LoadScene("Development");
//            }
//            return;
//        }

//        //遷移先を決める
//        switch (TouchParam.GetInstance.touchState)
//        {
//            case TouchState.Move:         //移動
//                animNumber = PlayerAnimationNumber.Run;
//                break;
//            case TouchState.Flick:        //回避
//                var direction = TouchParam.GetInstance.touchDirection;
//                LookAtDirection(direction);
//                animNumber = PlayerAnimationNumber.Avoidance;
//                break;
//            case TouchState.Tap:          //攻撃
//                animNumber = PlayerAnimationNumber.Attack;
//                break;
//            default:                      //待機

//                break;

//        }
//        pAnim.ChangeAnimation(animNumber);
//    }

//    void Move(float speed)
//    {
//        transform.localPosition += transform.forward * speed;
//    }

//    void Attack01()
//    {
//        weaponCollider.enabled = true;
//        isAttack = true;
//    }

//    //指定の方向にプレイヤーを回転させる
//    void LookAtDirection(Vector2 vec)
//    {
//        Vector3 direction = new Vector3(vec.x, 0, vec.y);
//        direction += transform.position;
//        transform.LookAt(direction);
//    }

//    void Rotate(Vector2 vec)
//    {
//        transform.Rotate(0, vec.x * _RotateSpeed, 0);
//    }

//    void ActionTest()
//    {
//        var playTime = pAnim.AnimNormalizedTime;
//        Debug.Log("TIME :" + playTime);
//        if (playTime >= 0.8f)
//        {
//            if (Time.time - actionStartTime <= actionTime)
//            {
//                _AtkCollider.GetComponent<SphereCollider>().enabled = true;
//                rigidBody.useGravity = false;
//                pAnim.PlaySpeed = 0.0f;
//                if (playEffect[0] == null) playEffect[0] = Instantiate(_FireWind, transform);
//                if (playEffect[1] == null) playEffect[1] = Instantiate(_MagicCircle, new Vector3(transform.position.x, 0, transform.position.z), transform.rotation);

//            }
//            else
//            {
//                _AtkCollider.GetComponent<SphereCollider>().enabled = false;
//                rigidBody.useGravity = true;
//                Destroy(playEffect[0]);
//                Destroy(playEffect[1]);
//                playEffect[0] = null;
//                playEffect[1] = null;
//                pAnim.PlaySpeed = 1.0f;
//            }
//        }

//    }
//}



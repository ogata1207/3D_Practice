using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStatus))]
public class ZombieController : MonoBehaviour {

    public GameObject _ParticlePrefab;

    public static GameObject player = null;
    public GameObject _AttackCollisionObject;
    [System.NonSerialized] public ZombieAttackCollision atkCollision;
    public CharacterStatus myStatus;
    public float searchLength = 1.0f;   //この距離までプレイヤーを追跡させる
    public float eyeAngle = 90;         //視界(攻撃範囲)
    public float moveSpeed = 0.1f;      //移動速度
    public float rotateSpeed = 10.0f;   //回転速度
    public bool isAlive = false;        //生存している時はtrue

    private Animator anim;
    private Coroutine deadAction;

    // Use this for initialization
	void Start () {
        if (player == null) { player = GameObject.FindWithTag("Player"); }

        isAlive = true;

        anim = GetComponent<Animator>();
        myStatus = GetComponent<CharacterStatus>();

        //hack:後で綺麗にまとめます
        atkCollision = _AttackCollisionObject.GetComponent<ZombieAttackCollision>();
        atkCollision.myStatus = GetComponent<CharacterStatus>();

	}

    void FixedUpdate()
    {

        //TODO:敵専用のアニメーションを管理するManagerを作成する

        //キャラクターのHPが0になった場合死亡アニメーションに遷移
        if (myStatus.isDead)
        {

            if (deadAction == null) StartCoroutine(DestroyObjectWhenDead());
            return;
        }

        var dist = Vector3.Distance(transform.position, player.transform.position);

        //自分の前方向とプレイヤーがいる方向の間の角度
        var angle = Vector3.Angle(transform.forward, (player.transform.position - transform.position));

        //プレイヤーからserchLength以上離れている時にプレイヤーを追跡
        if (dist > searchLength)
        {
            anim.SetInteger("State", 1);
            ApproachToPlayer();


        }
        //プレイヤーが視界に入っている時
        else if (angle < eyeAngle)
        {
            anim.SetInteger("State", 2);
            atkCollision.isAttack = true;
        }
        //自分が暇な時(どれにも当てはまらない)
        else
        {
            anim.SetInteger("State", 0);
            atkCollision.isAttack = false;
        }
    }

    //プレイヤーの方向を向き歩いていく
    void ApproachToPlayer()
    {
   
        //斜めに回転するのを防ぐためにプレイヤーの高さに合わせる
        var rotatePosition = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);

        //ゆっくり回転させる
        var rotate = Quaternion.LookRotation(player.transform.position - rotatePosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime*rotateSpeed);

        //前進
        transform.position += transform.forward * moveSpeed;
    }

    IEnumerator DestroyObjectWhenDead()
    {
        anim.SetInteger("State", 3);

        //ヒット判定用のコライダーを切る
        Destroy(GetComponent<BoxCollider>());
        Destroy(GetComponent<CapsuleCollider>());
        Destroy(GetComponent<Rigidbody>());

        //ゲージを非表示にする
        myStatus.HideHPGauge();

        var effect = Instantiate(_ParticlePrefab, transform.position, transform.rotation);
        effect.transform.parent = transform;
        yield return new WaitForSeconds(1.0f);

        Destroy(effect);
        Destroy(gameObject);

    }
}

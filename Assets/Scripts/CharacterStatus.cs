using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterStatus : MonoBehaviour {

    public float _Speed = 0.1f;

    public int _MaxHealth;
    public int _AttackPower;

    public int currentHealth = 0;
    public int currentAttack = 0;

    public float startDelayTime;
    public GameObject _HpGauge;
    private Slider hpSlider;

    //ヒット後に数秒間ダメージを通さなくする(無敵)
    public float HitDelay = 1.0f;

    //Delay中はtrue
    public bool isDelay = false;

    //HPが0以下ならtrue
    public bool isDead
    {
        get { return (currentHealth <= 0) ? true : false; }
    }

    // Use this for initialization
    void Start()
    {
        hpSlider = _HpGauge.GetComponent<Slider>();

        //初期化
        Regeneration();
    }

    //ステータスの初期化
    public void Regeneration()
    {
        currentHealth = _MaxHealth;
        currentAttack = _AttackPower;
    }

    public void Damage(int power)
    {
        if(!isDelay)currentHealth -= power;
    }

    

   

    void Update()
    {
        //ゲージ更新(現在のHPの割合)
        hpSlider.value = ((float)currentHealth / (float)_MaxHealth);

        if(isDelay)
        {
            if(Time.time - startDelayTime > HitDelay)
            {
                startDelayTime = 0;
                isDelay = false;
            }
        }

    }

    public void HideHPGauge()
    {
        _HpGauge.SetActive(false);
    }

    public void Delay()
    {
        isDelay = true;
        startDelayTime = Time.time;
    }

}

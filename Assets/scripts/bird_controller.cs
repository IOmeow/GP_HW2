using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird_controller : monster
{
    private bool attacking = false;
    private int cd = 1;
    private float prev_attacking_time;

    private bool delaying = false;
    private float delay_till;

    private Vector3 target_pos;
    private Rigidbody rb;
    Animator animator;
    public GameObject hiteffect;
    private float hitcd;

    HealthManagerScript health;
    SoundManager sound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        hitcd = 0;
        // damage, hp, speed, see_range
        Init(5, 3, 8, 5);

        health = GameObject.Find("HealthManager").GetComponent<HealthManagerScript>();
        sound = GameObject.Find("Sound").GetComponent<SoundManager>();
    }

    void Update() {
        // 檢查是否已經飛到定點
        if(attacking && Vector3.Distance(transform.position, target_pos) <= 0.1f){
            rb.velocity = Vector3.zero;
            animator.SetBool("attacking",false);
            attacking = false;
            immune = attacking;
            prev_attacking_time = Time.time;
        }
    }

    void FixedUpdate() {
        // 正在攻擊, 繼續飛行
        if(attacking){
        animator.SetBool("attacking",true);
        }
        // 等待鳥先轉向玩家
        else if(delaying){
            look_at_player();

            // 鳥已經轉向玩家, 且距離上次攻擊時間夠長
            if(Time.time >= delay_till && Time.time >= prev_attacking_time + cd){
                delaying = false;
                // 攻擊
                attack();
            }
        }

        // 玩家夠靠近
        else if(close_enough()){
            // 計算需多久才能面向玩家
            delay_till = Time.time + calculate_delay(look_at_player());
            delaying = true;
        }
        if(hitcd>=0)
        {
            hitcd-=Time.fixedDeltaTime;
        }
    }

    private void attack(){
        Vector3 player_pos = GameManager.Instance.player_pos;
        Vector3 dir = player_pos - transform.position;
        dir = Vector3.Normalize(new Vector3(dir.x, 0, dir.z));
        target_pos = new Vector3(player_pos.x, transform.position.y, player_pos.z) + dir * 2;

        rb.velocity = dir * speed;
        attacking = true;
        immune = attacking;
        sound.playBirdSE();
    }
    void OnTriggerStay(Collider collision)
    {
        if(collision.tag=="Player"&&hitcd<=0&&attacking==true)  
        {
            hitcd=1f;
            Instantiate(hiteffect,new Vector3(collision.transform.position.x,collision.transform.position.y+1f,collision.transform.position.z),Quaternion.identity);
            health.TakeDamage(damage);
        }
    }
}

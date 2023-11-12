using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class shooter_controller : monster
{
    [SerializeField] private GameObject bullet_pref;

    private bool shooting = false;
    private float cd = 2.3f;
    private float ani_start_to_shoot = 0.9f;
    private float prev_shooting_time;
    private bool waiting_for_ani = false;
    private bool do_shoot = false;

    private int bullet_speed = 10;
    public GameObject hand;
    public Animator animator;

    SoundManager sound;

    // Start is called before the first frame update
    void Start()
    {
        // damage, hp, speed, see_range, max_hit_count
        Init(1, 3, 0, 5, 2);
        sound = GameObject.Find("Sound").GetComponent<SoundManager>();
    }

    void FixedUpdate() {
        // 玩家夠靠近
        if(close_enough()){
            // 面向玩家
            var rot = look_at_player();

            // 已經開始射擊
            if(shooting){
                if(waiting_for_ani && Time.time - prev_shooting_time >= ani_start_to_shoot){
                    shoot();
                    waiting_for_ani = false;
                    do_shoot = true;
                }
                else if(Time.time - prev_shooting_time >= cd){
                    do_shoot = false;
                    waiting_for_ani = true;
                    prev_shooting_time = Time.time;
                }
            }

            // 還沒開始射擊, 且距離上一發時間夠長
            else if(!shooting && Time.time - prev_shooting_time >= cd){
                // 開始射擊
                animator.SetBool("attack",true);
                shooting = true;
                waiting_for_ani = true;

                // update last time we shoot
                prev_shooting_time = Time.time;
            }
        }
        
        // 正在射擊, 但玩家距離太遠
        else if(shooting){
            shooting = false;
            animator.SetBool("attack",false);

            if(!do_shoot) prev_shooting_time -= cd;
        }
    }

    // 射擊
    private void shoot(){
        // create bullet
        var bullet_rb = Instantiate(bullet_pref, hand.transform.position, Quaternion.identity)
            .GetComponent<Rigidbody>();

        // give the bullet initial velocity
        bullet_rb.velocity = transform.forward * bullet_speed;
        
        sound.playShooterSE();
    }
}
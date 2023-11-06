// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class shooter_controller : monster
{
    [SerializeField] private GameObject bullet_pref;

    private bool shooting = false;
    private int cd = 1;
    private float prev_shooting_time;

    private int bullet_speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        // damage, hp, speed, see_range
        Init(1, 3, 0, 5);
    }

    void FixedUpdate() {
        // 玩家夠靠近
        if(close_enough()){
            // 面向玩家
            var rot = look_at_player();

            // 還沒開始射擊, 且距離上一發時間夠長
            if(!shooting && Time.time - prev_shooting_time >= cd){
                // 開始射擊
                //InvokeRepeating("shoot", rot / rotate_ang * Time.fixedDeltaTime, cd);
                InvokeRepeating("shoot", calculate_delay(rot), cd);
                shooting = true;
            }
        }
        
        // 正在射擊, 但玩家距離太遠
        else if(shooting){
            CancelInvoke("shoot");
            shooting = false;
        }
    }

    // 射擊
    private void shoot(){
        // create bullet
        var bullet_rb = Instantiate(bullet_pref, transform.position + transform.forward * 0.7f, Quaternion.identity)
            .GetComponent<Rigidbody>();
        // give the bullet initial velocity
        bullet_rb.velocity = transform.forward * bullet_speed;

        // update last time we shoot
        prev_shooting_time = Time.time;
    }
}

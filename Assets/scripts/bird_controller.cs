// using System.Collections;
// using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // damage, hp, speed, see_range
        Init(1, 3, 8, 5);
    }

    void Update() {
        // 檢查是否已經飛到定點
        if(attacking && Vector3.Distance(transform.position, target_pos) <= 0.1f){
            rb.velocity = Vector3.zero;
            attacking = false;
            prev_attacking_time = Time.time;
        }
    }

    void FixedUpdate() {
        // 正在攻擊, 繼續飛行
        if(attacking){}

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
    }

    private void attack(){
        Vector3 player_pos = GameManager.Instance.player_pos;
        Vector3 dir = Vector3.Normalize(player_pos - transform.position);
        target_pos = player_pos + dir * 2;

        rb.velocity = dir * speed;
        attacking = true;
    }
}

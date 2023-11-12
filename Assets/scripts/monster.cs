using UnityEngine;

public abstract class monster : MonoBehaviour
{
    protected int damage;   // 傷害
    protected int hp;       // 血量
    protected int speed;    // 走路速度
    protected int see_range;    // 怪物可視範圍
    //protected float rotate_ang = 7.2f;  // 轉向player每次轉多少度
    protected float rotate_ratio = 0.2f;    // 轉向player每次轉多少%
    protected int ang_thresh = 5;   // 和玩家夾多少度就視為已經面向玩家
    protected bool immune = false;
    // 初始化 damage, hp, walking speed
    protected void Init(int d, int h, int s, int r){
        damage = d;
        hp = h;
        speed = s;
        see_range = r;
    }

    // 讓怪物面向玩家
    protected float look_at_player(){
        Vector3 dir = GameManager.Instance.player_pos - transform.position;

        var target_rot_y = Mathf.Rad2Deg * Mathf.Atan2(dir.x, dir.z);
        var rot = target_rot_y - transform.eulerAngles.y;

        // -180 <= rot <= 180
        rot = rot > 180 ? (rot - 360) : rot < -180 ? (rot + 360) : rot;

        // rotate
        // if(Mathf.Abs(rot) <= rotate_ang) transform.forward = dir;
        // else transform.Rotate(0, rotate_ang * Mathf.Sign(rot), 0);

        // smooth rotate
        transform.Rotate(0, rot * rotate_ratio, 0);

        return Mathf.Abs(rot);
    }

    // 檢查玩家和怪物距離是否夠近
    protected bool close_enough(){
        return Vector3.Distance(GameManager.Instance.player_pos, transform.position) <= see_range;
    }

    // 計算從現在到面向玩家大概需要多久
    protected float calculate_delay(float rot){
        return Mathf.Log(ang_thresh / rot) / Mathf.Log(1 - rotate_ratio) * Time.fixedDeltaTime;
    }
    public void Damaged()
    {
        print("hit");
        this.hp--;
        if (hp <= 0)
        {
            Destroy(gameObject);
            LevelManagerScript.Instance.KillEnemy();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        var otherObject = other.gameObject;
        if (otherObject.tag == "PlayerWeapon" && !immune)
            Damaged();
    }
}

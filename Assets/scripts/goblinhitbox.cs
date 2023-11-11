using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class goblinhitbox : MonoBehaviour
{
    public Animator animator;
    public GameObject hiteffect;
    private bool hit;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hit==true&&timer>=0)
        {
            timer-=Time.fixedDeltaTime;
        }
        else
        {
            timer=1.5f;
            hit=false;
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        if(collision.tag=="Player" &&hit==false)
        {
            Instantiate(hiteffect,transform.position,Quaternion.identity);
            timer =8f;
            hit=true;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource BGM, SE;
    public AudioClip BGM0, BGM1, BGM2, BGM3;
    public AudioClip cureSE, dieSE, hurtSE, transportSE, attackSE, victorySE;

    public void playBGM(int stage){
        switch(stage){
            case 0:
                BGM.clip = BGM0;
                BGM.Play();
                Debug.Log("bgm0");
                break;
            case 1:
                BGM.clip = BGM1;
                BGM.Play();
                Debug.Log("bgm1");
                break;
            case 2:
                BGM.clip = BGM2;
                BGM.Play();
                Debug.Log("bgm2");
                break;
            case 3:
                BGM.clip = BGM3;
                BGM.Play();
                Debug.Log("bgm3");
                break;
        }
    }

    public void playCureSE(){
        SE.PlayOneShot(cureSE);
    }
    public void playDieSE(){
        SE.PlayOneShot(dieSE);
    }
    public void playHurtSE(){
        SE.PlayOneShot(hurtSE);
    }
    public void playTransportSE(){
        SE.PlayOneShot(transportSE);
    }
    public void playAttackSE(){
        SE.PlayOneShot(attackSE);
    }

    public void playVictorySE(){
        BGM.volume = 0.1f;
        SE.PlayOneShot(victorySE);
        Invoke("setBGM", 10);
    }
    private void setBGM(){
        BGM.volume = 1f;
    }
}

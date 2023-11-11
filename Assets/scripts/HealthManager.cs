using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;
    private SoundManager sound;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        sound = GameObject.Find("Sound").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(healthAmount <= 0)
        {
            // Application.LoadLevel(Application.loadedLevel);
            sound.playDieSE();
            healthAmount = 100;
            SceneManager.LoadScene("Level1");
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(10);
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            Heal(5);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;

        sound.playHurtSE();
    }

    public void Heal(float healingAmount)
    {
        sound.playCureSE();
        if(healthAmount>=100)return;
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
    }
}

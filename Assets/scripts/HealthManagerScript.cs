using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManagerScript : MonoBehaviour
{
    public static HealthManagerScript Instance;
    public Image healthBar;
    public float healthAmount = 100f;
    private SoundManager sound;
    // Start is called before the first frame update
    public void Start()
    {
        if (Instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
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
            Instance.TakeDamage(10);
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            Instance.Heal(5);
        }
    }
    public void SetHealth(float hp)
    {
        healthAmount = hp;
        Instance.healthBar.fillAmount = Instance.healthAmount / 100f;
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
        Instance.healthAmount += healingAmount;
        Instance.healthAmount = Mathf.Clamp(Instance.healthAmount, 0, 100);

        Instance.healthBar.fillAmount = Instance.healthAmount / 100f;
    }
    public void EnableCanvas(bool flag)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(flag);
    }
}

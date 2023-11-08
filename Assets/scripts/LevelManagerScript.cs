using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerScript : MonoBehaviour
{

    private static LevelManagerScript _instance;
    [SerializeField]
    private GameObject Player;

    [InspectorName("HealingBlock")]
    [SerializeField]
    private GameObject healingBlock;
    [InspectorName("NumbersOfHealingBlocks")]
    [SerializeField]
    private int numbersOfHealingBlocks;

    [InspectorName("FadeInOutCanvas")]
    [SerializeField]
    private Canvas canvas;
    [InspectorName("FadeInOutTime")]
    [SerializeField]
    private float fadeOutTime = 1;

    [InspectorName("Level1 Enemy")]
    [SerializeField]
    private GameObject level1_enemy;
    [InspectorName("Level1 Enemy Count")]
    [SerializeField]
    private int level1_enemyCount;

    [InspectorName("Level1 Rocks")]
    [SerializeField]
    private GameObject[] level1_rocks;

    [InspectorName("Number of Rocks")]
    [SerializeField]
    private int level1_rockCount;


    [InspectorName("Level2 Enemy")]
    [SerializeField]
    private GameObject level2_enemy;
    [InspectorName("Level2 Enemy Count")]
    [SerializeField]
    private int level2_enemyCount;
    [InspectorName("Level2 Pillar")]
    [SerializeField]
    private GameObject level2_pillar;
    [InspectorName("Number of Pillars")]
    [SerializeField]
    private int level2_pillarCount;

    [InspectorName("Level3 Enemy")]
    [SerializeField]
    private GameObject level3_enemy;
    [InspectorName("Level3 Enemy Count")]
    [SerializeField]
    private int level3_enemyCount;

    private bool fadein = false;
    private bool fadeout = false;

    private CanvasGroup currentCanvasGroup;
    private int currentLevel = 1;
    private GameObject floor;
    void Start()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }

        Physics.IgnoreLayerCollision(8, 9);
        _instance = this;
        DontDestroyOnLoad(gameObject);
        var tempCanvas = Instantiate(canvas);
        currentCanvasGroup = tempCanvas.GetComponent<CanvasGroup>();
        DontDestroyOnLoad(tempCanvas);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;

        SceneManager_sceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);

    }
    private void createPlayer()
    {
        var player = Instantiate(Player, new Vector3(Random.Range(-floor.transform.localScale.x / 2, floor.transform.localScale.x / 2),2, Random.Range(-floor.transform.localScale.z / 2, floor.transform.localScale.z / 2)), Quaternion.identity);
        GameManager.Instance.player_trans = player.transform;
    }


    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        floor = GameObject.Find("Floor");
        createHealingBox();
        createPlayer();

        if (scene.name == "Level1")
        {
            createRocks();
            createEnemies(level1_enemy, level1_enemyCount);
        }
        if (scene.name == "Level2")
        {
            currentLevel = 2;
            createPillars();
        }
        else if (scene.name == "Level3")
        {
            currentLevel = 3;
        }

        fadein = true;
    }
    private void createHealingBox()
    {
        for (int i = 0; i < numbersOfHealingBlocks; i++)
        {
            Vector3 randomLocation = new Vector3(Random.Range(-floor.transform.localScale.x / 2, floor.transform.localScale.x / 2), 2, Random.Range(-floor.transform.localScale.z / 2, floor.transform.localScale.z / 2));
            GameObject healing = Instantiate(healingBlock, randomLocation, Quaternion.identity);
        }
    }
    private void createRocks()
    {
        for (int i = 0; i < level1_rockCount; i++)
        {
            int randomRock = Random.Range(0, level1_rocks.Length);
            Vector3 randomLocation = new Vector3(Random.Range(-floor.transform.localScale.x / 2, floor.transform.localScale.x / 2), 2, Random.Range(-floor.transform.localScale.z / 2, floor.transform.localScale.z / 2));

            GameObject newRock = Instantiate(level1_rocks[randomRock], randomLocation, Quaternion.identity);
            GameObject newGame = new GameObject();
            newGame.layer = 9;
            newRock.transform.localScale = new Vector3(Random.Range(1, 4), Random.Range(1, 4), Random.Range(1, 4));
            newRock.transform.parent = newGame.transform;
            newRock.transform.root.localScale = new Vector3(1f, 1f, 1f);
            Utility.AddColliderAroundChildren(newGame);
        }
    }
    private void createPillars()
    {
        for (int i = 0; i < level2_pillarCount; i++)
        {
            Vector3 randomLocation = new Vector3(Random.Range(-floor.transform.localScale.x / 2, floor.transform.localScale.x / 2), 0, Random.Range(-floor.transform.localScale.z / 2, floor.transform.localScale.z / 2));
            GameObject newPillar = Instantiate(level2_pillar, randomLocation, Quaternion.identity);
            newPillar.layer = 9;
            float randomSize = Random.Range(1, 5);
            newPillar.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
            //newPillar.transform.rotation = new Quaternion(20,20,20,0);
            newPillar.transform.eulerAngles = new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), Random.Range(-30, 30));
            Utility.AddColliderAroundChildren(newPillar);
        }
    }
    private void createEnemies(GameObject enemy, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 randomLocation = new Vector3(Random.Range(-floor.transform.localScale.x / 2, floor.transform.localScale.x / 2), 0, Random.Range(-floor.transform.localScale.z / 2, floor.transform.localScale.z / 2));
            GameObject newPillar = Instantiate(enemy, randomLocation, Quaternion.identity);
        }
    }
    private void fadeinout()
    {
        if (fadein)
        {
            if (currentCanvasGroup.alpha >= 0)
            {
                currentCanvasGroup.alpha -= fadeOutTime * Time.deltaTime;
            }
            if (currentCanvasGroup.alpha <= 0)
            {
                fadein = false;
            }
        }
        if (fadeout)
        {
            if (currentCanvasGroup.alpha < 1)
            {
                currentCanvasGroup.alpha += fadeOutTime * Time.deltaTime;
            }
            if (currentCanvasGroup.alpha >= 1)
            {
                if (currentLevel < 3)
                {
                    currentLevel++;
                    fadeout = false;
                    SceneManager.LoadScene("Level" + currentLevel);

                }
                else
                {
                    //Show win screen
                }
            }
        }
    }
    private void Update()
    {
        fadeinout();
        if (Input.GetKeyDown(KeyCode.N))
            NextLevel();
    }
    public void NextLevel()
    {
        print("NextLevel");
        fadeout = true;

    }
}

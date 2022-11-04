using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private DefaultInput defaultInput;
    
    public int enemiesAlive = 0;

    public int killed = 0;

    public int round = 0;

    public GameObject[] spawnPoints;

    public GameObject[] lifeSpawnPoints;

    public GameObject enemy;
    public GameObject heart;

    public GameObject endScreen;

    // TEXT
    //public Text roundText;
    public TextMeshProUGUI roundNumber;

    public TextMeshProUGUI enemiesAliveText;

    public TextMeshProUGUI enemiesKilledText;

    public TextMeshProUGUI enemiesKilledDisplay;

    public TextMeshProUGUI roundsSurvived;

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        defaultInput = new DefaultInput();
        defaultInput.Character.Pause.performed += e => Pause();
        defaultInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesAlive == 0)
        {
            round++;
            roundNumber.text = "Round " + round.ToString();
            NewWave (round);
            if (round % 2 == 0f)
            {
                GenerateHeart();
            }
        }

    }

    public void NewWave(int round)
    {
        //Debug.Log("Calling next wave: " + round);
        for (var x = 0; x < round; x++)
        {
            GameObject spawnPoint =
                spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemyCreated =
                Instantiate(enemy,
                spawnPoint.transform.position,
                Quaternion.identity);
            enemyCreated.GetComponent<zombieController>().gameManager =
                GetComponent<GameManager>();

            enemiesAlive++;
        }
        enemiesAliveText.text = "ZOMBIES ALIVE " + enemiesAlive.ToString();
    }

    public void GenerateHeart()
    {
        //Debug.Log("Generating Heart ... ");
        
        GameObject lifeSpawnPoint =
            lifeSpawnPoints[Random.Range(0, lifeSpawnPoints.Length)];
        GameObject heartCreated =
            Instantiate(heart,
            lifeSpawnPoint.transform.position,
            Quaternion.identity);
        //heartCreated.GetComponent<zombieController>().gameManager =
        //    GetComponent<GameManager>();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        roundsSurvived.text = round.ToString();
        endScreen.SetActive(true);
        enemiesAliveText.gameObject.SetActive(false);
        enemiesKilledText.gameObject.SetActive(false);
        roundNumber.gameObject.SetActive(false);
    }

    public void Pause()
    {
        //Debug.Log("Pause");
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
        }
    }
}

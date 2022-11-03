using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int enemiesAlive = 0;
    public int killed = 0;
    public int round = 0;
    public GameObject[] spawnPoints;
    public GameObject enemy;
    public GameObject endScreen;

    // TEXT
    //public Text roundText;
    public TextMeshProUGUI roundNumber;
    public TextMeshProUGUI enemiesAliveText;
    public TextMeshProUGUI enemiesKilledText;
    public TextMeshProUGUI roundsSurvived;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (enemiesAlive == 0)
        {
            round++;
            roundNumber.text = "Round " + round.ToString();
            NewWave(round);
        }
        
    }

    public void NewWave(int round)
    {
        Debug.Log("Calling next wave: " + round);
        for (var x = 0; x<round; x++)
        {
            GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemyCreated = Instantiate(enemy, spawnPoint.transform.position, Quaternion.identity);
            enemyCreated.GetComponent<zombieController>().gameManager = GetComponent<GameManager>();

            enemiesAlive++;
        }
        enemiesAliveText.text = "ZOMBIES ALIVE " + enemiesAlive.ToString();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        roundsSurvived.text = round.ToString();
        endScreen.SetActive(true);
    }
}

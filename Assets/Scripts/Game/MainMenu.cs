using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider volumeListener;
    public TextMeshProUGUI roundNumber;
    public TextMeshProUGUI killedNumber;

   

    void Start()
    {
        if (!PlayerPrefs.HasKey("volume"))
        {


            Load();
        }
        else
        {
            Load();
        }

        //PlayerPrefs.SetInt("roundHighScore", 0);
        //PlayerPrefs.SetInt("killedHighScore", 0);
    }

    public void LoadHighScore()
    {
        if (PlayerPrefs.HasKey("roundHighScore"))
        {
            roundNumber.text = PlayerPrefs.GetInt("roundHighScore").ToString();
        }

        if (PlayerPrefs.HasKey("killedHighScore"))
        {
            killedNumber.text = PlayerPrefs.GetInt("killedHighScore").ToString();
        }

    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeListener.value;
        Save();
    }

    private void Load()
    {
        volumeListener.value = PlayerPrefs.GetFloat("volume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("volume", volumeListener.value);
    }

}

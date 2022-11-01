using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerManager : MonoBehaviour
{
    public float health = 100f;
    public healthBar healthBar;

    public GameManager gameManager;

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.SetHealth(health);
        if (health <= 0f)
        {
            Debug.Log("Player is dead");
            gameManager.EndGame();
        }
    }

    public void Start()
    {
        healthBar.SetMaxHealth(health);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    private GameObject player;

    public float damage = 20f;

    private AudioSource zombieAudio;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        zombieAudio = GetComponent<AudioSource>();
    }

    public void ZombieSoundTrigger()
    {
        zombieAudio.Play();
    }

    public void Attack()
    {
        Debug.Log("Player hit");
        player.GetComponent<playerManager>().TakeDamage(damage);
    }
}

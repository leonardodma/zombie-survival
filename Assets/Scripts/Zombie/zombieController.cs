using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;
using Unity.VisualScripting;

public class zombieController : MonoBehaviour
{
    private GameObject player;

    public Animator animator;

    public float damage = 10f;

    public float health = 100f;

    public bool isDead = false;

    public GameManager gameManager;

    private Rigidbody rbZombie;

    public float timer = 0f;

    private AudioSource zombieAudio;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rbZombie = GetComponent<Rigidbody>();
        zombieAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 2.5f;

        timer += Time.deltaTime;

        if (isDead == false)
        {
            agent.SetDestination(player.transform.position);
        }

        if (agent.velocity.magnitude > 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (NearPlayer())
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", true);
            //agent.isStopped = true;
        }
        else
        {
            //agent.isStopped = false;
            animator.SetBool("isAttacking", false);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //if (collision.gameObject.tag == "Player" && health > 0)
    //    //{
    //    //    Debug.Log("Player hit");
    //    //    player.GetComponent<playerManager>().TakeDamage(damage);
    //    //}

    //    // wait for 1 second before attacking again
    //    //StartCoroutine(AttackDelay());
    //    // Sound effect
    //    zombieAudio.Play();
    //}

    

    public void TakeDamage(float amount)
    {
        health -= amount;
        //Debug.Log("Zombie health: " + health);
        if (health <= 0f && !isDead)
        {
            gameManager.enemiesAlive--;
            gameManager.killed++;

            gameManager.enemiesAliveText.text =
                "ZOMBIES ALIVE " + gameManager.enemiesAlive.ToString();
            gameManager.enemiesKilledText.text =
                "ZOMBIES KILLED " + gameManager.killed.ToString();
            gameManager.enemiesKilledDisplay.text =
                gameManager.killed.ToString();
            isDead = true;

            rbZombie.isKinematic = true;
            rbZombie.detectCollisions = false;
            GetComponent<NavMeshAgent>().enabled = false;

            animator.SetBool("isDying", true);
            StartCoroutine(CheckAnimationCompleted("Dying",
            () =>
            {
                //animator.SetBool("isDying", false);
                Destroy (gameObject);
            }));
        }
    }

    public IEnumerator
    CheckAnimationCompleted(string CurrentAnimTag, Action Oncomplete)
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsTag(CurrentAnimTag))
        yield return null;

        //Now, Wait until the current state is done playing
        while ((animator.GetCurrentAnimatorStateInfo(0).normalizedTime) % 1 <
            0.99f
        )
        yield return null;

        if (Oncomplete != null) Oncomplete();
    }

    public bool NearPlayer()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class zombieController : MonoBehaviour
{
    private GameObject player;

    public Animator animator;

    public float damage = 20f;

    public float health = 100f;

    public bool isDead = false;

    public GameManager gameManager;

    public Rigidbody rbZombie;

   

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rbZombie = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && health>0)
        {
            Debug.Log("Player hit");
            player.GetComponent<playerManager>().TakeDamage(damage);
            animator.SetBool("isAttacking", true);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Zombie health: " + health);
        if (health <= 0f && !isDead)
        {
            gameManager.enemiesAlive--;
            gameManager.killed++;

            gameManager.enemiesAliveText.text = "ZOMBIES ALIVE " + gameManager.enemiesAlive.ToString();
            gameManager.enemiesKilledText.text = "ZOMBIES KILLED " + gameManager.killed.ToString();

            isDead = true;

            rbZombie.isKinematic = true;
            rbZombie.detectCollisions = false;
            GetComponent<NavMeshAgent>().enabled = false;

            animator.SetBool("isDying", true);
            StartCoroutine(CheckAnimationCompleted("Dying", () =>
            {
                //animator.SetBool("isDying", false);
                Destroy(gameObject);
            }
            ));
        }
        
    }

    public IEnumerator CheckAnimationCompleted(string CurrentAnimTag, Action Oncomplete)
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsTag(CurrentAnimTag))
            yield return null;

        //Now, Wait until the current state is done playing
        while ((animator.GetCurrentAnimatorStateInfo(0).normalizedTime) % 1 < 0.99f)
            yield return null;
        


        if (Oncomplete != null)
            Oncomplete();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombieController : MonoBehaviour
{
    private GameObject player;

    public Animator animator;

    public float damage = 20f;

    public float health = 100f;

    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        if (collision.gameObject.tag == "Player")
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
        if (health <= 0f)
        {
            isDead = true;
            animator.SetBool("isDead", true);
            //Destroy (gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour
{
    public bool rotate; // do you want it to rotate?

    public float rotationSpeed;

    public AudioClip collectSound;

    public GameObject collectEffect;

    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
            transform
                .Rotate(Vector3.up * rotationSpeed * Time.deltaTime,
                Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Collect();
        }
    }

    public void Collect()
    {
        if (collectSound)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        if (collectEffect)
            Instantiate(collectEffect, transform.position, Quaternion.identity);

        player.GetComponent<playerManager>().Heal(20);
        Destroy (gameObject);
    }
}

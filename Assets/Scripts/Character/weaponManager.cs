using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponManager : MonoBehaviour
{
    private DefaultInput defaultInput;

    public GameObject playerCam;

    public float range = 100f;

    public float damage = 20f;

    public Animator playerAnimator;

    private AudioSource gunAudio;

    // Start is called before the first frame update
    void Start()
    {
        // Get the DefaultInput asset
        defaultInput = new DefaultInput();
        defaultInput.Character.Shoot.performed += e => Shoot();
        defaultInput.Character.Aim.performed += e => Aim();
        defaultInput.Character.Aim.canceled += e => CancelAim();
        defaultInput.Enable();

        gunAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (playerAnimator.GetBool("isShooting"))
        //{
        //    playerAnimator.SetBool("isShooting", false);
        //}
    }

    public void Shoot()
    {
        RaycastHit hit;

        playerAnimator.SetBool("isShooting", true);
        Debug.Log("Shooting");
        //Debug.Log(playerAnimator.GetBool("isShooting"));
        if (
            Physics
                .Raycast(playerCam.transform.position,
                transform.forward,
                out hit,
                range)
        )
        {
            zombieController zombieController =
                hit.transform.GetComponent<zombieController>();
            if (zombieController != null)
            {
                zombieController.TakeDamage (damage);
            }
        }

        gunAudio.Play();
    }

    public void Aim()
    {
        playerAnimator.SetBool("isAiming", true);
    }

    public void CancelAim()
    {
        playerAnimator.SetBool("isAiming", false);
    }
}

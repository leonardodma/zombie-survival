using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponManager : MonoBehaviour
{
    private DefaultInput defaultInput;

    public GameObject playerCam;

    public float range = 100f;

    // Start is called before the first frame update
    void Start()
    {
        // Get the DefaultInput asset
        defaultInput = new DefaultInput();
        defaultInput.Character.Shoot.performed += e => Shoot();
        defaultInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (
            Physics
                .Raycast(playerCam.transform.position,
                transform.forward,
                out hit,
                range)
        )
        {
            if (hit.transform.tag == "Zombie")
            {
                hit.transform.GetComponent<zombieController>().TakeDamage(20f);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereKillerController : MonoBehaviour
{
    GameMaster gm;
    Rigidbody rb;

    private void Start()
    {
        gm = GameMaster.GM;
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && rb.velocity.magnitude > 5)
        {
            gm.playerController.Damage(20);
        }
    }
}

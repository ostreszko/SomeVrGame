using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretController : MonoBehaviour
{
    GameMaster gm;

    private void Start()
    {
        gm = GameMaster.GM;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.playerObject.transform.position = new Vector3(108, 83, 89);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRigController : MonoBehaviour
{
    GameMaster gm;
    void Start()
    {
        gm = GameMaster.GM;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HealingCross"))
        {
            gm.playerController.Heal(4);
        }
    }
}

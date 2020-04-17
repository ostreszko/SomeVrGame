using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    bool triggered = false;
    ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            objectPooler.SpawnFromPool("SphereKiller", new Vector3(6f,30f,74f), transform.rotation);
            Debug.LogWarning("trap activated");
        }
    }
}

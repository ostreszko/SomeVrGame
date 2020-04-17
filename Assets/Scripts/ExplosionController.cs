using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    ParticleSystem particleSystem;
    public float duration;

    private void Start()
    {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        StartCoroutine(DisableParticleAfterTime(duration));
    }

    IEnumerator DisableParticleAfterTime(float dur)
    {
        yield return new WaitForSeconds(dur);
        gameObject.SetActive(false);
    }
}

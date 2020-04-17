using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    public float speed;
    ObjectPooler objectPooller;
    AudioManager audioManager;
    void Start()
    {
        objectPooller = ObjectPooler.Instance;
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject explosion = objectPooller.SpawnFromPool("Explosion", gameObject.transform.position + gameObject.transform.forward, gameObject.transform.rotation);
        audioManager.Play("FireballExplosionSound");
        gameObject.SetActive(false);
    }
}

using UnityEngine;

public class HealingCrossController : MonoBehaviour
{
    float floatingSpeed;
    float actualHeight;
    GameMaster gm;
    AudioManager audioManager;
    void Start()
    {
        actualHeight = transform.position.y;
        floatingSpeed = 2f;
        gm = GameMaster.GM;
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        actualHeight = transform.position.y;
        transform.position = new Vector3(transform.position.x, actualHeight + gm.healingCrossFloating * Mathf.Sin(floatingSpeed * Time.time), transform.position.z);
        transform.Rotate(Vector3.up,1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioManager.Play("HealthUpSound");
            gameObject.SetActive(false);
        }
    }
}

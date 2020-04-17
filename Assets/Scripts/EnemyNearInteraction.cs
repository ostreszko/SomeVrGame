using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = System.Random;

public class EnemyNearInteraction : MonoBehaviour
{
    float timer = 0f;
    public float gazeTime = 2f;
    bool gazedAt = false;
    Image imgGaze;
    GameMaster gm;
    Transform playerTransform;
    Animator anim;
    public float hp;
    float currentHp;
    public float attackDistance = 3.0f;
    public float hitPlayerDistance = 4.0f;
    bool isAlive = true;
    ObjectPooler objectPool;
    public Image healthBar;
    Random rnd;
    AudioManager audioManager;
    bool GotAggro;

    public enum EnemyActions
    {
        Idle = 0,
        Walk = 1,
        Death = 2,
        Attack = 3
    };

    NavMeshAgent navMeshAgent;

    private void OnEnable()
    {
        isAlive = true;
        currentHp = hp;
        healthBar.fillAmount = currentHp / hp;
    }
    void Start()
    {
        rnd = new Random();
        gm = GameMaster.GM;
        objectPool = ObjectPooler.Instance;
        imgGaze = gm.viewFinder;
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerTransform = gm.playerObject.transform;
        anim = gameObject.GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        navMeshAgent.enabled = false;
    }

    void Update()
    {
        if (!gm.gameEnded)
        {
            ChasePlayer();
            Attack();
            PlayerIsGazing();
        }
        else
        {
            anim.SetInteger("EnemyAction", (int)EnemyActions.Idle);
        }
    }

    void LateUpdate()

    {
        healthBar.transform.LookAt(gm.playerObject.transform);
        healthBar.transform.forward = -transform.forward;
    }

    public void OnPointerEnter()
    {
        gazedAt = true;
    }
    public void OnPointerExit()
    {
        gazedAt = false;
        timer = 0f;
        imgGaze.fillAmount = 0f;
    }

    public void OnPointerDown()
    {
        gm.playerController.Shoot();
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyGetHit(other);
    }

    private void EnemyGetHit(Collider other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            currentHp--;
            healthBar.fillAmount = currentHp / hp;
            if (currentHp == 0)
            {
                if (gazedAt)
                {
                    OnPointerExit();
                }
                gm.playerController.Score(100);
                navMeshAgent.isStopped = true;
                isAlive = false;
                anim.SetInteger("EnemyAction", (int)EnemyActions.Death);
                Invoke("EraseBody", 2);
            }
        }
    }

    void EraseBody()
    {
        gm.activeEnemies--;
        gm.killedEnemies++;
        SpawnObject("HealingCross");
        gameObject.SetActive(false);
    }

    void SpawnObject(string objectName)
    {
        if (rnd.Next(10) > 6)
        {
            Vector3 pos = transform.position;
            pos.y += 0.6f;
            objectPool.SpawnFromPool(objectName, pos, transform.rotation);
        }
    }

    void ChasePlayer()
    {
        if (isAlive && Vector3.Distance(transform.position,gm.playerObject.transform.position) < 15)
        {
            if (!GotAggro)
            {
                GotAggro = true;
                audioManager.Play("BeziSound");
                navMeshAgent.enabled = true;

            }
            navMeshAgent.SetDestination(playerTransform.position);
            if (navMeshAgent.velocity.magnitude > 0f)
            {
                anim.SetInteger("EnemyAction", (int)EnemyActions.Walk);
            }
            else if (navMeshAgent.velocity.magnitude == 0f)
            {
                anim.SetInteger("EnemyAction", (int)EnemyActions.Idle);
            }
        }
    }

    void PlayerIsGazing()
    {
        if (gazedAt && isAlive)
        {
            timer += Time.deltaTime;
            imgGaze.fillAmount = (float)(timer / gazeTime);
            if (timer >= gazeTime)
            {
                ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
                timer = 0f;
            }
        }
    }

    void Attack()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < attackDistance && isAlive)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        navMeshAgent.isStopped = true;
        anim.SetInteger("EnemyAction", (int)EnemyActions.Attack);
        yield return new WaitForSeconds(0.6f);
        navMeshAgent.isStopped = false;
    }

    void HitPlayer()
    {
        audioManager.Play("KickSound");
        if (Vector3.Distance(transform.position, playerTransform.position) < hitPlayerDistance)
        {
            gm.playerController.Damage(20);
        }
    }
}

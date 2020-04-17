using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    ObjectPooler objectPooller;
    public float speed = 5f;
    public float moveAngle = 30f;
    CharacterController characterController;
    Transform paerentTransform;
    GameMaster gameMaster;
    Vector3 playerEulerAngles;
    static float guiAppearSpeed = 30f;
    public Text scoreText;
    public Vector3 projectileSpacing;
    AudioManager audioManager;
    public Image deathPanel;
    public Text deathText;
    public GameObject deathCanvas;
    GlobalsKeeper gk;
    bool grounded;

    public int CurrentHealth { get;  set; }
    public int MaxHealth { get; private set; }
    public Score scoreObj; 
    public event EventHandler<HealedEventArgs> Healed;
    public event EventHandler<DamagedEventArgs> Damaged;
    public event EventHandler<ScoredEventArgs> Scored;

    public PlayerController(int currentHealth = 20, int maxHealth = 20)
    {
        if (currentHealth < 0) throw new ArgumentOutOfRangeException("current health can't be negative");
        if (currentHealth > maxHealth) throw new ArgumentOutOfRangeException("current health can't be higher than max health");
        CurrentHealth = currentHealth;
        MaxHealth = maxHealth;
    }

    void Awake()
    {
        scoreObj = new Score(scoreText);
        objectPooller = ObjectPooler.Instance;
        characterController = gameObject.GetComponentInParent<CharacterController>();
        paerentTransform = gameObject.GetComponentInParent<Transform>();
        gameMaster = GameMaster.GM;
        CurrentHealth = 20;
        MaxHealth = 20;
        gk = GlobalsKeeper.GK;
    }

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        playerEulerAngles = gameObject.transform.rotation.eulerAngles;
        Move();
        GuiVisibility();
    }

    public void Heal(int amount)
    {
        var newHealth = Math.Min(CurrentHealth + amount, MaxHealth);
        if (Healed != null)
            Healed(this,new HealedEventArgs(newHealth - CurrentHealth));
        CurrentHealth = newHealth;
    }

    public void Score(int amount)
    {
        if (Scored != null)
            Scored(this, new ScoredEventArgs(amount));
    }

    public void Damage(int damage)
    {
        var newHealth = Math.Max(CurrentHealth - damage, 0);
        if (Damaged != null)
            Damaged(this, new DamagedEventArgs(CurrentHealth - newHealth));
        CurrentHealth = newHealth;
        IfPlayerDead();
    }

    void IfPlayerDead()
    {
        if (CurrentHealth <= 0 && !gameMaster.gameEnded)
        {
            gk.playerScore = scoreObj.scoreResult;
            gameMaster.gameEnded = true;
            deathCanvas.SetActive(true);
            audioManager.Play("DeathSound");
            StartCoroutine(IfPlayerDead(0f,1f,2f));
        }
    }

    public IEnumerator IfPlayerDead(float oldValue, float newValue, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            deathPanel.color = new Color(deathPanel.color.r,deathPanel.color.g,deathPanel.color.g, Mathf.Lerp(oldValue, newValue, t / duration));
            yield return null;
        }
        deathPanel.color = new Color(deathPanel.color.r, deathPanel.color.g, deathPanel.color.g, newValue);
        for (float x = 0f; x < duration; x += Time.deltaTime)
        {
            deathText.color = new Color(deathText.color.r, deathText.color.g, deathText.color.g, Mathf.Lerp(oldValue, newValue, x / duration));
            yield return null;
        }
        deathText.color = new Color(deathText.color.r, deathText.color.g, deathText.color.g, newValue);
        Invoke("GoToDeathScene", 2f);
    }
    void GoToDeathScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Move()
    {
        characterController.SimpleMove(Vector3.down);

        if (playerEulerAngles.x > 25f && playerEulerAngles.x < 90f)
        {
            Vector3 forward = paerentTransform.TransformDirection(Vector3.forward);
            if (!audioManager.IsPlaying("StepsSound"))
            {
                audioManager.Play("StepsSound");
            }
            characterController.SimpleMove(forward * speed);
        }else
        if (playerEulerAngles.x < 335f && playerEulerAngles.x > 270f)
        {
            Vector3 backward = paerentTransform.TransformDirection(-Vector3.forward);
            characterController.SimpleMove(backward * speed);
        }
        else
        {
            if (audioManager.IsPlaying("StepsSound"))
            {
                audioManager.Pause("StepsSound");
            }
        }
    }
    void GuiVisibility()
    {
        if (playerEulerAngles.x > 10f && playerEulerAngles.x < 90f)
        {
            gameMaster.guiCanvasGroup.alpha = (playerEulerAngles.x - (guiAppearSpeed - 22f)) / guiAppearSpeed;
        }
        else
        {
            gameMaster.guiCanvasGroup.alpha = 0f;
        }
    }
    public void Shoot()
    {
        GameObject fireball = objectPooller.SpawnFromPool("PlayerProjectile", gameObject.transform.position + gameObject.transform.forward + projectileSpacing, gameObject.transform.rotation);
        audioManager.Play("FireballSound");
        projectileSpacing = -projectileSpacing;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            grounded = true;
        }
    }
    public class HealedEventArgs : EventArgs
    {
       public HealedEventArgs(int amount)
        {
            Amount = amount;
        }
        public int Amount { get; private set; }
    }
    public class DamagedEventArgs : EventArgs
    {
        public DamagedEventArgs(int amount)
        {
            Amount = amount;
        }
        public int Amount { get; private set; }
    }

    public class ScoredEventArgs : EventArgs
    {
        public ScoredEventArgs(int amount)
        {
            Amount = amount;
        }
        public int Amount { get; private set; }
    }
}

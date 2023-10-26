using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : BoolEventInvoker
{
    [SerializeField] public int health;
    [SerializeField]
    public GameObject deathPrefab;
    private Timer takeDamageTimer;
    private int maxHealth = 15;
    public bool takingDamage = false;
    private float timerSeconds = 0.5f;
    public bool invulnerable = false;
    public bool isDead = false;
    private Transform playerDeathPosition;
    private void Awake()
    {
        takeDamageTimer = gameObject.AddComponent<Timer>();
    }
    void Start()
    {
        unityBoolEvents.Add(EventName.AllEnemiesDied, new AllEnemiesDied());
        EventManager.AddBoolInvoker(EventName.AllEnemiesDied, this);
        takeDamageTimer.Duration = timerSeconds;
        takeDamageTimer.AddTimerFinishedEventListener(HandleHomingTimerFinishedEvent);
    }

    
    public void TakeDamage(int damage, GameObject deathPrefab = null)
    {
        if (invulnerable == false)
        {
            takingDamage = true;
            takeDamageTimer.Run();
            health -= damage;
            if (health <= 0)
            {
                if (gameObject.CompareTag("Player") && !isDead)
                {
                    isDead= true;
                    playerDeathPosition = GameManager.instance.playerPos;
                    SceneManager.LoadScene("DeathScene", LoadSceneMode.Additive);
                }
                if (gameObject.CompareTag("Enemy"))
                {
                    GameManager.instance.enemyList.Remove(gameObject);
                    if(GameManager.instance.enemyList.Count == 0)
                    {
                        unityBoolEvents[EventName.AllEnemiesDied].Invoke(true);
                    }
                }
                if (deathPrefab != null)
                {


                    Instantiate(deathPrefab, gameObject.transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
            else
            {
                    isDead= false;
            }
        }

    }
    void HandleHomingTimerFinishedEvent()
    {
        takingDamage= false;
        takeDamageTimer.Run();
    } 
    public void Heal(int healPower)
    {
        if (health + healPower <= maxHealth)
        {
            health += healPower;
        }
        else
        {
            health = maxHealth;
        }


    }
    public void CopyHealthFrom(Health other)
    {
        this.health = other.health;
    }

    public void InvulnerableModeActivate()
    {
        StartCoroutine(InvulnerableMode());
    }
    public IEnumerator InvulnerableMode()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.color = new Color(0, 183, 255);
        invulnerable = true;
        yield return new WaitForSeconds(2.5f);
        invulnerable= false;
        sr.color = Color.white;
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField]
    public GameObject deathPrefab;
    private Timer takeDamageTimer;
    private int maxHealth = 15;
    public bool takingDamage = false;
    private float timerSeconds = 0.5f;
    public bool invulnerable = false;
    private void Awake()
    {
        takeDamageTimer = gameObject.AddComponent<Timer>();
    }
    void Start()
    {
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
                if (deathPrefab != null)
                {
                    Instantiate(deathPrefab, gameObject.transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
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
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField]
    public GameObject deathPrefab;
    private Timer takeDamageTimer;
    public bool takingDamage = false;
    public Vector2 finalPos= Vector2.zero;
    private float timerSeconds = 0.5f;
    private void Awake()
    {
        takeDamageTimer = gameObject.AddComponent<Timer>();
    }
    void Start()
    {
        takeDamageTimer.Duration = timerSeconds;
        takeDamageTimer.AddTimerFinishedEventListener(HandleHomingTimerFinishedEvent);
    }

    
    public void TakeDamage(int damage,GameObject deathPrefab)
    {
        takingDamage = true;
        takeDamageTimer.Run();
        health -= damage;
        if (health <= 0)
        {
            Instantiate(deathPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    void HandleHomingTimerFinishedEvent()
    {
        takingDamage= false;
        takeDamageTimer.Run();
    } 
}


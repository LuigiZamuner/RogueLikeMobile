using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredRunEnemy : MonoBehaviour
{
    [SerializeField]
    GameObject deathPrefab;
    private Follow follow;
    private Rigidbody2D rb;
    private Animator anim;
    private Health health;


    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health= GetComponent<Health>();
        anim = GetComponent<Animator>();
        follow= GetComponent<Follow>();
        follow.SetImpulseForce(7);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            StartCoroutine(WaitForSeconds(2));
            follow.SetImpulseForce(0);
            anim.SetTrigger("stun");
            anim.SetBool("moving", false);

        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.TakeDamage(4,deathPrefab);
        }
    }
    private void Update()
    {
        //mudar pra corotina
        if(health.takingDamage == true)
        {
            anim.SetTrigger("hit");
        }
    }

    private IEnumerator WaitForSeconds(float seconds)
    {
       
        yield return new WaitForSeconds(seconds);

        follow.SetImpulseForce(7);
        anim.SetBool("moving", true);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class FinalForm : MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Health health;
    private Animator anim;
    private int speed = 10;
    private float waitForAtackSeconds = 1;
    private int wallHitCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        health = GetComponent<Health>();
        rb= GetComponent<Rigidbody2D>();
        AtackPlayer1();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(wallHitCount);
        if (health.takingDamage == true)
        {
            anim.SetTrigger("hit");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector3.zero;
            StartCoroutine(WaitToAtack(2));
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.TakeDamage(3);
        }
        else
        {
            wallHitCount++;
            rb.velocity = Vector3.zero;
            if (wallHitCount <= 5)
            {
                waitForAtackSeconds = 1;
                speed = 10;
                StartCoroutine(WaitToAtack(waitForAtackSeconds));
            }
            else if(wallHitCount <= 10 && wallHitCount >=6 )
            {
                sr.color = new Color(1f, 0.55f, 0.55f, 1f);
                speed = 15;
                waitForAtackSeconds = 0.5f;
                StartCoroutine(WaitToAtack(waitForAtackSeconds));
            }
            else if(wallHitCount <= 16 && wallHitCount >= 11)
            {
                sr.color = new Color(1f, 0f, 0f, 1f);
                speed = 20;
                waitForAtackSeconds = 0.25f;
                StartCoroutine(WaitToAtack(waitForAtackSeconds));
            }
            else if(wallHitCount == 17)
            {
                waitForAtackSeconds = 1;
                speed = 10;
                sr.color = new Color(1f, 1f, 1f, 1f);
                GetStuned();
            }

        }
    }
    private void AtackPlayer1()
    {

        Vector2 direction = (GameManager.instance.playerPos.position - transform.position).normalized;
        rb.velocity = direction * speed;

    }
    private void GetStuned()
    {
        anim.SetBool("moving", false);
        anim.SetTrigger("stun");
        wallHitCount = 0;
        health.invulnerable = false;
        rb.velocity = Vector2.zero;
        StartCoroutine(WaitToRecover(6));

    }
    private IEnumerator WaitToAtack(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        AtackPlayer1();
    }
    private IEnumerator WaitToRecover(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        anim.SetBool("moving", true);
        health.invulnerable = true;
        AtackPlayer1();
    }

}

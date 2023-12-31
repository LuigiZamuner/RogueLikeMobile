using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFormTatu : MonoBehaviour
{
    [SerializeField]
    GameObject tornadoPrefab;
    [SerializeField]
    GameObject finalFormPrefab;
    private Animator anim;
    private Health health;
    private Follow follow;
    private CapsuleCollider2D collider;
    private Timer timerToAtack;
    private int timerDuration = 14;
    private bool atacking =false;
    private int atackCount;


    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CapsuleCollider2D>();
        timerToAtack= gameObject.AddComponent<Timer>();
        follow= GetComponent<Follow>();
        anim= GetComponent<Animator>();
        health= GetComponent<Health>();
        anim.SetBool("roll", true);

        timerToAtack.Duration = timerDuration;
        //atacou
        StartCoroutine(WaitForAtack(2.5f));
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(atackCount);
        //mudar pra corotina
        if (health.takingDamage == true)
        {
            follow.SetImpulseForce(0);
            anim.SetTrigger("hit");
        }
        if (timerToAtack.Finished)
        {
            if (atackCount % 3 == 0 && atackCount != 7)
            {
                Stuned();
                StartCoroutine(Wait(2.5f));
                StartCoroutine(WaitForAtack(4.5f));
            }
            else if(atackCount == 7)
            {
                Health finalFormHealth = finalFormPrefab.GetComponent<Health>();
                finalFormHealth.CopyHealthFrom(health);
                Instantiate(finalFormPrefab, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(WaitForAtack(2.5f));
            }
        }
        }
    private IEnumerator WaitForAtack(float seconds)
    {

        atacking = true;
        follow.SetImpulseForce(0);
        yield return new WaitForSeconds(seconds);

        TornadoAtack();
        timerToAtack.Run();
        StartCoroutine(WaitForMove(7f));
    }
    private IEnumerator WaitForMove(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        follow.SetImpulseForce(7);
        collider.enabled = true;
    }
    private IEnumerator Wait(float seconds)
    {
        anim.SetBool("roll", false);
        anim.SetTrigger("stun");
        yield return new WaitForSeconds(seconds);
        health.invulnerable = true;
        anim.ResetTrigger("stun");
        anim.SetBool("roll", true);

    }

    private void TornadoAtack()
    {
        if (atacking)
        {
            atackCount += 1;
            collider.enabled = false;

            if (atackCount > 5)
            {
                for (int i = 0; i < 8; i++)
                {
                    Instantiate(tornadoPrefab, gameObject.transform.position, Quaternion.identity);
                }
            }
            else if(atackCount <= 5 &&  atackCount > 2)
            {
                for (int i = 0; i < 5; i++)
                {
                    Instantiate(tornadoPrefab, gameObject.transform.position, Quaternion.identity);
                }
            }
            else if(atackCount <= 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(tornadoPrefab, gameObject.transform.position, Quaternion.identity);
                }
            }
            atacking = false;

        }

    }
    private void Stuned()
    {
        health.invulnerable = false;
        follow.SetImpulseForce(0);
    }
    }


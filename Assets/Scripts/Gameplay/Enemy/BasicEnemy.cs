using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField]
    private float atackCooldown;
    [SerializeField]
    GameObject projectilePrefab;
    private float cooldowntimer = Mathf.Infinity;

    private FieldOfView fieldOfView;
    private Animator anim;
    private Follow follow;
    private Health health;
    private Timer freezeTimer;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        follow = GetComponent<Follow>();
        rb2D = GetComponent<Rigidbody2D>();
        fieldOfView = GetComponent<FieldOfView>();
        health = GetComponent<Health>();
    }
    private void Start()
    {
        freezeTimer = GetComponent<Timer>();
        freezeTimer.Duration = 0.5f;
        follow.SetImpulseForce(0);
    }
    private void Update()
    {
        cooldowntimer += Time.deltaTime;
        if (fieldOfView.FieldOfViewEnemy())
        {
            follow.followOrOposite = true;
            follow.SetImpulseForce(3);
            anim.SetTrigger("isMoving");
            if (cooldowntimer >= atackCooldown)
            {
                cooldowntimer = 0;
                Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity);

            }
            if (health.takingDamage == false)
            {
                rb2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                anim.SetTrigger("hit");
                rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }

        }
        else
        {
            follow.SetImpulseForce(0);
            anim.SetBool("isMoving", false);

        }

        if (fieldOfView.PlayerContact())
        {
            anim.SetBool("isMoving", false);
            follow.SetImpulseForce(1.5f);
            follow.followOrOposite = false;




        }

    }


}

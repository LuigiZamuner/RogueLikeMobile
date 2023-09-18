using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : IntEventInvoker
{
    private Animator anim;
    [SerializeField]
    private GameObject fishDropPrefab;
    private bool isOpen = false;

    private void Start()
    {

        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Damage") && isOpen == false)
        {
            isOpen= true;
            anim.SetTrigger("open");
            Instantiate(fishDropPrefab, gameObject.transform.position, Quaternion.identity);
        }
    }
}

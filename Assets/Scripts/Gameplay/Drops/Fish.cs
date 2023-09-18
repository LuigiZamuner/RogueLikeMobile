using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fish : IntEventInvoker
{
    private Follow follow;
    private Rigidbody2D rb;
    private Image image;

    private void Awake()
    {
        
    }
    void Start()
    {
        unityIntEvents.Add(EventName.CoinsAddedEvent, new CoinsAddedEvent());
        EventManager.AddIntInvoker(EventName.CoinsAddedEvent, this);
        image = GetComponent<Image>();
        rb = GetComponent<Rigidbody2D>();
        follow = GetComponent<Follow>();
        StartCoroutine(ExecutarAposEsperar(1.5f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            unityIntEvents[EventName.CoinsAddedEvent].Invoke(1);
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
    }
    IEnumerator ExecutarAposEsperar(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        follow.SetImpulseForce(4);
    }
}




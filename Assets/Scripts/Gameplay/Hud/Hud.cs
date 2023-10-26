using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    // Start is called before the first frame update
    // score support
    [SerializeField]
    TextMeshProUGUI coinsText;
    public int coins;



    void Start()
    {
        coins += GameManager.instance.fishPointCounter;
        EventManager.AddIntListener(EventName.CoinsAddedEvent, HandleCoinsAddedEvent);
        coinsText.text = ": " + coins;
    }
    private void Update()
    {
        coinsText.text = ": " + coins;
    }

    public int Coins
    {
        get { return coins; }
    }



    private void HandleCoinsAddedEvent(int points)
    {
        coins += points;
        coinsText.text = "Souls: " + coins;
        GameManager.instance.fishPointCounter += points;
    }
}
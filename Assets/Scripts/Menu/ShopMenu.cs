using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI coinsText;

    private void Start()
    {
        coinsText.text = ": " + GameManager.instance.fishPointCounter;
    }



    public void CloseShopMenu()
    {
        SceneManager.UnloadScene("ShopScene");
    }
}

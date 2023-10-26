using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class ShopMenuButton : MonoBehaviour
{
    [SerializeField]
    AnimatorController skinControler1;
    [SerializeField]
    TextMeshProUGUI buyText;
    [SerializeField]
    private bool bought;
    public void BuySkin(int price)
    {
        if (!bought)
        {
            if (GameManager.instance.fishPointCounter >= price)
            {
                GameManager.instance.fishPointCounter -= price;
                bought = true;
                buyText.text = "";
            }
        }
        else
        {
            GameManager.instance.ChangePlayerAnimatorController(skinControler1);
            buyText.text = "Equipped";
        }

    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dungeon1Enter : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.FreezePlayer();
            SceneController.instance.NextLevel("Rooms");
            DOTween.To(() => AudioManager.instance.musicSource.volume, x => AudioManager.instance.musicSource.volume = x, 0f, 2.5f).OnComplete(() =>
            {
            });
        }
    }
}
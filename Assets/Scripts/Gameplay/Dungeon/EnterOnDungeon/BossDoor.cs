using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDoor : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneController.instance.NextLevel("BossBattle");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : AdsInitializer
{
    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()
    {
        SceneController.instance.NextLevel("InitialPlace");
        Destroy(gameObject);
    }
}

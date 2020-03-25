using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : SceneController
{
    public Transform player;

    public override void Start()
    {
        base.Start();



        if (prevScene == "Scene2" && currentScene != "SchnapHouse" && currentScene != "End")
        {
            player.position = new Vector2(0.23f, -2.77f);
            Camera.main.transform.position = new Vector3(0.23f, -1.67f, -7.4f);
        }
        
        if (prevScene == "SchnapHouse")
        {
            player.position = new Vector2(1.84f, -2.77f);

            Camera.main.transform.position = new Vector3(1.84f, -1.67f, -7.4f);
        }

        if (prevScene == "End" && currentScene == "Scene2")
        {
            player.position = new Vector2(7.5f, -2.77f);

            Camera.main.transform.position = new Vector3(7.5f, -1.67f, -7.4f);
        }

        /*if (prevScene == "End" && currentScene == "Scene1")
        {
            player.position = new Vector2(-22.55f, -2.77f);

            Camera.main.transform.position = new Vector3(-22.55f, -1.67f, -7.4f);

        }*/
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Services : MonoBehaviour
{
    public static AILifecycle aiManager;
    public static CubeManager cubeManager;
    public static EventManager eventManager;

    public static PlayerMovement player;
    public static GameManager gameManager;

    public static void Init()
    {
        //aiManager = new AILifecycle();
        //cubeManager = new CubeManager();
        eventManager = new EventManager();
    }
}

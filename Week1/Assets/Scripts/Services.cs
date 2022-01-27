using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Services : MonoBehaviour
{
    public static PlayerMovement player;
    public static AILifecycle aiManager;
    public static CubeManager cubeManager;

    public static void Init()
    {
        aiManager = new AILifecycle();
        cubeManager = new CubeManager();
    }
}

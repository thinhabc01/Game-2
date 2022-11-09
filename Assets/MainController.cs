using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class MainController : MonoBehaviour
{
    void Start()
    {
        Manager.LoadingSceneName = LoadingController.LOADING_SCENE_NAME;
        Manager.Load(HomeController.HOME_SCENE_NAME);
    }
}

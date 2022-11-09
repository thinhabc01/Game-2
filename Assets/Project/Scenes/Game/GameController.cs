using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GameController : Controller
{
    public const string GAME_SCENE_NAME = "Game";

    public override string SceneName()
    {
        return GAME_SCENE_NAME;
    }

    public void OverGame()
    {
        Manager.Add(WinGameController.WINGAME_SCENE_NAME, "Game Over!");
    }
    public void WinGame()
    {
        Manager.Add(WinGameController.WINGAME_SCENE_NAME, "Game Win!");
    }
}
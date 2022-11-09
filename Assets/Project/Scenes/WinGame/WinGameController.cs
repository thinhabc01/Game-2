using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SS.View;

public class WinGameController : Controller
{
    public const string WINGAME_SCENE_NAME = "WinGame";
    [SerializeField] TextMeshProUGUI text;


    public override string SceneName()
    {
        return WINGAME_SCENE_NAME;
    }
    public override void OnActive(object data)
    {
        base.OnActive(data);
        if (data != null)
        {
            text.text = data.ToString();
        }
    }
    public void HomeButton()
    {
        Manager.Load(HomeController.HOME_SCENE_NAME);
    }
    public void PlayAgainButton()
    {
        Manager.Load(GameController.GAME_SCENE_NAME);
    }
}
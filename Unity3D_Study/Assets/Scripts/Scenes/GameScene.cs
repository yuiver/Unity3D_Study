using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameScene : BaseScene
{

    protected override void Init()
    { 
        base.Init();

        SceneType = Define.Scene.Game;

        //Managers.Resource.Instantiate("UI/UI_Button");

        Managers.UI.ShowSceneUI<UI_Inven>();
        //Managers.UI.ShowPopupUI<UI_Button>();

        //UI_Button ui =
        //Managers.UI.ClosePopupUI(ui);

        //for (int i = 0; i < 5; i++)
        //Managers.Resource.Instantiate("UnityChan");

        Dictionary<int,Stat> dict = Managers.Data.StatDict;
 
    }
    public override void Clear()
    {
     
    }
}

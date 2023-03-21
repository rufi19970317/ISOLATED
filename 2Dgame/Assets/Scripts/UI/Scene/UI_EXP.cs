using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_EXP : UI_Scene
{
    List<UI_EXP_Set> expSets = new List<UI_EXP_Set>();

    enum GameObjects
    {
        GridPanel
    }

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);

        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        for (int i = 0; i < 3; i++)
        {
            UI_EXP_Set _expSet = Managers.UI.MakeSubItem<UI_EXP_Set>(gridPanel.transform);
            _expSet.SetInfo((Define.EXPType)i);
            expSets.Add(_expSet);
        }
    }

    public void UpdateEXP(Define.EXPType expType, int expNum, float expRatio)
    {
        expSets[(int)expType].OnUpdate(expNum, expRatio);
    }
}

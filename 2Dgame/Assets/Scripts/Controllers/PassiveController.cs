using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PassiveController : ItemController
{
    /*
    public Dictionary<Define.Passive, string> passiveToString = new Dictionary<Define.Passive, string>();
    public Dictionary<Define.Passive, PassiveStat> passiveStat = new Dictionary<Define.Passive, PassiveStat>();

    #region Passive Set
    public void SetPassive(Define.Passive passive)
    {
        if (passiveStat.ContainsKey(passive))
            passiveStat[passive] = Managers.Data.PassiveLv[0][passive.ToString()];
        else
            passiveStat.Add(passive, Managers.Data.PassiveLv[0][passive.ToString()]);

        if (passiveToString.ContainsKey(passive))
            passiveToString[passive] = passive.ToString();
        else
            passiveToString.Add(passive, passive.ToString());

        UI_Inven _invenUI = GameObject.Find("UI_Inven").GetComponent<UI_Inven>();
        _invenUI.SetPassiveItem(passive.ToString(), 1);
    }

    public void PassiveLevelUp(Define.Passive passive)
    {
        if (passiveStat.ContainsKey(passive))
            SetPassiveStat(passive);
        else
            SetPassive(passive);
    }

    public void SetPassiveStat(Define.Passive passive)
    {
        if (passiveStat.ContainsKey(passive))
        {
            int level = passiveStat[passive].level;
            passiveStat[passive] = Managers.Data.PassiveLv[level][passive.ToString()];

            UI_Inven _invenUI = GameObject.Find("UI_Inven").GetComponent<UI_Inven>();
            _invenUI.SetPassiveItem(passive.ToString(), level);
        }
    }
    #endregion
    */
}

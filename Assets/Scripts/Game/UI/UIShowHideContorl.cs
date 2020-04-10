using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShowHideContorl : MonoBehaviour {

    public void StatusClick()
    {
        StatusContorl._intance.ShowStatus();
    }
    public void BagClick()
    {
        Inventory._instance.ShowInventory();
    }
    public void EquipClick()
    {
        EquipMent._instance.ShowEquip();
    }
    public void SkillClick()
    {
        SkillUI._instance.Show();
    }
    public void SettingClick()
    {

    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RARC_PlayMenuUITab : MonoBehaviour
{
    ////////////////////////////////

    [Header("Panels")]
    public GameObject saveSlot_New;
    public GameObject saveSlot_Load;

    [Header("Text")]
    public TextMeshProUGUI weeks_Text;

    public TextMeshProUGUI fuel_Text;
    public TextMeshProUGUI scrap_Text;
    public TextMeshProUGUI food_Text;

    public TextMeshProUGUI humans_Text;
    public TextMeshProUGUI bots_Text;

    /////////////////////////////////////////////////////////////////

    public void NewPanel()
    {
        saveSlot_New.SetActive(true);
        saveSlot_Load.SetActive(false);
    }

    public void LoadPanel(RARC_ShipSaveData saveData, int debugWeekCount)
    {
        saveSlot_New.SetActive(false);
        saveSlot_Load.SetActive(true);

        weeks_Text.text = "Weeks Survived: " + debugWeekCount;
        fuel_Text.text = "x" + saveData.shipResource_Fuel.resourceCount.ToString();
        scrap_Text.text = "x" + saveData.shipResource_Scrap.resourceCount.ToString();
        food_Text.text = "x" + saveData.shipResource_Food.resourceCount.ToString();

        humans_Text.text = "x" + saveData.shipData_Crew_List.Count.ToString();
        bots_Text.text = "x" + saveData.shipData_Bots_List.Count.ToString();
    }

    /////////////////////////////////////////////////////////////////
}

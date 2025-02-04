﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RARC_ConstructionRoomUITab : MonoBehaviour
{
    ////////////////////////////////

    [HideInInspector]
    public RARC_Rooms_SO roomSO;

    [Header("Tab Info")]
    public Image roomIcon;
    public TextMeshProUGUI name_Text;
    public TextMeshProUGUI limit_Text;
    public RARC_RequirementsUITab requirementsTab;

    /////////////////////////////////////////////////////////////////

    public void SetupTab(RARC_Rooms_SO newRoomSO)
    {
        //Setup Info
        roomSO = newRoomSO;
        name_Text.text = roomSO.roomName;
        limit_Text.text = "Limit " + RARC_GameStateController.Instance.CountRoomsOnShip(newRoomSO.roomType) + "/" + newRoomSO.roomLimit;
        //roomIcon.sprite = roomSO.activeRoomSprite;

        //Setup and Refresh UI
        RefreshRequirementsUI();

       
        if (RARC_GameStateController.Instance.CountRoomsOnShip(newRoomSO.roomType) >= newRoomSO.roomLimit)
        {
            gameObject.GetComponent<Button>().interactable = false;
            limit_Text.text = "<color=" + RARC_ButtonController_Game.Instance.colorValues_Red + ">" + "Limit " + RARC_GameStateController.Instance.CountRoomsOnShip(newRoomSO.roomType) + "/" + newRoomSO.roomLimit;
        }
        else
        {
            limit_Text.text = "Limit " + RARC_GameStateController.Instance.CountRoomsOnShip(newRoomSO.roomType) + "/" + newRoomSO.roomLimit;
        }
    }

    public void Button_ContructRoom()
    {
        //Build Room On Last selected Room
        RARC_ButtonController_Game.Instance.Button_Construction_BuildRoom(this);

        //Remove All Required Resources
        List<RARC_Resource> newResource_List = GetResourcesList();
        requirementsTab.RemoveRequirementsFromPlayer(newResource_List);
    }

    public void RefreshRequirementsUI()
    {
        //Setup Tab and allow this button to be clicked or not based off the critrea given
        List<RARC_Resource> newResource_List = GetResourcesList();
        requirementsTab.SetupTab(newResource_List, RARC_ButtonController_Game.Instance.colorValues_Red, RARC_ButtonController_Game.Instance.colorValues_White);
    }

    private List<RARC_Resource> GetResourcesList()
    {
        //Setup List
        List<RARC_Resource> newResource_List = new List<RARC_Resource>();

        //Gather Resources
        RARC_Resource newResource1 = new RARC_Resource(roomSO.resourceRequiredAmount_1, roomSO.resourceRequired_1);
        newResource_List.Add(newResource1);
        RARC_Resource newResource2 = new RARC_Resource(roomSO.resourceRequiredAmount_2, roomSO.resourceRequired_2);
        newResource_List.Add(newResource2);
        RARC_Resource newResource3 = new RARC_Resource(roomSO.resourceRequiredAmount_3, roomSO.resourceRequired_3);
        newResource_List.Add(newResource3);
        RARC_Resource newResource4 = new RARC_Resource(roomSO.resourceRequiredAmount_4, roomSO.resourceRequired_4);
        newResource_List.Add(newResource4);
        RARC_Resource newResource5 = new RARC_Resource(roomSO.resourceRequiredAmount_5, roomSO.resourceRequired_5);
        newResource_List.Add(newResource5);

        return newResource_List;
    }

    /////////////////////////////////////////////////////////////////
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_CrewBotsController : MonoBehaviour
{
    ////////////////////////////////

    public static RARC_CrewBotsController Instance;

    ////////////////////////////////

    [Header("BLANKVAR")]
    public GameObject crew_Prefab;
    public GameObject bot_Prefab;

    [Header("BLANKVAR")]
    public GameObject crew_Container;
    public GameObject bot_Container;

    /////////////////////////////////////////////////////////////////

    private void Awake()
    {
        //Set Static Singleton Self Refference
        Instance = this;
    }

    private void Start()
    {
        //AddNewCrew();
        RespawnAllCrewAndBots();
    }

    /////////////////////////////////////////////////////////////////

    public void RespawnAllCrewAndBots()
    {
        //Delete Old Crew
        foreach (Transform child in crew_Container.transform)
        {
            Destroy(child.gameObject);
        }

        //Delete Old Bots
        foreach (Transform child in bot_Container.transform)
        {
            Destroy(child.gameObject);
        }

        //Spawn Crew
        foreach (RARC_Crew child in RARC_DatabaseController.Instance.ship_SaveData.shipData_Crew_List)
        {
            SpawnCrewInRoom(RARC_RoomsController.Instance.roomsInShip_List[Random.Range(0, RARC_RoomsController.Instance.roomsInShip_List.Count)], child);
        }

        //Spawn Bots
        foreach (RARC_Crew child in RARC_DatabaseController.Instance.ship_SaveData.shipData_Bots_List)
        {
            SpawnBotInRoom(RARC_RoomsController.Instance.roomsInShip_List[Random.Range(0, RARC_RoomsController.Instance.roomsInShip_List.Count)], child);
        }
    }

    /////////////////////////////////////////////////////////////////

    public void AddNewBot()
    {
        RARC_Crew newCrewMember = new RARC_Crew();
        RARC_DatabaseController.Instance.ship_SaveData.shipData_Bots_List.Add(newCrewMember);
        SpawnBotInRoom(RARC_RoomsController.Instance.roomsInShip_List[Random.Range(0, RARC_RoomsController.Instance.roomsInShip_List.Count)], newCrewMember);
        RARC_ButtonController_Game.Instance.RefreshUI_ResourcesAndStorage();
    }

    public void AddNewCrew()
    {
        RARC_Crew newCrewMember = new RARC_Crew();
        RARC_DatabaseController.Instance.ship_SaveData.shipData_Crew_List.Add(newCrewMember);
        SpawnCrewInRoom(RARC_RoomsController.Instance.roomsInShip_List[Random.Range(0, RARC_RoomsController.Instance.roomsInShip_List.Count)], newCrewMember);
        RARC_ButtonController_Game.Instance.RefreshUI_ResourcesAndStorage();
    }

    /////////////////////////////////////////////////////////////////

    public void RemoveCrewMember()
    {
        RARC_DatabaseController.Instance.ship_SaveData.shipData_Crew_List.RemoveAt(0);
        if (crew_Container.transform.childCount != 0)
        {
            DestroyImmediate(crew_Container.transform.GetChild(0).gameObject);
        }

        RARC_ButtonController_Game.Instance.RefreshUI_ResourcesAndStorage();
    }

    public void RemoveBotMember()
    {
        RARC_DatabaseController.Instance.ship_SaveData.shipData_Bots_List.RemoveAt(0);
        if (bot_Container.transform.childCount != 0)
        {
            DestroyImmediate(bot_Container.transform.GetChild(0).gameObject);
        }

        RARC_ButtonController_Game.Instance.RefreshUI_ResourcesAndStorage();
    }

    /////////////////////////////////////////////////////////////////

    public void SpawnBotInRoom(RARC_RoomTab roomTab, RARC_Crew newCrewMember)
    {
        Vector3 spawnPoint = roomTab.GetRandomNode(new Vector3(0, 0, 0)).transform.position;

        GameObject newCrewMember_GO = Instantiate(bot_Prefab, bot_Container.transform);
        newCrewMember_GO.transform.position = spawnPoint;

        //Set Crew Info For Current Level
        newCrewMember_GO.GetComponent<RARC_CrewAgent>().crewCurrentShipFloor = roomTab.currentFloorLevel;
    }

    public void SpawnCrewInRoom(RARC_RoomTab roomTab, RARC_Crew newCrewMember)
    {
        Vector3 spawnPoint = roomTab.GetRandomNode(new Vector3(0,0,0)).transform.position;

        GameObject newCrewMember_GO = Instantiate(crew_Prefab, crew_Container.transform);
        newCrewMember_GO.transform.position = spawnPoint;

        //Set Crew Info For Current Level
        newCrewMember_GO.GetComponent<RARC_CrewAgent>().crewCurrentShipFloor = roomTab.currentFloorLevel;
    }

    /////////////////////////////////////////////////////////////////

    public GameObject GetWanderingNodePosition(int shipFloorLevel, Vector3 currentMemberPosition)
    {
        //Filter Rooms On Level Then Grab a Room Then Grab a Node In Room
        List<RARC_RoomTab> roomsOnFloorLevel_List = RARC_RoomsController.Instance.GetRoomsOnFloorLevel(shipFloorLevel);
        RARC_RoomTab selectedRoom = roomsOnFloorLevel_List[Random.Range(0, roomsOnFloorLevel_List.Count)];
        return selectedRoom.GetRandomNode(currentMemberPosition);
    }

    public GameObject GetTravelingPosition(RARC_RoomTab workstationRoomTab, Vector3 currentMemberPosition)
    {
        //Filter Rooms On Level Then Grab a Room Then Grab a Node In Room
        return workstationRoomTab.GetRandomNode(currentMemberPosition);
    }

    public GameObject GetClosestElevatorPosition(int currentFloor)
    {
        //Get Closest Elevator Then Return The Elevator Node
        return RARC_RoomsController.Instance.GetElevatorCurrentFloorLevel(currentFloor).elevatorNode_GO;
    }

    public GameObject GetNextElevatorPosition(int currentFloor, int goalFloor)
    {
        //Get Closest Elevator Then Return The Elevator Node
        return RARC_RoomsController.Instance.GetNextElevatorFloorLevel(currentFloor, goalFloor).elevatorNode_GO;
    }

    /////////////////////////////////////////////////////////////////
}

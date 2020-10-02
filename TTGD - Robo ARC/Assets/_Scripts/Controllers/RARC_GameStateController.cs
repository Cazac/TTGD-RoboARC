﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RARC_GameStateController : MonoBehaviour
{
    ////////////////////////////////

    public static RARC_GameStateController Instance;

    ////////////////////////////////

    [Header("Cursor State")]
    public CursorState currentCursorState;
    public enum CursorState
    {
        NORMAL,
        BUILD_RESEARCH, BUILD_MEDICAL, BUILD_FOOD, BUILD_RECREATION, BUILD_FACTORY, BUILD_STORAGE
    }

    ////////////////////////////////

    [Header("System Ready States")]
    public bool isReady_Launch;
    public bool isReady_Navigation;
    public bool isReady_Explore;
    public bool isReady_Event;
    public bool isReady_Research;
    public bool isReady_Contruction;
    public bool isReady_Crew;

    [Header("Interatablity Cover")]
    public Image blackoutCurtain_Image;

    [Header("Animators")]
    public Animator blacokoutCurtain_Animator;
    public Animator ship_Animator;
    public Animator cutScene_Animator;
    public Animator gameover_Animator;

    public IEnumerator currentCutscene_IEnum;

    [Header("Navigation Options")]
    public List<RARC_Planet> navigationPossiblePlanets_List;

    [Header("BLANKVAR")]
    public readonly int fuelRequired = 5;
    public readonly int foodRequired = 10;

    /////////////////////////////////////////////////////////////////

    private void Awake()
    {
        //Set Static Singleton Self Refference
        Instance = this;
    }

    private void Start()
    {
        //Check if its the first week On load
        if (RARC_DatabaseController.Instance.ship_SaveData.shipInfo_WeeksSurvived == 0)
        {
            System_GenerateNewWeek(true, true);
        }
        else
        {
            System_GenerateNewWeek(false, true);
        }

        //Refresh All UI On Startup As Well
        RARC_ButtonController_Game.Instance.RefreshUI_WeeksInSpace();
        RARC_ButtonController_Game.Instance.RefreshUI_NavigationDestination();
        RARC_ButtonController_Game.Instance.RefreshUI_UrgentIcons();
        RARC_ButtonController_Game.Instance.RefreshUI_ResourcesAndStorage();
        RARC_ButtonController_Game.Instance.RefreshUI_ButtonInteractablity();
        RARC_ButtonController_Game.Instance.RefreshUI_LaunchResources();
        


        //Load System Data

        //set cursor state
        currentCursorState = CursorState.NORMAL;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (RARC_ButtonController_Game.Instance.PauseMenu_Main.activeSelf != true)
            {
                RARC_ButtonController_Game.Instance.Button_Pause();
            }
            else
            {
                RARC_ButtonController_Game.Instance.Button_Pause_Close();
            }
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButton(1) && currentCursorState != CursorState.NORMAL)
        {
            currentCursorState = CursorState.NORMAL;
        }
    }

    /////////////////////////////////////////////////////////////////

    public void System_GenerateNewWeek(bool isFirstWeek, bool isFirstLoad)
    {
        //All Bools default to Ready
        isReady_Navigation = true;
        isReady_Event = true;
        isReady_Crew = true;
        isReady_Explore = true;
        isReady_Research = true;
        isReady_Contruction = true;

        //Execpt Launch
        isReady_Launch = false;



        //Check For New Save File
        if (isFirstWeek)
        {
            //Play Cutscene As Enum
            currentCutscene_IEnum = Player_StartCutscene();
            StartCoroutine(Player_StartCutscene()); 
 
            //Give First Backstory Event
            RARC_DatabaseController.Instance.ship_SaveData.shipCurrentEvents_List.Add(new RARC_Event(RARC_DatabaseController.Instance.events_DB.event_ANewHope));

            //Set Planet BG / Space BG
            RARC_ButtonController_Game.Instance.space_Tab.spacePlanet_Tab.ClearPlanet();
            RARC_ButtonController_Game.Instance.space_Tab.SetSpace_Black();
        }
        else if (isFirstLoad)
        {

            print("Test Code: Load Stuff Here");


        }
        else
        {
            //Progress Time
            RARC_DatabaseController.Instance.ship_SaveData.shipInfo_WeeksSurvived++;
            RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationTripProgress++;


            //Generate Possible Events

        }
       



        //Remove Old Current Location
        RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation = null;


        if (RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination != null)
        {
            if (RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationTripProgress >= RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination.planetTravelTime)
            {
                //Set Location
                RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation = RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination;

                //Set Explore Requirement
                isReady_Explore = false;

                //Reset Counter
                RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationTripProgress = 0;

                //Remove Planet Destination
                RARC_DatabaseController.Instance.ship_SaveData.shipData_NavigationDestination = null;
            }
        }


        //Stop Space Animations if 
        if (RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation == null)
        {

        }
        else
        {

        }




        //Reset Navigation Planets
        navigationPossiblePlanets_List = new List<RARC_Planet>();
        navigationPossiblePlanets_List.Add(RARC_DatabaseController.Instance.planet_SO.GeneratePlanet_Rocky());
        navigationPossiblePlanets_List.Add(RARC_DatabaseController.Instance.planet_SO.GeneratePlanet_Rocky());
        navigationPossiblePlanets_List.Add(RARC_DatabaseController.Instance.planet_SO.GeneratePlanet_Rocky());
    }

    /////////////////////////////////////////////////////////////////

    public IEnumerator Player_StartCutscene()
    {
        cutScene_Animator.gameObject.SetActive(true);

        yield return new WaitForSeconds(18.5f);

        if (currentCutscene_IEnum == null)
        {
            yield break;
        }

        print("Test Code: Ending Cutscene");

        cutScene_Animator.gameObject.SetActive(false);

        blacokoutCurtain_Animator.Play("Fade Out");

        currentCutscene_IEnum = null;
    }

    public IEnumerator Player_StartWeek()
    {
        //Do Visuals Here



        yield return new WaitForSeconds(2f);

        if (RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation != null)
        {
            //Set Planet BG / Space BG
            RARC_ButtonController_Game.Instance.space_Tab.spacePlanet_Tab.SetPlanet(RARC_DatabaseController.Instance.ship_SaveData.shipData_currentLocation);
            RARC_ButtonController_Game.Instance.space_Tab.PlayPlanetSpace();
            RARC_ButtonController_Game.Instance.space_Tab.PlanetAnimatorSpeeds();
        }
        else
        {
            print("Test Code: NOPE NOTHING");

            //Set Planet BG / Space BG
            RARC_ButtonController_Game.Instance.space_Tab.spacePlanet_Tab.ClearPlanet();
            RARC_ButtonController_Game.Instance.space_Tab.SetSpace_Black();
            RARC_ButtonController_Game.Instance.space_Tab.EmptySpaceAnimatorSpeeds();
        }


        //Refresh All UI
        RARC_ButtonController_Game.Instance.RefreshUI_WeeksInSpace();
        RARC_ButtonController_Game.Instance.RefreshUI_NavigationDestination();
        RARC_ButtonController_Game.Instance.RefreshUI_UrgentIcons();
        RARC_ButtonController_Game.Instance.RefreshUI_ResourcesAndStorage();
        RARC_ButtonController_Game.Instance.RefreshUI_ButtonInteractablity();

        yield return new WaitForSeconds(2f);

        //print("Test Code: Ready");

        //Add Interatablity
        blackoutCurtain_Image.raycastTarget = false;

        yield break;
    }

    public void Player_FinishWeek()
    {
        //Remove Interatablity
        RARC_ButtonController_Game.Instance.LaunchButton_Main.interactable = false;
        blackoutCurtain_Image.raycastTarget = true;

        //Play ANimaitons
        ship_Animator.Play("Travel");
        blacokoutCurtain_Animator.Play("Fade In");
    }

    /////////////////////////////////////////////////////////////////

    public void LoadBackground()
    {

        //if ()
        {

        }


    }

    /////////////////////////////////////////////////////////////////

    public RARC_Resource.ResourceType ConvertTypes(RARC_EventOutcome_SO.OutcomeType outcomeType)
    {
        //Resource Types
        RARC_Resource.ResourceType resourceType;

        switch (outcomeType)
        {
            case RARC_EventOutcome_SO.OutcomeType.SCRAPMETAL_CHANGE:
                resourceType = RARC_Resource.ResourceType.ScrapMetal;
                break;
            case RARC_EventOutcome_SO.OutcomeType.FUEL_CHANGE:
                resourceType = RARC_Resource.ResourceType.Fuel;
                break;
            case RARC_EventOutcome_SO.OutcomeType.FOOD_CHANGE:
                resourceType = RARC_Resource.ResourceType.Food;
                break;
            case RARC_EventOutcome_SO.OutcomeType.TITANIUM_CHANGE:
                resourceType = RARC_Resource.ResourceType.Titanium;
                break;
            case RARC_EventOutcome_SO.OutcomeType.SILICON_CHANGE:
                resourceType = RARC_Resource.ResourceType.Silicon;
                break;
            case RARC_EventOutcome_SO.OutcomeType.CARBON_CHANGE:
                resourceType = RARC_Resource.ResourceType.Carbon;
                break;
            case RARC_EventOutcome_SO.OutcomeType.ORGANICS_CHANGE:
                resourceType = RARC_Resource.ResourceType.Organics;
                break;
            case RARC_EventOutcome_SO.OutcomeType.MEDKITS_CHANGE:
                resourceType = RARC_Resource.ResourceType.Medkit;
                break;
            case RARC_EventOutcome_SO.OutcomeType.HYDROGEN_CHANGE:
                resourceType = RARC_Resource.ResourceType.Hydrogen;
                break;
            case RARC_EventOutcome_SO.OutcomeType.NITROGEN_CHANGE:
                resourceType = RARC_Resource.ResourceType.Nitrogen;
                break;
            default:
                resourceType = RARC_Resource.ResourceType.NULL;
                break;
        }

        //Return
        return resourceType;
    }

    public void GainResources(string resourceName, RARC_Resource.ResourceType resourceType, int resourceCount)
    {
        switch (resourceType)
        {
            case RARC_Resource.ResourceType.ScrapMetal:
                RARC_DatabaseController.Instance.ship_SaveData.shipResource_Scrap.resourceCount += resourceCount;
                break;

            case RARC_Resource.ResourceType.Fuel:
                RARC_DatabaseController.Instance.ship_SaveData.shipResource_Fuel.resourceCount += resourceCount;
                break;

            case RARC_Resource.ResourceType.Food:
                RARC_DatabaseController.Instance.ship_SaveData.shipResource_Food.resourceCount += resourceCount;
                break;

            default:
                int resourceSlot = 99;
                int i = 0;

                foreach (RARC_Resource resourceInShip in RARC_DatabaseController.Instance.ship_SaveData.shipStorage_List)
                {
                    if (resourceInShip.resourceType == resourceType)
                    {
                        resourceSlot = i;
                    }
                    i++;
                }

                if (resourceSlot != 99)
                {
                    RARC_DatabaseController.Instance.ship_SaveData.shipStorage_List[resourceSlot].resourceCount += resourceCount;
                }
                else
                {
                    RARC_Resource resource = new RARC_Resource(resourceName, resourceCount, resourceType);
                    RARC_DatabaseController.Instance.ship_SaveData.shipStorage_List.Add(resource);
                }
                break;
        }

        RARC_ButtonController_Game.Instance.RefreshUI_ResourcesAndStorage();
    }

    public void LoseResources(RARC_Resource.ResourceType resourceType, int resourceCount)
    {

    }

    public void ChangeHull(int amount)
    {
        //Refresh
        RARC_DatabaseController.Instance.ship_SaveData.shipHullHealth = Mathf.Clamp(RARC_DatabaseController.Instance.ship_SaveData.shipHullHealth + amount, 0, 100);
        //RARC_ButtonController_Game.Instance.RefreshUI_LaunchResources
    }

    public void GainCrew(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            RARC_CrewBotsController.Instance.AddNewCrew();
        }
    }

    public void LoseCrew(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            RARC_CrewBotsController.Instance.RemoveCrewMember();
        }
    }

    public void GainBot(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            RARC_CrewBotsController.Instance.AddNewBot();
        }
    }

    public void LoseBot(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            RARC_CrewBotsController.Instance.RemoveBotMember();
        }
    }

    /////////////////////////////////////////////////////////////////

    public void System_Gameover(string reason)
    {


        RARC_ButtonController_Game.Instance.gameoverContainer_GO.SetActive(true);


        switch (reason)
        {
            case "Crew":
                RARC_ButtonController_Game.Instance.gameoverImage_Crew.SetActive(true);
                RARC_ButtonController_Game.Instance.gameoverImage_Hull.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Food.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Fuel.SetActive(false);

                break;

            case "Hull":
                RARC_ButtonController_Game.Instance.gameoverImage_Crew.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Hull.SetActive(true);
                RARC_ButtonController_Game.Instance.gameoverImage_Food.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Fuel.SetActive(false);

                break;

            case "Food":
                RARC_ButtonController_Game.Instance.gameoverImage_Crew.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Hull.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Food.SetActive(true);
                RARC_ButtonController_Game.Instance.gameoverImage_Fuel.SetActive(false);

                break;

            case "Fuel":
                RARC_ButtonController_Game.Instance.gameoverImage_Crew.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Hull.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Food.SetActive(false);
                RARC_ButtonController_Game.Instance.gameoverImage_Fuel.SetActive(true);

                break;
        }

        //Debug
        RARC_ButtonController_Game.Instance.gameoverImage_Fuel.SetActive(true);



        gameover_Animator.Play("Fade In Gameover");
 





        print("Test Code: Gameover!");
    }

    /////////////////////////////////////////////////////////////////
}

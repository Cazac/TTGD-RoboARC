﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RARC_ResourceData : MonoBehaviour
{
    [Header("Basics")]
    public RARC_Resource scrapMetal_Resource;
    public RARC_Resource fuel_Resource;
    public RARC_Resource food_Resource;

    [Header("Others")]
    public RARC_Resource titanium_Resource;
    public RARC_Resource silicon_Resource;
    public RARC_Resource carbon_Resource;
    public RARC_Resource organics_Resource;

    [Header("Gases")]
    public RARC_Resource hydrogen_Resource;
    public RARC_Resource nitrogen_Resource;

    /////////////////////////////////////////////////////////////////

    public void BuildDatabase()
    {
        //Set New Resource Bases
        scrapMetal_Resource = new RARC_Resource
        (
            "Scrap",
            30,
            RARC_Resource.ResourceType.ScrapMetal
        );

        fuel_Resource = new RARC_Resource
        (
            "Fuel",
            0,
            RARC_Resource.ResourceType.Fuel
        );

        food_Resource = new RARC_Resource
        (
            "Food",
            0,
            RARC_Resource.ResourceType.Food
        );

        titanium_Resource = new RARC_Resource
        (
            "Titanium",
            0,
            RARC_Resource.ResourceType.Titanium
        );

        silicon_Resource = new RARC_Resource
        (
            "Silicon",
            0,
            RARC_Resource.ResourceType.Silicon
        );

        carbon_Resource = new RARC_Resource
        (
            "Carbon",
            0,
            RARC_Resource.ResourceType.Carbon
        );

        organics_Resource = new RARC_Resource
        (
            "Organics",
            0,
            RARC_Resource.ResourceType.Organics
        );

        titanium_Resource = new RARC_Resource
        (
            "Hydrogen",
            0,
            RARC_Resource.ResourceType.Hydrogen
        );

        titanium_Resource = new RARC_Resource
        (
            "Nitrogen",
            0,
            RARC_Resource.ResourceType.Nitrogen
        );
    }

    public RARC_Resource GetResource(RARC_Resource.ResourceType type)
    {
        RARC_Resource resource = null;

        switch (type)
        {
            case RARC_Resource.ResourceType.ScrapMetal:
                resource = scrapMetal_Resource;
                break;

            case RARC_Resource.ResourceType.Fuel:
                resource = fuel_Resource;
                break;

            case RARC_Resource.ResourceType.Food:
                resource = food_Resource;
                break;


            case RARC_Resource.ResourceType.Titanium:
                resource = titanium_Resource;
                break;

            case RARC_Resource.ResourceType.Silicon:
                resource = silicon_Resource;
                break;

            case RARC_Resource.ResourceType.Carbon:
                resource = carbon_Resource;
                break;

            case RARC_Resource.ResourceType.Organics:
                resource = organics_Resource;
                break;

            case RARC_Resource.ResourceType.Hydrogen:
                resource = hydrogen_Resource;
                break;

            case RARC_Resource.ResourceType.Nitrogen:
                resource = nitrogen_Resource;
                break;

            default:
                resource = scrapMetal_Resource;
                break;
        }

        return resource;
    }

    /////////////////////////////////////////////////////////////////
}

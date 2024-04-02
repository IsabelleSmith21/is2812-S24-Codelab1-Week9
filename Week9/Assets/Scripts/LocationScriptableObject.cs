using System.Collections;
using System.Collections.Generic;
using UnityEditor; // Required for editor 
using UnityEngine;

// Create a new ScriptableObject for Location
[CreateAssetMenu
    (
    fileName = "New Location",
    menuName = "ScriptableObjectLocation",
    order = 0 )
]
public class LocationScriptableObject : ScriptableObject
{
    // variables to store location name and description
    public string locationName;
    public string locationDesc;

    // references to adjacent locations
    public LocationScriptableObject north;
    public LocationScriptableObject south;
    public LocationScriptableObject east;
    public LocationScriptableObject west;

    // print location details to console
    public void PrintLocation()
    {
        string printStr = "\nlocation Name:" + locationName +
                          "\nLocation desc" + locationDesc;
        Debug.Log(printStr);
    }

    // update current location in GameManager
    public void UpdateCurrentLocation(GameManager gm)
    {
        // update UI elements with location details
        gm.titleUI.text = locationName;
        gm.descriptionUI.text = locationDesc;

        // activate or deactivate buttons 
        if (north == null)
        {
            gm.buttonNorth.gameObject.SetActive(false);
        }
        else
        {
            gm.buttonNorth.gameObject.SetActive(true);
            // set the south reference of the adjacent location to this location
            north.south = this;
        }

        if (south == null)
        {
            gm.buttonSouth.gameObject.SetActive(false);
        }
        else
        {
            gm.buttonSouth.gameObject.SetActive(true);
            // set the north reference of the adjacent location to this location
            south.north = this;
        }

        if (east == null)
        {
            gm.buttonEast.gameObject.SetActive(false);
        }
        else
        {
            gm.buttonEast.gameObject.SetActive(true);
            // set the west reference of the adjacent location to this location
            east.west = this;
        }

        if (west == null)
        {
            gm.buttonWest.gameObject.SetActive(false);
        }
        else
        {
            gm.buttonWest.gameObject.SetActive(true);
            // set the east reference of the adjacent location to this location
            west.east = this;
        }
    }
}


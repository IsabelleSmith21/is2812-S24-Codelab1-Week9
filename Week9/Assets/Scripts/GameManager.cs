using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // References to UI elements that will have title and description 
    public TextMeshProUGUI titleUI;
    public TextMeshProUGUI descriptionUI;

    // Reference to the current location
    public LocationScriptableObject currentLocation;

    // References to buttons
    public Button buttonNorth;
    public Button buttonSouth;
    public Button buttonWest;
    public Button buttonEast;

    // References to the green tint image 
    public GameObject greenTint; // Reference to the green tint image GameObject

    // Movement variables for buttons
    public float moveDistance = 0.1f; // The distance buttons will move
    public float moveSpeed = 2f;      // The speed at which buttons will move

    // Original positions of directional buttons
    private Vector3 originalPositionNorth;
    private Vector3 originalPositionSouth;
    private Vector3 originalPositionWest;
    private Vector3 originalPositionEast;

    // Flag to indicate direction of movement
    private bool moveRight = true;

    // Start is called before the first frame update
    void Start()
    {
        // Print and update the initial location
        currentLocation.PrintLocation();
        currentLocation.UpdateCurrentLocation(this);

        // Store the original positions of the buttons
        originalPositionNorth = buttonNorth.transform.position;
        originalPositionSouth = buttonSouth.transform.position;
        originalPositionWest = buttonWest.transform.position;
        originalPositionEast = buttonEast.transform.position;

        // Hide the green tint at the start
        greenTint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the current location is the roof and start moving buttons if it is
        if (currentLocation.locationName == "Roof")
        {
            MoveButtons();
        }
        // Check if the current location is Floor 2 Right Window
        else if (currentLocation.locationName == "Floor 2 Right Window")
        {
            // Show the green tint
            greenTint.SetActive(true);
        }
    }

    // Move buttons back and forth
    void MoveButtons()
    {
        // Calculate movement direction
        Vector3 movementDirection = moveRight ? Vector3.right : Vector3.left;

        // Move buttons to the right or left based on the movement direction
        MoveButton(buttonNorth, originalPositionNorth + movementDirection * moveDistance);
        MoveButton(buttonSouth, originalPositionSouth + movementDirection * moveDistance);
        MoveButton(buttonWest, originalPositionWest + movementDirection * moveDistance);
        MoveButton(buttonEast, originalPositionEast + movementDirection * moveDistance);

        // Toggle movement direction
        if (Vector3.Distance(buttonNorth.transform.position, originalPositionNorth + movementDirection * moveDistance) >= moveDistance)
        {
            moveRight = !moveRight;
        }
    }

    // move buttons to target position
    void MoveButton(Button button, Vector3 targetPosition)
    {
        button.transform.position = Vector3.Lerp(button.transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    // handle button clicks and move to the corresponding direction
    public void MoveDir(string dirChar)
    {
        switch (dirChar)
        {
            case "N":
                currentLocation = currentLocation.north;
                break;
            case "S":
                currentLocation = currentLocation.south;
                break;
            case "W":
                currentLocation = currentLocation.west;
                break;
            case "E":
                currentLocation = currentLocation.east;
                break;
        }
        currentLocation.UpdateCurrentLocation(this);
    }
}

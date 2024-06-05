using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HintsToggle : MonoBehaviour
{
    public GameObject hintsPresent; //Hints and warning objects
    public GameObject warningsPresent;
    public GameObject hintsFuture;
    public GameObject warningsFuture;

    private PlayerInput playerControls; //Links actions to the input system, using the E and Q keys
    public InputAction off;
    public InputAction on;


    private void Awake()
    {
        playerControls = new PlayerInput();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        ToggleHintsAndWarnings(); //Calls the togglig function
    }

    private void OnEnable()
    {
        off = playerControls.Player.HintsOff; //Matches the actions to the bindings in the input system
        on = playerControls.Player.HintsOn;
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void ToggleHintsAndWarnings()
    {
        if (off.WasPressedThisFrame()) //Q key disables the hints and warnings
        {
            hintsPresent.SetActive(false);
            warningsPresent.SetActive(false);
            hintsFuture.SetActive(false);
            warningsFuture.SetActive(false);
        }

        if (on.WasPressedThisFrame()) //E key enables the hints and warnings
        {
            hintsPresent.SetActive(true);
            warningsPresent.SetActive(true);
            hintsFuture.SetActive(true);
            warningsFuture.SetActive(true);
        }
    }


}

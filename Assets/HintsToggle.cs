using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HintsToggle : MonoBehaviour
{
    public GameObject hintsPresent;
    public GameObject warningsPresent;
    public GameObject hintsFuture;
    public GameObject warningsFuture;

    private PlayerInput playerControls;
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
        ToggleHintsAndWarnings();
    }

    private void OnEnable()
    {
        off = playerControls.Player.HintsOff;
        on = playerControls.Player.HintsOn;
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void ToggleHintsAndWarnings()
    {
        if (off.WasPressedThisFrame())
        {
            hintsPresent.SetActive(false);
            warningsPresent.SetActive(false);
            hintsFuture.SetActive(false);
            warningsFuture.SetActive(false);
        }

        if (on.WasPressedThisFrame())
        {
            hintsPresent.SetActive(true);
            warningsPresent.SetActive(true);
            hintsFuture.SetActive(true);
            warningsFuture.SetActive(true);
        }
    }


}

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
    private InputAction off;

    private void Awake()
    {
        playerControls = new PlayerInput();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        off = playerControls.Player.HintsOff;
        playerControls.Enable();
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
    }
}

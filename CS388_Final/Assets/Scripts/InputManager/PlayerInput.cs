using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public class PlayerInputData
    {
        public Vector2 MoveDir = Vector2.zero;
        public Vector2 Aim = Vector2.zero;
        public bool Dodge = false;
        public bool Fire = false;
        public bool Reload = false;
        public bool SwitchGun = false;
    }

    public static PlayerInput Instance { get; private set; }
    public PlayerInputData InputData;
    public bool ForcedPC;
    public bool IsController => (ForcedPC != true) && controllerInput == true;

    private bool controllerInput;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InputData = new PlayerInputData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Switch)
        {
            UpdateSwitchInput();
        }
        else
        {
            UpdatePCInput();
        }
    }

    private void UpdateSwitchInput()
    {

    }

    private void UpdatePCInput()
    {
        var usedGamepad = Input.GetJoystickNames()[0] != string.Empty;
        controllerInput = usedGamepad;
        PlayerInputData newInput = new PlayerInputData();

        if (ForcedPC)
            usedGamepad = false;

        if (usedGamepad)
        {
            newInput.MoveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            newInput.Aim = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            newInput.Dodge = Input.GetButtonDown("Dodge");
            newInput.Fire = Input.GetButton("Fire");
            newInput.Reload = Input.GetButtonDown("Reload");
            newInput.SwitchGun = Input.GetButtonDown("SwitchGun");
        }
        else
        {
            newInput.MoveDir = new Vector2(Input.GetAxisRaw("Horizontal_PC"), Input.GetAxisRaw("Vertical_PC"));
            newInput.Aim = new Vector2(Input.GetAxis("Mouse X_PC"), Input.GetAxis("Mouse Y_PC"));
            newInput.Dodge = Input.GetButtonDown("Dodge_PC");
            newInput.Fire = Input.GetButton("Fire_PC");
            newInput.Reload = Input.GetButtonDown("Reload_PC");
            newInput.SwitchGun = Input.GetButtonDown("SwitchGun_PC");
        }

        InputData = newInput;
    }
}

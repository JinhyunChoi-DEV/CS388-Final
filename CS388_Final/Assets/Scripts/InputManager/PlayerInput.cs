using nn.hid;
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
    public bool IsController => ((ForcedPC != true) && controllerInput == true) || Application.platform == RuntimePlatform.Switch;

    private bool controllerInput;
    private NpadId npadId = NpadId.Handheld;
    private NpadState npadState = new NpadState();
    private bool lTriggerUsed = false;
    private bool rTriggerUsed = false;

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

    private void Start()
    {
        if(Application.platform == RuntimePlatform.Switch)
        {
            Npad.Initialize();
            Npad.SetSupportedIdType(new NpadId[] { NpadId.Handheld, NpadId.No1 });
            Npad.SetSupportedStyleSet(NpadStyle.FullKey | NpadStyle.Handheld | NpadStyle.JoyDual);
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
        PlayerInputData newInput = new PlayerInputData();
        var style = Npad.GetStyleSet(NpadId.Handheld);
        Npad.GetState(ref npadState, npadId, style);

        newInput.MoveDir = new Vector2(npadState.analogStickL.fx, npadState.analogStickL.fy);
        newInput.Aim = new Vector2(npadState.analogStickR.fx, npadState.analogStickR.fy);
        newInput.Fire = npadState.GetButton(NpadButton.L);
        newInput.Dodge = npadState.GetButtonDown(NpadButton.R);
        newInput.Reload = npadState.GetButtonDown(NpadButton.ZL);
        newInput.SwitchGun = npadState.GetButtonDown(NpadButton.ZR);

        InputData = newInput;
    }

    private void UpdatePCInput()
    {
        var usedGamepad = false;

        if (ForcedPC)
        {
            usedGamepad = false;
        }
        else
        {
            if (Input.GetJoystickNames().Length == 0)
                usedGamepad = false;
            else
                usedGamepad = Input.GetJoystickNames()[0] != string.Empty;
        }

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
            if (!lTriggerUsed && Input.GetAxis("Reload") >= 0.9f)
            {
                newInput.Reload = true;
                lTriggerUsed = true;
            }
            else if (lTriggerUsed && Input.GetAxis("Reload") == 0f)
            {
                newInput.Reload = false;
                lTriggerUsed = false;
            }

            if (!rTriggerUsed && Input.GetAxis("SwitchGun") >= 0.9f)
            {
                newInput.SwitchGun = true;
                rTriggerUsed = true;
            }
            else if (rTriggerUsed && Input.GetAxis("SwitchGun") == 0f)
            {
                newInput.SwitchGun = false;
                rTriggerUsed = false;
            }
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

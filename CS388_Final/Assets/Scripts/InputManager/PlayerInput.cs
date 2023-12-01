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
        if (Application.platform != RuntimePlatform.Switch)
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
        PlayerInputData newInput = new PlayerInputData();

        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        newInput.MoveDir = new Vector2(vertical, horizontal);

    }
}

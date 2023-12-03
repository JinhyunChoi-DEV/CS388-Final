using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private PlayerState state;
    public static bool IsAlive { get; private set; }
    public float CurrentHP { get; private set; }
    public float MaxHP;
    public float DodgeSpeed;
    public float WalkSpeed;

    private bool ignoreDamageCollision;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHP = MaxHP;
        IsAlive = true;

        ignoreDamageCollision = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            ApplyDamage();


        if (CurrentHP <= 0)
        {
            IsAlive = false;
            //TODO: Dead
        }

        ignoreDamageCollision = state.State == State.Dodge;
    }

    public void ApplyDamage()
    {
        if (CurrentHP <= 0)
            return;

        if (ignoreDamageCollision)
            return;

        CurrentHP -= 0.5f;
    }
}

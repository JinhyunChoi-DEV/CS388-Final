using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private PlayerState state;
    public float MaxHP;
    public float DodgeSpeed;
    public float WalkSpeed;

    private float currentHP;
    private bool ignoreDamageCollision;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = MaxHP;

        ignoreDamageCollision = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentHP <= 0)
        {
            //TODO: Dead
        }

        ignoreDamageCollision = state.State == State.Dodge;
    }

    public void ApplyDamage(float damage)
    {
        if (ignoreDamageCollision)
            return;

        currentHP -= damage;
    }
}

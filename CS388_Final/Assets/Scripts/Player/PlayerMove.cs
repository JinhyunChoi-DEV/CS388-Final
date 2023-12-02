using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private PlayerState state;
    [SerializeField] private PlayerAim aim;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private PlayerData data;

    void Start()
    { }

    void Update()
    {
        if (state.State == State.Dodge)
            Dodge();
        else if (state.State == State.Move)
            Walk();
        else
            Idle();
    }

    private void Dodge()
    {
        var aimAngle = aim.Angle;
        var dir = new Vector2(Mathf.Sin(aimAngle * Mathf.Deg2Rad), Mathf.Cos(aimAngle * Mathf.Deg2Rad)).normalized;

        rigidbody.velocity = dir * data.DodgeSpeed;
    }

    private void Walk()
    {
        var moveDir = PlayerInput.Instance.InputData.MoveDir;

        rigidbody.velocity = moveDir * data.WalkSpeed;
    }

    private void Idle()
    {
        rigidbody.velocity = Vector3.zero;
    }
}

using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private PlayerState state;
    [SerializeField] private PlayerAim aim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerData data;

    void Start()
    { }

    void Update()
    {
        if (!PlayerData.IsAlive)
        {
            rb.velocity = new Vector3(0, 0, 0);
            return;
        }

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

        rb.velocity = dir * data.DodgeSpeed;
    }

    private void Walk()
    {
        var moveDir = PlayerInput.Instance.InputData.MoveDir;

        rb.velocity = moveDir * data.WalkSpeed;
    }

    private void Idle()
    {
        rb.velocity = Vector3.zero;
    }
}

using UnityEngine;

public enum State
{
    Idle,
    Move,
    Dodge,
    DoingDodge
}

public class PlayerState : MonoBehaviour
{
    [SerializeField] private PlayerMove move;
    public State State { get; private set; }

    private bool canDodge;
    private float dodgeTime = 0.35f;
    private float timer = 0.0f;

    void Start()
    {
        State = State.Idle;
        canDodge = true;
    }

    void Update()
    {
        var input = PlayerInput.Instance.InputData;

        //if (State == State.Dodge || State == State.DoingDodge)
        //    UpdateDodge();
        //else 
        if (input.Dodge && canDodge)
            State = State.Dodge;
        else
            State = IsIdle(input.MoveDir) ? State.Idle : State.Move;
    }

    private void UpdateDodge()
    {
        State = State.DoingDodge;

        canDodge = false;
        timer += Time.deltaTime;
        if (timer >= dodgeTime)
        {
            canDodge = true;
            timer = 0.0f;
        }
    }

    private bool IsIdle(Vector2 moveDir)
    {
        return moveDir == Vector2.zero;
    }
}

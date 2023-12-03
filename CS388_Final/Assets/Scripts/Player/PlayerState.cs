using UnityEngine;

public enum State
{
    Idle,
    Move,
    Dodge
}

public class PlayerState : MonoBehaviour
{
    [SerializeField] private PlayerMove move;
    public State State { get; private set; }

    private bool canDodge;
    private float dodgeTime = 0.4f;
    private float timer = 0.0f;

    void Start()
    {
        State = State.Idle;
        canDodge = true;
    }

    void Update()
    {
        if (!PlayerData.IsAlive)
            return;

        var input = PlayerInput.Instance.InputData;

        if (State == State.Dodge)
        {
            UpdateDodge(input.MoveDir);
            return;
        }

        if (input.Dodge && canDodge)
            State = State.Dodge;
        else
            State = IsIdle(input.MoveDir) ? State.Idle : State.Move;
    }

    private void UpdateDodge(Vector2 movedir)
    {
        canDodge = false;
        timer += Time.deltaTime;

        if (timer >= dodgeTime)
        {
            canDodge = true;
            timer = 0.0f;

            if (IsIdle(movedir))
                State = State.Idle;
            else
                State = State.Move;
        }
    }

    private bool IsIdle(Vector2 moveDir)
    {
        return moveDir == Vector2.zero;
    }
}

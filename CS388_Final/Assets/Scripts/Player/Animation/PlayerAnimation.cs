using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] PlayerState playerState;
    [SerializeField] private PlayerAim aim;
    [SerializeField] private Transform renderObject;

    private int idleId;
    private int moveId;
    private int dodgeId;
    private int angle;

    void Start()
    {
        idleId = Animator.StringToHash("Idle");
        moveId = Animator.StringToHash("Move");
        dodgeId = Animator.StringToHash("Dodge");
        angle = Animator.StringToHash("Angle");
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, aim.Angle < 0 ? 180 : 0, 0));

        animator.SetBool(dodgeId, playerState.State == State.Dodge);
        animator.SetBool(idleId, playerState.State == State.Idle);
        animator.SetBool(moveId, playerState.State == State.Move);
        animator.SetFloat(angle, Mathf.Abs(aim.Angle));
    }
}
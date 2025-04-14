using Fusion;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public int speedRotate = 10;
    private Vector3 move;
    public float speed = 5f;

    private RoomManager roomManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        roomManager = FindAnyObjectByType<RoomManager>();
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasInputAuthority || roomManager == null)
            return;

        // Di chuyển khi GameStarted = true
        if (roomManager.GameStarted)
        {
            Move();
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        move = new Vector3(horizontal, 0, vertical);
        PlayerMoving();
    }

    private void PlayerMoving()
    {
        //if (move.sqrMagnitude >= 0.001f)
        //{
        //    Quaternion targetRotation = Quaternion.LookRotation(move);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotate * Runner.DeltaTime);
        //}
        controller.Move(move * speed * Runner.DeltaTime);
    }
}

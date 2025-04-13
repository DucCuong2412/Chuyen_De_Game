using Fusion;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public int speedRotate = 10;
    private Vector3 move;
    public RoomManager roomManager;

      public  bool isStartGame = false;


    public float speed = 5f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        roomManager = FindAnyObjectByType<RoomManager>();

    }
    private void Update()
    {
        Debug.Log(roomManager.isStartGame);
        if (roomManager.isStartGame)
        {
            isStartGame = true;
        }
        else
        {
            isStartGame = false;
        }
        
    }
    public override void FixedUpdateNetwork()
    {
        
        if (!Object.HasInputAuthority) return;
        if (isStartGame )
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
        if (move.sqrMagnitude >= 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Runner.DeltaTime);
        }
        controller.Move(move * speed * Runner.DeltaTime);
    }




}


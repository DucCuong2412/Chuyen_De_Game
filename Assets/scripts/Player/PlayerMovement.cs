using Fusion;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : NetworkBehaviour
{
    public CharacterController controller;
    private Animator animator;
    public int speedRotate = 10;
    private Vector3 move;


    public float speed = 5f;

    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasInputAuthority) return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
         move = new Vector3(horizontal, 0, vertical);
        PlayerMoving();
        //gravity
        controller.Move(Vector3.down * 5 * Runner.DeltaTime);
    }

    private void PlayerMoving()
    {
        if (move.sqrMagnitude >= 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Runner.DeltaTime);
        }
        controller.Move(move * speed * Runner.DeltaTime);
       // AnimationSpeed = controller.velocity.magnitude;
    }




}


using Fusion;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public CharacterController controller;
    public Animator animator;
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
        //controller.Move(Vector3.down * 5 * Runner.DeltaTime);
        //if (horizontal != 0 || vertical != 0)
        //{
        //    animator.SetTrigger("Run");
        //}
        //else
        //{
        //    animator.SetTrigger("Ide");

        //}
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


using Fusion;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public CharacterController controller;
    private Animator animator;
    public int speedRotate = 10;


    public float speed = 5f;

    private void Start()
    {
        animator = GetComponent<Animator>();

    }
    public override void FixedUpdateNetwork()
    {
        //base.FixedUpdateNetwork();
        if (!Object.HasInputAuthority) return;
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(horizontal, 0, vertical);
        controller.Move(move * speed * Runner.DeltaTime);
        if (horizontal != 0 || vertical != 0)
        {
            animator.SetTrigger("Run");




        }
        else
        {
            //   animator.SetBool("Run",false);
            animator.SetTrigger("Ide");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("Atk");
        }
        if (horizontal != 0 || vertical != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(controller.transform.rotation, targetRotation, speedRotate * Runner.DeltaTime);
        }

    }



}


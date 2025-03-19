using Fusion;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public CharacterController controller;
    public float speed = 5f;

    public override void FixedUpdateNetwork()
    {
        //base.FixedUpdateNetwork();
        if (!Object.HasInputAuthority) return;
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(horizontal, 0, vertical);
        controller.Move(move * speed * Runner.DeltaTime);
    }

}
public class PlayerSetup : NetworkBehaviour
{
    public void SetupCamera()
    {
        if (Object.HasInputAuthority)
        {
            //CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
            CameraFlow cameraFollow = FindFirstObjectByType<CameraFlow>();
            if (cameraFollow != null)
            {
                cameraFollow.AsighCamera(transform);
            }
        }
    }
}
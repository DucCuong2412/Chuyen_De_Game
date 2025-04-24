using Fusion;
using UnityEngine;

public class PlayerCollision : NetworkBehaviour
{
    public AudioClip collisionSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Gửi RPC đến tất cả client để phát âm thanh
            Debug.Log("player touch other player");
            RPC_PlayCollisionSound();
        }
    }


    [Rpc(RpcSources.All, RpcTargets.All)]
    void RPC_PlayCollisionSound()
    {
        AudioSource.PlayClipAtPoint(collisionSound, transform.position);
    }
}

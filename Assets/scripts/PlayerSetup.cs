using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
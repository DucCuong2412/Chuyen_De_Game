using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    private void Start()
    {
    }
    public void SetupCamera()
    {
        if (Object.HasInputAuthority)
        {

            CameraFlow cameraFollow = FindFirstObjectByType<CameraFlow>();
            if (cameraFollow != null)
            {
                cameraFollow.AsighCamera(transform);
            }
        }
    }
    public void SetupCamera2()
    {
        if (Object.HasInputAuthority)
        {

            CameraFlow cameraFollow = FindFirstObjectByType<CameraFlow>();
            if (cameraFollow != null)
            {
                cameraFollow.AsighCamera2(transform);
            }
        }
    }
    public void SetupCamera3()
    {
        if (Object.HasInputAuthority)
        {

            CameraFlow cameraFollow = FindFirstObjectByType<CameraFlow>();
            if (cameraFollow != null)
            {
                cameraFollow.AsighCamera3(transform);
            }
        }
    }



}
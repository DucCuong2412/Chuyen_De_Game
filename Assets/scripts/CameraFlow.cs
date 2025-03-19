using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    public void AsighCamera(Transform Playertranfrorm)
    {
        virtualCamera.Follow = Playertranfrorm;
        virtualCamera.LookAt = Playertranfrorm;
    }
}

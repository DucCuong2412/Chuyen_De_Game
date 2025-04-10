using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    public CinemachineFreeLook virtualCamera;
    public CinemachineVirtualCamera virtualCamera2, virtualCamera3;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            virtualCamera.Priority = 10;
            virtualCamera2.Priority = 0;
            virtualCamera3.Priority = 0;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            virtualCamera.Priority = 0;
            virtualCamera2.Priority = 10;
            virtualCamera3.Priority = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            virtualCamera.Priority = 0;
            virtualCamera2.Priority = 0;
            virtualCamera3.Priority = 10;
        }
    }
    public void AsighCamera(Transform Playertranfrorm)
    {

        virtualCamera.Priority = 10;
        virtualCamera2.Priority = 0;
        virtualCamera3.Priority = 0;

        virtualCamera.Follow = Playertranfrorm;
        virtualCamera.LookAt = Playertranfrorm;
        Debug.Log("1111111111111111");

    }
    public void AsighCamera2(Transform Playertranfrorm)
    {
        virtualCamera.Priority = 0;
        virtualCamera2.Priority = 10;
        virtualCamera3.Priority = 0;

        // virtualCamera2.Follow =  Playertranfrorm;
        virtualCamera2.LookAt = Playertranfrorm;
        Debug.Log("2222222222");
    }
    public void AsighCamera3(Transform Playertranfrorm)
    {
        virtualCamera.Priority = 0;
        virtualCamera2.Priority = 0;
        virtualCamera3.Priority = 10;

        // virtualCamera3.Follow = Playertranfrorm;
        virtualCamera3.LookAt = Playertranfrorm;
        Debug.Log("3333333333333333");
    }


}


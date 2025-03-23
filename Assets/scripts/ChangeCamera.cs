using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        var _player = GetComponent<PlayerSetup>();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _player.SetupCamera();
            Debug.Log("1111111111111111");


        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _player.SetupCamera2();
            Debug.Log("222222222222222");

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _player.SetupCamera3();
            Debug.Log("333333333333333333333");

        }
    }
}

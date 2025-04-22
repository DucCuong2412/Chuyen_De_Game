using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private int count = 1;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            count++;
        }
        if (count == 4)
        {
            count = 1;
        }

        var _player = GetComponent<PlayerSetup>();
        if (count == 1)
        {
            _player.SetupCamera();
            Debug.Log("1111111111111111");


        }
        else if (count == 2)
        {
            _player.SetupCamera2();
            Debug.Log("222222222222222");

        }
        else if (count == 3)
        {
            _player.SetupCamera3();
            Debug.Log("333333333333333333333");

        }
    }
}

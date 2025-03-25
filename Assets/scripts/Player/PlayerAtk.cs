using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtk : NetworkBehaviour
{
    public GameObject weapon;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            weapon.GetComponent<BoxCollider>().enabled = true;
        }
        else if (Input.GetMouseButton(1))
        {
            weapon.GetComponent<BoxCollider>().enabled = false;
        }
    }
}

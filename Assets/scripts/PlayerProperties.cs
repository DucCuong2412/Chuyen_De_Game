using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProperties : NetworkBehaviour
{
    [Networked,OnChangedRender(nameof(OnHealthChanged))]

    private int health {  get; set; }
    public Slider Slider;
    private void OnHealthChanged()
    {
        Slider.value = health;
        Debug.Log($" heath changed to {health}");

    }
    private void Start()
    {
        health = 100;
        Slider.value = health;
    }
    void Update()
    {
        if (HasInputAuthority)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                health -= 10;
            }
        }

    }

}

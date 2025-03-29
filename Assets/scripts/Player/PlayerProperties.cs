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
        if (health <= 0)
        {
            Debug.Log("player is dead");
        }
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
    private void OnTriggerEnter(Collider other)
    {
        if (Object.HasInputAuthority)
        {
            if (other.gameObject.CompareTag("at"))
            {
                // var targetPlayer = other.gameObject.GetComponent<NetworkObject>().InputAuthority;
                //TakeDamege(10);
                health -= 10;

                Debug.Log("heal-=10");
            }
        }

    }
    public void TakeDamege(int Damage)
    {
        if (HasInputAuthority)
        {
            health = Mathf.Max(0, health - Damage);
        }
    }
}

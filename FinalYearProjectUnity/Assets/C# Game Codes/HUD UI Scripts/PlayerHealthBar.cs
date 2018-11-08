using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {

    public Image healthBar;

    // starting with 100 health
    public float maxHealth = 100;
    private float currentHealth;


    // Use this for initialization
    void Start ()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayHealthBar();
        PressKeyToDamage();
        RefillHealth();
    }

    void DisplayHealthBar()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    void RefillHealth()
    {
        if (currentHealth >= maxHealth)
        {
            CancelInvoke();
        }
        else
        {
            Invoke("WaitToRefillHealthBar", 5);
        }
    }

    void PressKeyToDamage()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            TakeDamage(5);
        }
    }

    void RegenerateHealth(float rate)
    {
        currentHealth += rate;
    }

    void WaitToRefillHealthBar()
    {
        RegenerateHealth(0.1f);
        Debug.Log("Wait 5 seconds to refill health");
    }

    void TakeDamage(int amount)
    {
        currentHealth -=  amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Die");
    }


    private void OnCollisionEnter(Collision collision)
    {
        Damage1(collision);
        Damage2(collision);
        Damage3(collision);
        Damage4(collision);
        Damage5(collision);
    }

    void Damage1(Collision collision)
    {
        // fixed collisions!!
        if (collision.gameObject.name == "Damage1")
        {
            TakeDamage(2);
            CancelInvoke();
            Debug.Log("Damaging");
        }
        else
        {
            RefillHealth();
        }
    }

    void Damage2(Collision collision)
    {
        if (collision.gameObject.name == "Damage2")
        {
            TakeDamage(5);
            Debug.Log("Damaging");
        }
    }

    void Damage3(Collision collision)
    {
        if (collision.gameObject.name == "Damage3")
        {
            TakeDamage(3);
            Debug.Log("Damaging");
        }
    }

    void Damage4(Collision collision)
    {
        if (collision.gameObject.name == "Damage4")
        {
            TakeDamage(7);
            Debug.Log("Damaging");
        }
    }

    void Damage5(Collision collision)
    {
        if (collision.gameObject.name == "Damage5")
        {
            TakeDamage(4);
            Debug.Log("Damaging");
        }
    }
}

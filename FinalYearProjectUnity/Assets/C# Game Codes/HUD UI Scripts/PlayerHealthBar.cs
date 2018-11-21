﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour, IDamageable {

    public float maxHealth = 100; // starting with 100 health
    public float currentHealth;
    public Image healthBar;

    public float GetDamage
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
        }
    }

    public void LightDamage(float lightDamAmount)
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start ()
    {
        currentHealth = maxHealth;
    }


    // Update is called once per frame
    void Update()
    {
        DisplayHealthBar();
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

        if (currentHealth <= 0)
        {
            currentHealth = 0;
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


    void DamageCollisionEasy(Collision collision)
    {
        GameObject hitObject = collision.gameObject;
        IDamageable easyDamageObj = hitObject.GetComponent<IDamageable>();

        if (hitObject.GetComponent<IDamageable>() != null)
        {
            easyDamageObj.LightDamage(10);
            CancelInvoke();
        }
        else
        {
            RefillHealth();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        DamageCollisionEasy(collision);
    }
}

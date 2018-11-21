using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightDamager : MonoBehaviour, IDamageable {

    PlayerHealthBar pb;
    public GameObject player;

    public float GetDamage
    {
        get
        {
            return pb.currentHealth;
        }

        set
        {
            pb.currentHealth = value;
        }
    }

    public void LightDamage(float lightDamAmount)
    {
        pb.currentHealth -= lightDamAmount;
        Debug.Log("I have taken: " + lightDamAmount);
    }

    void Start()
    {
        pb = player.GetComponent<PlayerHealthBar>();
        pb.currentHealth = pb.maxHealth;
    }

    void Update()
    {
        pb = player.GetComponent<PlayerHealthBar>();
        pb.healthBar.fillAmount = pb.currentHealth / pb.maxHealth;
    }
}

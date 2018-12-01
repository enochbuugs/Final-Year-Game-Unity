using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageGiver : MonoBehaviour, IDamageable {

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

    public void DamageTaken(float amount)
    {
        pb.currentHealth -= amount;
        Debug.Log("I have taken: " + amount);
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

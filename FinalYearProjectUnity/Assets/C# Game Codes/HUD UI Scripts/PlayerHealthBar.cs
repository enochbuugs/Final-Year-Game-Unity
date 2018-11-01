using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {

    public Image healthBar;

    // starting with 100 health
    public float maxHealth = 100;
    private float currentHealth;


    float timer = 0f;
    public float regenRate = 1;
    float healthTimer = 1;
    bool isRegeneratingHealth;
    IEnumerator currentCoroutine;


    // Use this for initialization
    void Start () {

        currentHealth = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;

        if ((currentHealth != maxHealth) && (!isRegeneratingHealth))
        {
            currentCoroutine = (WaitToRegenHealth());
            StartCoroutine(currentCoroutine);
        }

        if ((currentHealth == maxHealth) && (isRegeneratingHealth))
        {
            StopAllCoroutines();
        }

        PressKeyToDamage();
    }

    private IEnumerator WaitToRegenHealth()
    {

        isRegeneratingHealth = true;

        yield return new WaitForSeconds(20);

        while (currentHealth < maxHealth)
        {
            RefillHealth();

            yield return 0;
        }

        yield return new WaitForSeconds(20);

        isRegeneratingHealth = false;

        //StopCoroutine(currentCoroutine);
        //currentCoroutine = WaitToRegenHealth();
        //StartCoroutine(currentCoroutine);
    }


    void RefillHealth()
    {
        timer += Time.deltaTime;

        if (currentHealth < maxHealth)
        {
            if (timer >= healthTimer)
            {
                RegenerateHealth(regenRate);
            }
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
        timer = 0f;
        currentHealth += rate;
    }

    void TakeDamage(int amount)
    {
        isRegeneratingHealth = false;
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

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.name == "Damage")
    //    {
    //        TakeDamage(10);
    //        Debug.Log("Damaged");
    //    }
    //}

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
        if (collision.gameObject.name == "Damage1")
        {
            TakeDamage(2);
            Debug.Log("Damaging");
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


    //void AddDamage(float damageAmount)
    //{
    //    _damage += damageAmount;

    //    damageBar.fillAmount = _damage / noDamage;

    //    if (_damage >= 100)
    //    {
    //        Die();
    //    }
    //}


}

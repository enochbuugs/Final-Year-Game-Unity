using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour, IDamageable, IScoreDamager {

    PlayerScoreManager psm;
    PlayerCarController pc;
    public float maxHealth = 100; // starting with 100 health
    public float currentHealth;
    public Image healthBar;
    public bool hasInvicibility = false;
    public bool canTakeDamage = false;

    public float timer;

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

    public float getScoreReduction
    {

        get
        {
            return psm.currentScore; 
        }

        set
        {
            psm.currentScore = value;
        }
    }

    public void ScoreReduction(float amount)
    {
        throw new System.NotImplementedException();
    }

    public void DamageTaken(float amount)
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start()
    {
        SetHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayHealthBar();
        //RefillHealth();
    }

    void SetHealthBar()
    {
        currentHealth = maxHealth;
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

    IEnumerator UnpauseScoreEasy()
    {
        psm = GetComponent<PlayerScoreManager>();
        psm.isScorePaused = true;
        psm = psm.GetComponent<PlayerScoreManager>();
        psm.pausedScore = psm.currentScore;
        yield return new WaitForSeconds(psm.easyPenaltyTimer);
        psm.currentScore = psm.pausedScore;
        psm.isScorePaused = false;
    }

    IEnumerator UnpauseScoreMeduim()
    {
        psm = GetComponent<PlayerScoreManager>();
        psm.isScorePaused = true;
        psm = psm.GetComponent<PlayerScoreManager>();
        psm.pausedScore = psm.currentScore;
        yield return new WaitForSeconds(psm.meduimPenaltyTimer);
        psm.currentScore = psm.pausedScore;
        psm.isScorePaused = false;
    }

    IEnumerator UnpauseScoreHard()
    {
        psm = GetComponent<PlayerScoreManager>();
        psm.isScorePaused = true;
        psm = psm.GetComponent<PlayerScoreManager>();
        psm.pausedScore = psm.currentScore;
        yield return new WaitForSeconds(psm.hardPenaltyTimer);
        psm.currentScore = psm.pausedScore;
        psm.isScorePaused = false;
    }



    #region ("Damage Level Collision Methods")
    void DamageCollisionEasy(Collision collision)
    {
        GameObject hitObject = collision.gameObject;
        IDamageable easyDamageObj = hitObject.GetComponent<IDamageable>();
        IScoreDamager easyScoreDamageObj = hitObject.GetComponent<IScoreDamager>();

        if (hitObject.GetComponent<IDamageable>() != null && (collision.collider.tag == "LightDamager") /*&& pc.isCarMovingForward == true*/)
        {
            canTakeDamage = true;
            easyDamageObj.DamageTaken(5f);
            easyScoreDamageObj.ScoreReduction(10f);
            StartCoroutine(UnpauseScoreEasy());
        }
    }

    void DamageCollisionMeduim(Collision collision)
    {
        GameObject hitObject = collision.gameObject;
        IDamageable meduimDamageObj = hitObject.GetComponent<IDamageable>();
        IScoreDamager meduimScoreDamageObj = hitObject.GetComponent<IScoreDamager>();

        if (hitObject.GetComponent<IDamageable>() != null && (collision.collider.tag == "MeduimDamager"))
        {
            meduimDamageObj.DamageTaken(10f);
            meduimScoreDamageObj.ScoreReduction(20f);
            StartCoroutine(UnpauseScoreMeduim());
        }
    }

    void DamageCollisionHard(Collision collision)
    {
        GameObject hitObject = collision.gameObject;
        IDamageable hardDamageObj = hitObject.GetComponent<IDamageable>();
        IScoreDamager hardScoreDamageObj = hitObject.GetComponent<IScoreDamager>();

        if (hitObject.GetComponent<IDamageable>() != null && (collision.collider.tag == "HardDamager"))
        {
            hardDamageObj.DamageTaken(15f);
            hardScoreDamageObj.ScoreReduction(30f);
            StartCoroutine(UnpauseScoreHard());
        }
    }

    void NoDamageCollisionEasy(Collision collision)
    {
        GameObject hitObject = collision.gameObject;
        IDamageable easyDamageObj = hitObject.GetComponent<IDamageable>();
        IScoreDamager easyScoreDamageObj = hitObject.GetComponent<IScoreDamager>();

        if (hitObject.GetComponent<IDamageable>() != null && (collision.collider.tag == "LightDamager"))
        {
            easyDamageObj.DamageTaken(0);
            easyScoreDamageObj.ScoreReduction(0);
            //CancelInvoke();
        }
        else
        {
            //RefillHealth();
        }
    }

    void NoDamageCollisionMeduim(Collision collision)
    {
        GameObject hitObject = collision.gameObject;
        IDamageable meduimDamageObj = hitObject.GetComponent<IDamageable>();
        IScoreDamager meduimScoreDamageObj = hitObject.GetComponent<IScoreDamager>();

        if (hitObject.GetComponent<IDamageable>() != null && (collision.collider.tag == "MeduimDamager"))
        {
            meduimDamageObj.DamageTaken(0);
            meduimScoreDamageObj.ScoreReduction(0);
            //CancelInvoke();
        }
        else
        {
            //RefillHealth();
        }
    }

    void NoDamageCollisionHard(Collision collision)
    {
        GameObject hitObject = collision.gameObject;
        IDamageable hardDamageObj = hitObject.GetComponent<IDamageable>();
        IScoreDamager hardScoreDamageObj = hitObject.GetComponent<IScoreDamager>();

        if (hitObject.GetComponent<IDamageable>() != null && (collision.collider.tag == "HardDamager"))
        {
            hardDamageObj.DamageTaken(0);
            hardScoreDamageObj.ScoreReduction(0);
            //CancelInvoke();
        }
        else
        {
            //RefillHealth();
        }
    }
    #endregion 

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasInvicibility)
        {
            hasInvicibility = false;
            //canTakeDamage = true;
            DamageCollisionEasy(collision);
            DamageCollisionMeduim(collision);
            DamageCollisionHard(collision);
        }

        if (hasInvicibility)
        {
            hasInvicibility = true;
            //canTakeDamage = false;
            NoDamageCollisionEasy(collision);
            NoDamageCollisionMeduim(collision);
            NoDamageCollisionHard(collision);
        }
    }


    public IEnumerator Shield()
    {
        hasInvicibility = true;
        //canTakeDamage = false;
        yield return new WaitForSeconds(5f);
        hasInvicibility = false;
        //canTakeDamage = true;
    }

}

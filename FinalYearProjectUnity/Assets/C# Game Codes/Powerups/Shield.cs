using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    public GameObject player;
    PlayerHealthBar phb;

	// Use this for initialization
	void Start () {

        phb = player.GetComponent<PlayerHealthBar>();
	}

    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject == player))
        {
            phb = other.GetComponentInParent<PlayerHealthBar>();
            phb.currentHealth = phb.maxHealth;
            StartCoroutine(ShieldTimer(other));
            Destroy(this.gameObject);
        }
    }

    IEnumerator ShieldTimer (Collider Player)
    {
        yield return new WaitForSeconds(10);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1;

    public virtual void Hit() {
        health--;
        if (health <= 0) {
            EffectManager.Instance.ApplyEffect(transform.position + Vector3.up * 1.0f, EffectManager.Instance.killEffectPrefab);
            Destroy(gameObject);
        } else {
            EffectManager.Instance.ApplyEffect(transform.position + Vector3.up * 1.0f, EffectManager.Instance.hitEffectPrefab);
        }
    }

    public void OnTriggerEnter(Collider otherCollider) {
        if (otherCollider.GetComponent<Arrow>() != null) {
            Hit();
            Destroy(otherCollider.gameObject);
        }
    }

    public void OnTriggerStay(Collider otherCollider) {
        if (otherCollider.GetComponent<Sword>() != null) {
            if (otherCollider.GetComponent<Sword>().JustAttacked) {
                // Debug.Log("Enemy hit by the sword!");
                Hit();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    protected int Health = 5;
    protected int Damage = 1;
    protected Rigidbody2D RB;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();    
    }

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
            Die();
    }

    public virtual void Die()
    {
        transform.gameObject.SetActive(false);
    }

    public virtual void Move(Vector2 velocity)
    {
        RB.velocity = velocity;
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerController>(out PlayerController player))
            player.TakeDamage(Damage);
    }
}

using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(EnemyView))]
public class Enemy : MonoBehaviour
{
    protected int Health = 5;
    protected int Damage = 1;
    protected int Reward = 3;

    protected Rigidbody2D RB;
    protected EnemyView View;
    protected Collider2D Collider;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        View = GetComponent<EnemyView>();
        Collider = GetComponent<Collider2D>();
    }

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
            Die();
    }

    public virtual void Die()
    {
        enabled = false;
        Collider.enabled = false;
        View.Die();
        GlobalEventManager.SendOnEnemyKilled(Reward);
    }

    public virtual void Move(Vector2 velocity)
    {
        RB.velocity = velocity;
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.TakeDamage(Damage);
            Die();
        }
    }
}

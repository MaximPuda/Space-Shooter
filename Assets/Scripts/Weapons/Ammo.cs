using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Ammo : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private int _damage = 5;
    [SerializeField] private float _timeBetweenShots = 0.5f;

    public float TimeBetweenShots => _timeBetweenShots;
    public void SetDirection(Vector2 direction) => _direction = direction;

    private Vector2 _direction;
    
    private void Update()
    {
        if(_direction != null)
        {
            transform.Translate(_direction * _speed * Time.deltaTime);
        }

        if (transform.position.y >= 15)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Enemy>(out Enemy target))
        {
            Destroy(this.gameObject);
            target.TakeDamage(_damage);
        }
    }
}

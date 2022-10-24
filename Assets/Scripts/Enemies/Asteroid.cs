using UnityEngine;

public class Asteroid : Enemy
{
    [SerializeField] private int _health = 5;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _speed = 3;
    [SerializeField] private int _reward = 3;

    private void Start()
    {
        Health = _health;
        Damage = _damage;
        Reward = _reward;
    }
}

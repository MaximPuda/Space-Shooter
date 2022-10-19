using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Enemy
{
    [SerializeField] private int _health = 5;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _speed = 3;

    private void Start()
    {
        Health = _health;
        Damage = _damage;
        Move(Vector2.down * _speed);
    }
}

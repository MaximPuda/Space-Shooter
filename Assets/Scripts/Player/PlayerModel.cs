using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private int _health = 3;
    [SerializeField] private float _speed = 5;
    [SerializeField] private Weapon _currentWeapon;
    public int Health => _health;
    public float Speed => _speed;
    public Weapon CurrentWeapon => _currentWeapon;

    public void DecreaseHealth(int deacreaser) => _health -= deacreaser;

    public void IncreaseHealth(int increaser) => _health += increaser;
}

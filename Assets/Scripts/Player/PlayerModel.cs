using UnityEngine;

public class PlayerModel : MonoBehaviour
{ 
    [SerializeField] private int _health = 3;
    [SerializeField] private Weapon _currentWeapon;

    public int Health => _health;
    public Weapon CurrentWeapon => _currentWeapon;
    public int Points { get; private set; }

    public void DecreaseHealth(int deacreaser) => _health -= deacreaser;
    public void IncreaseHealth(int increaser) => _health += increaser;

    public void AddPoints(int points)
    {
        if (points > 0)
        {
            Points += points;
            GlobalEventManager.SendOnPlayerGetPoints(Points);
        }
        else Debug.LogError("Points must be greater then zero!");
    }
}

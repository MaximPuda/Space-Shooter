using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Ammo _ammo;

    private bool _canShoot;
    private float _currentTime = 0;
    
    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime < _ammo.TimeBetweenShots)
            _canShoot = false;
        else
        {
            _canShoot = true;
            _currentTime = 0;
        }
    }

    public void Shoot(Vector2 direction)
    {
        if(_canShoot)
            Instantiate(_ammo, transform.position, Quaternion.identity).SetDirection(direction);
    }

    public void SetAmmo(Ammo ammo)
    {
        if(ammo != null)
            _ammo = ammo;
    }
}

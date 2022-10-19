using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerModel))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick _joy;

    private PlayerModel _model;
    private Rigidbody2D _rb;

    private Enemy _target;
    private Transform _shootPoint;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _model = GetComponent<PlayerModel>();
    }

    private void Start()
    {
        _shootPoint = _model.CurrentWeapon.transform;
    }

    private void OnEnable()
    {
        _joy.OnJoystickMove += Move;
    }

    private void OnDisable()
    {
        _joy.OnJoystickMove -= Move;
    }

    private void Update()
    {
        _target = TryGetTarget();
        if (_target != null)
            Shoot();
    }

    private Enemy TryGetTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(_shootPoint.position, Vector2.up);
        if (hit.collider != null)
            if (hit.collider.TryGetComponent<Enemy>(out Enemy target))
                return target;
        
        return null;
    }

    public void Move(Vector2 direction)
    {
        _rb.velocity = direction * _model.Speed;
    }

    public void TakeDamage(int damage)
    {
        if(_model.Health > 0)
        {
            _model.DecreaseHealth(damage);
            Debug.Log("Player take damage " + damage);
        }

        if (_model.Health <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log("Player is dead");
        _rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void Shoot()
    {
        _model.CurrentWeapon.Shoot(Vector2.up);
    }
}

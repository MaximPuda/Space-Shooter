using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerModel))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private float _moveSpeed;

    private PlayerModel _model;
    private Rigidbody2D _rb;

    private Enemy _shootTarget;
    private Transform _shootPoint;

    private Vector2 _targetMovePos;
    private Vector2 _startMovePos;
    private float _maxXpos;
    private float _maxYpos;

    private void Awake()
    {
        _model = GetComponent<PlayerModel>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _shootPoint = _model.CurrentWeapon.transform;
        _maxXpos = GameManager.Instance.MaxX;
        _maxYpos = GameManager.Instance.MaxY;
    }

    private void OnEnable()
    {
        _input.OnTouchMove += Move;
        _input.OnTouchStart += StartMove;
        GlobalEventManager.OnLevelCompleted += PlayerDeactivate;
        GlobalEventManager.OnEnemyKilled += TakeReward;
    }

    private void OnDisable()
    {
        _input.OnTouchMove -= Move;
        _input.OnTouchStart -= StartMove;
        GlobalEventManager.OnLevelCompleted -= PlayerDeactivate;
        GlobalEventManager.OnEnemyKilled -= TakeReward;
    }

    private void Update()
    {
        _shootTarget = TryGetTarget();
        if (_shootTarget != null)
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

    private void Move(Vector2 moveTarget)
    {
        if(moveTarget != Vector2.zero)
        {
            _targetMovePos = _startMovePos + moveTarget;
            _targetMovePos.x = Mathf.Clamp(_targetMovePos.x, -_maxXpos, _maxXpos);
            _targetMovePos.y = Mathf.Clamp(_targetMovePos.y, -_maxYpos, _maxYpos);

            transform.position = Vector2.MoveTowards(transform.position, _targetMovePos, _moveSpeed * Time.deltaTime);
        }
    }

    private void StartMove()
    {
        _startMovePos = transform.position;
    }

    public void TakeDamage(int damage)
    {
        if(_model.Health > 0)
        {
            _model.DecreaseHealth(damage);
            GlobalEventManager.SendOnPlayerTakeDamge(_model.Health);
        }

        if (_model.Health <= 0)
            Die();
    }

    public void TakeReward(int reward)
    {
        _model.AddPoints(reward);
    }

    private void Die()
    {
        GlobalEventManager.SendOnPlayerDie();
        _rb.velocity = Vector2.zero;
        _model.CurrentWeapon.gameObject.SetActive(false);
        enabled = false;
    }

    private void Shoot()
    {
        _model.CurrentWeapon.Shoot(Vector2.up);
    }

    private void PlayerDeactivate()
    {
        transform.gameObject.SetActive(false);
    }
}

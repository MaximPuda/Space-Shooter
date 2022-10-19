using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Joystick : MonoBehaviour
{
    [SerializeField] private Image _handler;
    [SerializeField] private Image _circle;

    public UnityAction<Vector2> OnJoystickMove;

    private Vector2 _direction = new Vector3();
    private Vector2 _startSwipe = new Vector2();

    private float _maxValue;

    private void Start()
    {
        _maxValue = _circle.sprite.rect.width;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            JoystickView(true);
            _startSwipe = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            _circle.transform.position = new Vector2(_startSwipe.x, _startSwipe.y);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 mousePos = Input.mousePosition;
            var delta = mousePos - _startSwipe;
            if (delta.magnitude < _maxValue)
                _handler.transform.position = mousePos;
            else
            {
                delta = Vector3.ClampMagnitude(delta, _maxValue);
                _handler.transform.position = _startSwipe + delta;
            }

            _direction = 1 / _maxValue * delta;
            OnJoystickMove?.Invoke(_direction);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            OnJoystickMove?.Invoke(Vector2.zero);
            JoystickView(false);
        }
    }

    private void JoystickView(bool enabled)
    {
        _circle.enabled = enabled;
        _handler.enabled = enabled;
    }
}

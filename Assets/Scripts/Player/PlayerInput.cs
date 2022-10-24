using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public event UnityAction<Vector2> OnTouchMove;
    public event UnityAction OnTouchStart;

    private Touch _touch;
    private Vector2 _startTouch;
    private Vector2 _deltaTouch;
    private Camera _camera;
    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Began)
            {
                _startTouch = _camera.ScreenToWorldPoint(_touch.position);
                OnTouchStart?.Invoke();
            }

            if (_touch.phase == TouchPhase.Moved)
                _deltaTouch = (Vector2)_camera.ScreenToWorldPoint(_touch.position) - _startTouch;

            if (_touch.phase == TouchPhase.Ended || _touch.phase == TouchPhase.Canceled)
                _deltaTouch = Vector2.zero;
            
            OnTouchMove?.Invoke(_deltaTouch);
        }

        if (Input.GetKeyDown(KeyCode.D))
            SaveSystem.DeleteSave();
    }
}

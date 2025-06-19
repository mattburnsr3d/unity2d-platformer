using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private float _currentPositionX;
    private Vector3 _velocity = Vector3.zero;

    private void Start()
    {
        _currentPositionX = transform.position.x;
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            new Vector3(_currentPositionX, transform.position.y, transform.position.z),
            ref _velocity,
            _speed
        );
    }

    public void MoveToNewRoom(Transform newRoom)
    {
        _currentPositionX = newRoom.position.x;
    }
}

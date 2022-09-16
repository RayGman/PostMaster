using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _leftBorder;
    [SerializeField] private float _rightBorder;
    [SerializeField] private float _speed;
    private Transform _playerTransform;

    private void Start()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
        }
        _playerTransform = _player.gameObject.transform;
    }

    private void Update()
    {
        if (transform.position.x != _playerTransform.position.x)
        {
            float playerX = _playerTransform.position.x - transform.position.x;
            transform.position += new Vector3(playerX, 0.0f, 0.0f) * _speed * Time.deltaTime;

            if (transform.position.x < _leftBorder)
            {
                transform.position = new Vector3(_leftBorder, transform.position.y, transform.position.z);
            }
            if (transform.position.x > _rightBorder)
            {
                transform.position = new Vector3(_rightBorder, transform.position.y, transform.position.z);
            }
            
        }
    }
}

using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private float _direction;
    private float _lifetime;
    private bool _hit;
    private BoxCollider2D _boxCollider;
    private Animator _animator;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_hit) return;
        float movementSpeed = _speed * Time.deltaTime * _direction;
        transform.Translate(movementSpeed, 0f, 0f);

        _lifetime += Time.deltaTime;
        if (_lifetime > 5f) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _hit = true;
        _boxCollider.enabled = false;
        _animator.SetTrigger("explode");
    }

    public void SetDirection(float direction)
    {
        gameObject.SetActive(true);
        _lifetime = 0;
        _direction = direction;
        _hit = false;
        _boxCollider.enabled = true;

        float localScaleXAxis = transform.localScale.x;
        if (Mathf.Sign(localScaleXAxis) != _direction) localScaleXAxis = -localScaleXAxis;
        transform.localScale = new Vector2(localScaleXAxis, transform.localScale.y);
    }

    private void Deactivate() => gameObject.SetActive(false);
}

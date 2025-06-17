using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float _attackCooldown;

    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private GameObject[] _fireballs;

    private Animator _animator;
    private PlayerMovement _playerMovement;
    private float _cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _cooldownTimer > _attackCooldown && _playerMovement.CanAttack())
            Attack();

        _cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        _animator.SetTrigger("attack");
        _cooldownTimer = 0;

        // Fireball object pooling
        _fireballs[FindFireball()].transform.position = _firePoint.position;
        _fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i < _fireballs.Length; i++)
            if (!_fireballs[i].activeInHierarchy)
                return i;
        return 0;
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class FinishHimEnemy : MonoBehaviour, IDamageable
{
    public bool CanTakeDamage;
    [SerializeField] int _enemyHealth;
    [SerializeField] Rigidbody _rb;
    [SerializeField] BoxCollider _collider;
    [SerializeField] Animator _animator;

    [SerializeField] Vector3 _launchAngle;

    bool IDamageable.CanTakeDamage => throw new System.NotImplementedException();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_enemyHealth > 0)
        {
            ProcessHitbox(other.GetComponent<Hitbox>());
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _animator.SetTrigger("Land");
        }
    }

    public void ProcessHitbox(Hitbox hitbox)
    {
        TakeDamage(hitbox.Damage);
    }

    public void TakeDamage(int incomingDamage)
    {
        _enemyHealth -= incomingDamage;
        if (_enemyHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GlobalInstance.AddScore();
        _animator.SetBool("isDead", true);
        _rb.AddForce(_launchAngle);
        _rb.useGravity = true;
        Debug.Log("I am dead");
    }
}

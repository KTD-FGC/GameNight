using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class ApproachMePlayer : MonoBehaviour, IDamageable
{
    public bool CanTakeDamage;
    [SerializeField] int _playerHealth;
    [SerializeField] Rigidbody _rb;
    [SerializeField] BoxCollider _collider;
    [SerializeField] Animator _animator;

    [SerializeField] Vector3 _launchAngle;

    bool IDamageable.CanTakeDamage => throw new System.NotImplementedException();

    bool isDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_playerHealth > 0)
        {
            Hitbox hitbox = other.GetComponent<Hitbox>();
            if(hitbox != null)
            {
                ProcessHitbox(other.GetComponent<Hitbox>());
            }

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
        _playerHealth -= incomingDamage;
        if (_playerHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        GlobalInstance.RemoveLives();
        Debug.Log("I am dead");
    }
}

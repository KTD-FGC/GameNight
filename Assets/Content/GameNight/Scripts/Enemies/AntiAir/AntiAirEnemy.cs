using UnityEngine;

public class AntiAirEnemy : MonoBehaviour
{


    Hurtbox _hurtbox;
    [SerializeField]
    Animator _animator;
    int _enemyHealth = 1;
    [SerializeField] Rigidbody _rb;
    [SerializeField] Vector3 _launchAngle;
    void OnEnable()
    {
        _hurtbox = GetComponentInChildren<Hurtbox>();
        if(_hurtbox != null)
        _hurtbox.OnTakeDamage += TakeDamage;
    }

    public void TakeDamage(int incomingDamage)
    {
        _enemyHealth += incomingDamage;
        if (_enemyHealth <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _animator.SetTrigger("Land");
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

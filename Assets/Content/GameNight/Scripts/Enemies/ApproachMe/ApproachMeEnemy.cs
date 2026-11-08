using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class ApproachMeEnemy : MonoBehaviour, IDamageable
{
    public bool CanTakeDamage;
    [SerializeField] int _enemyHealth;
    [SerializeField] Rigidbody _rb;
    [SerializeField] BoxCollider _collider;
    [SerializeField] Animator _animator;

    [SerializeField] Vector3 _launchAngle;

    bool IDamageable.CanTakeDamage => throw new System.NotImplementedException();
    private int frameCounter = 0;
    private int fireballTiming = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fireballTiming = Random.Range(120, 240);
    }

    void FixedUpdate()
    {
        frameCounter++;
        if (frameCounter == fireballTiming)
        {
            _animator.SetBool("isThrowingFireball", true);
            //TrackMoveDuration();
        }
        if (frameCounter == fireballTiming + 1)
        {
            _animator.SetBool("isThrowingFireball", false);
            fireballTiming = RandomizeFireballTiming();
            frameCounter = 0;
        }
    }

    int RandomizeFireballTiming()
    {
        return Random.Range(60, 120);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*IEnumerator TrackMoveDuration()
    {
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        float time = stateInfo.normalizedTime;

        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        Debug.Log("Move fireball finished!");
        frameCounter = 0;
        yield return null;
    }*/

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

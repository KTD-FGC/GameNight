using System.Collections;
using UnityEngine;

public class KeepAwayEnemy : MonoBehaviour, IDamageable
{
    Animator _animator;
    Rigidbody _rb;
    [SerializeField]
    float moveSpeed, hitStunTime, loseDistance;
    [SerializeField]
    Transform playerCharacter;
    bool hitStun;

    public bool CanTakeDamage => true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Walking", true);
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hitStun)
        {
            _rb.linearVelocity = Vector3.Normalize(playerCharacter.position - transform.position) * moveSpeed;
        }
        if (Vector3.Magnitude(playerCharacter.position - transform.position) <= loseDistance && !_animator.GetBool("Attack"))
        {

            _rb.linearVelocity = Vector3.zero;
            _animator.SetBool("Attack", true);
            GlobalInstance.RemoveLives();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            ProcessHitbox(other.GetComponent<Hitbox>());
        }
    }

    public void ProcessHitbox(Hitbox hitbox)
    {
        if (hitbox != null)
        {
            TakeDamage(0);
        }
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(ProcessHitStun());
    }

    IEnumerator ProcessHitStun()
    {
        Debug.Log("Hit Enemy");
        hitStun = true;
        _rb.linearVelocity = Vector3.zero;
        _rb.AddForce(Vector3.Normalize(transform.position - playerCharacter.position) * 2, ForceMode.Impulse);
        _animator.SetBool("Damaged", true);
        yield return new WaitForSeconds(hitStunTime / 4);
        for(int i = 15; i > 0; i--)
        {
            _rb.linearVelocity = _rb.linearVelocity * i / 15;
            yield return new WaitForSeconds(hitStunTime/60);
        }
        _rb.linearVelocity = Vector3.zero;
        yield return new WaitForSeconds(hitStunTime / 2);
        hitStun = false;
        _animator.SetBool("Damaged", false);
    }
}

using System;
using UnityEngine;
[RequireComponent (typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Hurtbox : MonoBehaviour, IDamageable
{
    private bool canTakeDamage = true;
    public bool CanTakeDamage => canTakeDamage;

    //TO:DO Replace with BoxCollider as we move to full 3D for collisions
    [SerializeField] BoxCollider _hurtboxCollider;

    HealthManager _currentManager;

    public Action<int> OnTakeDamage;
    /// <summary>
    /// Method for processing the hitbox and all effects caused by the interaction
    /// </summary>
    /// <param name="hitbox"></param>
    public void ProcessHitbox(Hitbox hitbox)
    {
        //GetComponentInParent<Player>();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(transform.parent.gameObject.name + " Got hit!");
        OnTakeDamage?.Invoke(-damage);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        _hurtboxCollider = GetComponent<BoxCollider>();

        _currentManager = GetComponentInParent<HealthManager>();
        if (_currentManager != null)
        {
            _currentManager.AssignHurtbox(this);
        }

        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue - new Color(0, 0, 0, .5f);
        Gizmos.DrawCube(transform.position + _hurtboxCollider.center, _hurtboxCollider.size);
        //Gizmos.DrawCube(transform.position + new Vector3(_hurtboxCollider.offset.x* transform.parent.localScale.x, _hurtboxCollider.offset.y,0 ), new Vector3(_hurtboxCollider.size.x, _hurtboxCollider.size.y, 1));
    }

}

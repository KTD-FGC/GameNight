using UnityEditor;
using UnityEngine;
using System;
[RequireComponent (typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Hitbox : MonoBehaviour
{

    public static Action<PlayerType, Hitbox> OnHit;
    public Action LocalHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public BoxCollider HitboxCollider;
    public PlayerType Owner;

    [Header("Hitbox Properties")]
    [Tooltip("How much damage?")]
    [SerializeField] private int _damage;
    [Tooltip("Should this hitbox knockdown an enemy?")]
    [SerializeField] private bool _canKnockdown;
    [Tooltip("Should this hitbox launch an enemy?")]
    [SerializeField] private bool _canLaunch;
    [Tooltip("Priority for canceling (0 = never cancel, 1 = cancel into special only, 2 = cancel into other valid moves, 3 = cancel always")]
    [SerializeField] private int _cancelPriority; //MAX VALUE of 3 = cancel always, 0 for never allow cancel
    [Tooltip("How much hit stun should the move do?")]
    [SerializeField] private int _hitStun;

    [Header("Pushback forces")]
    [SerializeField] private float _attackerPushback;
    [SerializeField] private float _defenderPushback;

    //These are the public accessible version of the properties
    public int CancelPriority => _cancelPriority;
    public int Damage => _damage;
    public bool CanKnockdown => _canKnockdown;
    public bool CanLaunch => _canLaunch;
    public float AttackerPushback => _attackerPushback;
    public float DefenderPushback => _defenderPushback;

    private bool _madeContact = false;
    void OnEnable()
    {
        HitboxCollider = GetComponent<BoxCollider>();
        GetComponent<Rigidbody>().useGravity = false;
        
    }
   
   //TO:DO Replace with normal "ontriggerEnter" since we will be moving to 3D collisions for everything regardless
    private void OnTriggerEnter(Collider collision)
    {
        if (_madeContact) 
        {
            return;
        }
        IDamageable target = collision.GetComponent<IDamageable>();
        //First, have the target take damage for simplicity
        target?.TakeDamage(_damage);

        //Pass in the hitbox and it's properties into the target to handle how it should respond based on it's state
        target?.ProcessHitbox(this);
        OnHit?.Invoke(Owner, this);
        LocalHit?.Invoke();

        if (target != null) 
        {
            _madeContact = true;
        }
    }

    //Draws the debug hitbox as a cube at the given location, based on itself when spawned
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red - new Color(0,0,0,.5f);
        if (transform.parent != null)
        {
            Gizmos.DrawCube(transform.position + HitboxCollider.center, HitboxCollider.size);
        }
        else
        {
            Gizmos.DrawCube(transform.position + HitboxCollider.center, HitboxCollider.size);
        }

        
    }





}

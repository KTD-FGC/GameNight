using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Local hitbox for projectile
    Hitbox _hitbox;
    //How fast should the projectile move
    public float projectileSpeed = 5f;

    void Start()
    {
        _hitbox = GetComponent<Hitbox>();
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 force = new Vector3(projectileSpeed * gameObject.transform.parent.localScale.x, 0, 0);
            rb.AddForce(force, ForceMode.VelocityChange);

            this.transform.parent = null;

            _hitbox.LocalHit += KillProjectile;
        

    }

    /// <summary>
    /// Called to destroy the projectile game object, either upon leaving the screen or hitting something
    /// </summary>
    private void KillProjectile()
    {
        _hitbox.LocalHit -= KillProjectile;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

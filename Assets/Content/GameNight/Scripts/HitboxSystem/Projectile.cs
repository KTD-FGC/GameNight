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
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

            rb.linearVelocityX = projectileSpeed * -gameObject.transform.parent.localScale.x;

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

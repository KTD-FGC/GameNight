using UnityEngine;
/// <summary>
/// Interface for all damagable things (anything that should be hit by a hitbox)
/// </summary>
public interface IDamageable
{
    bool CanTakeDamage { get;}
    void ProcessHitbox(Hitbox hitbox);
    void TakeDamage(int damage);
}

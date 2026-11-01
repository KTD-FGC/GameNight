using System;
using TMPro;
using UnityEngine;

public class MoveHandler : MonoBehaviour
{
    [SerializeField] LayerMask _hitboxLayer;
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] PlayerType _playerPort;

    [SerializeField] Animator _animator;
    GameObject _activeMove;
    [SerializeField] AttackManager _attackManager;

    bool _moveIsActive = false;
    public bool CancelWindowOpen = false;

    public void OnEnable()
    {
        Hitbox.OnHit += HandleMoveContact;

        if (_attackManager != null)
        {
            _attackManager.ExecuteMove += ExecuteMove;
        }

        //Debug.Log(MoveTypes.LightPunch.GetHashCode());
        //ExecuteMove(MoveTypes.Special3);
    }

    public void OnDisable()
    {
        Hitbox.OnHit -= HandleMoveContact;
        if (_attackManager != null)
        {
            _attackManager.ExecuteMove -= ExecuteMove;
        }
    }

    private void HandleMoveContact(PlayerType type, Hitbox hitbox)
    {
        if (type == _playerPort) //Is this the right player?
        {
            //Get the hitbox cancel priority
            int cancel = hitbox.CancelPriority;
            if (cancel > 1)
            {
                _moveIsActive = false;
            }
            //Debug.Log("Contact made on " +type.ToString());
            _moveIsActive = false;
        }
    }

    public void LoadMovePrefab(GameObject hitbox)
    {
            ClearActiveMove();
        _activeMove = Instantiate(hitbox, this.transform);
        Hitbox collider = _activeMove.GetComponent<Hitbox>();
        collider.Owner = _playerPort;
        //_activeMove.layer = _hitboxLayer.value;
        collider.HitboxCollider.excludeLayers = _hitboxLayer;
        collider.HitboxCollider.includeLayers = _enemyLayer;
        _activeMove.GetComponent<Rigidbody2D>().excludeLayers = _hitboxLayer;
        _activeMove.GetComponent<Rigidbody2D>().includeLayers = _enemyLayer;
        //_moveIsActive = true;

    }

    public void ClearActiveMove()
    {
        Destroy(_activeMove);
        //_moveIsActive = false;
    }

    void FinishMove()
    {
        _animator.SetTrigger("FinishMove");
        _animator.SetInteger("Strength", 0);
        _moveIsActive = false;
        //GetComponent<EntityMovement>().CanMove = true;


    }


    void ExecuteMove(MoveTypes moveType, int strength)
    {
        if (!_moveIsActive)
        {
            _animator.SetInteger("Strength", strength);
            _animator.Play(moveType.ToString());
            _moveIsActive = true;
        }


        //GetComponent<EntityMovement>().CanMove = false;

    }
}

using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private ThirdPersonMovement _playerMovement;
    [SerializeField] private PlayerPickup _playerPickup;
    [SerializeField] private Animator _animator;
    private int _moveHash;
    private int _attackHash;
    private int _dashHash;

    private void Start()
    {
        // hash animations id for performance
        _moveHash = Animator.StringToHash("Walking");
        _attackHash = Animator.StringToHash("Attack");
        _dashHash = Animator.StringToHash("Dash");
    }

    private void OnEnable()
    {
        // subscribing to events
        _playerMovement.OnMoveChange += SetMove;
        _playerMovement.OnDash += Dash;
        _playerPickup.OnUsePickup += Attack;
    }

    private void OnDisable()
    {
        // unsubscribing to events
        _playerMovement.OnMoveChange -= SetMove;
        _playerMovement.OnDash -= Dash;
        _playerPickup.OnUsePickup -= Attack;
    }

    private void SetMove(bool value)
    {
        _animator.SetBool(_moveHash, value);
    }

    private void Attack()
    {
        _animator.SetTrigger(_attackHash);
    }

    private void Dash(bool value)
    {
        _animator.SetTrigger(_dashHash);
    }
}

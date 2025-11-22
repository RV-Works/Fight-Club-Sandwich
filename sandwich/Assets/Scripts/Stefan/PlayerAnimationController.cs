using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private ThirdPersonMovement _playerMovement;
    [SerializeField] private Animator _animator;
    private int _moveHash;

    private void Start()
    {
        // hash animations id for performance
        _moveHash = Animator.StringToHash("Walking");

        // subscribing to events
        _playerMovement.onMoveChange += SetMove;
    }

    private void OnDisable()
    {
        // unsubscribing to events
        _playerMovement.onMoveChange -= SetMove;
    }

    private void SetMove(bool value)
    {
        _animator.SetBool(_moveHash, value);
    }
}

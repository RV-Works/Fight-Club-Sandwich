using UnityEngine;

public class AnimationReader : MonoBehaviour
{
    [SerializeField] private PlayerPickup _playerPickup;

    public delegate void AnimationCallback();

    public AnimationCallback ThrowPerformedCallback;

    public void ThrowPerformed()
    {
        _playerPickup.OnThrowAnimationFinish();
    }
}

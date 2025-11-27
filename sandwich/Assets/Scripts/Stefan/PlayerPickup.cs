using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public delegate void PlayerPickupCallback();

    [SerializeField] private InputManager _inputManager;
    [SerializeField] private PlayerAnimationController _animationController;
    [SerializeField] private GameObject _ingredientParent;
    private SabotageItem _currentItem = null;

    public event PlayerPickupCallback OnUsePickup;

    private void OnEnable()
    {
        _inputManager.useItemEvent += Use;
    }

    private void OnDisable()
    {
        _inputManager.useItemEvent -= Use;
    }

    private void Use()
    {
        if (_currentItem == null)
            return;

        
        _currentItem.Activate();
        OnUsePickup?.Invoke();
    }

    public void TrySetPickup(SabotageItem item)
    {
        if (_currentItem != null)
            return;

        // add to stack
        item.transform.SetParent(_ingredientParent.transform, true);
        item.transform.localPosition = Vector3.zero;
        _currentItem = item;

    }
}

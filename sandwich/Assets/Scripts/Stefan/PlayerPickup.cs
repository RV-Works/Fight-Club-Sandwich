using UnityEngine;
using UnityEngine.UI;

public class PlayerPickup : MonoBehaviour
{
    public delegate void PlayerPickupCallback();

    [SerializeField] private InputManager _inputManager;
    [SerializeField] private PlayerAnimationController _animationController;
    [SerializeField] private GameObject _ingredientParent;
    private SabotageItem _currentItem = null;
    private bool _usedItem;
    private PickupUi _itemUi;
    public event PlayerPickupCallback OnUsePickup;

    private void OnEnable()
    {
        _inputManager.useItemEvent += Use;
    }

    private void OnDisable()
    {
        _inputManager.useItemEvent -= Use;
    }

    private void Start()
    {
        _itemUi = UiManager.Instance.GetPlayerUi();
    }

    private void Use()
    {
        if (_currentItem == null || _usedItem == true)
            return;
        
        OnUsePickup?.Invoke();
        _usedItem = true;
    }

    public void OnThrowAnimationFinish()
    {
        _currentItem.Activate();
        Destroy(_currentItem.gameObject);
        _usedItem = false;
        _currentItem = null;
    }

    public bool TrySetPickup(SabotageItem item)
    {
        if (_currentItem != null)
            return false;

        // add to stack
        item.transform.SetParent(_ingredientParent.transform, true);
        item.transform.localPosition = Vector3.zero;
        Quaternion rotation = Quaternion.FromToRotation(new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z), new Vector3(0, 0, 0));
        item.transform.localRotation = rotation;
        _currentItem = item;

        _itemUi.ShowItem(item.Sprite);

        return true;
    }
}

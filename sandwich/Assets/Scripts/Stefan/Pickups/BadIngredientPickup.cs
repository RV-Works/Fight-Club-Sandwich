using UnityEngine;

public class BadIngredientPickup : SabotageItem
{
    [SerializeField] private GameObject _throwPrefab;
    [SerializeField] private float _throwSpeed = 10f;
    private BoxCollider _boxCollider;
    private GameObject _player;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    public override void Collect(GameObject player)
    {
        base.Collect(player);

        if (hasAcceptedItem)
        {
            _player = player;
            _boxCollider.enabled = false;
        }
    }

    public override void Activate()
    {
        // throw ingredient
        GameObject throwableObject = Instantiate(_throwPrefab, transform.position, _player.transform.rotation);

        if (throwableObject.TryGetComponent<IThrowable>(out IThrowable throwable))
        {
            throwable.Throw(_player);
        }

        throwableObject.GetComponent<Rigidbody>().AddForce(_player.transform.forward * _throwSpeed);

        Debug.Log("Activated: " + gameObject.name);
    }
}

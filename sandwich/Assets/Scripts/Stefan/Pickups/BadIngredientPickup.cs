using Unity.VisualScripting;
using UnityEngine;

public class BadIngredientPickup : SabotageItem
{
    [SerializeField] private GameObject _throwPrefab;

    public override void Collect(GameObject player)
    {
        base.Collect(player);
    }

    public override void Activate()
    {
        // throw ingredient
        // Instantiate(_throwPrefab, transform.position, Quaternion.identity);
        Debug.Log("Activate: " + gameObject.name);
    }
}

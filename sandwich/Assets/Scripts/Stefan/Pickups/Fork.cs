using UnityEngine;

public class Fork : SabotageItem
{
    private BoxCollider _boxCollider;
    private bool _collected;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    public override void Collect(GameObject player)
    {
        if (_collected)
            return;

        base.Collect(player);
        
        if (hasAcceptedItem)
            _collected = true;
    }

    public override void Activate()
    {
        // use fork

        
    }
}

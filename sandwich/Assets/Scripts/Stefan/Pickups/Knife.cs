using UnityEngine;

public class Knife : SabotageItem
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
        _collected = true;
    }

    public override void Activate()
    {
        // use fork

    }
}

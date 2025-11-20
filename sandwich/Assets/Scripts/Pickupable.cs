using UnityEngine;

public class Pickupable : MonoBehaviour
{
    [SerializeField] private bool Knife = false;
    [SerializeField] private bool Fork = false;
    [SerializeField] private bool Spoon = false;
    [SerializeField] private bool ButterBottle = false;
    public Weapons weapons;

    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;

        // prefer the explicitly assigned weapons reference; otherwise try to find one on the colliding object (or its parents)
        var targetWeapons = weapons ?? other.GetComponentInParent<Weapons>();
        if (targetWeapons == null)
            return;

        if (Knife)
        {
            targetWeapons.ActivateKnife();
            Destroy(gameObject);
            return;
        }

        if (Fork)
        {
            targetWeapons.ActivateFork();
            Destroy(gameObject);
            return;
        }

        if (Spoon)
        {
            targetWeapons.ActivateSpoon();
            Destroy(gameObject);
            return;
        }

        if (ButterBottle)
        {
            targetWeapons.ActivateButterBottle();
            Destroy(gameObject);
            return;
        }
    }
}
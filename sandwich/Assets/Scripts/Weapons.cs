using UnityEngine;


public class Weapons : MonoBehaviour
{
    [SerializeField] private bool HoldingKnife = false;
    [SerializeField] private bool HoldingFork = false;
    [SerializeField] private bool HoldingSpoon = false;
    [SerializeField] private bool HoldingButterBottle = false;
    // Made public for test/assignment in Inspector
    public GameObject KnifeObject;
    public GameObject ForkObject;
    public GameObject SpoonObject;
    public GameObject ButterBottleObject;

    public void Throw()
    {
    }

    public void ActivateKnife()
    {
        HoldingKnife = true;
        if (KnifeObject != null) KnifeObject.SetActive(true);
    }

    public void ActivateFork()
    {
        HoldingFork = true;
        if (ForkObject != null) ForkObject.SetActive(true);
    }

    public void ActivateSpoon()
    {
        HoldingSpoon = true;
        if (SpoonObject != null) SpoonObject.SetActive(true);
    }

    public void ActivateButterBottle()
    {
        HoldingButterBottle = true;
        if (ButterBottleObject != null) ButterBottleObject.SetActive(true);
    }
}
using UnityEngine;


public class Weapons : MonoBehaviour
{
    [SerializeField] private bool HoldingKnife = false;
    [SerializeField] private bool HoldingFork = false;
    [SerializeField] private bool HoldingSpoon = false;
    [SerializeField] private bool HoldingButterBottle = false;
    [SerializeField] private bool HoldingVacuum = false;

    [SerializeField] private bool HoldingSauce = false;
    [SerializeField] private bool HoldingWater = false;
    // Made public for test/assignment in Inspector
    public GameObject KnifeObject;
    public GameObject ForkObject;
    public GameObject SpoonObject;
    public GameObject ButterBottleObject;
    public GameObject VacuumObject;
    public GameObject Butter;
    public GameObject Sauce;
    public GameObject Water;

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
    public void ActivateVacuum()
    {
        HoldingVacuum = true;
        if (VacuumObject != null) VacuumObject.SetActive(true);
    }
    public void ActivateWater()
    {
        HoldingWater = true;
        if (Water != null) Water.SetActive(true);
    }
    public void ActivateSauce()
    {
        HoldingSauce = true;
        if (Sauce != null) Sauce.SetActive(true);
    }
}

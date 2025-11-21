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
    public GameObject KnifeObject;
    public GameObject ForkObject;
    public GameObject SpoonObject;
    public GameObject ButterBottleObject;
    public GameObject VacuumObject;
    public GameObject Butter;
    public GameObject Sauce;
    public GameObject Water;
    [SerializeField] private GameObject Projectile;
    public ThirdPersonMovement Player;


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
    // the throwy thingy
    public void ThrowFork()
    {
        HoldingFork = false;
        if (ForkObject != null) ForkObject.SetActive(false);
        Instantiate(Projectile, transform.position + transform.forward * 2, transform.rotation);
    }
    public void ThrowSpoon()
    {
        HoldingSpoon = false;
        if (SpoonObject != null) SpoonObject.SetActive(false);
        Instantiate(Projectile, transform.position + transform.forward * 2, transform.rotation);
    }
    public void ThrowWater()
    {
        HoldingWater = false;
        if (Water != null) Water.SetActive(false);
        Instantiate(Projectile, transform.position + transform.forward * 2, transform.rotation);
    }
    public void ThrowSauce()
    {
        HoldingSauce = false;
        if (Sauce != null) Sauce.SetActive(false);
        Instantiate(Projectile, transform.position + transform.forward * 2, transform.rotation);
    }
    public void UseButter()
    {
        HoldingButterBottle = false;
        if (ButterBottleObject != null) ButterBottleObject.SetActive(false);
        if (Butter != null) Butter.SetActive(true);

        // Double the player's speed for 1 second
        if (Player != null)
            Player.ApplySpeedMultiplier(2f, 1f);
    }
    public void UseVacuum()
    {
        HoldingVacuum = false;
        if (VacuumObject != null) VacuumObject.SetActive(false);
    }

}
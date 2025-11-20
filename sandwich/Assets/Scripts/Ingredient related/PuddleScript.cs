using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PuddleScript : MonoBehaviour
{
    // Optional reference to the player's movement component (can be assigned in Inspector).
    public ThirdPersonMovement PlayerMovement;

    [Tooltip("Angular drag to apply while inside the puddle (lower = less angular damping).")]
    public float slipperyAngularDrag = 0.0f;

    [Tooltip("Linear drag to apply while inside the puddle (optional).")]
    public float slipperyDrag = 0.5f;

    // Store original values so they can be restored on exit.
    private readonly Dictionary<Rigidbody, float> originalAngularDrags = new Dictionary<Rigidbody, float>();
    private readonly Dictionary<Rigidbody, float> originalDrags = new Dictionary<Rigidbody, float>();

    void Start()
    {
    }

    void Update()
    {
        // No per-frame work required for the puddle effect.
        // Keep this only if you need PlayerMovement for other purposes.
    }

    private void ApplySlippery(Rigidbody rb)
    {
        if (rb == null)
            return;

        if (!originalAngularDrags.ContainsKey(rb))
            originalAngularDrags[rb] = rb.angularDamping;

        if (!originalDrags.ContainsKey(rb))
            originalDrags[rb] = rb.linearDamping;

        rb.angularDamping = slipperyAngularDrag;
        rb.linearDamping = slipperyDrag;
    }

    private void RestoreDrags(Rigidbody rb)
    {
        if (rb == null)
            return;

        if (originalAngularDrags.TryGetValue(rb, out float originalAngular))
        {
            rb.angularDamping = originalAngular;
            originalAngularDrags.Remove(rb);
        }

        if (originalDrags.TryGetValue(rb, out float originalLinear))
        {
            rb.linearDamping = originalLinear;
            originalDrags.Remove(rb);
        }
    }

    private Rigidbody GetTargetRigidbody(Collider other)
    {
        // Prefer explicit PlayerMovement reference if assigned and matches the collider's gameObject.
        if (PlayerMovement != null && PlayerMovement.gameObject == other.gameObject)
        {
            var rb = PlayerMovement.GetComponent<Rigidbody>();
            if (rb != null) return rb;
        }

        // Otherwise use the collider's attached Rigidbody or try to get one from the same GameObject.
        return other.attachedRigidbody ?? other.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        Debug.Log("Player entered puddle");
        Rigidbody rb = GetTargetRigidbody(other);
        ApplySlippery(rb);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        Debug.Log("Player exited puddle");
        Rigidbody rb = GetTargetRigidbody(other);
        RestoreDrags(rb);
    }

    private void OnDisable()
    {
        // In case the puddle is disabled while players are inside, restore any cached values.
        foreach (var kvp in new List<Rigidbody>(originalAngularDrags.Keys))
            RestoreDrags(kvp);
    }
}
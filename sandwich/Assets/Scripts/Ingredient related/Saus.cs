using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saus : MonoBehaviour, IThrowable
{
    private GameObject thrownPlayer;
    // COOL LINE HERE CUZ IM LAZY ASF - RV
    public ThirdPersonMovement PlayerMovement;
    // COOL LINE HERE CUZ IM LAZY ASF - RV
    public float slipperyAngularDrag = 0.0f;
    public float slipperyDrag = 0.5f;

    public float slipperyDuration = 1.5f;

    private readonly Dictionary<Rigidbody, float> originalAngularDrags = new Dictionary<Rigidbody, float>();
    private readonly Dictionary<Rigidbody, float> originalDrags = new Dictionary<Rigidbody, float>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == thrownPlayer)
            return;

        if (collision.collider.CompareTag("Player"))
        {
            if (collision.collider.TryGetComponent<ThirdPersonMovement>(out ThirdPersonMovement playerMovement))
            {
                Rigidbody rb = GetTargetRigidbody(collision.collider);
                ApplySlippery(rb);

                // Start a timer that uses Time.deltaTime for a precise 1.5s delay, then restore drags.
                StartCoroutine(RestoreAfterDelay(rb));
            }
        }
    }

    public void Throw(GameObject player)
    {
        thrownPlayer = player;
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

    private IEnumerator RestoreAfterDelay(Rigidbody rb)
    {
        float timer = slipperyDuration;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        RestoreDrags(rb);

    }

    private Rigidbody GetTargetRigidbody(Collider other)
    {
        if (PlayerMovement != null && PlayerMovement.gameObject == other.gameObject)
        {
            var rb = PlayerMovement.GetComponent<Rigidbody>();
            if (rb != null) return rb;
        }

        return other.attachedRigidbody ?? other.GetComponent<Rigidbody>();
    }
}
using UnityEngine;

public abstract class SabotageItem : MonoBehaviour, ICollectable
{
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private LayerMask playerLayer;
    private LayerMask nothingLayer;
    private bool isGrounded;
    private const RigidbodyConstraints onGroundConstraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    private const float GroundY = 0.02f;
    internal bool hasAcceptedItem;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        playerLayer = LayerMask.GetMask("Player");
        nothingLayer = LayerMask.GetMask("Nothing");
    }

    private void Update()
    {
        if (rb == null) return;

        // When ingredient reaches Y <= 0.02 turn off gravity once
        if (!isGrounded && transform.position.y <= GroundY)
        {
            Grounded();
            transform.position = new Vector3(transform.position.x, GroundY, transform.position.z);
        }
    }

    private void Grounded()
    {
        // player can interact
        boxCollider.excludeLayers = nothingLayer;
        boxCollider.isTrigger = true;

        isGrounded = true;
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // disable RigidBody X and Y movement
        rb.constraints = onGroundConstraints;
    }

    public virtual void Collect(GameObject player)
    {
        hasAcceptedItem = player.GetComponent<PlayerPickup>().TrySetPickup(this);
        if (!hasAcceptedItem)
            return;
        Debug.Log("Collect: " + gameObject.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Grounded();
        }
    }

    public abstract void Activate();
}

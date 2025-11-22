using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonMovement : MonoBehaviour
{
    public delegate void animationDelagete(bool isDoing);

    [SerializeField] private float m_speed = 6;
    [SerializeField] private float m_turnSpeed = 5f;
    [SerializeField] private float m_dashForce = 10f;
    [SerializeField] private float m_dashCooldown = 2f;
    [SerializeField] private InputManager m_inputManager;

    private Vector3 moveInput;

    private Rigidbody m_rigidBody;
    private bool m_sprinting = false;
    private float m_currentDashCooldown = 0f;

    private float baseSpeed;
    private Coroutine speedCoroutine;

    private bool moving = false;
    public event animationDelagete onMoveChange;

    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        baseSpeed = m_speed;
    }

    private void OnEnable()
    {
        // subscribing to callbacks
        m_inputManager.moveEvent += SetMove;
        m_inputManager.dashEvent += Dash;
    }

    private void OnDisable()
    {
        // unsubscribing from callbacks
        m_inputManager.moveEvent -= SetMove;
        m_inputManager.dashEvent -= Dash;
    }

    private void FixedUpdate()
    {
        if (m_inputManager == null)
            return;

        // moving
        if (moveInput.x != 0f || moveInput.z != 0f)
        {
            if (!moving)
            {
                moving = true;
                onMoveChange.Invoke(true);
            }

            // move character
            m_rigidBody.AddForce(moveInput * m_speed);

            // rotate towards movement
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, m_turnSpeed * Time.deltaTime);
        }
        else
        // not moving
        {
            if (moving)
            {
                moving = false;
                onMoveChange?.Invoke(false);
            }
        }

        if (m_currentDashCooldown > 0f)
            m_currentDashCooldown -= Time.deltaTime;
    }

    private void SetMove(Vector2 setMoveInput)
    {
        // set input
        moveInput = new Vector3(setMoveInput.x, 0, setMoveInput.y).normalized;
    }

    private void Dash()
    {
        // dash
        if (m_currentDashCooldown <= 0f)
        {
            m_rigidBody.AddRelativeForce(Vector3.forward * m_dashForce);
            m_currentDashCooldown = m_dashCooldown;
        }
    }
    public void ApplySpeedMultiplier(float multiplier, float duration)
    {
        if (speedCoroutine != null)
            StopCoroutine(speedCoroutine);

        speedCoroutine = StartCoroutine(ApplySpeedMultiplierCoroutine(multiplier, duration));
    }

    private IEnumerator ApplySpeedMultiplierCoroutine(float multiplier, float duration)
    {
        m_speed = baseSpeed * multiplier;
        yield return new WaitForSeconds(duration);
        m_speed = baseSpeed;
        speedCoroutine = null;
    }
}


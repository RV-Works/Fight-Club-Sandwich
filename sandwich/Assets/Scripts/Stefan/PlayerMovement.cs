using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private float m_speed = 6;
    [SerializeField] private float m_turnSpeed = 5f;
    [SerializeField] private float m_dashForce = 10f;
    [SerializeField] private float m_dashCooldown = 2f;
    [SerializeField] private InputManager m_inputManager;

    private Vector3 moveInput;

    private Rigidbody m_rigidBody;
    private bool m_sprinting = false;
    private Vector3 m_velocity;
    private float m_currentDashCooldown = 0f;

    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
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

        // set input without event
        //moveInput = new Vector3(m_inputManager.MoveInput.x,0,m_inputManager.MoveInput.y).normalized;

        // moving
        if (moveInput.x != 0f || moveInput.z != 0f)
        {
            // move character
            float speed = m_sprinting ? m_speed * 2.5f : m_speed;
            m_rigidBody.AddForce(moveInput * speed);

            // rotate towards movement
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            //m_rigidBody.MoveRotation(targetRotation);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, m_turnSpeed * Time.deltaTime);
        }
        else
        // not moving
        {
            m_sprinting = false;
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
}

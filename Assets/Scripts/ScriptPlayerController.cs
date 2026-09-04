using UnityEngine;

public class ScriptPlayerController : MonoBehaviour
{
    public float forwardSpeed = 8f;
    public float laneChangeSpeed = 12f;

    public float jumpHeight = 1.5f;
    public float gravity = -25f;
    private float verticalVelocity;

    private CharacterController controller;
    private int currentLane;
    private float targetX;

    public float hitCooldown = 0.6f;
    private float hitTimer;

    public float invincibleDuration = 10f, magnetDuration = 10f;
    public float magnetRadius = 3.5f, magnetPullSpeed = 14f;
    public float invincibleTimer, magnetTimer;

    public bool IsInvincible => invincibleTimer > 0f;

    public bool IsMagnetActive => magnetTimer > 0f;

    public void ActivateInvincibility() => invincibleTimer = invincibleDuration;

    public void ActivateMagnet() => magnetTimer = magnetDuration;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentLane = GameConstants.CenterLane;
        targetX = GameConstants.LaneToX(currentLane);
    }

    void Update()
    {
        HandleLaneInput();
        Move();

        if (invincibleTimer > 0f)
            invincibleTimer -= Time.deltaTime;
        if (magnetTimer > 0f)
            magnetTimer -= Time.deltaTime;

        HandleMagenet();

        if (hitTimer > 0f)
            hitTimer -= Time.deltaTime;
    }

    void HandleMagenet()
    {
        if (magnetTimer <= 0) return;
        foreach (Collider c in Physics.OverlapSphere(transform.position, magnetRadius))
        {
            if (c.CompareTag("Coin"))
            {
                c.transform.position = Vector3.MoveTowards(c.transform.position,
                    transform.position, magnetPullSpeed * Time.deltaTime);
            }
        }
    }

    void HandleLaneInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            currentLane = Mathf.Max(0, currentLane - 1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            currentLane = Mathf.Min(GameConstants.LaneCount - 1, currentLane + 1);
        }

        targetX = GameConstants.LaneToX(currentLane);
    }

    void Move()
    {
        Vector3 move = Vector3.zero;
        move.z = forwardSpeed;

        float newX = Mathf.MoveTowards(transform.position.x, 
                                       targetX, laneChangeSpeed * Time.deltaTime);

        move.x = (newX - transform.position.x) / Time.deltaTime;

        if (controller.isGrounded)
        {
            verticalVelocity = -1f;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;

        controller.Move(move * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            ScriptGameManager.Instance.AddCoin();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("PowerUp"))
        {
            //ScriptPowerUp p = other.GetComponent<ScriptPowerUp>();
            //if (p != null) p.Apply(this);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Obstacle"))
        {
            if (IsInvincible || hitTimer > 0f) return;

            hitTimer = hitCooldown;
            ScriptGameManager.Instance.TakeDamage(1);
        }
    }
}

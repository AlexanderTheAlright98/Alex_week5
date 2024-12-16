using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRb;
    public float moveSpeed = 5;
    public float jumpHeight = 10;

    [SerializeField] Transform focalPoint;
    [SerializeField] bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDir = new Vector2(horizontalInput, verticalInput).normalized;
        playerRb.AddForce((focalPoint.forward * moveDir.y + focalPoint.right * moveDir.x) * moveSpeed);

        focalPoint.position = transform.position;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded )
        {
            playerRb.linearVelocity = Vector3.up * jumpHeight;
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Deadzone")
        {
            transform.position = new Vector3(0, 0, 0);
            playerRb.linearVelocity = new Vector3(0,0,0);
        }

        if (collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}

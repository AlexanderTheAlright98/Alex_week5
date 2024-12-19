using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRb;
    public float moveSpeed = 5;
    public float fastMoveSpeed = 20;
    public float jumpHeight = 10;
    public bool hasLightningPower;
    public bool hasSpeedPower;
    public bool speedPowerOnCooldown;

    public Image lightningIcon;
    public Image speedIcon;

    [SerializeField] int playerHP = 3;
    [SerializeField] Transform focalPoint;
    [SerializeField] bool isGrounded;
    [SerializeField] float lightningForce;
    [SerializeField] GameObject lightningIndicator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDir = new Vector2(horizontalInput, verticalInput).normalized;
        playerRb.AddForce((focalPoint.forward * moveDir.y + focalPoint.right * moveDir.x) * moveSpeed);
        focalPoint.position = transform.position;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRb.linearVelocity = Vector3.up * jumpHeight;
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !speedPowerOnCooldown)
        {
            StartCoroutine(SuperSpeedCooldown());
        }

        if (hasSpeedPower && !speedPowerOnCooldown)
        {
            playerRb.linearVelocity = focalPoint.forward * fastMoveSpeed;
        }

        //The line below means that if hasLightningPower is true, the lightning ring will be active. If it's false, it'll be inactive
        lightningIndicator.SetActive(hasLightningPower);
        lightningIndicator.transform.position = transform.position;
        lightningIcon.enabled = hasLightningPower;
        speedIcon.enabled = hasSpeedPower;

        if (playerHP <= 0)
        {
            Time.timeScale = 0;
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Deadzone")
        {
            playerHP--;
            transform.position = new Vector3(0,0,0);
            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
        }

        if (collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }

        if (collision.transform.tag == "Enemy" && hasLightningPower)
        {
            //"collision" in the following lines refers to anything with the Enemy tag
            Vector3 repelDirection = (collision.transform.position - transform.position).normalized;
            Rigidbody enemyRb = collision.transform.GetComponent<Rigidbody>();
            enemyRb.AddForce(repelDirection * lightningForce, ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "LightningPower")
        {
            Destroy(other.gameObject);
            StartCoroutine(LightningPowerCooldown());
        }
    }
    IEnumerator LightningPowerCooldown()
    {
        hasLightningPower = true;
        yield return new WaitForSeconds(5);
        hasLightningPower = false;
    }

    IEnumerator SuperSpeedCooldown()
    {
        hasSpeedPower = true;
        yield return new WaitForSeconds(0.75f);
        hasSpeedPower = false;
        speedPowerOnCooldown = true;
        yield return new WaitForSeconds(2);
        speedPowerOnCooldown = false;
    }
}

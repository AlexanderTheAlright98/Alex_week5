using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRb;
    public Renderer rend;
    public ParticleSystem speedParticle;
    public float moveSpeed = 5;
    public float fastMoveSpeed = 20;
    public float jumpHeight = 10;
    public bool hasLightningPower;
    public bool hasSpeedPower;
    public bool speedPowerOnCooldown;
    public int playerHP = 3;

    public Image lightningIcon;
    public Image speedIcon;

    public AudioClip lightningAudio;
    public AudioClip speedAudio;
    private AudioSource sfxAudio;

    [SerializeField] Transform focalPoint;
    [SerializeField] bool isGrounded;
    [SerializeField] float lightningForce;
    [SerializeField] GameObject lightningIndicator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
        playerRb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        sfxAudio = GetComponent<AudioSource>();
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
            sfxAudio.PlayOneShot(speedAudio);
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
            rend.enabled = false;

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("MainGame");
            }
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

        if (collision.transform.tag == "Enemy" && hasLightningPower || collision.transform.tag == "Boss" && hasLightningPower)
        {
            //"collision" in the following lines refers to anything with the Enemy tag
            Vector3 repelDirection = (collision.transform.position - transform.position).normalized;
            Rigidbody enemyRb = collision.transform.GetComponent<Rigidbody>();
            enemyRb.AddForce(repelDirection * lightningForce, ForceMode.Impulse);
            sfxAudio.PlayOneShot(lightningAudio);
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

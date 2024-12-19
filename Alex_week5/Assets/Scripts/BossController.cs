using UnityEngine;

public class BossController : MonoBehaviour
{
    public float bossForce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LightningPower")
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 repelDirection = (collision.transform.position - transform.position).normalized;
            Rigidbody playerRb = collision.transform.GetComponent<Rigidbody>();
            playerRb.AddForce(repelDirection * bossForce, ForceMode.Impulse);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            collision.transform.localScale += new Vector3(1.5f, 1.5f, 1.5f);
        }
    }
}

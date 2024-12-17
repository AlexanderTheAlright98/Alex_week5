using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemySpeed = 4;

    Rigidbody enemyRb;
    Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindFirstObjectByType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = (playerTransform.position - transform.position).normalized;
        enemyRb.AddForce(moveDirection * enemySpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Deadzone")
        {
            Destroy(gameObject);
        }

    }
}

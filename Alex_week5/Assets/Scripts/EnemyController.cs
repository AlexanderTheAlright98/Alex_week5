using UnityEngine;
using UnityEngine.Rendering;

public class EnemyController : MonoBehaviour
{
    public float enemySpeed;

    Rigidbody enemyRb;
    Transform playerTransform;
    bool canFollow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindFirstObjectByType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (canFollow)
        {
            Vector3 moveDirection = (playerTransform.position - transform.position).normalized;
            enemyRb.AddForce(moveDirection * enemySpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Deadzone")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        canFollow = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        canFollow = false;
    }
}

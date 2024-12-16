using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] float mouseSensitivity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseXInput = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * mouseSensitivity * Time.deltaTime * mouseXInput);

        float mouseYInput = Input.GetAxis("Mouse Y");
        transform.GetChild(0).Rotate(Vector3.right * mouseSensitivity * Time.deltaTime * -mouseYInput);
    }
}

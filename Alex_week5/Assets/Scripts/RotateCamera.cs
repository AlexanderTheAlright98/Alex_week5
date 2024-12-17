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

        //float mouseYInput = Input.GetAxis("Mouse Y");
        //transform.GetChild(0).Rotate(Vector3.right * mouseSensitivity * Time.deltaTime * -mouseYInput);
        //Mathf.Clamp(transform.GetChild(0).eulerAngles.x, -25, 25);

        Vector3 currentRotation = transform.GetChild(0).localEulerAngles;
        currentRotation.x += mouseSensitivity * Time.deltaTime * -Input.GetAxis("Mouse Y");
        if (currentRotation.x > 180) currentRotation.x -= 360;
        currentRotation.x = Mathf.Clamp(currentRotation.x, -25, 25);
        transform.GetChild(0).localEulerAngles = currentRotation;
    }
}

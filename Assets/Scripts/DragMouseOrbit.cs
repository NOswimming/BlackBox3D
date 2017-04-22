using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse drag Orbit with zoom")]
public class DragMouseOrbit : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 10.0f;
    public float ySpeed = 10.0f;
    public float yMinLimit = -90f;
    public float yMaxLimit = 90f;
    public float distanceMin = 3f;
    public float distanceMax = 100f;
    public float smoothTime = 2f;
    float rotationYAxis = 0.0f;
    float rotationXAxis = 0.0f;
    float velocityX = 0.0f;
    float velocityY = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationYAxis = angles.y;
        rotationXAxis = angles.x;
        // Make the rigid body not change rotation
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.freezeRotation = true;
        }
    }
    void LateUpdate()
    {
        if (target)
        {
            // Snap to rotation
            if (Input.GetKeyDown(KeyCode.UpArrow))
                rotationXAxis = SnapTo90(rotationXAxis + 90);
            if (Input.GetKeyDown(KeyCode.DownArrow))
                rotationXAxis = SnapTo90(rotationXAxis - 90);
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                rotationYAxis = SnapTo90(rotationYAxis + 90);
            if (Input.GetKeyDown(KeyCode.RightArrow))
                rotationYAxis = SnapTo90(rotationYAxis - 90);

            if (Input.GetMouseButton(1))
            {
                velocityX += xSpeed * Input.GetAxis("Mouse X") * distance * 0.02f;
                velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
            }
            rotationYAxis += velocityX;
            rotationXAxis -= velocityY;
            rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);
            Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
            Quaternion rotation = toRotation;

            /*
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit))
            {
                distance -= hit.distance;
            }
            */
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
            velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
            velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
        }
    }
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
    public static float SnapTo90(float angle)
    {
        if (angle % 90 >= 0 && angle % 90 < 45)
            return angle - angle % 90;
        if (angle % 90 >= 45)
            return angle - angle % 90 + 90;

        if (angle % 90 < 0 && angle % 90 > -45)
            return angle - angle % 90;
        if (angle % 90 <= -45)
            return angle - angle % 90 - 90;

        return angle;
    }
}
using UnityEngine;

public class ScriptCameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 1.8f, -4f);
    public float followSmooth = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desired = new Vector3(offset.x, offset.y, target.position.z + offset.z);

        transform.position = Vector3.Lerp(transform.position, desired, followSmooth * Time.deltaTime);
    }
}

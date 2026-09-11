using UnityEngine;

public class ScriptCoin : MonoBehaviour
{
    public float rotateSpeed = 120f;
    public float bobHeight = 0.15f;
    public float bobSpeed = 2f;

    private Vector3 startPos;

    void Start() => startPos = transform.position;
    
    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);

        float y = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(startPos.x, y, startPos.z);
    }
}

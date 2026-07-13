using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}

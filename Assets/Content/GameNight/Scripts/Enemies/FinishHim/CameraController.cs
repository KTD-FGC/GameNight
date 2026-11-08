using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float minX, maxX, minY, maxY;
    [SerializeField] GameObject _enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(Mathf.Clamp(_enemy.transform.position.x, minX, maxX),
            Mathf.Clamp(_enemy.transform.position.y, minY, maxY),
            this.transform.position.z);
    }
}

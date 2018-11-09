using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour {

    public Vector3 rotationSpeed = Vector3.one;
    
    void Update()
    {
        transform.Rotate(   (transform.rotation.x + rotationSpeed.x) * Time.deltaTime,
                            (transform.rotation.y + rotationSpeed.y) * Time.deltaTime,
                            (transform.rotation.z + rotationSpeed.z) * Time.deltaTime);
    }
}

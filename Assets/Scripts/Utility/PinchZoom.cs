//Adapted from Unity Learn Tutorials
using UnityEngine;
using UnityEngine.UI;

public class PinchZoom : MonoBehaviour
{

    public float zoomSpeed = .05f;

    public float minSize = .2f;
    public float maxSize = 3f;
    public GameObject targetObject;
    public ScrollRect scroll;

    void Update()
    {
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            scroll.enabled = false;

            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            targetObject.transform.localScale -= Vector3.one * (deltaMagnitudeDiff * zoomSpeed);
            if (targetObject.transform.localScale.x >= maxSize) targetObject.transform.localScale = Vector3.one * maxSize;
            if (targetObject.transform.localScale.x <= minSize) targetObject.transform.localScale = Vector3.one * minSize;
        }
        else
        {
            scroll.enabled = true;
        }
    }
}
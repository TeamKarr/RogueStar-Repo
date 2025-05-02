using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class cameraPosition : MonoBehaviour
{
    // Start is called before the first frame update

    [Tooltip("The player object")]
    public Transform player;

    public bool useMousePos = true;

    public float maxDistanceFromTarget = 5.0f;

    [Range(0, 0.75f)] public float freeCameraMouseTracking = 0.5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float z = transform.position.z;
        Vector3 m = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 t = player.transform.position;
        if (!useMousePos) {
            transform.position = new Vector3(t.x, t.y, z);
        } else
        {
            Vector3 desiredPosition = Vector3.Lerp(t, m, freeCameraMouseTracking);
            Vector3 difference = desiredPosition - t;
            difference = Vector3.ClampMagnitude(difference, maxDistanceFromTarget);
            Vector3 result = t + difference;
            transform.position = new Vector3(result.x, result.y, z);
          //super cool code  
        }
        
    }
}

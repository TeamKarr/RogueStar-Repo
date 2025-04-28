using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPointer : MonoBehaviour
{
    Health temp;
    public GameObject targetObject;
    private Vector2 rbpPosition, rbePosition;

    private float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    private Camera cam;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        temp = targetObject.GetComponent<Health>();
        
        //targetObject = this.transform.parent.gameObject;
        cam = Camera.main;

        // Get the size of the object (assuming SpriteRenderer or Renderer)
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Debug.Log(objectWidth + " " + objectHeight);
            objectWidth = sr.bounds.extents.x;
            objectHeight = sr.bounds.extents.y;
        }
    }

    void Update()
    {
        bool isdead = temp.isDead;
        if (isdead == true)
        {
            Destroy(this.gameObject);
        }

        Vector3 viewPos = cam.WorldToViewportPoint(targetObject.transform.position);

        bool isVisible = viewPos.x >= 0 && viewPos.x <= 1 &&
                         viewPos.y >= 0 && viewPos.y <= 1 &&
                         viewPos.z > 0;
        if (isVisible)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else if(!isVisible)
        {
            if (GetComponent<SpriteRenderer>().enabled == false)
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }
            Track();
        }
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;

        // Get camera bounds in world units
        Vector3 min = cam.ViewportToWorldPoint(new Vector3(0.025f, 0.025f, pos.z - cam.transform.position.z));
        Vector3 max = cam.ViewportToWorldPoint(new Vector3(0.975f, 0.975f, pos.z - cam.transform.position.z));

        // Clamp position so it doesn't go out of bounds
        pos.x = Mathf.Clamp(pos.x, min.x + objectWidth, max.x - objectWidth);
        pos.y = Mathf.Clamp(pos.y, min.y + objectHeight, max.y - objectHeight);

        transform.position = pos;
    }
    private void Track()
    {
        //target position
        rbpPosition = targetObject.transform.position;
        rbePosition = transform.position;
        // rotate pointer
        transform.Rotate(new Vector3(0, 0, 1), 1);
        Vector3 direction = ((rbpPosition - rbePosition)).normalized;
        transform.up = direction;

        // move pointer
        transform.position = Vector3.SmoothDamp(transform.position, targetObject.transform.position, ref velocity, smoothTime);

        float distanceToCamera = Vector3.Distance(targetObject.transform.position, transform.position);
        float scale = Mathf.Clamp(1 / distanceToCamera, 0.25f, 1f);
        transform.localScale = new Vector3(scale, scale, 1f);
    }
}
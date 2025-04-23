using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    [Tooltip("Whether to destroy child gameobjects when this gameobject is destroyed")]
    public bool destroyChildrenOnDeath = true;
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
        {
            if (destroyChildrenOnDeath && Application.isPlaying)
            {
                int childCount = transform.childCount;
                for (int i = childCount - 1; i >= 0; i--)
                {
                    GameObject childObject = transform.GetChild(i).gameObject;
                    if (childObject != null)
                    {
                        DestroyImmediate(childObject);
                    }
                }
            }
            transform.DetachChildren();
        }
    private void OnCollisionEnter2D()
    {
        Destroy(this.gameObject);
    }
}

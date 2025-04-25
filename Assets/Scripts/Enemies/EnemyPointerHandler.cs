using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPointerHandler : MonoBehaviour
{

    public EnemyPointer enemyPointer;

    // Start is called before the first frame update
    void Start()
    {
        var pointer = Instantiate(enemyPointer, transform.position, Quaternion.identity, transform);
        pointer.GetComponent<EnemyPointer>().targetObject = gameObject;
        
    }

}

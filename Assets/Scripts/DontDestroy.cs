using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static object Instance;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }


    // Update is called once per frame
        void Update()
    {
        
    }
}

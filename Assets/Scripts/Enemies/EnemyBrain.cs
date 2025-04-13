using System;
using Unity.Collections;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    // Start is called before the first frame update

    // list of states the enemny could be in

    // Each state has actions

    // Wander State
    // Chase State -can shoot
    // Attack State -if motion is part of attacking
    public int DefaultState = 0;

    [ReadOnly] public int CurrentState = 0;

    [ReadOnly] public string CurrentStateLabel;

    private State[] states;


    void Start()
    {
        
        states = GetComponents<State>();
        if (states != null)
        {
            foreach (var state in states)
            {
                Debug.Log(state);
            }
        } else
        {
            Debug.Log("did not find any states");
        }
        setState(DefaultState); // set default state
    }

    // Update is called once per frame
    void Update()
    {
        // run current state
        if (states != null)
        {
            states[CurrentState].Action(); // gets current state
        }
    }

    public void setState(int i)
    {
        if (i >= 0 && i < states.Length)
        {
            CurrentState = i;
            Debug.Log(i + states[i].GetType().ToString());
            CurrentStateLabel = states[i].GetType().ToString();
        }
            
    }

    public void setState<T>() where T : State
    {
        int i = indexOf<T>();
        setState(i);
    }

    private int indexOf<T>() where T : State
    {
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].GetType() == typeof(T))
            {
                return i;
            }
        }
        return -1;
    }

    public abstract class State : MonoBehaviour
    {
        public GameObject Player;

        public abstract void Action(); // ran everytick when in this state;


    }

    public void stopMoving()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    public string expAttribute = "Experience";
    Attribute experience;

    Rigidbody2D orb;//hehe thats funny(it stands for orb rigid body)
    public float speed = 5;
    public float radius = 10;

    // private GameObject player;
    private Vector2 rbpPosition, rboPosition;

    private GameObject Player;

    void Start()
    {
        experience = (GetComponent<AttributeManager>()).getAttribute(expAttribute);
        orb = GetComponent<Rigidbody2D>();

        //find the player object(not prefab)
        // GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        // foreach (GameObject obj in allObjects)
        // {
        //     if (obj.layer == 3)
        //     {
        //         player = obj;
        //     }
        // }
        Player = GameObject.FindGameObjectWithTag("Player");
    }



    void Update()
    {

        //player position
        rbpPosition = Player.transform.position;
        rboPosition = orb.position;
        
        //look at player
        transform.Rotate(new Vector3(0, 0, 1), 1);
        Vector3 direction = (rbpPosition - rboPosition).normalized;
        transform.up = direction;


        float setSpeed = speed / ((rbpPosition - rboPosition).magnitude / radius);
        //move the orb
        orb.velocity = transform.up * setSpeed;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerExperience playerExp = collision.gameObject.GetComponent<PlayerExperience>();
        if (collision.gameObject.layer != 9)
        {
            if (playerExp != null)
            {
                playerExp.ExpGain(experience.getvalue());
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

    }
}

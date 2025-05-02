using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    public string expAttribute = "Experience";
    Attribute experience;

    Rigidbody2D orb;//hehe thats funny(it stands for orb rigid body)
    public float maxSpeed;
    private GameObject player;
    private Vector2 rbpPosition, rboPosition;

    void Start()
    {
        experience = (GetComponent<AttributeManager>()).getAttribute(expAttribute);
        orb = GetComponent<Rigidbody2D>();

        //find the player object(not prefab)
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == 3)
            {
                player = obj;
            }
        }

    }



    void Update()
    {

        //player position
        rbpPosition = player.transform.position;
        rboPosition = orb.position;
        //look at player
        transform.Rotate(new Vector3(0, 0, 1), 1);
        Vector3 direction = ((rbpPosition - rboPosition)).normalized;
        transform.up = direction;

        //move the orb
        orb.AddForce(transform.up * .25f, ForceMode2D.Force);

        //max speed
        Vector2 velocity = orb.velocity;
        float currentSpeed = velocity.magnitude;

        if (currentSpeed > maxSpeed)
        {
            Vector2 normalizedVelocity = velocity.normalized;

            orb.velocity = normalizedVelocity * maxSpeed;
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    public string expAttribute = "Experience";
    Attribute experience;



    // Start is called before the first frame update
    void Start()
    {
        experience = (GetComponent<AttributeManager>()).getAttribute(expAttribute);
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

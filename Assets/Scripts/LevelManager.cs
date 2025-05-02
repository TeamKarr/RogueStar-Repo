using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;

    void Start()
    {
        var gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager != null)
        {
            var playerAttributes = player.GetComponent<AttributeManager>();
            if (playerAttributes != null)
            {
                foreach (var item in gameManager.defaultAttributes.Keys)
                {
                    playerAttributes.getAttribute(item).setBaseValue(gameManager.defaultAttributes[item].amount);
                }
            }
        }
    }


}

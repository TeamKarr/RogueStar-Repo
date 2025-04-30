using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;

    void Awake()
    {
        var gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager != null)
        {
            var playerAttributes = player.GetComponent<AttributeManager>();
            if (playerAttributes != null)
            {
                playerAttributes.getAttribute("MaxSpeed").setBaseValue(gameManager.MaxSpeed);
                playerAttributes.getAttribute("Acceleration").setBaseValue(gameManager.Acceleration);
                playerAttributes.getAttribute("RotationSpeed").setBaseValue(gameManager.RotationSpeed);
                playerAttributes.getAttribute("Damage").setBaseValue(gameManager.Damage);
                playerAttributes.getAttribute("FireRate").setBaseValue(gameManager.FireRate);
                playerAttributes.getAttribute("MaxHealth").setBaseValue(gameManager.MaxHealth);
            }
        }
    }


}

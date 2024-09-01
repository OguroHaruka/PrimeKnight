using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GamaManagerScript : MonoBehaviour
{
    bool playerDead = PlayerScript.isDead;
    bool enemyDead = EnemyScript.isDead;

    public GameObject victoryText;
    public GameObject failedText;
    public GameObject spaceKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDead == true)
        {
            failedText.SetActive(true);
            spaceKey.SetActive(true);
        }else if(enemyDead == true)
        {
            victoryText.SetActive(true);
            spaceKey.SetActive(true);
        }
    }
}

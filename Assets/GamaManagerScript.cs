using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GamaManagerScript : MonoBehaviour
{

    public GameObject victoryText;
    public GameObject failedText;
    public GameObject spaceKey;

    // Start is called before the first frame update
    void Start()
    {
        failedText.SetActive(false);
        spaceKey.SetActive(false);
        victoryText.SetActive(false);
        PlayerScript.isDead = false;
        BossScript.isDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerScript.isDead == true)
        {
            failedText.SetActive(true);
            spaceKey.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
        else if(BossScript.isDown == true)
        {
            victoryText.SetActive(true);
            spaceKey.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
    }
}

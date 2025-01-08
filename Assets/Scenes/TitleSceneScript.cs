using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleSceneScript : MonoBehaviour
{

    //Image fade;
    //float fadeSpeed = 0.2f;
    //float alpha;

    // Start is called before the first frame update
    void Start()
    {
        //fade = GetComponent<Image>();
        //fade.enabled = false;
        //alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //FadeIn();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            SceneManager.LoadScene("SampleScene");
        }
    }

    void FadeIn()
    {
        //alpha -= fadeSpeed;
        //fade.color = new Color(0, 0, 0,alpha);
    }

    void FadeOut()
    {

    }

}

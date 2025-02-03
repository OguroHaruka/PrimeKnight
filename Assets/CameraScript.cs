using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject player;
    public float rotateSpeed = 2.0f;
    private Vector3 offset;

    Vector3 currentPos;
    Vector3 pastPos;

    void Start()
    {
        this.player = GameObject.Find("Player");
        pastPos = player.transform.position;
        GetComponent<PostEffectAttacher>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerScript.isDead == true)
        {
            GetComponent<PostEffectAttacher>().enabled = true;
        }

        currentPos = player.transform.position;
        offset = currentPos - pastPos;
        transform.position = Vector3.Lerp(transform.position,transform.position + offset,1.0f);
        pastPos = currentPos;
        rotateCamera();
    }

    private void rotateCamera()
    {
        Vector3 angle = new Vector3(Input.GetAxis("Mouse X") * rotateSpeed, 0, 0);
        transform.RotateAround(player.transform.position, Vector3.up, angle.x);
    }
}

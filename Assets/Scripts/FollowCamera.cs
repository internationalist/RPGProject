using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    private Transform cameraArm;
    public float camClimbSpeed, minCamHeight, maxCamHeight;
    public Transform player;

	// Use this for initialization
	void Start () {
        cameraArm = transform.parent.transform;
    }
	
	void Update ()
    {
        //Pan the camera around the player with horizontal mouse move or controller stick move on x-axis.
        cameraArm.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X"), 0);
        //Track the player using the camera arm with a vertical height of .5m.
        cameraArm.transform.position = player.position + Vector3.up * .5f;

        //Calculate the camera height on mouse move on y-axis.
        float camHeightY = Input.GetAxis("Mouse Y") * Time.deltaTime * camClimbSpeed * -1;
        
        //Camera height is clamped between minCamHeight and maxCamHeight from editor.
        if (ClampCameraHeight(camHeightY))
        {
            transform.position += new Vector3(0, camHeightY, 0);
        }
    }

    private void LateUpdate()
    {
        //rotate the camera towards the cameraArm, bound to the player.
        transform.LookAt(cameraArm);
    }

    private bool ClampCameraHeight(float camHeightY)
    {
        return (transform.position.y > minCamHeight || camHeightY > 0) && (transform.position.y < maxCamHeight || camHeightY < 0);
    }
}

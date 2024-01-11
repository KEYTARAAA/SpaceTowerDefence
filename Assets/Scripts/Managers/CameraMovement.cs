using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static statics;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float panSpeed = 30f, rotateSpeed = 10f, borderThickness = 10f, scrollSpeed = 5f, minY = 10f, maxY = 80f, rotationMinX = 1f, rotationMaxX = 70f;
    [SerializeField] CAMERAMODE mode = CAMERAMODE.PAN;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mode = CAMERAMODE.LOCK;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            mode = CAMERAMODE.PAN;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            mode = CAMERAMODE.ROTATE;
        }

        if (mode == CAMERAMODE.LOCK)
        {
            return;
        }

        if (mode == CAMERAMODE.PAN) {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - borderThickness)
            {
                transform.Translate(transform.forward * panSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= borderThickness)
            {
                transform.Translate(-transform.forward * panSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - borderThickness)
            {
                transform.Translate(transform.right * panSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= borderThickness)
            {
                transform.Translate(-transform.right * panSpeed * Time.deltaTime, Space.World);
            }
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;


        if (mode == CAMERAMODE.ROTATE)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - borderThickness)
            {
                transform.Rotate( new Vector3(-rotateSpeed, 0,0));
                //transform.Rotate(new Vector3(transform.rotation.x + (rotateSpeed), transform.rotation.y, transform.rotation.z));
                //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= borderThickness)
            {
                transform.Rotate( new Vector3(rotateSpeed, 0,0));
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - borderThickness)
            {
                transform.Rotate(new Vector3( 0,rotateSpeed, 0));
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= borderThickness)
            {
                transform.Rotate(new Vector3( 0, -rotateSpeed, 0));
            }

            Vector3 rot = transform.localEulerAngles;

            if (rot.x < rotationMinX || rot.x > 300f)
            {
                rot.x = rotationMinX;
            }
            else if (rot.x > rotationMaxX)
            {
                rot.x = rotationMaxX;
            }
            rot.z = 0;
            //rot.x = Mathf.Clamp(rot.x, rotationMinX, rotationMaxX);
            transform.localEulerAngles = rot;
        }
    }

    public void setModeLock()
    {
        mode = CAMERAMODE.LOCK;
    }
    public void setModePan()
    {
        mode = CAMERAMODE.PAN;
    }
    public void setModeRotate()
    {
        mode = CAMERAMODE.ROTATE;
    }
}

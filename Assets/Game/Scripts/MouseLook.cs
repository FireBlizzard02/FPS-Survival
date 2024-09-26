using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private Transform lookRoot;

    [SerializeField]
    private float sensivity = 5f;

    [SerializeField]
    private Vector2 default_Look_Limits = new Vector2(-70f, 80f);

    private Vector2 look_Angles;
    private Vector2 current_Mouse_Look;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        LockAndUnlockCursor();
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }
    void LockAndUnlockCursor()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (Cursor.lockState == CursorLockMode.Locked)
            {

                Cursor.lockState = CursorLockMode.None;


            }
            else
            {

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

            }

        }

    } // lock and unlock

    void LookAround()
    {

        current_Mouse_Look = new Vector2(
            Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        look_Angles.x += current_Mouse_Look.x * sensivity *-1;
        look_Angles.y += current_Mouse_Look.y * sensivity;

        look_Angles.x = Mathf.Clamp(look_Angles.x, default_Look_Limits.x, default_Look_Limits.y);

        //current_Roll_Angle =
        //Mathf.Lerp(current_Roll_Angle, Input.GetAxisRaw(MouseAxis.MOUSE_X)
        //* roll_Angle, Time.deltaTime * roll_Speed);

        lookRoot.localRotation = Quaternion.Euler(look_Angles.x, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, look_Angles.y, 0f);


    } // look around
}

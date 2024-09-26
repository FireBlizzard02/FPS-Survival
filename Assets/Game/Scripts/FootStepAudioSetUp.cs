using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepAudioSetUp : MonoBehaviour
{
    private CharacterController characterController;
    private float accumulated_Distance;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
        {
            if (characterController.velocity.sqrMagnitude > 0)
            {
                accumulated_Distance += Time.deltaTime;
                if (accumulated_Distance > .5f)
                {
                    AudioManager._instance.PlayFootStepAtDirt();
                    accumulated_Distance = 0;
                }

            }
        }
        else
        {
            accumulated_Distance = 0;
        }
    }
}

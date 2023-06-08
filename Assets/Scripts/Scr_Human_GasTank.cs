using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Human_GasTank : MonoBehaviour
{
    float speed = 5;
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        Move();
    }
    void Update()
    {

    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal") * speed;
        float y = Input.GetAxis("Vertical") * speed;
        Vector2 motion = new Vector2(x, y) * Time.deltaTime;
        characterController.Move(motion);
    }
}

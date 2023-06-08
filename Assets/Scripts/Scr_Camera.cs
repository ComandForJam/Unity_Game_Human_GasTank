using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Camera : MonoBehaviour
{
    public Transform _player;
    public Transform _chainsaw;
    Camera _camera;

    float speed = 5;
    Vector2 motion;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        motion = ((_player.position + _chainsaw.position) / 2) - transform.position;
        motion *= speed * Time.fixedDeltaTime;
        transform.Translate(motion);

        float distance = Vector2.Distance(_player.position, _chainsaw.position) / 1.6f;
        distance = Mathf.Clamp(distance, 4, 8);
        _camera.orthographicSize = distance;
    }
}

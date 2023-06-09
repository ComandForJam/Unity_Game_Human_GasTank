using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Camera : MonoBehaviour
{
    public Transform _player;
    public Transform _chainsaw;
    Camera _camera;

    public bool isScaleActive = true;

    float speed = 5;
    Vector2 motion;
    float targetSize = 5;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Scale();
    }
    private void FixedUpdate()
    {
        Move();
        if (targetSize != _camera.orthographicSize)
        {
            _camera.orthographicSize += (targetSize - _camera.orthographicSize) / 10;
        }
    }
    void Move()
    {
        motion = ((_player.position + _chainsaw.position) / 2) - transform.position;
        motion *= speed * Time.fixedDeltaTime;
        transform.Translate(motion);
    }
    void Scale()
    {
        if (isScaleActive)
        {
            /*
            float distance = Vector2.Distance(_player.position, _chainsaw.position) / 1.6f;
            distance = Mathf.Clamp(distance, 4, 8);
            _camera.orthographicSize = distance;*/
        }
        
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            targetSize = _camera.orthographicSize - scroll * 25;
            targetSize = Mathf.Clamp(targetSize, 3, 8);
        }
        
    }
}

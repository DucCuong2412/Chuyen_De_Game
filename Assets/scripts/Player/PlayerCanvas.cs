using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera camera;

    void Start()
    {
        camera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cam = camera.transform.position;
        transform.LookAt(cam);
        cam.z = 0;
        cam.y = 0;

    }
}

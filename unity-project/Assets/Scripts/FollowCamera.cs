using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, 5);

    private Transform cameraTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        cameraTransform.position = target.position + (target.rotation * offset);
        cameraTransform.LookAt(target);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;

    [SerializeField] private Transform camLoc;
    [SerializeField] private Transform ship;
    [SerializeField] private Vector3 vel = Vector3.zero;


    void FixedUpdate()
    {
        transform.position = camLoc.position;
        transform.rotation = MyQuat.SLERP(transform.rotation, ship.rotation, Time.deltaTime * 10f);
        
    }
}

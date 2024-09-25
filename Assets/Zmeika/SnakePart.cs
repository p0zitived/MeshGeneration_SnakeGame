using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePart : MonoBehaviour
{
    public Transform transformToFollow;
    public float targetDistanceToFollower = 1;

    public void SetUp(Transform toFollow,float targetDistance)
    {
        transformToFollow = toFollow;
        targetDistanceToFollower = targetDistance;

        transform.position = toFollow.position + toFollow.forward * -targetDistance;
        transform.rotation = Quaternion.LookRotation(transformToFollow.position - transform.position);
    }

    private void Update()
    {
        Vector3 dir = transformToFollow.position - transform.position;
        transform.position = transform.position + dir.normalized * (dir.magnitude - targetDistanceToFollower);
        transform.rotation = Quaternion.LookRotation(transformToFollow.position - transform.position);
    }
}

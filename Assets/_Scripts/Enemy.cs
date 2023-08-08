using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float distanceToEnd = 0;

    private void Update()
    {
        distanceToEnd = GetComponent<FollowPath>().RemainingDistance();
    }
}

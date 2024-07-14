using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float minX, maxX;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    void FixedUpdate()
    {
        if(player.position.x >= transform.position.x) {
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), 0, transform.position.z);
    }
}

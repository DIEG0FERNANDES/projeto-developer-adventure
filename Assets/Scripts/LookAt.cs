using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Look = transform.InverseTransformPoint(Target.transform.position);
        float Angel = Mathf.Atan2(Look.y, Look.x) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, 1);
    }
}

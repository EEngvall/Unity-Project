using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
public GameObject target;
public Vector3 offset;
public float focusSpeed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 2, -3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, Time.deltaTime * focusSpeed);
    }
}

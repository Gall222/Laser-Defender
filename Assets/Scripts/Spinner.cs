using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField]float spinSpeed = 360f;
    // Update is called once per frame
    void Update()
    {
        //rotation++;
        //gameObject.transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime, Space.Self);
        //gameObject.transform.Rotate(new Vector3(0,0, spinSpeed * Time.deltaTime));
    }
}

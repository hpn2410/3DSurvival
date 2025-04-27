using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamaera : MonoBehaviour
{
    private Transform localTransform;
    // Start is called before the first frame update
    void Start()
    {
        localTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Camera.main)
        {
            localTransform.LookAt(2 * localTransform.position - Camera.main.transform.position);
        }
    }
}

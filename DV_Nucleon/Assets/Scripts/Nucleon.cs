using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Nucleon : MonoBehaviour
{

    public float attractionForce;
    Rigidbody body;
    LineRenderer lineRenderer;
    Material material;

    // Use this for initialization
    void Awake()
    {
        body = GetComponent<Rigidbody>();
        lineRenderer =GetComponent<LineRenderer>();
     
        material= new Material(Shader.Find("Standard"));
        lineRenderer.material = material;


    }
    
    void FixedUpdate()
    {
        body.AddForce(transform.localPosition * (-attractionForce));
        lineRenderer.SetPosition(1,transform.localPosition);
        lineRenderer.widthMultiplier = 0.1f;
    }
    // Update is called once per frame


}


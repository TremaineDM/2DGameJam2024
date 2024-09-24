using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody rigidBody;
    public float strikeForce;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        //rigidBody.AddForce(InitialForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StrikeBall(strikeForce);
        }
        float sumValue = Sum(2.1f, 9.3f);

    }
    float Sum(float A, float B)
    {
        return A + B;
    }
    public void StrikeBall(float strength = 1.0f) 
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Math.Abs(transform.position.y - Camera.main.transform.position.y));
        Vector3 mouseToWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 mouseToBall = transform.position - mouseToWorld;
        mouseToBall = mouseToBall.normalized * strength ;
        rigidBody.AddForce(mouseToBall, ForceMode.Impulse);
    }
}

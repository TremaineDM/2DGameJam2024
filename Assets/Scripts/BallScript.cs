using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    private Rigidbody rigidBody;
    public float strikeForce = 10.0f;
    public Slider powerSlider;
    public ContactPointScript contactPointScript;
    public Vector3 TorqueTest;

    private bool HasPressedButton;


    // Sphere with radius of 1
    private static readonly Vector4[] s_UnitSphere = MakeUnitSphere(16);

    private static Vector4[] MakeUnitSphere(int len)
    {
        Debug.Assert(len > 2);
        var v = new Vector4[len * 3];
        for (int i = 0; i < len; i++)
        {
            var f = i / (float)len;
            float c = Mathf.Cos(f * (float)(Math.PI * 2.0));
            float s = Mathf.Sin(f * (float)(Math.PI * 2.0));
            v[0 * len + i] = new Vector4(c, s, 0, 1);
            v[1 * len + i] = new Vector4(0, c, s, 1);
            v[2 * len + i] = new Vector4(s, 0, c, 1);
        }
        return v;
    }

    private Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HasPressedButton = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = startingPosition;
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        if(HasPressedButton)
        {
            StrikeBall(powerSlider.value * strikeForce);
            HasPressedButton = false;
        }
    }

    public void StrikeBall(float strength = 1.0f) 
    {
        //rigidBody.AddForceAtPosition(,contactPointScript.relativeContactPoint);

        
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Math.Abs(transform.position.y - Camera.main.transform.position.y));
        Vector3 mouseToWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 mouseToBall = transform.position - mouseToWorld;
        mouseToBall = mouseToBall.normalized * strength;

        Vector3 WorldTorque = new Vector3(contactPointScript.relativeContactPoint.y, contactPointScript.relativeContactPoint.x, 0.0f);
        Quaternion Direction = Quaternion.LookRotation(mouseToBall.normalized);
        WorldTorque = Direction * WorldTorque;
        WorldTorque *= 1000.0f;

        Vector3 ContactPoint = transform.position;
        rigidBody.AddForceAtPosition(mouseToBall, ContactPoint, ForceMode.Impulse);
        rigidBody.AddRelativeTorque(TorqueTest, ForceMode.VelocityChange);


        Debug.DrawLine(ContactPoint - mouseToBall * 2.0f, ContactPoint, Color.red, 5.0f, false);
        DrawSphere(ContactPoint, 0.5f, Color.red);
        //rigidBody.AddTorque(0, strength, 0, ForceMode.Impulse);
    }

    public static void DrawSphere(Vector4 pos, float radius, Color color)
    {
        Vector4[] v = s_UnitSphere;
        int len = s_UnitSphere.Length / 3;
        for (int i = 0; i < len; i++)
        {
            var sX = pos + radius * v[0 * len + i];
            var eX = pos + radius * v[0 * len + (i + 1) % len];
            var sY = pos + radius * v[1 * len + i];
            var eY = pos + radius * v[1 * len + (i + 1) % len];
            var sZ = pos + radius * v[2 * len + i];
            var eZ = pos + radius * v[2 * len + (i + 1) % len];
            Debug.DrawLine(sX, eX, color, 1.0f, false);
            Debug.DrawLine(sY, eY, color, 1.0f, false);
            Debug.DrawLine(sZ, eZ, color, 1.0f, false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_controller : MonoBehaviour
{
    private inputManager Keyboards;
    public WheelCollider[] wheels = new WheelCollider[4];
    public GameObject[] wheelMesh = new GameObject[4];
    private Rigidbody carRigidbody;
    private GameObject centerOfMass;
    public float KPH;
    public float brakePower;
    public float DownForceValue = 50;
    public float radius = 6;
    public int motorTorque = 100;
    public float steeringMax = 4;

    public float[] slip = new float[4];


    void Start()
    {
        getObject();
    }

    private void FixedUpdate()
    {
        animateWheels();
        moveVehicle();
        addDownForce();
        getFriction();

    }

    private void moveVehicle()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].motorTorque = Keyboards.vertical * motorTorque;
        }

        for (int i = 0; i < wheels.Length - 2; i++)
        {
            wheels[i].steerAngle = Keyboards.horizontal * steeringMax;
        }

        if (Keyboards.handbrake)
        {
            wheels[2].brakeTorque = brakePower;
            wheels[3].brakeTorque = brakePower;
        }
        else
        {
            wheels[2].brakeTorque = 0;
            wheels[3].brakeTorque = 0;
        }

        KPH = carRigidbody.velocity.magnitude * 3.6f;
    }

    private void steerVehicle()
    {
        if (Keyboards.horizontal > 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * Keyboards.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * Keyboards.horizontal;
        }
        else if (Keyboards.horizontal < 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * Keyboards.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * Keyboards.horizontal;
        }
        else
        {
            wheels[0].steerAngle = 0;
            wheels[1].steerAngle = 0;
        }
    }

    private void animateWheels()
    {
        Vector3 wheelPosition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].GetWorldPose(out wheelPosition, out wheelRotation);
            wheelMesh[i].transform.rotation = wheelRotation;
            wheelMesh[i].transform.position = wheelPosition;
        }
    }

    private void getObject()
    {
        Keyboards = GetComponent<inputManager>();
        carRigidbody = GetComponent<Rigidbody>();
        centerOfMass = GameObject.Find("mass");
        carRigidbody.centerOfMass = centerOfMass.transform.localPosition;
    }

    private void addDownForce()
    {
        carRigidbody.AddForce(-transform.up * DownForceValue * carRigidbody.velocity.magnitude);
    }

    private void getFriction()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            WheelHit wheelHit;
            wheels[i].GetGroundHit(out wheelHit);

            slip[1] = wheelHit.sidewaysSlip;
        }
    }
}

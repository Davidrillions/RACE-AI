using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public car_controller playerVision;

    public GameObject arrow ;
    private float startPosition = 220f, endPosition = -41;
    private float desiredPosition;

    public float vehicleSpeed;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        vehicleSpeed = playerVision.KPH;
        updateArrow();
    }

    public void updateArrow()
    {
        desiredPosition = startPosition - endPosition;
        float temp = vehicleSpeed / 180;
        arrow.transform.eulerAngles = new Vector3(0, 0, startPosition - temp * desiredPosition);

    }
}

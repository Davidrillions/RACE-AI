using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputManager : MonoBehaviour
{
    internal enum driver
    {
        AI_race,
        keyboard_race,
        mobile_race
    }
    [SerializeField] driver driveController;

    public float vertical ;
    public float horizontal ;
    public bool handbrake ;

    private void FixedUpdate()
    {
        switch (driveController)
        {
            case driver.AI_race:
                break;
            case driver.keyboard_race: keyboardDrive();
                break;
            case driver.mobile_race:
                break;
        }

    }

    public TrackPoints waypoints;
    public List<Transform> nodes = new List<Transform>();

    private void Awake()
    {
        waypoints = GameObject.FindGameObjectWithTag("path").GetComponent<TrackPoints>();
        nodes = waypoints.nodes;
    }

    private void AIDrive()
    {

    }

    private void keyboardDrive()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        handbrake = Input.GetAxis("Jump") != 0;
    }

    private void mobileDrive()
    {

    }

    private void calculateDistance()
    {

    }
}

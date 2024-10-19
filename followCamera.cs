using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    private GameObject Player;
    private GameObject child;
    private GameObject cameralookAt;
    private car_controller playerVision;
    public float speed;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        child = Player.transform.Find("camera constraint").gameObject;
        cameralookAt = Player.transform.Find("camera lookAt").gameObject;
        playerVision = Player.GetComponent<car_controller>();
    }

    private void FixedUpdate()
    {
        follow();

        speed = (playerVision.KPH >= 80) ? 20 : playerVision.KPH / 4;
    }

    private void follow()
    {
        gameObject.transform.position = Vector3.Lerp(transform.position, child.transform.position, Time.deltaTime * speed);
        gameObject.transform.LookAt(Player.gameObject.transform.position);
    }
}

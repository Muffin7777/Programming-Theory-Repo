using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(10.7F, 1.43F, 8.5F);

    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("playerAnimal");
        player = players[0];
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}

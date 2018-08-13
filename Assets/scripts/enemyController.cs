using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public int MoveSpeed = 5;

    public Transform Player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
    }
}
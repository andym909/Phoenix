using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAttack : MonoBehaviour {

    public GameObject player;
    public Transform target;
    public float speed = 2f;
    private float minDistance = 1f;
    private float range;

	// Use this for initialization
	void Start () {
        target = player.transform;
	}
	
	// Update is called once per frame
	void Update () {
        range = Vector2.Distance(transform.position, target.position);
        if(range > minDistance)
        {
            Debug.Log(range);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
	}
}

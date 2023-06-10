using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreGround : MonoBehaviour
{
    public GameObject Ground;

    private void Start()
    {
        Ground = GameObject.Find("ground");
        Physics.IgnoreCollision(Ground.GetComponent<Collider>(),gameObject.transform.GetComponent<Collider>());
    }
    
}

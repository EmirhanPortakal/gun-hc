
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

	public GameObject destroyedVersion;

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Bullet" || other.gameObject.tag == "Player"){
			Instantiate(destroyedVersion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}

}

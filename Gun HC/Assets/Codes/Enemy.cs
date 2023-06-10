using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float Health;
    public float HealthMax;
    public GameObject sliderObject;
    private Slider slider;

    
    public float bodyDamage = 34;
    public float speed = 5f;
    public float HSDMG = 100;

    void Start()

    {
        HealthMax = 100;
        Health = HealthMax;

        if (sliderObject == null)
        {
            Debug.LogError("SliderObject is not assigned!");
            return;
        }
        else
        {
            Debug.Log("sliderObject is not null");
        }

        slider = sliderObject.GetComponent<Slider>();
        if (slider == null)
        {
            Debug.LogError("Could not find Slider component on " + sliderObject.name);
            return;
        }
        else
        {
            Debug.Log("slider is not null");
        }
    }

    void Update()
    {
        Vector3 movementDirection = new Vector3(0f, 0f, -1f);
        movementDirection = movementDirection.normalized;
        Vector3 velocity = movementDirection * speed;
        transform.Translate(velocity * Time.deltaTime);
        slider.value = Health;

        if(Health <=0){
           Destroy(gameObject);  
        }
    }

    public void GetBodyDamage()
    {
       
        Health -= bodyDamage;
    }
    public void GetHeadShotDamage(){
        Health -= HSDMG;
    }


    private void OnCollisionEnter(Collision collision) {
        
         if (collision.collider.gameObject.tag == "Bullet" || collision.collider.gameObject.tag == "Player") {
            // Deal damage to enemy
            if(GameObject.FindGameObjectWithTag("Player").GetComponent<GunSprint>().Headshotted){
               GetHeadShotDamage();
            }
            else{
                GetBodyDamage();
            }
        }
        
        
        
       
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private GameObject _trail;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private GameObject GunObj;
    private GameObject enemyObject;
    private Rigidbody _rb;
    public float damage = 34;

    

    private void Awake() 
    {
        _rb = GetComponent<Rigidbody>();
        GunObj = GameObject.FindGameObjectWithTag("Player");
        //_enemy = enemyObject.GetComponent<Enemy>();
        
    } 
    void Update(){
        
        enemyObject = GameObject.FindGameObjectWithTag("Enemy");
        // if (_hitsTarget) {
        //    _enemy.GetDamage(damage); 
        // }
    }

    public void Init(Vector3 vel) {
        _rb.velocity = vel;
        //if (_hitsTarget) _trail.SetActive(true);
    }
    

    private void OnTriggerEnter(Collider other) {
       // Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        
            
        
        Destroy(gameObject);
    }
}

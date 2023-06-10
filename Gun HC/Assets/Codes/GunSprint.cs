using UnityEngine;

public class GunSprint : MonoBehaviour {
    [SerializeField] private Bullet _bulletPrefab; 
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private ParticleSystem _smokeSystem;
    [SerializeField] private LayerMask _targetLayer;
    private AudioSource audioSource;

    [SerializeField] private float _bulletSpeed = 12;
    [SerializeField] private float _torque = 120;
    [SerializeField] private float _maxTorqueBonus = 150;

    [SerializeField] private float _maxAngularVelocity = 10;

    [SerializeField] private float _forceAmount = 600;
    [SerializeField] private float _maxUpAssist = 30;

    [SerializeField] private float _smokeLength = 0.5f;

    [SerializeField] private float _maxY = 10;

    [SerializeField] private Enemy _enemy;
    
    private Rigidbody _rb;
    private float _lastFired;
    private bool _fire;

    public bool Headshotted;
    
    public GameObject Ground;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        // _enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
    }

    void Update() {
        // Clamp max velocity
        _rb.angularVelocity = new Vector3(0, 0, Mathf.Clamp(_rb.angularVelocity.z, -_maxAngularVelocity, _maxAngularVelocity));
        
        //rotate spawnpoint as well.
        _spawnPoint.transform.rotation = gameObject.transform.rotation;

        if (Input.GetMouseButtonDown(0)) {
            
            // Check if on target
            var hitsTarget = Physics.Raycast(_spawnPoint.position, _spawnPoint.forward, float.PositiveInfinity, _targetLayer);
            RaycastHit hit;
            if (Physics.Raycast(_spawnPoint.position, _spawnPoint.forward, out hit))
            {
                if (hit.collider != null && hit.collider.gameObject.GetComponent<EnemyHeadCollider>() != null)
                {
                    Debug.Log("Headshot!");
                    // _enemy.GetHeadShotDamage();
                    // Do something to the enemy, e.g. increase score
                    Headshotted = true;
                }
                else
                {
                    // _enemy.GetBodyDamage();
                    Headshotted = false;
                    Debug.Log("Body shot!");
                }
            
            }
            //if (hitsTarget) GameManager.Instance.ToggleSlowMo(true);

            // Spawn
            var bullet = Instantiate(_bulletPrefab, _spawnPoint.position, _spawnPoint.rotation);
            bullet.Init(_spawnPoint.forward * _bulletSpeed);
            _smokeSystem.Play();
            _lastFired = Time.time;

            // Apply force - More up assist depending on y position
            var assistPoint = Mathf.InverseLerp(0, _maxY, _rb.position.y);
            var assistAmount = Mathf.Lerp(_maxUpAssist, 0, assistPoint);
            var forceDir = -transform.forward * _forceAmount + Vector3.up * assistAmount;
            if (_rb.position.y > _maxY) forceDir.y = Mathf.Min(0, forceDir.y);
            _rb.AddForce(forceDir);

            // Determine the additional torque to apply when swapping direction
            var angularPoint = Mathf.InverseLerp(0, _maxAngularVelocity, Mathf.Abs(_rb.angularVelocity.z));
            var amount = Mathf.Lerp(0, _maxTorqueBonus, angularPoint);
            var torque = _torque + amount;
            
            // Apply torque
            var dir = Vector3.Dot(_spawnPoint.forward, Vector3.right) < 0 ? Vector3.back : Vector3.forward;
            _rb.AddTorque(dir * torque);

            audioSource.Play();
        }

        if (_smokeSystem.isPlaying && _lastFired + _smokeLength < Time.time) _smokeSystem.Stop();
    }
}

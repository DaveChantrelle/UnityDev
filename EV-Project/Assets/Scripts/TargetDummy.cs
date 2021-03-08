using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour, IDamagable
{
    GameObject _target;
    float _health = 500;
    [SerializeField]
    float _speed = 30;
    float _targetOffset = 10;
    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Vehicle");
    }

    // Update is called once per frame
    void Update()
    {
        /*//transform.LookAt(_target.transform);
        // Set step size to a rotation speed times frame time.
        float _singleStep = 100 * Time.deltaTime;

        // Rotate the up vector towards the target direction by one step
        Vector3 _direction = Vector3.RotateTowards(transform.up, (_target.transform.position - transform.position).normalized, _singleStep, 0f);

        //zero out the z value
        _direction.z = 0;

        // set the transform look rotation
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _direction);
        // move to target
        if(Vector3.Distance(transform.position,_target.transform.position)> _targetOffset)
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
        Vector3 tp = transform.position;
        transform.position = new Vector3(tp.x, tp.y, 0);
        transform.localEulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        
        */
        if (_health <= 0)
        {
            Destroy(gameObject);
        }

    }
    public void Damage(float _incomingDamage)
    {
        _health -= _incomingDamage;
        transform.localScale *= 1.1f;
        GetComponent<MeshRenderer>().material.color = Color.red;
        StartCoroutine(RegisterHitCoroutine());
    }
    public IEnumerator RegisterHitCoroutine()
    {
        yield return new WaitForSeconds(1);
        GetComponent<MeshRenderer>().material.color = Color.white;
    }
}

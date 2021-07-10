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

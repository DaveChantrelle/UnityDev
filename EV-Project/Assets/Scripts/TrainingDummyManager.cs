using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDummyManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] dummys;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTrainingWave());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnTrainingWave()
    {
        yield return new WaitForSeconds(3);
        GameObject d;
        bool DisNull()
        {
            return (d == null);
        }
        for(int i = 0; i < dummys.Length; i++)
        {
            d = Instantiate(dummys[i], transform);
            yield return new WaitForSeconds(1);
            d = Instantiate(dummys[i], transform);
            yield return new WaitUntil(() => DisNull());
        }
        
    }
}

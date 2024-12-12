using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    [SerializeField]
    EnemyController _bat1;
    [SerializeField]
    EnemyController _bat2;
    [SerializeField]
    EnemyController _bat3;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnBat()
    {
        _bat1.gameObject.SetActive(true);
        _bat2.gameObject.SetActive(true);
        _bat3.gameObject.SetActive(true);
        _bat1.Init(new Vector2(-8, -11), null);
        _bat2.Init(new Vector2(86, -17), null);
        _bat3.Init(new Vector2(77, 20), null);
    }
    public void DeSpawnBat()
    {
        _bat1.gameObject.SetActive(false);
        _bat2.gameObject.SetActive(false);
        _bat3.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Pecas;
    private int idproxPeca; //Peça Seguinte
   
    void Start()
    {
        Spawn();
    }


    public void Spawn()
    {
        setPeca();
        Instantiate(Pecas[idproxPeca], transform.position, Quaternion.identity);


    }
    void setPeca()
    {
        idproxPeca = Random.Range(0, Pecas.Length);
    }
}

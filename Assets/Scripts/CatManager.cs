using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{
    public CatData WorkerAmount;
    public GameObject CatSpawner;
    // Start is called before the first frame update
    void Start()
    {
      int workers = Convert.ToInt32(WorkerAmount.amount);  
        for(int i = 0; i < workers; i++)
        {
            Instantiate(CatSpawner);
        }
    }

  
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CatScript : MonoBehaviour
{

    public bool isIdle = true;
    public enum WorkingOn { Mice,Catnip,Recruiting,None}
    public WorkingOn workingOn;
    public int maxMotivation;
    public int motivation;
    public bool willAbandon = false;
    public CatData CatnipStash;
    public CatData WorkerAmount;

    // Start is called before the first frame update

    void Start()
    {
        updateWorkerSO();
        //WorkerAmount.amount++;
        motivation = maxMotivation;
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void CatAbandon()
    {
        WorkerAmount.amount--;
        Destroy(gameObject);
    }
    public void GetCatnip()
    {
        if (CatnipStash.amount > 0)
        {
            CatnipStash.amount--;
            motivation = maxMotivation;
        }
        else
        {
            willAbandon = true;

        }
    }
    void updateWorkerSO()
    {
        GameObject[] workers = GameObject.FindGameObjectsWithTag("Worker");

        // Update the amount field in the WorkerAmount scriptable object
        WorkerAmount.amount = workers.Length;
    }
}

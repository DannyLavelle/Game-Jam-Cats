using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class Task : MonoBehaviour
{
    public GameObject catWorker;
    int idleWorkers = 0;
    int busyWorkers = 0;
    List<GameObject> assignedWorkers;
    public float baseTaskRequirements;//seconds for the task to complete 
    private float taskReuirements;
    public float taskProgress;
    private float workTimer;
    public TMP_Text workerDisplay;
    public enum TaskType { Mice, Catnip, Recruiting }
    public TaskType taskType;
    //public string miceObjectName = "Mice";
    public CatData miceData;
   public CatData CatnipData;
    [SerializeField]
    public Slider progressSlider;

    // Start is called before the first frame update
    void Start()
    {
        assignedWorkers = new List<GameObject>();

        taskReuirements = baseTaskRequirements;
    }

    // Update is called once per frame
    void Update()
    {
        workTimer += Time.deltaTime;
        if(workTimer >=1)//every second updates the task for the workers 
        {
            taskProgress += assignedWorkers.Count;
            if (taskProgress >= taskReuirements)
            {
                
                Debug.Log("finished task");
                //add resources to pool
                
                finishTask();
            }
            workTimer = 0;
        }
        progressSlider.value = taskProgress / taskReuirements;
    }

    public void addWorkers()
    {
        GameObject[] Workers = GameObject.FindGameObjectsWithTag("Worker");
        updateWorkerNumbers(Workers);
       
        if (idleWorkers >= 1)
        {
            //Debug.Log("Workers available: " + Workers.Length);
            assignWorker(Workers);
            updateWorkerDisplay();
            busyWorkers++;
            idleWorkers--;
        }
    }
    public void removeWorker()
    {
        if (assignedWorkers.Count > 0)
        {
            // Find the worker with the lowest motivation
            GameObject lowestMotivationWorker = FindWorkerWithLowestMotivation(assignedWorkers);

            // Remove the worker from the assigned workers list
            assignedWorkers.Remove(lowestMotivationWorker);

            // Update worker display and decrease idle/busy counters accordingly
            updateWorkerDisplay();
            busyWorkers--;
            idleWorkers++;

            // Optionally, reset the workingOn property for the removed worker
            CatScript cat = lowestMotivationWorker.GetComponent<CatScript>();
            cat.workingOn = CatScript.WorkingOn.None;
        }
    }

    GameObject FindWorkerWithLowestMotivation(List<GameObject> workers)
    {
        GameObject lowestMotivationWorker = null;
        float lowestMotivation = float.MaxValue;

        foreach (GameObject worker in workers)
        {
            CatScript cat = worker.GetComponent<CatScript>();

            // Compare motivation and update the lowestMotivationWorker if needed
            if (cat.motivation < lowestMotivation)
            {
                lowestMotivation = cat.motivation;
                lowestMotivationWorker = worker;
            }
        }

        return lowestMotivationWorker;
    }
    void updateWorkerDisplay()
    {
        workerDisplay.text= Convert.ToString(assignedWorkers.Count);
        
    }//takes workerDisplay, converts to int, adds amount , converts back to string, updates workerDisplay
    void updateWorkerNumbers(GameObject[] workers)//updates free workers  
    {
        idleWorkers = 0;
        foreach(GameObject worker in workers)
        {
            CatScript catScript = worker.GetComponent<CatScript>();
            if(catScript.workingOn == CatScript.WorkingOn.None)
            {
                Debug.Log(idleWorkers);
                idleWorkers++;
            }
           

        }
    }
    void assignWorker(GameObject[] Workers)
    {
      
        List<GameObject> sortedWorkers = SortWorkersByMotivation(Workers);
       sortedWorkers = PurgeBusyWorkers(sortedWorkers);
        if(sortedWorkers.Count > 0)
        {
            CatScript cat = sortedWorkers[0].GetComponent<CatScript>();
            switch (taskType)
            {
                case TaskType.Mice:
                cat.workingOn = CatScript.WorkingOn.Mice;

                assignedWorkers.Add(sortedWorkers[0]);
                break;
                case TaskType.Catnip:
                cat.workingOn = CatScript.WorkingOn.Catnip;
                assignedWorkers.Add(sortedWorkers[0]); ;
                break;
                case TaskType.Recruiting:
                cat.workingOn = CatScript.WorkingOn.Recruiting;
                assignedWorkers.Add(sortedWorkers[0]);
                break;


            }
            GameObject topworker = sortedWorkers[0];
        }
        
      
       
    }
    List<GameObject> SortWorkersByMotivation(GameObject[] workers)
    {
       
        List<GameObject> sortedWorkers = new List<GameObject>(workers);
        sortedWorkers.Sort((a, b) =>
        {
            CatScript catA = a.GetComponent<CatScript>();
            CatScript catB = b.GetComponent<CatScript>();
            return catB.motivation.CompareTo(catA.motivation);
        });

        return sortedWorkers;
    }

    void DecreaseMotivation()
    {
        //GameObject[] Workers = GameObject.FindGameObjectsWithTag("Worker");
        if(assignedWorkers.Count > 0)
        {
            foreach (GameObject worker in assignedWorkers)
            {
                CatScript cat = worker.GetComponent<CatScript>();
                if (taskType == TaskType.Mice)
                {
                    cat.motivation -= 3; // Decrease motivation by 3 for Mice task
                }
                else if (taskType == TaskType.Catnip)
                {
                    cat.motivation -= 8; // Decrease motivation by 8 for Catnip task
                }
                else if (taskType == TaskType.Recruiting)
                {
                    cat.motivation -= 10; // Decrease motivation by 10 for Recruiting task
                }
                Debug.Log("Cat Motivation : " + cat.motivation);
                if(cat.motivation <= 0)
                {
                    cat.GetCatnip();
                }
                if (cat.willAbandon)
                {


                    cat.workingOn = CatScript.WorkingOn.None;
                    assignedWorkers.Remove(worker);
                    updateWorkerDisplay();
                    cat.CatAbandon();
                }
            }
        }
       
    }
    void finishTask()
    {
        // Decrease motivation before updating resource amounts
       

        switch (taskType)
        {
            case TaskType.Mice:
            miceData.amount += 1;
            break;
            case TaskType.Catnip:
            CatnipData.amount += 1;
            break;
            case TaskType.Recruiting:
            Instantiate(catWorker);
            break;
        }
        DecreaseMotivation();
        // Reset workingOn property for workers assigned to this task
        //foreach (GameObject worker in assignedWorkers)
        //{
        //    CatScript cat = worker.GetComponent<CatScript>();
        //    cat.workingOn = CatScript.WorkingOn.None;
        //}
        taskProgress = 0;
        // Clear the list of assigned workers
      
    }
    //List<GameObject> PurgeBusyWorkers(List<GameObject> sortedWorkers)
    // {
    //     if(sortedWorkers.Count > 1)
    //     {
    //         foreach (GameObject Worker in sortedWorkers)
    //         {
    //             CatScript cat = Worker.GetComponent<CatScript>();
    //             if (cat.workingOn != CatScript.WorkingOn.None)
    //             {
    //                 sortedWorkers.Remove(Worker);
    //             }
    //         }
    //     }
    //     else if(sortedWorkers.Count == 1)
    //     {
    //         CatScript cat = sortedWorkers[0].GetComponent<CatScript>();
    //         if (cat.workingOn != CatScript.WorkingOn.None)
    //         {
    //             sortedWorkers.Remove(sortedWorkers[0]);
    //         }
    //     }

    //     return sortedWorkers;
    // }
    List<GameObject> PurgeBusyWorkers(List<GameObject> sortedWorkers)
    {
        sortedWorkers.RemoveAll(worker => worker.GetComponent<CatScript>().workingOn != CatScript.WorkingOn.None);
        return sortedWorkers;
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyUpdater : MonoBehaviour
{
    public CatData Currency;
    public TMP_Text curencyDisplay;
    public enum TaskType { Mice, Catnip,Workers}
    public TaskType Type;
    string pretext;
      
    void Start()
    {
       switch(Type)
        {
            case TaskType.Mice:
            pretext = ("Mice Caught :\r\n");
                break;
            case TaskType.Catnip:
            pretext = ("Catnip :\r\n");
            break ;
            case TaskType.Workers:
            pretext = ("Cats :\r\n");
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        curencyDisplay.text = ( pretext +Currency.amount.ToString());
    }
}

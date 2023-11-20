using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCat", menuName = "ScriptableObject/Currency")]
public class CatData : ScriptableObject
{
    public string Name;
    public float amount;

    
    
    // Add other properties as needed
}


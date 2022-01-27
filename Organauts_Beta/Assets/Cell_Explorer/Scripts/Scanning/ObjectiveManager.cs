using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{


    [SerializeField] private Organell[] objectives;
    private bool[] statuses;


    private void Start()
    {
        statuses = new bool[objectives.Length];
        //Create UI Objective list

    }

    public bool AlreadyAccomplished(Organell organelle)
    {
        int arrayIndex = ObjectiveIndexOf(organelle);

        if(arrayIndex < statuses.Length)
        {
            return statuses[arrayIndex];
        }

        else
        {
            Debug.Log( organelle.OrganelleName +  " is not in current level objectives list, please add it to the list or remove it from scene.");
            
            return true;
            //Returns true to prohibit the scanner from scanning an object that is not on the objective list.
        }
        
    }

    public void Accomplish(Organell organelle)
    {
        statuses[ObjectiveIndexOf(organelle)] = true;
    }

    private int ObjectiveIndexOf(Organell organelle)
    {
        int index = 0;

        foreach (Organell objective in objectives)
        {
            if (organelle == objective)
            {
                return index;
            }

            index++;
        }

        return index;
    }
}


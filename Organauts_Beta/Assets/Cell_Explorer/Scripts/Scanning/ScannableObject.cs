using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScannableObject : MonoBehaviour
{
    [SerializeField] private Organell organelleData;

    private bool rightHandScanning = false;
    private Scanner scanner;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("RightScanCube"))
        {
            rightHandScanning = true;
            CheckScan();
        }

        else if (other.gameObject.tag.Equals("LeftScanCube"))
        {
            scanner = other.GetComponent<Scanner>();
            CheckScan();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("RightScanCube"))
        {
            rightHandScanning = false;
            ScanInterrupted();
        }

        else if (other.gameObject.tag.Equals("LeftScanCube"))
        {
            ScanInterrupted();
            scanner = null;
        }
    }

    private void CheckScan()
    {
        if (scanner != null && rightHandScanning)
        {
            scanner.StartScan(this);
        }
    }

    private void ScanInterrupted()
    {
        if (scanner != null)
        {
            scanner.StopScan();
        }
    }

    public Organell GetOrganelleData()
    {
        return organelleData;
    }
}



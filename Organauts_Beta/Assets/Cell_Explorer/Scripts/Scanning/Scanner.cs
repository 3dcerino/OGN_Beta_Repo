using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private GameObject scanVFX;
    [SerializeField] private Light scanLight;
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private ObjectiveManager levelObjectives;
    [SerializeField] private Transform rightHandCube;

    [Header("Scanning Parameters")]
    [SerializeField] private float timeToScan = 2.5f;
    [SerializeField] private float lightStrobeSpeed = 1.3f;

    private bool scanning = false;
    private bool canInterrupt = true;
    private Coroutine scanRoutine;

    public void StartScan(ScannableObject scanTarget)
    {
        Debug.Log("Trying to scan " + scanTarget.GetOrganelleData().OrganelleName);

        if (levelObjectives.AlreadyAccomplished(scanTarget.GetOrganelleData()))
        {
            Debug.Log(scanTarget.GetOrganelleData().OrganelleName + " already scanned");

            //Insert here what happens when user attempts to scan an already scanned organelle
        }

        else
        {
            scanLight.transform.position = scanTarget.transform.position + scanTarget.GetOrganelleData().LightPositionOffset;
            scanRoutine = StartCoroutine(Scan(scanTarget.GetOrganelleData()));
        }

    }

    public void StopScan()
    {
        if (!scanning && !canInterrupt)
            return;

        Debug.Log("Scan Interrupted");

        StopCoroutine(scanRoutine);
        ResetScanParameters();
        
        //Maybe we can turn off the light slowly if scan is interrupted? 
    }

    private IEnumerator Scan(Organell target)
    {
        scanning = true;
        canInterrupt = true;
        scanLight.enabled = true;

        Vector3 midPoint = transform.position + (Vector3.Normalize(rightHandCube.position - transform.position) * (Vector3.Distance(transform.position, rightHandCube.position) / 2));
        Instantiate(scanVFX, midPoint, Quaternion.identity);


        float elapsedScanTime = 0;
        float interpolator = 0;
        int interpolDirection = 1;

        while (elapsedScanTime < timeToScan)
        {           

            if (interpolator > 1 || interpolator < 0)
            {
                interpolDirection *= -1;
            }

            interpolator += (lightStrobeSpeed * Time.deltaTime) * interpolDirection;
            scanLight.range = Mathf.Lerp(target.MinLightRange, target.MaxLightRange, interpolator);

            elapsedScanTime += Time.deltaTime;
            yield return null;
        }

        canInterrupt = false;
        audioPlayer.PlayOneShot(target.DescriptionClip);

        ResetScanParameters();
        levelObjectives.Accomplish(target);

        Debug.Log("Scanned " + target.OrganelleName + " succesfully");
    }

    private void ResetScanParameters()
    {
        scanLight.enabled = false;
        scanLight.transform.localPosition = Vector3.zero;
        scanning = false;
    }
}



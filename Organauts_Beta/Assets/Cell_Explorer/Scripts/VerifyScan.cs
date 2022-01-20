using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifyScan : MonoBehaviour
{
    public bool scanEnableLeft = false;
    public bool scanEnableRight = false;
    public bool scanEnable = false;
    public bool isScanning = false;
    public float timeToScan = 0f;
    public Light scanLight;
    public float minLightRange = 1;
    public float maxLightRange = 8;
    public float interpolator = 0;
    public GameObject scanVFX;
    public GameObject scanCubeL;
    public GameObject scanCubeR;
    public AudioSource audioDescription;
    public AudioClip descriptionClip;

    void Start()
    {
        scanLight.enabled = false;
    }

    void Update() 
    {
        //Debug.Log("Time Elapsed: " + timeToScan);

        if (!isScanning)
        {
            if (scanEnableLeft && scanEnableRight)
            {
                Debug.Log("Both Sides On, Timer Started");
                timeToScan += Time.deltaTime;
                if (timeToScan >= 3)
                {
                    scanEnable = true;
                    scanEnableLeft = false;
                    scanEnableRight = false;
                }

               
            }

            if (scanEnable)
            {
                Debug.Log("START SCAN");
                isScanning = true;
                StartCoroutine(Scan());
                scanEnable = false;
            }
        }
        else 
        {
            StopCoroutine(Scan());
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == scanCubeL)
        {
            scanEnableLeft = true;
            Debug.Log("LEFT SCAN");
        }

        if (other.gameObject == scanCubeR)
        {
            scanEnableRight = true;
            Debug.Log("RIGHT SCAN");

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == scanCubeL)
        {
            scanEnableLeft = false;
            timeToScan = 0;
        }

        if (other.gameObject == scanCubeR)
        {
            scanEnableRight = false;
            timeToScan = 0;
        }
    }

    private IEnumerator Scan()
    {
        //Turn on scanning light
        scanLight.enabled = true;

        yield return new WaitForSeconds(.4f);

        //Lerp through two different light ranges
        scanLight.range = Mathf.Lerp(minLightRange, maxLightRange, interpolator);

        //Make interpolator progress through time
        interpolator += 0.5f * Time.deltaTime;

        //Call for voice description
        audioDescription.PlayOneShot(descriptionClip);

        //Instantiate the scan vfx
        Vector3 midPoint = new Vector3((scanCubeL.transform.position.x + scanCubeR.transform.position.x) / 2, (scanCubeL.transform.position.y + scanCubeR.transform.position.y) / 2, (scanCubeL.transform.position.z + scanCubeR.transform.position.z) / 2);

        GameObject clone;
        clone = Instantiate(scanVFX, midPoint, transform.rotation);

        //Invert the min and max range values when interpolator has reached it's max
        if (interpolator > 1.0f)
        {
            float temp = maxLightRange;
            maxLightRange = minLightRange;
            minLightRange = 0;
            interpolator = 0.0f;
        }

        //Make interpolator progress through time again, now with inverted values
        interpolator += 1f * Time.deltaTime;

        yield return new WaitForSeconds(2f);

        //Turn off light
        scanLight.enabled = false;

        //Make isScanning loop available again
        isScanning = false;
        Debug.Log("SCANNED");
    
    }


}

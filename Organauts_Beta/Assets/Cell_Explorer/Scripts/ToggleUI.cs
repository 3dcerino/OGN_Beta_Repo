using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    [SerializeField] GameObject playerUI;
    [SerializeField] private float handViewAngle = 100;
    [SerializeField] private float controllerViewAngle = 120;

    private float viewAngle = 100;

    Vector3 zeroScale = new Vector3(0, 0, 0);
    Vector3 defaultScale;

    float angle;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        defaultScale = new Vector3 (6.280809e-05f, 6.280809e-05f, 6.280809e-05f);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 direction = playerUI.transform.position - transform.position;

        //Get the angle between the camera and the ui, if its below 40, ui pops up
        angle = Vector3.Angle(playerUI.transform.forward, transform.forward * -1);

        if (angle > viewAngle)
        {
            playerUI.transform.localScale = Vector3.MoveTowards(playerUI.transform.localScale, defaultScale, Time.deltaTime * speed);
            //StopAllCoroutines();
            //StartCoroutine(ScaleUIin());
        }
        else
        {
            playerUI.transform.localScale = Vector3.MoveTowards(playerUI.transform.localScale, Vector3.zero, Time.deltaTime * speed);

            //StopAllCoroutines();
            //playerUI.SetActive(false);
            //playerUI.transform.localScale = zeroScale;
        }
    }

    private IEnumerator ScaleInterfaceUp()
    {
        //Turn on UI
        playerUI.SetActive(true);
        playerUI.transform.localScale = zeroScale;


        yield return new WaitForSeconds(.2f);
    }

    private IEnumerator ScaleInterfaceDown()
    {
        //Turn on UI
        playerUI.SetActive(true);
        playerUI.transform.localScale = zeroScale;


        yield return new WaitForSeconds(.2f);
    }

    private IEnumerator ScaleUIin()
    {
        playerUI.SetActive(true);
        float t = 0;
        //wait one frame
        while (t < 1)
        {
            t += 0.01f;
            playerUI.transform.localScale = Vector3.Lerp(Vector3.zero, defaultScale, t);

            yield return null;
        }
    
    }

    public void SetViewAngle(bool isHands)
    {
        viewAngle = isHands ? handViewAngle : controllerViewAngle;
    }
}

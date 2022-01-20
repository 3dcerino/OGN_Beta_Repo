using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingTest : MonoBehaviour
{
    /*[SerializeField] GameObject swimHandR;
    [SerializeField] GameObject swimHandL;
    [SerializeField] GameObject swimBoundR;
    [SerializeField] GameObject swimBoundL;*/
    [SerializeField] Camera playerCam;
    [SerializeField] GameObject playerRig;
    public VerifySwimLeft swimLeft;
    public VerifySwimRight swimRight;
    //public GameObject swimLight;
    public float speed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (swimLeft.swimEnableLeft && swimRight.swimEnableRight)
        {
            //swimLight.SetActive(true);
            playerRig.transform.position = playerRig.transform.position + playerCam.transform.forward * speed * Time.deltaTime;
            //Debug.Log("SWIM");
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
public class TEstingButtons : MonoBehaviour
{
    public Renderer aRenderer;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Interactable>().InteractableStateChanged.AddListener(OnChanged);
    }

    private void OnChanged(InteractableStateArgs State)
    {
        aRenderer.material.color = Color.HSVToRGB(Random.value, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

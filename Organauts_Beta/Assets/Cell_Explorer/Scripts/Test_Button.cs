using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OculusSampleFramework;

public class Test_Button : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip loadSFX;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Interactable>().InteractableStateChanged.AddListener(OnChangedState);
    }

    void OnChangedState(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ActionState)
        {
            StartCoroutine(LoadScene());
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.timeSamples = 0;
        audioSource.clip = clip;
        audioSource.Play();
    }


    private IEnumerator LoadScene()
    {
        PlaySound(loadSFX);

        yield return new WaitForSeconds(4);

        SceneManager.LoadScene("Cell_Scene");
    }
}

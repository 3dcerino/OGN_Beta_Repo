using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OculusSampleFramework;

public class Button_SelectSpecimen : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip loadSFX;
    public List<GameObject> uiElementsOff = new List<GameObject>();
    public List<GameObject> uiElementsOn = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Interactable>().InteractableStateChanged.AddListener(OnChangedState);
    }

    void OnChangedState(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ActionState)
        {
            foreach (GameObject uiElement in uiElementsOff)
            {
                uiElement.SetActive(false);
            }

            foreach (GameObject uiElement in uiElementsOn)
            {
                uiElement.SetActive(true);
            }

            PlaySound(loadSFX);
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveUI : MonoBehaviour
{
    [Header("Canvas position when using hands")]
    [SerializeField] private Vector3 distanceFromHand;
    [SerializeField] private Vector3 panelRotation_Hands;

    [Header("Canvas position when using Controller")]
    [SerializeField] private Vector3 distanceFromController;
    [SerializeField] private Vector3 panelRotation_Controller;

    [Header("Sprite layout parameters")]
    [SerializeField] private float spriteSize = 350;
    [SerializeField] private float spriteSpacing = 30;
    [SerializeField] private Color deactivatedSpriteShade = Color.black;

    [Header("Object References")]
    [SerializeField] private GridLayoutGroup spriteContainer;
    [SerializeField] private Text countDisplay;
    [SerializeField] private GameObject questCompletedUI;


    private Image[] objectiveSprites;
    private int objectivesScanned;



    public void PopulateSpriteList(Organell[] organelleList)
    {
        if (organelleList == null)
            return;



        SetDisplayParameters();

        objectivesScanned = 0;
        objectiveSprites = new Image[organelleList.Length];

        int index = 0;
        foreach (Organell organelle in organelleList)
        {
            GameObject spriteObj = new GameObject(organelle.OrganelleName + "_sprite", typeof(RectTransform), typeof(Image));
            Image spriteImage = spriteObj.GetComponent<Image>();

            spriteImage.sprite = organelle.Sprite;
            spriteImage.color = deactivatedSpriteShade;
            spriteImage.preserveAspect = true;

            spriteObj.transform.SetParent(spriteContainer.gameObject.transform, true);
            spriteObj.transform.localScale = Vector3.one;
            spriteObj.transform.localPosition = Vector3.zero;
            spriteObj.transform.localRotation = Quaternion.identity;

            objectiveSprites[index] = spriteImage;

            index++;
        }

        UpdateUIText();

    }

    public void ActivateSprite(int organelleIndex)
    {
        objectiveSprites[organelleIndex].color = Color.white;

        //Conditional makes sure count will never go above total objectives, in case of an error:
        if (objectivesScanned < objectiveSprites.Length)
        {
            objectivesScanned++;
            UpdateUIText();
            CheckQuestStatus();
        }

    }

    private void CheckQuestStatus()
    {
        if(objectivesScanned >= objectiveSprites.Length)
        {
            QuestCompleted();
        }
    }

    private void UpdateUIText()
    {
        countDisplay.text = "" + objectivesScanned + "/" + objectiveSprites.Length;
    }

    private void SetDisplayParameters()
    {
        spriteContainer.cellSize = Vector2.one * spriteSize;
        spriteContainer.spacing = Vector2.one * spriteSpacing;
    } 


    public void AdjustUIToHands(bool usingHandTracking)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = usingHandTracking ? Quaternion.Euler(90, 0, -90) : Quaternion.identity;
            
        transform.Translate(usingHandTracking ? distanceFromHand : distanceFromController, Space.Self);
        transform.Rotate(usingHandTracking ? panelRotation_Hands : panelRotation_Controller, Space.Self);
    }


    private void QuestCompleted()
    {
        //Provisional behaviour:
        Invoke("ShowCompletedCanvas", 6);
        Invoke("ReturnToMenu",19);
    }

    //Provisional Methods:
    void ReturnToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    void ShowCompletedCanvas()
    {
        questCompletedUI.SetActive(true);

    }
}

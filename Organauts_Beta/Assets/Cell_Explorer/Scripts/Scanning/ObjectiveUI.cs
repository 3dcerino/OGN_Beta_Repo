using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveUI : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup spriteContainer;
    [SerializeField] private Text countDisplay;

    [Header("Sprite layout parameters")]
    [SerializeField] private float spriteSize = 350;
    [SerializeField] private float spriteSpacing = 30;
    [SerializeField] private Color deactivatedSpriteShade = Color.black;

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
            GameObject spriteObj = new GameObject(organelle.OrganelleName + "_sprite", typeof(RectTransform),typeof(Image));
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
}

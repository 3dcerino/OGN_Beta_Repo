using UnityEngine;


[CreateAssetMenu(fileName = "OrganelleData", menuName = "ScriptableObjects/Organelle", order = 1)]
public class Organell : ScriptableObject
{
    [Header("Basic Parameters")]
    [SerializeField] private string organelleName = "Unnamed Organell";
    [SerializeField] private AudioClip descriptionClip;
    [SerializeField] private Sprite uISprite;

    [Header("Light Effect Parameters")]
    [SerializeField] private Color lightColor = Color.white;
    [SerializeField] private float lightIntensity = 1;
    [SerializeField] private float minLightRange = 1;
    [SerializeField] private float maxLightRange = 8;
    [SerializeField] private Vector3 lightPositionOffset; // -> Where the light should be placed, relative to the organelle's position. 

    public string OrganelleName => organelleName;
    public AudioClip DescriptionClip => descriptionClip;
    public Sprite Sprite => uISprite;
    public Color LightColor => lightColor;
    public float LightIntensity => lightIntensity;
    public float MinLightRange => minLightRange;
    public float MaxLightRange => maxLightRange;
    public Vector3 LightPositionOffset => lightPositionOffset;

}

using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManagerScript : MonoBehaviour
{
    public MajorAbilityScript[] majorAbilites;
    [SerializeField]
    private LineRenderer lineObj;
    public Color[] lineColors;
    public TextAsset skillInformation;
    private string[] skillData;

    // Variables relating to the text box that displays the skills name and description
    public Canvas textBoxArea;
    private Text[] skillTextBoxes;
    private bool isFadingIn;
    private bool isFadingOut;
    [SerializeField]
    float fadeInSpeed;
    [SerializeField]
    float fadeOutSpeed;
    [SerializeField]
    float maxAlpha;
    private float textFadeInSpeed;
    private float textFadeOutSpeed;


    void Awake()
    {
        skillTextBoxes = textBoxArea.GetComponentsInChildren<Text>();
        textFadeInSpeed = fadeInSpeed / maxAlpha;
        textFadeOutSpeed = fadeOutSpeed / maxAlpha;
        skillData = new string[majorAbilites.Length * 2];
        if(skillInformation != null)
        {
            skillData = skillInformation.text.Split('\n');
        } else
        {
            Debug.Log("No Skill Data file exists!");
        }
        instantiateMajorAbilities();
    }
    private void instantiateMajorAbilities()
    {
        for (int i = 0; i < majorAbilites.Length; i++)
        {
            if(i * 2 >= skillData.Length)
            {
                Debug.Log("No Data found for Skill Num: " + i);
                majorAbilites[i].StartMajorAbilityScript(lineObj, lineColors, "", "");
            } else
            {
                majorAbilites[i].StartMajorAbilityScript(lineObj, lineColors, skillData[i * 2], skillData[i * 2 + 1]);
            }
           
        }
    }
    public void hoverOverAbility(string skillName, string skillDescription)
    {
        isFadingIn = true;
        isFadingOut = false;
        skillTextBoxes[0].text = skillName;
        skillTextBoxes[1].text = skillDescription;
        
    }
    public void exitOverAbility()
    {
        isFadingIn = false;
        isFadingOut = true;
    }
    private void Update()
    {
        if(isFadingIn)
        {
            Color color = textBoxArea.GetComponentInChildren<Image>().color;
            Color textColor = skillTextBoxes[0].color;
            if (color.a + fadeInSpeed >= maxAlpha)
            {
                color.a = maxAlpha;
                textColor.a = 1.0f;
                isFadingIn = false;
            }
            else
            {
                color.a += fadeInSpeed;
                textColor.a += textFadeInSpeed;
            }
            textBoxArea.GetComponentInChildren<Image>().color = color;
            for(int i = 0; i < skillTextBoxes.Length; i++)
            {
                skillTextBoxes[i].color = textColor;
            }
        }
        if (isFadingOut)
        {
            Color color = textBoxArea.GetComponentInChildren<Image>().color;
            Color textColor = skillTextBoxes[0].color;
            if (color.a - fadeOutSpeed <= 0.0f)
            {
                color.a = 0.0f;
                textColor.a = 0.0f;
                isFadingOut = false;
            }
            else
            {
                color.a -= fadeOutSpeed;
                textColor.a -= textFadeOutSpeed;
            }
            textBoxArea.GetComponentInChildren<Image>().color = color;
            for (int i = 0; i < skillTextBoxes.Length; i++)
            {
                skillTextBoxes[i].color = textColor;
            }
        }
    }
}

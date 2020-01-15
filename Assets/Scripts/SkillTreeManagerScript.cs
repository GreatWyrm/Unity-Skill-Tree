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
    public Canvas textBoxArea;
    private Text[] skillTextBox;

    void Awake()
    {
        textBoxArea.enabled = false;
        skillTextBox = textBoxArea.GetComponentsInChildren<Text>();
        skillTextBox[0].text = "";
        skillTextBox[1].text = "";
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
        textBoxArea.enabled = true;
        skillTextBox[0].text = skillName;
        skillTextBox[1].text = skillDescription;
    }
    public void exitOverAbility()
    {
        textBoxArea.enabled = false;
        skillTextBox[0].text = "";
        skillTextBox[1].text = "";
    }
}

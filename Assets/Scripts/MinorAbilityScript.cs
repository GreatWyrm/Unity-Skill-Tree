using UnityEngine;

[System.Serializable]
public class MinorAbilityScript : MonoBehaviour
{
    [SerializeField]
    private string skillName;
    [SerializeField]
    private string skillDescription;
    private bool isAbilityActive;
    private MajorAbilityScript parentAbility;
    public void StartMinorAbilityScript(MajorAbilityScript parent)
    {
        parentAbility = parent;
    }
    public bool getAbilityActive()
    {
        return isAbilityActive;
    }
    public void setAbilityActive(bool state)
    {
        isAbilityActive = state;
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButton(0) && !isAbilityActive)
        {
            isAbilityActive = true;
            parentAbility.updateFromNextAbility();
        }
    }
    private void OnMouseEnter()
    {

    }
    public void disableSkill()
    {
        isAbilityActive = false;
    }
}

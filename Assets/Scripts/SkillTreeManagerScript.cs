using UnityEngine;

public class SkillTreeManagerScript : MonoBehaviour
{
    public MajorAbilityScript[] majorAbilites;
    [SerializeField]
    private LineRenderer lineObj;
    public Color[] lineColors;

    void Awake()
    {
        instantiateMajorAbilities();
    }
    private void instantiateMajorAbilities()
    {
        for(int i = 0; i < majorAbilites.Length; i++)
        {
            majorAbilites[i].StartMajorAbilityScript(lineObj, lineColors);
        }
    }
}

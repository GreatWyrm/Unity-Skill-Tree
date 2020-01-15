using UnityEngine;

[System.Serializable]
public class MajorAbilityScript : MonoBehaviour
{
    public MajorAbilityScript[] nextAbilities;
    public MajorAbilityScript[] previousAbilities;
    public MinorAbilityScript[] minorAbilties;
    private bool isAbilityActive;
    public Animator animator;
    private LineRenderer lineGenerator;
    private LineRenderer[] allLines;
    [SerializeField]
    private string skillName;
    [SerializeField]
    private string skillDescription;
    private Color[] lineColors;

    public void StartMajorAbilityScript(LineRenderer lineGen, Color[] lineColors, string name, string description)
    {
        lineGenerator = lineGen;
        this.lineColors = lineColors;
        instantiateMinorAbilities();
        allLines = new LineRenderer[(nextAbilities.Length + minorAbilties.Length)];
        initialDrawToNextAbilties();
        initialDrawToMinorAbilities();
        skillName = name;
        skillDescription = description;
    }
    public bool getAbilityActive()
    {
        return isAbilityActive;
    }
    public void setAbilityActive(bool state)
    {
        isAbilityActive = state;
    }
    private void instantiateMinorAbilities()
    {

    }
    // Uses the first part of the allLines Array
    private void initialDrawToNextAbilties()
    {
        Vector3 selfPos = GetComponent<Transform>().position;
        for (int i = 0; i < nextAbilities.Length; i++)
        {
            allLines[i] = Instantiate(lineGenerator);
            allLines[i].SetPosition(0, selfPos);
            allLines[i].SetPosition(1, nextAbilities[i].transform.position);
            GradientColorKey firstKey;
            GradientColorKey secondKey;
            Gradient gradient = new Gradient();
            if (isAbilityActive)
            {
                firstKey = new GradientColorKey(lineColors[1], 0.0f);
            } else
            {
                firstKey = new GradientColorKey(lineColors[0], 0.0f);
            }
            if (nextAbilities[i].getAbilityActive())
            {
                secondKey = new GradientColorKey(lineColors[1], 1.0f);
            }
            else
            {
                secondKey = new GradientColorKey(lineColors[0], 1.0f);
            }
            gradient.SetKeys(
            new GradientColorKey[] { firstKey, secondKey },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
            );
            allLines[i].colorGradient = gradient;
        }

    }
    private void updateColors()
    {
        for (int i = 0; i < nextAbilities.Length; i++)
        {
            GradientColorKey firstKey;
            GradientColorKey secondKey;
            Gradient gradient = new Gradient();
            if (isAbilityActive)
            {
                firstKey = new GradientColorKey(lineColors[1], 0.0f);
            }
            else
            {
                firstKey = new GradientColorKey(lineColors[0], 0.0f);
            }
            if (nextAbilities[i].getAbilityActive())
            {
                secondKey = new GradientColorKey(lineColors[1], 1.0f);
            }
            else
            {
                secondKey = new GradientColorKey(lineColors[0], 1.0f);
            }
            gradient.SetKeys(
            new GradientColorKey[] { firstKey, secondKey },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
            );
            allLines[i].colorGradient = gradient;
        }
        for (int i = 0; i < minorAbilties.Length; i++)
        {
            int actualArrayPos = i + nextAbilities.Length;
            GradientColorKey firstKey;
            GradientColorKey secondKey;
            Gradient gradient = new Gradient();
            if (isAbilityActive)
            {
                firstKey = new GradientColorKey(lineColors[1], 0.0f);
            }
            else
            {
                firstKey = new GradientColorKey(lineColors[0], 0.0f);
            }
            if (minorAbilties[i].getAbilityActive())
            {
                secondKey = new GradientColorKey(lineColors[3], 1.0f);
            }
            else
            {
                secondKey = new GradientColorKey(lineColors[2], 1.0f);
            }
            gradient.SetKeys(
            new GradientColorKey[] { firstKey, secondKey },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
            );
            allLines[actualArrayPos].colorGradient = gradient;
        }
    }
    // Uses the second part of the allLines Array
    private void initialDrawToMinorAbilities()
    {
        Vector3 selfPos = GetComponent<Transform>().position;
        for (int i = 0; i < minorAbilties.Length; i++)
        { 
            int actualArrayPos = i + nextAbilities.Length;
            allLines[actualArrayPos] = Instantiate(lineGenerator);
            allLines[actualArrayPos].SetPosition(0, selfPos);
            allLines[actualArrayPos].SetPosition(1, minorAbilties[i].transform.position);
            GradientColorKey firstKey;
            GradientColorKey secondKey;
            Gradient gradient = new Gradient();
            if (isAbilityActive)
            {
                firstKey = new GradientColorKey(lineColors[1], 0.0f);
            }
            else
            {
                firstKey = new GradientColorKey(lineColors[0], 0.0f);
            }
            if (minorAbilties[i].getAbilityActive())
            {
                secondKey = new GradientColorKey(lineColors[3], 1.0f);
            }
            else
            {
                secondKey = new GradientColorKey(lineColors[2], 1.0f);
            }
            gradient.SetKeys(
            new GradientColorKey[] { firstKey, secondKey },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
            );
            allLines[actualArrayPos].colorGradient = gradient;
        }
    }
    void OnMouseOver()
    {
        if(Input.GetMouseButton(0) && !isAbilityActive)
        {
            ParticleSystem particles = GetComponentInChildren<ParticleSystem>();
            particles.Emit(60);
            isAbilityActive = true;
            Debug.Log("Activated: " + skillName);
            updateColors();
            for(int i = 0; i < previousAbilities.Length; i++)
            {
                previousAbilities[i].updateFromNextAbility();
            }
        }
    }
    public void updateFromNextAbility()
    {
        updateColors();
    }
    private void OnMouseEnter()
    {
        animator.SetBool("isMouseOver", true);
        GetComponentInParent<SkillTreeManagerScript>().hoverOverAbility(skillName, skillDescription);
    }
    private void OnMouseExit()
    {
        animator.SetBool("isMouseOver", false);
        GetComponentInParent<SkillTreeManagerScript>().exitOverAbility();
    }
    public void resetSkill()
    {
        isAbilityActive = false;
        for(int i = 0; i < minorAbilties.Length; i++)
        {
            minorAbilties[i].disableSkill();
        }
        updateColors();
    }
}

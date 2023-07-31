using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTree : MonoBehaviour
{
    #region inpector
    [SerializeField]
    GameObject rowPrefab;
    [SerializeField]
    SkillTier[] tiers;
    [SerializeField]
    GameObject iconBase;
    [SerializeField]
    Color tint, mouseOverTint, selectedTint;
    #endregion inpector

    #region public
    public event UpdateSkills Update;

    #endregion public

    List<SkillInfo> activeSkills;
    List<SkillInfo> skills;
    SkillInfo selectedSkill;

    int skillPoints = 0;


    void Start()
    {
        skills = new List<SkillInfo>();
        int tierHeight = (Screen.height-100)/tiers.Length;
        foreach(SkillTier tier in tiers)
        {
            GameObject r  = Instantiate(rowPrefab, transform);
            foreach(SkillInfo prev in tier.icons)
            {
                skills.Add(prev);

                GameObject i = Instantiate(iconBase, r.transform);
                
                MouseOverListener l = i.AddComponent<MouseOverListener>();
                l.enabled = true;

                RectTransform t = i.GetComponent<RectTransform>();
                Button button = i.GetComponent<Button>();
                
                float scale = tierHeight / t.rect.height * .95f;
                
                
                t.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tierHeight * .95f);
                t.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tierHeight * .95f);


                if (prev.icon != null)
                {
                    i.GetComponentInChildren<Image>().sprite = prev.icon;
                }
                
                TMP_Text text = i.GetComponentInChildren<TMP_Text>();
                text.text = prev.skillName;
                text.fontSize = text.fontSize * scale;
            }
            
        }
        ResetSkills();

    }

    bool ActivateSkill(SkillInfo skill)
    {
        if (skillPoints < skill.cost)
        {
            print("Not enough skiilpoints for " + skill.skillName);
            return false;
        }
        skillPoints -= skill.cost;
        activeSkills.Add(skill);
        Update?.Invoke();
        return true;
    }
    void ResetSkills()
    {
        if(activeSkills != null)
        {
            foreach (SkillInfo skill in activeSkills)
            {
                skillPoints += skill.cost;
            }
        }
        
        activeSkills = new List<SkillInfo>();
        Update?.Invoke();
    }

}
[Serializable]
public struct SkillTier
{
    public SkillInfo[] icons;
}
[Serializable]
[CreateAssetMenu(fileName ="SkillInfo", menuName = "ScriptableObjects/SkillInfo")]
public class SkillInfo: ScriptableObject
{
    public Sprite icon;
    public string skillName;
    public string description;
    public int cost;

    

}
public delegate SkillInfo[] UpdateSkills();


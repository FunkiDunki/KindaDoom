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

    #endregion inpector

    #region public
    public event UpdateSkills Update;

    #endregion public

    List<SkillInfo> activeSkills;

    void Start()
    {

        int tierHeight = (Screen.height-100)/tiers.Length;
        foreach(SkillTier tier in tiers)
        {
            GameObject r  = Instantiate(rowPrefab, transform);
            foreach(SkillInfo prev in tier.icons)
            {
                GameObject i = Instantiate(iconBase, r.transform);
                
                RectTransform t = i.GetComponent<RectTransform>();
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
        
    }

    void AddActiveSkill(SkillInfo skill)
    {
        activeSkills.Add(skill);
        Update.Invoke();
    }
    void ResetSkills()
    {
        activeSkills = new List<SkillInfo>();
        Update.Invoke();
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

    

}
public delegate SkillInfo[] UpdateSkills();

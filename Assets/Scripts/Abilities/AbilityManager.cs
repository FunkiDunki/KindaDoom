using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    [Serializable]
    public struct NamedAbility
    {
        public string button;
        public Ability ability;
        public Image abilityImage;
        public TextMeshProUGUI abilityCool;
        public Image coverImage;
        public AbilityInfo info;
    }

    [Header("Abilities")]
    [SerializeField] List<NamedAbility> abilities;
    [SerializeField] int currentPowerCells;
    [SerializeField] GameObject ui_powercell;//prefab
    [SerializeField] GameObject ui_cellList;


    // Start is called before the first frame update
    void Start()
    {
        foreach (NamedAbility ability in abilities)
        {
            ability.ability.Init(ability.abilityImage, ability.abilityCool, ability.coverImage, ability.info);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckControls();
        foreach (NamedAbility ability in abilities)
        {
            ability.ability.Tick(Time.deltaTime);
        }
    }
    public void GetCell()
    {
        Instantiate(ui_powercell, ui_cellList.transform);
        currentPowerCells++;
    }
    void SpendCells(int cost)
    {
        currentPowerCells -= cost;
        for (int i = 0; i < cost; i++)
        {
            ui_cellList.transform.GetChild(ui_cellList.transform.childCount - 1 - i).GetComponentInChildren<Animator>().SetTrigger("Use");
        }
    }

    void CheckControls()
    {
        foreach (NamedAbility ability in abilities)
        {
            if (Input.GetButtonDown(ability.button) && ability.ability.canUse() && currentPowerCells >= ability.info.cost)
            {
                if (ability.ability.UseDown())
                {
                    SpendCells(ability.info.cost);
                }
            }
            if (Input.GetButtonUp(ability.button))
            {
                ability.ability.UseUp();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powercell"))
        {
            Destroy(other.transform.parent.gameObject);
            GetCell();
        }
    }
}

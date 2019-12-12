using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    public GameObject slotPrefab;
    private List<Button> skillButtonList = new List<Button>();

    // Start is called before the first frame update
    void Start()
    {
        var player = (Character)BattleManager.instance.Player.GetComponent(typeof(Character));

        foreach (var skill in player.Skills)
        {
            this.UpdatePanel(skill.GetComponent<Skill>());
        }
    }

    void UpdatePanel(Skill skill)
    {
        var skillObj = (GameObject)Instantiate(slotPrefab);
        var skillObjRect = (RectTransform)skillObj.GetComponent(typeof(RectTransform));
        var button = (UnityEngine.UI.Button)skillObj.GetComponent(typeof(UnityEngine.UI.Button));
        button.onClick.AddListener(() => SkillCallBack(skill));
        ///skillObj.AddComponent(typeof(Skill));
        skillObj.name = skill.Name;
        var obj = (GameObject)Instantiate(skill.gameObject);
        obj.transform.SetParent(skillObj.transform);

        button.GetComponent<Image>().sprite = skill.Icon;
        skillObj.transform.SetParent(gameObject.transform);
        skillObjRect.localScale = new Vector3(1f, 1f, 1f);
        this.skillButtonList.Add(button);
    }

    private void SkillCallBack(Skill skill)
    {
        BattleManager.instance.PlayerAttack(skill);
    }
    // Update is called once per frame
    void Update()
    {

    }
    void OnDisable()
    {
        foreach(var button in skillButtonList){
            button.onClick.RemoveAllListeners();
        }
    }
}

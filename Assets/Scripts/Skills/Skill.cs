using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Базовый класс Навыка
public class Skill : MonoBehaviour
{
    public int Level;
    public string Name;
    public double Value;

    public Sprite Icon;

    public SkillLogic Logic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cast(Character character, Character target){
        this.Logic.Cast(character,target,this);
    }

}

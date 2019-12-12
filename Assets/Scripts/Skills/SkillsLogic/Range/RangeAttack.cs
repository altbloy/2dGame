using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class RangeAttack : SkillLogic
{
    Vector2 _startPosition;

    Character _character;
    Character _target;
    Skill _skillInfo;
    
    public override void Cast(Character character, Character target, Skill skillInfo)
    {
        this._character = character;
        this._skillInfo = skillInfo;
        this._target = target;
        this._startPosition = new Vector2(this._character.transform.position.x, this._character.transform.position.y);

        character.StartCoroutine(this.Move());

        // Управление анимацией атакующго и цели
    }

    IEnumerator Move()
    {
        // Логика рукопашной атаки
        // Подписываемся на ивент окнчания движения
        //this._character.Movement.MovementIsOver += this.Attack;
        // Двигаем персонажа
        yield return new WaitForSeconds(3f);
        var characterAnimator = (Animator)this._character.GetComponent(typeof(Animator));
        characterAnimator.ResetTrigger("Attack");
        characterAnimator.SetTrigger("Attack");
        var animation = characterAnimator.runtimeAnimatorController.animationClips.First(x => x.name == "attackMelle01");
        yield return new WaitForSeconds(animation.length);
        this._target.TakeDamage(this._skillInfo.Value + this._character.BaseAttack);
        yield return new WaitForSeconds(2);
        this._character.CastIsOver.Invoke();

    }
}

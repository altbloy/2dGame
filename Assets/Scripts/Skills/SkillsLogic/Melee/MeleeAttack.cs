using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeleeAttack : SkillLogic
{
    Vector2 _startPosition;

    Character _character;
    Character _target;
    Skill _skillInfo;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void Cast(Character character, Character target, Skill skillInfo)
    {
        this._character = character;
        this._skillInfo = skillInfo;
        this._target = target;
        this._startPosition = new Vector2(this._character.transform.position.x, this._character.transform.position.y);

        character.StartCoroutine(this.Move());

        // Управление анимацией атакующго и цели
    }
    private void Attack()
    {
        // Запускаем анимацию атаки
        // нанесение урона

    }

    IEnumerator Move()
    {
        // Логика рукопашной атаки
        // Подписываемся на ивент окнчания движения
        this._character.Movement.MovementIsOver += this.Attack;
        // Двигаем персонажа
        var dir = this._target.gameObject.transform.position.x > 0 ? 1.5f : -1.5f;
        this._character.Movement.Move(new Vector2(this._target.gameObject.transform.position.x - dir
        , this._target.gameObject.transform.position.y));
        yield return new WaitForSeconds(2f);
        var characterAnimator = (Animator)this._character.GetComponent(typeof(Animator));
        characterAnimator.ResetTrigger("Attack");
        characterAnimator.SetTrigger("Attack");
        var animation = characterAnimator.runtimeAnimatorController.animationClips.First(x => x.name == "attackMelle01");
        yield return new WaitForSeconds(animation.length);
        this._target.TakeDamage(this._skillInfo.Value + this._character.BaseAttack);
        yield return new WaitForSeconds(1);
        this._character.Movement.Move(this._startPosition);
        yield return new WaitForSeconds(2);
        this._character.CastIsOver.Invoke();

    }

}

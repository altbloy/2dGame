using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Базовый клас персонажа
public class Character : MonoBehaviour
{
    public string Name;
    public double Health;
    public double Mana;

    public double Deffens;

    public double BaseAttack;

    public List<Skill> Skills;

    public CharacterMovement Movement;


    // Ивенты
    public delegate void CharacterEvent();
    public CharacterEvent CastIsOver;
    public CharacterEvent TakeDamageEvent;


    public virtual void TakeDamage(double value)
    {
        var damage = value - Deffens;
        if (damage > 0)
        {
            this.Health -= damage;
            this.TakeDamageEvent.Invoke();
        }
    }

    public virtual void CastSkill(Character target, Skill Skill)
    {
        Skill.Cast(this, target);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.Movement = (CharacterMovement)gameObject.AddComponent(typeof(CharacterMovement));
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class CharacterMovement : MonoBehaviour
{
    private GameObject _characterObject;

    public Vector2 _cordsToMove;
    public bool IsMove = false;

    public delegate void MovementEvent();
    public event MovementEvent MovementIsOver;
    void Start()
    {
    }

    public virtual void Move(Vector2 cords)
    {
        this._cordsToMove = cords;
        IsMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMove)
        {
            this.transform.position = Vector2.Lerp(this.transform.position, _cordsToMove, Time.deltaTime * 2);
            var xCord = (this._cordsToMove - (Vector2)this.transform.position) / this.transform.position.magnitude;

            if (xCord.x < 0.01f && xCord.x > -0.01f)
            {
                IsMove = false;
                this.MovementIsOver.Invoke();
            }
        }
    }

    public void WaitSec(float sec)
    {
        StartCoroutine(this.WaitingCor(sec));
    }
    private IEnumerator WaitingCor(float sec)
    {
        yield return new WaitForSeconds(sec);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance = null;

    public GameObject Player;
    public GameObject Enemy;
    public Character _playerController;
    public Character _enemyStat;

    public int Turn = 0;
    public bool TurnIsOver;
    public bool PlayerTurn;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        { // Экземпляр менеджера был найден
            instance = this; // Задаем ссылку на экземпляр объекта

            this.Player = GameObject.Find("Player");
            this._playerController = (Character)this.Player.GetComponent(typeof(Character));

            this.Enemy = GameObject.Find("Enemy");
            this._enemyStat = (Character)this.Enemy.GetComponent(nameof(Character));

            PlayerTurn = true;
        }
        else if (instance == this)
        { // Экземпляр объекта уже существует на сцене
            Destroy(gameObject); // Удаляем объект
        }
    }
    void Start()
    {
        this._playerController.CastIsOver += ChangeState;
        this._enemyStat.CastIsOver += ChangeState;
        this.PlayerTurn = true;
        this.TurnIsOver = true;
    }
     public void ChangeState()
    {
        this.PlayerTurn = PlayerTurn == true ? false : true;
        this.TurnIsOver = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!PlayerTurn && TurnIsOver)
        {
            Attack(_enemyStat.BaseAttack);
        }
    }
    public void PlayerAttack(Skill skill)
    {
        var list = new List<Component>();
        skill.gameObject.GetComponents(list);
        if (PlayerTurn && TurnIsOver)
        {
            TurnIsOver = false;
            skill.Cast(_playerController, _enemyStat);
        }
    }
    public void Attack(double dmg)
    {
        if (TurnIsOver)
        {
            TurnIsOver = false;
            _enemyStat.Skills[0].GetComponent<Skill>().Cast(_enemyStat, _playerController);
        }
    }
}

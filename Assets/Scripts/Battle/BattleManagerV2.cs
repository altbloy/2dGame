using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleManagerV2 : MonoBehaviour
{
     public AnimationCurve curve;
     public (AnimationCurve cur , int cc) fff ;
    public float speed = 2f;
    // Start is called before the first frame update
    public BattleState currentState;

    public GameObject BattleGroundsParentObj;
    public GameObject CubeForMovePrefab;
    public List<CubeForMove> BattleGrounds;

    public bool IsMove;


    public static BattleManagerV2 instance = null;

    public GameObject Player;
    public GameObject Enemy;
    public Character _playerController;
    public Character _enemyStat;

    void Awake()
    {
        if (instance == null)
        { // Экземпляр менеджера был найден
            instance = this; // Задаем ссылку на экземпляр объекта

            this.Player = GameObject.Find("Player");
            this._playerController = (Character)this.Player.GetComponent(typeof(Character));

            this.Enemy = GameObject.Find("Enemy");
            this._enemyStat = (Character)this.Enemy.GetComponent(nameof(Character));
            currentState = BattleState.Player1;
        }
        else if (instance == this)
        { // Экземпляр объекта уже существует на сцене
            Destroy(gameObject); // Удаляем объект
        }
    }

    void Start()
    {
        for(int i = 0 ; i < 15 ; i++)
        {
            GameObject obj = Instantiate(CubeForMovePrefab,Vector3.zero,CubeForMovePrefab.transform.rotation);
            obj.transform.position = BattleGroundsParentObj.transform.position;
            CubeForMove objStat = obj.GetComponent<CubeForMove>();
            objStat.Position = i;
            obj.transform.SetParent(this.BattleGroundsParentObj.transform);
            float step = i;
            obj.transform.localPosition =  new Vector3(step*2, 0, 0);
            this.BattleGrounds.Add(objStat);
        }
        CubeForMove playerCubeStart = this.BattleGrounds.FirstOrDefault(x=>x.Position == 0);
        Player.transform.position = playerCubeStart.transform.position;//new Vector2(playerCubeStart.transform.position.x,Player.transform.position.y);
        playerCubeStart.IsUsed = true;
        playerCubeStart.Player = Player.GetComponent<Character>();

        CubeForMove player2CubeStart = this.BattleGrounds.FirstOrDefault(x=>x.Position == 10);
        Enemy.transform.position = player2CubeStart.transform.position;//new Vector2(player2CubeStart.transform.position.x,Enemy.transform.position.y);
        player2CubeStart.IsUsed = true;
        player2CubeStart.Player = Enemy.GetComponent<Character>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!IsMove)
        switch(currentState){
            case BattleState.Player1 :
                // отслеживать действия
                if(Input.GetKey(KeyCode.A)){
                    // Найти куб на котором стоит игрок
                    CubeForMove current = this.BattleGrounds.First(x=>x.Player == this.Player.GetComponent<Character>());
                    if(!current.IsFirst){
                        CubeForMove next = this.BattleGrounds
                        .FirstOrDefault(x=>x.Position == current.Position -1);
                        
                        if(next!=null && !next.IsUsed){
                            // Обнулить значение кубов и задать новые
                        current.IsUsed=false;
                        current.Player = null;

                        next.IsUsed = true;
                        next.Player = Player.GetComponent<Character>();
                        // передвинуть на один куб вперед если это возможно
                        StartCoroutine(this.Move(Player, next.gameObject));
                        }
                    }
                }
                if(Input.GetKey(KeyCode.D)){
                    // Найти куб на котором стоит игрок
                    CubeForMove current = this.BattleGrounds.First(x=>x.Player == this.Player.GetComponent<Character>());
                    if(!current.ISLast){
                        CubeForMove next = this.BattleGrounds
                        .FirstOrDefault(x=>x.Position == current.Position + 1);

                        if(next!=null && !next.IsUsed){
                        // Обнулить значение кубов и задать новые
                        current.IsUsed=false;
                        current.Player = null;

                        next.IsUsed = true;
                        next.Player = Player.GetComponent<Character>();
                        // передвинуть на один куб вперед если это возможно
                        StartCoroutine(this.Move(Player, next.gameObject));
                        }
                    }
                }
            break;
            case BattleState.Player2 :

            break;
        }
    }

    IEnumerator Move(GameObject obj , GameObject target){
        Animator characterAnimator = obj.GetComponent<Animator>();
        characterAnimator.ResetTrigger("Walk");
        characterAnimator.SetTrigger("Walk");
        var animation = characterAnimator.runtimeAnimatorController.animationClips.First(x => x.name == "Walk");
        //yield return new WaitForSeconds(animation.length);
        Vector2 targerPos = target.transform.position;
        targerPos.y = obj.transform.position.y;

        this.IsMove = true;
        float speed = 0f;// animation.length /100; // 0.01f; //  скорость прогресса (от начальной до конечной позиции)
        while (true)
        {
            float dist = Vector2.Distance(targerPos ,(Vector2)obj.transform.position);
             if(dist==0 || dist <= 0.01f){
                 this.IsMove=false;
                 obj.transform.position = targerPos;
                yield break;
             }
            if(speed<= animation.length)
                speed += Time.deltaTime; 

            Debug.Log( "length "+animation.length+" -  " +this.curve.Evaluate(animation.length - Time.deltaTime) + "time" + speed);
            obj.transform.position = Vector2.MoveTowards(obj.transform.position,targerPos,this.curve.Evaluate(speed));
           
            // выход из корутины, если находимся в конечной позиции
            yield return null; // если выхода из корутины не произошло, то продолжаем выполнять цикл while в следующем кадре

        }
    }
}
public enum BattleState {
    Player1,
    Player2
}


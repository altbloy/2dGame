using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterMoveManager : MonoBehaviour
{
     public AnimationCurve curve;
    
    public bool IsMove;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

   // public 

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CubeForMove : MonoBehaviour
{
    public Character Player;
    public int Position;
    public bool IsUsed;
    public bool IsFirst {
        get{
            return Position == 0 ? true : false;
        }
    }
    public bool ISLast{
        get{
            return Position == this.gameObject.transform.parent.childCount-1? true:false;
        }
    }

    // public CubeForMove GetNext(){
    //      List<CubeForMove> list= new List<CubeForMove>();
    //      this.gameObject.transform.parent.transform.GetComponents<CubeForMove>(list);
    // }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

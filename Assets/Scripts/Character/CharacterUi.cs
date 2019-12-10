using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUi : MonoBehaviour
{
    private Character _parent;

    public Text Hp;

    void Start()
    {
        this._parent = (Character)gameObject.GetComponent(typeof(Character));

        this._parent.TakeDamageEvent += UpdateHp;
        this.UpdateHp();
    }
    public void UpdateHp()
    {
        this.Hp.text = this._parent.Health.ToString();
    }
}

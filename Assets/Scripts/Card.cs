using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Card : MonoBehaviour
{
    public string cardText;
    public Vector2Int startPosition{get; set;}
    public Vector2Int endPosition{get; set;}

    public void SetText(){
        this.GetComponentInChildren<TMP_Text>().text = string.Format(
            "{0}{1}->{2}{3}",
            GlobalVars.instance.posToColumn[this.startPosition.y], this.startPosition.x,
            GlobalVars.instance.posToColumn[this.endPosition.y], endPosition.x
        );
    }
}

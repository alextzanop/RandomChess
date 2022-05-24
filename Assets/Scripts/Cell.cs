using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
  public class Cell : MonoBehaviour
{
    public Color color;
    [SerializeField]
    public Piece currentPiece;
    public Piece initPiece;
    public Vector2Int boardPosition = Vector2Int.zero;
    public int row;
    public string column;

    public void InitCell(Color _color, int _i, int _j){
        this.color = _color;
        this.currentPiece = this.initPiece;
        this.boardPosition = new Vector2Int(_i,_j);
         //delete later
        this.GetComponent<Button>().onClick.AddListener(this.OnClick);
        
        this.row = 8-this.boardPosition[0];
        GlobalVars.instance.posToColumn.TryGetValue(this.boardPosition[1], out this.column);

        this.GetComponentInChildren<TMP_Text>().text = string.Format("{1}{0}",this.row, this.column);

    }

    public void SetCurrPiece(Piece _newPiece){
        this.currentPiece = _newPiece;
    }

    void OnClick(){
        if(!this.currentPiece || (this.currentPiece && this.gameObject.GetComponent<Image>().color != this.color))
            return;
        BoardController.instance.UnhighlightAll();
        BoardController.instance.CalculateLegalMoves(this.gameObject);
        this.Highlight();
    }

    public void Highlight(){
        Image img = this.gameObject.GetComponent<Image>();
        img.color = img.color == this.color ? Color.yellow : this.color;
    }

    public void Unhighlight(){
        Image img = this.gameObject.GetComponent<Image>();
        img.color = this.color;
    }

}

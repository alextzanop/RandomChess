using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piece : MonoBehaviour
{
    protected Color color;
    protected Sprite pieceImg;
    Cell initCell;
    public Cell currCell;

    //(horizontal, vertical, mainDiag, secDiag)
    protected List<Vector2Int> moves;
    protected List<Vector2Int> legalMoves;
    void Start(){
        Image img = this.gameObject.GetComponent<Image>();
        if(img) {
            img.sprite = this.pieceImg;
            img.color = this.color;
        }
    }

    protected virtual void SetupMoves(){}
    public virtual void SetColor(Color _color){
        this.color = _color;
    }
    public Color GetColor(){
        return this.color;
    }
    public void SetLegalMoves(List<Vector2Int> _legalMoves){
        this.legalMoves = _legalMoves;
    }

    public List<Vector2Int> GetMoves(){
        return this.moves;
    }
    public List<Vector2Int> GetLegalMoves(){
        return this.legalMoves;
    }

    public void SetCurrCell(Cell _newCell){
        this.currCell = _newCell;
    }

}

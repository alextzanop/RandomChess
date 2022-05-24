using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public string playerName;
    public Color color;
    public List<Piece> pieces = new List<Piece>();


    public void AddPiece(Piece _p){
        if(!this.pieces.Contains(_p))
            this.pieces.Add(_p);
    }
    public void RemovePiece(Piece _p){
        if(this.pieces.Contains(_p))
            this.pieces.Remove(_p);
    }

    public List<Vector2Int> GetLegalMoves(){
        List<Vector2Int> legalMoves = new List<Vector2Int>();
        foreach(Piece p in this.pieces){
            BoardController.instance.CalculateLegalMoves(p.currCell.gameObject);
            foreach(Vector2Int move in p.GetLegalMoves())
                legalMoves.Add(move);
        }
        return legalMoves;
    }
}

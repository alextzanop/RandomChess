using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    bool firstMove;

    public override void SetColor(Color _color){
        this.color = _color;
        this.SetupMoves();
    }

   void Awake(){
       this.firstMove = true;
       this.pieceImg = GlobalVars.pieceImagesStat[0];
   }

   protected override void SetupMoves()
    {
        int multiplier = 1;
        this.moves = new List<Vector2Int>();
        if(this.color == Color.black) multiplier=-1;
        this.moves.Add(new Vector2Int(multiplier*1, 0)); //up
        this.moves.Add(new Vector2Int(multiplier*2, 0)); //only for first move

    }
}

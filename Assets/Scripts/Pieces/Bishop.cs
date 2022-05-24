using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
   void Awake(){
       this.pieceImg = GlobalVars.pieceImagesStat[3];
       this.SetupMoves();
   }

   protected override void SetupMoves()
    {
        this.moves = new List<Vector2Int>();
        for (int i = 1; i < 8; i++) {
            this.moves.Add(new Vector2Int(i, i)); //up right
            this.moves.Add(new Vector2Int(i, -i)); //up left
            this.moves.Add(new Vector2Int(-i, i)); //down right
            this.moves.Add(new Vector2Int(-i, -i)); //down left
        }
    }
}

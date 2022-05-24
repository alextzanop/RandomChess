using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
   void Awake(){
       this.pieceImg = GlobalVars.pieceImagesStat[2];
       this.SetupMoves();
   }

    protected override void SetupMoves()
    {
        this.moves = new List<Vector2Int>{
            new Vector2Int(2,1),//up right
            new Vector2Int(2,-1),//up left
            new Vector2Int(-2,1),//down right
            new Vector2Int(-2,-1),//down left
            new Vector2Int(1,2),//right up
            new Vector2Int(-1,2),//right down
            new Vector2Int(1,-2),//left up
            new Vector2Int(-1,-2)//left down
        };
    }
}

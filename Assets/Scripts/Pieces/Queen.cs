using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    void Awake(){
       this.pieceImg = GlobalVars.pieceImagesStat[4];
       this.SetupMoves();
   }

   protected override void SetupMoves()
    {
        this.moves = new List<Vector2Int>();
        for (int i = 1; i < 8; i++) {
            this.moves.Add(new Vector2Int(i, 0)); //up
            this.moves.Add(new Vector2Int(i, i)); //up right
            this.moves.Add(new Vector2Int(0, i)); //right
            this.moves.Add(new Vector2Int(-i, i)); //down right
            this.moves.Add(new Vector2Int(-i, 0)); //down
            this.moves.Add(new Vector2Int(-i, -i)); //down left
            this.moves.Add(new Vector2Int(0, -i)); //left
            this.moves.Add(new Vector2Int(i, -i)); //up left   
        }
    }
}

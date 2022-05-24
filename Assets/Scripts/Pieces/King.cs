using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    void Awake(){
       this.pieceImg = GlobalVars.pieceImagesStat[5];
       this.SetupMoves();
   }

   protected override void SetupMoves()
    {
        this.moves = new List<Vector2Int>();
        this.moves.Add(new Vector2Int(1, 0)); //up
        this.moves.Add(new Vector2Int(1, 1)); //up right
        this.moves.Add(new Vector2Int(0, 1)); //right
        this.moves.Add(new Vector2Int(-1, 1)); //down right
        this.moves.Add(new Vector2Int(-1, 0)); //down
        this.moves.Add(new Vector2Int(-1, -1)); //down left
        this.moves.Add(new Vector2Int(0, -1)); //left
        this.moves.Add(new Vector2Int(1, -1)); //up left
    }
}

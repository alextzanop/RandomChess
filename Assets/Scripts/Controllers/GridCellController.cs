using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridCellController : MonoBehaviour
{
    [SerializeField]
    GameObject container;

    [SerializeField]
    float multiplierW;

    [SerializeField]
    float multiplierH;
    
    void Update(){
        this.ResizeCell();
    }

    // Resize cell to fit GridLayoutGroup
    void ResizeCell(){
        float width = container.GetComponent<RectTransform>().rect.width;
        float height = container.GetComponent<RectTransform>().rect.height;
        Vector2 newSize = new Vector2(width/multiplierW, height/multiplierH);
        container.GetComponent<GridLayoutGroup>().cellSize = newSize;
    }

}

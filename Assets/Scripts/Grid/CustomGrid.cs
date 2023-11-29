using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomGrid : MonoBehaviour
{
    static CustomGrid instance;
    public static CustomGrid Instance 
    {  get 
        { 
            if (instance == null)
            {
                instance = new CustomGrid();
            }
            return instance; 
        } 
    }


    public Tilemap walls;
    public Tilemap ground;
    public Tilemap environment;
    private Dictionary<ElementTypes, Vector3Int> elementsGrid;

    private void Awake()
    {
        Dictionary<ElementTypes, Vector3Int> elementsGrid = new Dictionary<ElementTypes, Vector3Int>();
    }

    public void CheckForValidHit(Vector2 position, ElementTypes element)
    {
        Vector3Int tilePosition = new Vector3Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y), 0);
        if (ground.HasTile(tilePosition))
        {
            if (environment.HasTile(tilePosition))
            {
                
            }
            else
            {

            }
        }
    }

    private void GetResultingElement()
    {

    }
}

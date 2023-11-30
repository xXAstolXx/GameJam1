using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomGrid : MonoBehaviour
{
    static CustomGrid instance;
    public static CustomGrid Instance 
    {  get 
        { 
            return instance; 
        } 
    }


    public Tilemap walls;
    public Tilemap ground;
    public Tilemap environment;
    private Dictionary<Vector3Int, ElementTypes> elementsGrid;

    [SerializeField]
    GameObject fireTile;
    [SerializeField]
    GameObject waterTile;
    [SerializeField]
    GameObject steamTile;
    [SerializeField]
    GameObject iceTile;


    private void Start()
    {
        instance = GetComponent<CustomGrid>();

        elementsGrid = new Dictionary<Vector3Int, ElementTypes>();

        for (int i = 0; i < environment.transform.childCount; i++)
        {
            Transform t = environment.transform.GetChild(i);
            elementsGrid.Add(new Vector3Int(Mathf.FloorToInt(t.position.x), Mathf.FloorToInt(t.position.y), 0), t.gameObject.GetComponent<ElementTile>().element);
        }
    }

    public ElementTypes GetElement(Vector3Int position)
    {
        ElementTypes result = ElementTypes.NONE;
        elementsGrid.TryGetValue(position, out result);
        return result;
    }

    public void SetElement(Vector3Int position, ElementTypes element)
    {
        
    }

    private void DeleteTile(Vector3Int position)
    {
        Debug.Log("Delete tile at pos: " + position);
        Vector3 realPos = new Vector3(position.x+0.5f, position.y+0.5f, 0);
        for (int i = 0; i < environment.transform.childCount; i++)
        {
            Transform t = environment.transform.GetChild(i);
            if (realPos == t.transform.position)
            {
                Debug.Log(t.gameObject.name);
                Destroy(t.gameObject);
                return;
            }
        }

        elementsGrid.Remove(position);
    }

    private void AddTile(Vector3Int position, ElementTypes element)
    {
        Vector3 realPos = new Vector3(position.x+0.5f, position.y+0.5f, 0);
        if (element == ElementTypes.ICE)
        {
            Instantiate(iceTile, realPos, Quaternion.identity, environment.transform);
        }
        else if (element == ElementTypes.FIRE)
        {
            Instantiate(fireTile, realPos, Quaternion.identity, environment.transform);
        }
        else if (element == ElementTypes.STEAM)
        {
            Instantiate(steamTile, realPos, Quaternion.identity, environment.transform);
        }
        else if (element == ElementTypes.WATER)
        {
            Instantiate(waterTile, realPos, Quaternion.identity, environment.transform);
        }
        if(!elementsGrid.ContainsKey(position))
        {
            elementsGrid.Add(position, element);
        }
    }

    private void GetResultingElement()
    {

    }

    public void AttackWith(Vector3Int position, ElementTypes element)
    {
        if (!elementsGrid.ContainsKey(position))
        {
            AddTile(position, element);
        }
        else
        {
            ElementTypes result;
            ElementSettings.Instance.transformElements.TryGetValue(new ElementTransform(element, GetElement(position)), out result);

            DeleteTile(position);
            AddTile(position, result);
        }
    }
}

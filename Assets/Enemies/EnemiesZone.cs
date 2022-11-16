using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

[ExecuteAlways]
public class EnemiesZone : MonoBehaviour
{
    private BoxCollider2D zone;
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private GameObject holder;

    public GameObject Holder => holder;

    private int prevR;
    private int prevC;
    private Bounds prevB;

    void Awake()
    {
        prevR = rows;
        prevC = columns;

        zone = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        
        
    }

    void Refresh()
    {
        prevC = columns;
        prevR = rows;
        prevB = zone.bounds;

        int max = holder.transform.childCount;
        for (int i = 0; i < max; i++)
        {
            DestroyImmediate(holder.transform.GetChild(0).gameObject);
        }

        float sizeX = zone.bounds.size.x / columns;
        float sizeY = zone.bounds.size.y / rows;
        Vector2 startPos = new Vector2(zone.bounds.min.x + sizeX / 2, zone.bounds.min.y + sizeY / 2);
        Vector2 pos = startPos;
        int index = 0;
        for (int Y = 0; Y < rows; Y++)
        {
            for (int X = 0; X < columns; X++)
            {
                GameObject go = new GameObject("test0" + index);
                go.transform.parent = holder.transform;
                go.transform.position = pos;

                var iconContent = EditorGUIUtility.IconContent("blendKey");
                EditorGUIUtility.SetIconForObject(go, (Texture2D)iconContent.image);

                ++index;
                pos.x += sizeX;
            }

            pos.x = startPos.x;
            pos.y += sizeY;
        }
    }

    void Update()
    {
        if (columns != prevC || rows != prevR || zone.bounds != prevB) Refresh();
    }
}

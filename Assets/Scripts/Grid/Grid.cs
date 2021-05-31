using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Grid <T>
{
    int width;
    int height;
    //int cellSize;
    T[,] gridArray;
    TextMeshPro[,] debugTextMesh;

    public static TextMeshPro GridText(string text, Transform parent, Vector2 localPosition, int fontSize, Color color)
    {
        GameObject gameObject = new GameObject("World Text", typeof(TextMeshPro));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition + new Vector2(0.5f, 0.5f);
        TextMeshPro textMesh = gameObject.GetComponent<TextMeshPro>();
        textMesh.autoSizeTextContainer = true;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.text = text;
        textMesh.outlineWidth = 0;
        //textMesh.GetComponent<MeshRenderer>().sortingLayerName = "Effects";
        return textMesh;
    }

    public Grid(int width, int height)
    {
        this.width = width;
        this.height = height;
        //this.cellSize = cellSize;

        gridArray = new T[width, height];
        debugTextMesh = new TextMeshPro[width, height];

        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {
                debugTextMesh[i, j] = GridText(gridArray[i, j].ToString(), null, new Vector2(i, j), 2, Color.black);
                Debug.DrawLine(new Vector2(i, j), new Vector2(i, j + 1), Color.red, 100f);
                Debug.DrawLine(new Vector2(i, j), new Vector2(i + 1, j), Color.red, 100f);
            }
        }
    }

    public void SetValue (int x, int y, T value)
    {
        gridArray[x, y] = value;
        debugTextMesh[x, y].text = value.ToString();
    }
}

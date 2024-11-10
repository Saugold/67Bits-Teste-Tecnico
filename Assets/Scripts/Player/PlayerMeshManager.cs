using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeshManager : MonoBehaviour
{
    private SkinnedMeshRenderer playerMesh;
    private List<Color> colors = new List<Color> { Color.red, Color.green, Color.blue, Color.yellow };
    private List<Color> usedColors = new List<Color> { };
    public static PlayerMeshManager _instance;

    private void Awake()
    {
        playerMesh = GetComponent<SkinnedMeshRenderer>();
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateColor()
    {
        if (colors.Count == 0)
        {
            colors.AddRange(usedColors);
            usedColors.Clear();
        }
        Color newColor = colors[0];
        playerMesh.material.color = newColor; 

        RemoveColor(newColor);
        AddUsedColor(newColor);
    }

    private void AddUsedColor(Color newColor)
    {
        if (!usedColors.Contains(newColor))
        {
            usedColors.Add(newColor);
        }
    }

    private void RemoveColor(Color colorToRemove)
    {
        if (colors.Contains(colorToRemove))
        {
            colors.Remove(colorToRemove);
        }
    }
}

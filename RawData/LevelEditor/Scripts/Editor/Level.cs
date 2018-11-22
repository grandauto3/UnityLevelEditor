using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Level
{
    public int Width { get; set; }
    public int Height { get; set; }
    public Texture2D Tex { get; set; }

    public float gridOffsetY = 0;
    public float gridOffsetX = 0;

    Rect Window
    {
        get { return MainWindow.instance.position; }
    }

    [SerializeField]
    float _gridSpacing = 20f;
    public float GridSpacing
    {
        get { return _gridSpacing + 1f; }
        set { _gridSpacing = Mathf.Clamp(value, 0, 140f); }
    }
    public Level(int width, int height)
    {
        Width = width;
        Height = height;

        Tex = new Texture2D(Width, Height);
    }

    public Rect GetImgRect()
    {
        float ratio = (float)Height / (float)Width;
        float w = GridSpacing * 30;
        float h = ratio * GridSpacing * 30;

        float xPos = (Window.width / 2f - w / 2f) + gridOffsetX;
        float yPos = (Window.height / 2f - h / 2f) + gridOffsetY;

        Rect r = new Rect(xPos, yPos, w, h);

        return r;
    }

    public Vector2 GetPixelCoordinate(Vector2 pos)
    {
        Rect texPos = GetImgRect();

        if (!texPos.Contains(pos))
        {
            return new Vector2(-1f, -1f);
        }

        float relX = (pos.x - texPos.x) / texPos.width;
        float relY = (texPos.y - pos.y) / texPos.height;

        int pixelX = (int)(Width * relX);
        int pixelY = (int)(Height * relY) - 1;

        return new Vector2(pixelX, pixelY);
    }

    public void SetPixelByPos(Color32 color, Vector2 pos)
    {
        Vector2 pixelCoordinate = GetPixelCoordinate(pos);

        if (pixelCoordinate == new Vector2(-1, -1))
            return;

        Undo.RecordObject(Tex, "ColorPixel");

        Tex.SetPixel((int)pixelCoordinate.x, (int)pixelCoordinate.y, color);

        Tex.filterMode = FilterMode.Point;

        Tex.Apply();
    }

    public Vector2 GetReadablePixelCoordinate(Vector2 pos)
    {
        Vector2 coord = GetPixelCoordinate(pos);

        if (coord.x == -1)
            return coord;

        coord.x += 1;
        coord.y *= -1;
        return coord;
    }
}

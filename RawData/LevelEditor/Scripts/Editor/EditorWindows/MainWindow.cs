using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MainWindow : EditorWindow
{
    public static MainWindow instance;
    public static ToolbarWindow toolwnd;

    public static Level lvl;
    Event e;

    float GridSpacing
    {
        get { return lvl.GridSpacing; }
        set { lvl.GridSpacing = value; }
    }

    float GridOffsetX
    {
        get { return lvl.gridOffsetX; }
        set { lvl.gridOffsetX = value; }
    }
    float GridOffsetY
    {
        get { return lvl.gridOffsetY; }
        set { lvl.gridOffsetY = value; }
    }

    [MenuItem("Window/Level Editor")]
    public static void Init()
    {
        toolwnd =  ToolbarWindow.Init();


        instance = GetWindow<MainWindow>("Level Editor", typeof(ToolbarWindow)) as MainWindow;
        instance.minSize = new Vector2(32, 32);
    }

    void OnGUI()
    {
        if(lvl == null)
        {

            if(GUI.Button(new Rect(instance.position.width / 2f - 140, instance.position.height / 2f - 25, 130, 50), "New Level"))
                CreateNewLevelWindow.Init();

            if(GUI.Button(new Rect(instance.position.width / 2f + 10, instance.position.height / 2f - 25, 130, 50), "Open Image"))
            {
                Texture2D tex = ImportExport.Import();

                lvl = new Level(tex.width, tex.height)
                {
                    Tex = tex,
                };
            }

            return;
        }
        if (!lvl.Tex)
        {
            lvl = null;
            return;
        }

        e = Event.current;

        DrawImage(lvl);

        Vector2 mousePos = e.mousePosition;

        if(e.button == 0)
        {

            // Mouse buttons
            if(e.isMouse & (e.type == EventType.MouseDrag | e.type == EventType.MouseDown))
            {
                if(mouseOverWindow == this)
                {
                    if(toolwnd.brushSelected == 0)
                        if(toolwnd.tiles != null)
                        {
                            if(!toolwnd.tiles[toolwnd.tileSelected].prefab)
                                Debug.LogWarning("Current Tile has no Prefab");

                            lvl.SetPixelByPos(toolwnd.tiles[toolwnd.tileSelected].color, mousePos);
                        }

                    if(toolwnd.brushSelected == 1)
                        lvl.SetPixelByPos(Color.clear, mousePos);
                }
            }


            if(e.type == EventType.KeyDown)
            {
                if(e.keyCode == KeyCode.W)
                {
                    GridOffsetY += 20f;
                }
                if(e.keyCode == KeyCode.S)
                {
                    GridOffsetY -= 20f;
                }
                if(e.keyCode == KeyCode.A)
                {
                    GridOffsetX += 20f;
                }
                if(e.keyCode == KeyCode.D)
                {
                    GridOffsetX -= 20f;
                }

                if(e.keyCode == KeyCode.UpArrow)
                {
                    GridSpacing *= 1.2f;
                }
                if(e.keyCode == KeyCode.DownArrow)
                {
                    GridSpacing *= 0.8f;
                    GridSpacing -= 2;
                }

            }
        }
        
        Repaint();
    }

    public static void DrawImage(Level lvl)
    {
        Rect texPos = lvl.GetImgRect();

        Texture2D bg = new Texture2D(1, 1);
        bg.SetPixel(0, 0, Color.clear);
        bg.Apply();
        EditorGUI.DrawTextureTransparent(texPos, bg);
        DestroyImmediate(bg);
        //Draw the image
        GUI.DrawTexture(texPos, lvl.Tex);

        // Draw a grid above the image (y axis first)
        for (int x = 0; x <= lvl.Tex.width; x += 1)
        {
            float posX = texPos.xMin + (float)texPos.width / (float)lvl.Tex.width * x - 0.2f;
            EditorGUI.DrawRect(new Rect(posX, texPos.yMin, 1, texPos.height), Color.black);
        }
        // Then x axis
        for (int y = 0; y <= lvl.Tex.height; y += 1)
        {
            float posY = texPos.yMin + (float)texPos.height / (float)lvl.Tex.height * y - 0.2f;
            EditorGUI.DrawRect(new Rect(texPos.xMin, posY, texPos.width, 1), Color.black);
        }
    }
}

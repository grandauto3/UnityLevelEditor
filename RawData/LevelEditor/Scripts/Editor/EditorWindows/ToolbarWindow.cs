using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum Brushes
{
    Paint,
    Eraser,
}

public class ToolbarWindow : EditorWindow
{
    static ToolbarWindow instance;

    public int brushSelected = 0;
    public int tileSelected = 0;

    public static ToolbarWindow Init()
    {
        instance = GetWindow<ToolbarWindow>("Toolbar");

        return instance;
    }

    public Tile[] tiles;

    void OnGUI()
    {
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty stringsProperty = so.FindProperty("tiles");

        string[] brushNames = System.Enum.GetNames(typeof(Brushes));

        brushSelected = GUILayout.SelectionGrid(brushSelected, brushNames, brushNames.Length, EditorStyles.miniButtonMid, GUILayout.Height(50f));

        EditorGUILayout.Space();

        if(GUILayout.Button("Generate Level"))
            GenerateLevelWindow.Init();

        GUILayout.BeginHorizontal();

        if(GUILayout.Button("Save Level"))
            ImportExport.Export(MainWindow.lvl.Tex);
        if(GUILayout.Button("Load Level"))
            MainWindow.lvl.Tex = ImportExport.Import();
        if(GUILayout.Button("New Level"))
            CreateNewLevelWindow.Init();

        GUILayout.EndHorizontal();


        if(tiles != null)
        {
            string[] tileNames = new string[tiles.Length];

            for(int i = 0; i < tiles.Length; i++)
            {
                if(tiles[i].name =="")
                    tiles[i].name = "Tile " + i;

                tileNames[i] = tiles[i].name;
            }

            tileSelected = GUILayout.SelectionGrid(tileSelected, tileNames, 1, EditorStyles.radioButton);
        }


        EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
        so.ApplyModifiedProperties(); // Remember to apply modified properties

    }
}

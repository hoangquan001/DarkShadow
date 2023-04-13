using System;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class Tools
{

    [MenuItem("GameObject/Create/GameInitialization")]
    public static void createGameInitialization()
    {
        var go = Resources.Load("Prefabs/System/GameInitialization");
        go = GameObject.Instantiate<GameObject>(go as GameObject);
        go.name = "GameInitialization";
    }

}
public class DropMonsterEditor : EditorWindow
{
    public Texture2D texture;
    Texture2D texture2;
    [MenuItem("DarkShadow/DropMonster")]
    private static void ShowWindow()
    {
        DropMonsterEditor window = GetWindowWithRect<DropMonsterEditor>(new Rect(0, 0, 720, 480));
        window.titleContent = new GUIContent("DropMonster");
        window.Show();


    }
    Vector2 scrollPos;
    GameObject[] Gos;
    public void Awake()
    {

        Gos = Resources.LoadAll<GameObject>("Prefabs/Character");
        UnityEngine.Debug.Log(Gos.Length);
        scrollPos = Vector2.zero;
    }
    private int selected = -1;

    private void OnGUI()
    {
        // string name = "";
        // GUI.DrawTexture(new Rect(0, 0, 50, 50), texture2);
        // EditorGUILayout.IntSlider(0, 0, 100);
        GUILayout.BeginHorizontal();
        string text = "";

        text = EditorGUILayout.TextField(selected.ToString());

        scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Height(500));
        int col = 5;
        int row = (Gos.Length - 1) / col + 1;

        for (int i = 0; i < row; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < col; j++)
            {
                if (i * col + j >= Gos.Length) break;
                texture2 = Gos[i * col + j].GetComponent<SpriteRenderer>().sprite.texture;

                if (GUILayout.Button(texture2, GUILayout.Width(100), GUILayout.Height(100)))
                {
                    selected = i * col + j;
                    // Repaint();
                }
            }

            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();


        GUILayout.EndHorizontal();



    }
}
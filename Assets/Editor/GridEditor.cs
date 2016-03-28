using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(GuizmoGrid))]
public class GridEditor : Editor {

    GuizmoGrid grid;

    public void OnEnable()
    {
        grid = (GuizmoGrid) target;
        SceneView.onSceneGUIDelegate = GridUpdate;
    }

    void GridUpdate(SceneView sceneview)
    {
        Event e = Event.current;

        if (e.isKey && e.character == 'a')
        {
            GameObject obj;
            Object prefab = PrefabUtility.GetPrefabParent(Selection.activeObject);

            if (prefab)
            {
                obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                obj.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            }
        }
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(" Grid X Width ");
        grid.xWidth = EditorGUILayout.FloatField(grid.xWidth, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(" Grid Z Width ");
        grid.zWidth = EditorGUILayout.FloatField(grid.zWidth, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(" Grid Height ");
        grid.height = EditorGUILayout.FloatField(grid.height, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        SceneView.RepaintAll();
    }

}

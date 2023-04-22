using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FlexSquareMesh))]
public sealed class FlexSquareMeshEditor : Editor
{
    private FlexSquareMesh _flexSquareMesh;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // インスペクター上で頂点を編集した場合、変更を反映する
        EditorGUI.BeginChangeCheck();
        for (int i = 0; i < _flexSquareMesh.Vertices.Length; i++)
        {
            Vector3 newVertex = EditorGUILayout.Vector3Field("Vertex " + i, _flexSquareMesh.Vertices[i]);
            if (newVertex != _flexSquareMesh.Vertices[i])
            {
                Undo.RecordObject(_flexSquareMesh, "Move Vertex");
                _flexSquareMesh.Vertices[i] = newVertex;
                _flexSquareMesh.GenerateSpriteSquareMesh();
            }
        }
        if (EditorGUI.EndChangeCheck())
        {
            // 変更を反映する
            EditorUtility.SetDirty(_flexSquareMesh);
        }

        GUILayout.Space(10); // スペースの追加
        if (GUILayout.Button("Reset Vertexes"))
        {
            Undo.RecordObject(_flexSquareMesh, "Reset Vertexes");
            _flexSquareMesh.ResetVertices();
        }
    }

    private void OnEnable()
    {
        _flexSquareMesh = (FlexSquareMesh)target;
        if (_flexSquareMesh.Vertices == null)
        {
            _flexSquareMesh.ResetVertices();
        }
    }

    private void OnSceneGUI()
    {
        for (int i = 0; i < _flexSquareMesh.Vertices.Length; i++)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 worldPos = _flexSquareMesh.transform.TransformPoint(_flexSquareMesh.Vertices[i]);
            Vector3 newWorldPos = Handles.PositionHandle(worldPos, Quaternion.identity);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_flexSquareMesh, "Move Vertex");
                _flexSquareMesh.Vertices[i] = _flexSquareMesh.transform.InverseTransformPoint(newWorldPos);
                _flexSquareMesh.GenerateSpriteSquareMesh();
            }
        }
    }
}
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UniqueIdentifier), true)]
public class UniqueIdentifierInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var uId = (UniqueIdentifier)target;

        if (!uId.ShouldStoreClassId())
        {
            return;
        }

        if (GUILayout.Button("Generate Class ID"))
        {
            if (string.IsNullOrEmpty(uId.ClassId))
            {
                // generate a new class ID
                uId.ClassId = System.Guid.NewGuid().ToString();
            }

            Debug.Log(uId.ClassId);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
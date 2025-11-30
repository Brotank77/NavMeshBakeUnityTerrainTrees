using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class TreeObstacleBaker : EditorWindow
{
    [MenuItem("Tools/Bake Trees With Temporary Capsules")]
    public static void ShowWindow()
    {
        GetWindow<TreeObstacleBaker>("Tree Obstacle Baker");
    }

    private void OnGUI()
    {
        GUILayout.Label("Bake Terrain Trees into NavMesh (temporary capsules)", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        if (GUILayout.Button("Generate Capsules for Trees"))
        {
            GenerateTreeCapsules();
        }
    }

    private void GenerateTreeCapsules()
    {
        Terrain terrain = Terrain.activeTerrain;
        if (terrain == null)
        {
            Debug.LogError("No active terrain found.");
            return;
        }

        TreeInstance[] trees = terrain.terrainData.treeInstances;
        if (trees.Length == 0)
        {
            Debug.LogWarning("No trees found on the terrain.");
            return;
        }

        // Remove previous obstacles
        GameObject oldParent = GameObject.Find("Tree_Obstacles");
        if (oldParent != null) DestroyImmediate(oldParent);

        GameObject parent = new GameObject("Tree_Obstacles");
        Undo.RegisterCreatedObjectUndo(parent, "Create Tree Obstacles");

        int createdCount = 0;

        foreach (TreeInstance tree in trees)
        {
            // Convert tree local position to world
            Vector3 worldPos = Vector3.Scale(tree.position, terrain.terrainData.size) + terrain.transform.position;

            GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            capsule.name = "TreeCapsule_" + createdCount;
            capsule.transform.position = worldPos;
            capsule.transform.SetParent(parent.transform);

            // Adjust scale (example: 1 unit radius, height = terrain tree height)
            float treeHeight = terrain.terrainData.size.y * 0.5f; // adjust as needed
            capsule.transform.localScale = new Vector3(1f, treeHeight / 2f, 1f); // height in Unity capsule = scale.y * 2
            capsule.isStatic = true; // mark as static for NavMesh baking

            // Remove collider if desired after bake
            // Or leave it for baking
            // Collider col = capsule.GetComponent<Collider>();

            createdCount++;
        }

        Debug.Log($"Created {createdCount} tree capsules. Bake NavMesh now and then you can delete the parent object.");
    }
}


using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviorTreeEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    private BTNodeGraph m_BTNodeGraph = null;

    [MenuItem("BehaviorTree/Behavior Tree Editor")]
    public static void ShowExample()
    {
        BehaviorTreeEditor wnd = GetWindow<BehaviorTreeEditor>();
        wnd.titleContent = new GUIContent("Behavior Tree Editor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        m_VisualTreeAsset.CloneTree(root);
        m_BTNodeGraph = root.Q<BTNodeGraph>();
    }

    // This is called when the selection is changed in the editor
    private void OnSelectionChange()
    {
        //Debug.Log($"new object selected: {Selection.activeObject.name}");
        BehaviorTree selectedAsTree = Selection.activeObject as BehaviorTree;
        if (selectedAsTree != null)
        {
            m_BTNodeGraph.PopulateTree(selectedAsTree);
        }
    }
}

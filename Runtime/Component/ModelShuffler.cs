using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExt;
using AttributeExt;

public class ModelShuffler : MonoBehaviour
{
    [Header("Base setup")]
    [SerializeField] List<GameObject> props;
    [SerializeField, CanNotEdit] GameObject selectedProp = null;
    [SerializeField, CanNotEdit] int selectedPropIndex = 0;

    List<UnityEngine.Object> changeListOnEditor = null;
    protected void RegisterEditorChange(UnityEngine.Object changedObject)
    {
        if (changeListOnEditor == null) { changeListOnEditor = new List<Object>(); }
        if (changeListOnEditor.Contains(changedObject) == false)
        {
            changeListOnEditor.Add(changedObject);
        }
    }
#if UNITY_EDITOR
    public void NextProp()
    {
        selectedPropIndex++;
        EditorUpd();
    }
    private void OnValidate()
    {
        EditorUpd();
    }

    public void EditorUpd()
    {
        if (Application.isPlaying) { return; }
        changeListOnEditor = new List<Object>();
        UpdatePropsOnEditor();
        RegisterEditorChange(this);
        changeListOnEditor.ExForEach((i) =>
        {
            if (i != null)
            {
                UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(i);
            }
        });
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
    }
    void UpdatePropsOnEditor()
    {
        props.ExForEach((i) =>
        {
            if (i != null)
            {
                i.SetActive(false);
                RegisterEditorChange(i);
            }
        });
        CheckIndex();
        if (props.ExIsValid())
        {
            selectedProp = props[selectedPropIndex];
            if (selectedProp != null)
            {
                RegisterEditorChange(selectedProp);
                selectedProp.SetActive(true);
            }
        }
        void CheckIndex()
        {
            if (selectedPropIndex > props.Count - 1)
            {
                selectedPropIndex = 0;
            }
        }
    }
#endif
    void Awake()
    {
        props.ExForEach((i) =>
        {
            if (i != null)
            {
                i.SetActive(false);
            }
        });
        if (props.ExIsValid())
        {
            selectedProp = props[selectedPropIndex];
            if (selectedProp != null && selectedProp.activeInHierarchy == false)
            {
                selectedProp.SetActive(true);
            }
        }
    }
}
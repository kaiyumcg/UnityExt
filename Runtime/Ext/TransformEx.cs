using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class TransformEx
{
    public static void ExSetGlobalScale(this Transform transform, Vector3 globalScale)
    {
        transform.localScale = Vector3.one;
        var currentGlobalScale = transform.lossyScale;
        transform.localScale = new Vector3(globalScale.x / currentGlobalScale.x, globalScale.y / currentGlobalScale.y, globalScale.z / currentGlobalScale.z);
    }
    public static void ExDestroyEverythingInside(this Transform holder, bool editorExecution = false)
    {
        var delList = new List<GameObject>();
        if (holder.childCount > 0)
        {
            for (int i = 0; i < holder.childCount; i++)
            {
                delList.Add(holder.GetChild(i).gameObject);
            }
        }

        if (delList != null && delList.Count > 0)
        {
            for (int i = 0; i < delList.Count; i++)
            {
                var d = delList[i];
                if (d == null) { continue; }
                if (editorExecution)
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.delayCall += () =>
                    {
                        GameObject.DestroyImmediate(d);
                    };
#endif
                }
                else
                {
                    GameObject.Destroy(d);
                }
            }
        }
    }
    public static List<Transform> ExGetImmediateChilds(this Transform transform)
    {
        var result = new List<Transform>();
        var count = transform.childCount;
        if (count == 0) { return result; }
        for (int i = 0; i < count; i++)
        {
            result.Add(transform.GetChild(i));
        }
        return result;
    }

    #region Foreach
    public static void ExForEachImmediateChilds(this Transform transform, OnDoAnything<Transform> Code)
    {
        var count = transform.childCount;
        if (count == 0) { return; }
        for (int i = 0; i < count; i++)
        {
            Code?.Invoke(transform.GetChild(i));
        }
    }
    public static async Task ExForEachImmediateChilds<T>(this Transform transform, Func<Transform, Task> Code)
    {
        var count = transform.childCount;
        if (count == 0) { return; }
        for (int i = 0; i < count; i++)
        {
            await Code?.Invoke(transform.GetChild(i));
        }
    }
    public static void ExForEachParentOnChain(this Transform transform, OnDoAnything<Transform> Code)
    {
        var tr = transform.parent;
        while (tr != null)
        {
            Code?.Invoke(tr);
            tr = tr.parent;
        }
    }
    public static async Task ExForEachParentOnChain<T>(this Transform transform, Func<Transform, Task> Code)
    {
        var tr = transform.parent;
        while (tr != null)
        {
            await Code?.Invoke(tr);
            tr = tr.parent;
        }
    }
    public static void ExForEachChilds(this Transform transform, OnDoAnything<Transform> Code)
    {
        CH(transform);
        void CH(Transform tr)
        {
            var len = tr.childCount;
            for (int i = 0; i < len; i++)
            {
                var chtr = tr.GetChild(i);
                Code?.Invoke(chtr);
                CH(chtr);
            }
        }
    }
    public static async Task ExForEachChilds<T>(this Transform transform, Func<Transform, Task> Code)
    {
        await CH(transform);
        async Task CH(Transform tr)
        {
            var len = tr.childCount;
            for (int i = 0; i < len; i++)
            {
                var chtr = tr.GetChild(i);
                await Code?.Invoke(chtr);
                await CH(chtr);
            }
        }
    }
    #endregion

    #region Foreach With ID
    public static void ExForEachImmediateChilds<T>(this Transform transform, OnDoAnything<Transform, int> Code)
    {
        var count = transform.childCount;
        if (count == 0) { return; }
        for (int i = 0; i < count; i++)
        {
            Code?.Invoke(transform.GetChild(i), i);
        }
    }
    public static async Task ExForEachImmediateChilds<T>(this Transform transform, Func<Transform, int, Task> Code)
    {
        var count = transform.childCount;
        if (count == 0) { return; }
        for (int i = 0; i < count; i++)
        {
            await Code?.Invoke(transform.GetChild(i), i);
        }
    }
    public static void ExForEachParentOnChain(this Transform transform, OnDoAnything<Transform, int> Code)
    {
        var tr = transform.parent;
        var counter = 0;
        while (tr != null)
        {
            counter++;
            Code?.Invoke(tr, counter);
            tr = tr.parent;
        }
    }
    public static async Task ExForEachParentOnChain<T>(this Transform transform, Func<Transform, int, Task> Code)
    {
        var tr = transform.parent;
        var counter = 0;
        while (tr != null)
        {
            counter++;
            await Code?.Invoke(tr, counter);
            tr = tr.parent;
        }
    }
    public static void ExForEachChilds(this Transform transform, OnDoAnything<Transform, int, int> Code)
    {
        int counter = 0;
        CH(transform);
        void CH(Transform tr)
        {
            var len = tr.childCount;
            for (int i = 0; i < len; i++)
            {
                var chtr = tr.GetChild(i);
                counter++;
                Code?.Invoke(chtr, counter, i);
                CH(chtr);
            }
        }
    }
    public static async Task ExForEachChilds<T>(this Transform transform, Func<Transform, int, int, Task> Code)
    {
        int counter = 0;
        await CH(transform);
        async Task CH(Transform tr)
        {
            var len = tr.childCount;
            for (int i = 0; i < len; i++)
            {
                var chtr = tr.GetChild(i);
                counter++;
                await Code?.Invoke(chtr, counter, i);
                await CH(chtr);
            }
        }
    }
    #endregion

    #region Foreach with break
    public static void ExForEachImmediateChilds(this Transform transform, OnDoAnything<Transform> Code, WhenToDoFunc<Transform> whenBreak)
    {
        var count = transform.childCount;
        if (count == 0) { return; }
        for (int i = 0; i < count; i++)
        {
            var t = transform.GetChild(i);
            if (whenBreak.Invoke(t)) { break; }
            Code?.Invoke(t);
        }
    }
    public static async Task ExForEachImmediateChilds<T>(this Transform transform, Func<Transform, Task> Code, WhenToDoFunc<Transform> whenBreak)
    {
        var count = transform.childCount;
        if (count == 0) { return; }
        for (int i = 0; i < count; i++)
        {
            var t = transform.GetChild(i);
            if (whenBreak.Invoke(t)) { break; }
            await Code?.Invoke(t);
        }
    }
    public static void ExForEachParentOnChain(this Transform transform, OnDoAnything<Transform> Code, WhenToDoFunc<Transform> whenBreak)
    {
        var tr = transform.parent;
        while (tr != null)
        {
            if (whenBreak.Invoke(tr)) { break; }
            Code?.Invoke(tr);
            tr = tr.parent;
        }
    }
    public static async Task ExForEachParentOnChain<T>(this Transform transform, Func<Transform, Task> Code, WhenToDoFunc<Transform> whenBreak)
    {
        var tr = transform.parent;
        while (tr != null)
        {
            if (whenBreak.Invoke(tr)) { break; }
            await Code?.Invoke(tr);
            tr = tr.parent;
        }
    }
    public static void ExForEachChilds(this Transform transform, OnDoAnything<Transform> Code, WhenToDoFunc<Transform> whenBreak)
    {
        CH(transform);
        void CH(Transform tr)
        {
            var len = tr.childCount;
            for (int i = 0; i < len; i++)
            {
                var chtr = tr.GetChild(i);
                if (whenBreak.Invoke(chtr)) { break; }
                Code?.Invoke(chtr);
                CH(chtr);
            }
        }
    }
    public static async Task ExForEachChilds<T>(this Transform transform, Func<Transform, Task> Code, WhenToDoFunc<Transform> whenBreak)
    {
        await CH(transform);
        async Task CH(Transform tr)
        {
            var len = tr.childCount;
            for (int i = 0; i < len; i++)
            {
                var chtr = tr.GetChild(i);
                if (whenBreak.Invoke(chtr)) { break; }
                await Code?.Invoke(chtr);
                await CH(chtr);
            }
        }
    }
    #endregion

    #region Foreach With ID with break
    public static void ExForEachImmediateChilds<T>(this Transform transform, OnDoAnything<Transform, int> Code, WhenToDoFunc<Transform, int> whenBreak)
    {
        var count = transform.childCount;
        if (count == 0) { return; }
        for (int i = 0; i < count; i++)
        {
            var tr = transform.GetChild(i);
            if (whenBreak.Invoke(tr, i)) { break; }
            Code?.Invoke(tr, i);
        }
    }
    public static async Task ExForEachImmediateChilds<T>(this Transform transform, Func<Transform, int, Task> Code, WhenToDoFunc<Transform, int> whenBreak)
    {
        var count = transform.childCount;
        if (count == 0) { return; }
        for (int i = 0; i < count; i++)
        {
            var tr = transform.GetChild(i);
            if (whenBreak.Invoke(tr, i)) { break; }
            await Code?.Invoke(tr, i);
        }
    }
    public static void ExForEachParentOnChain(this Transform transform, OnDoAnything<Transform, int> Code, WhenToDoFunc<Transform, int> whenBreak)
    {
        var tr = transform.parent;
        var counter = 0;
        while (tr != null)
        {
            counter++;
            if (whenBreak.Invoke(tr, counter)) { break; }
            Code?.Invoke(tr, counter);
            tr = tr.parent;
        }
    }
    public static async Task ExForEachParentOnChain<T>(this Transform transform, Func<Transform, int, Task> Code, WhenToDoFunc<Transform, int> whenBreak)
    {
        var tr = transform.parent;
        var counter = 0;
        while (tr != null)
        {
            counter++;
            if (whenBreak.Invoke(tr, counter)) { break; }
            await Code?.Invoke(tr, counter);
            tr = tr.parent;
        }
    }
    public static void ExForEachChilds(this Transform transform, OnDoAnything<Transform, int, int> Code, WhenToDoFunc<Transform, int, int> whenBreak)
    {
        int counter = 0;
        CH(transform);
        void CH(Transform tr)
        {
            var len = tr.childCount;
            for (int i = 0; i < len; i++)
            {
                var chtr = tr.GetChild(i);
                counter++;
                if (whenBreak.Invoke(tr, counter, i)) { break; }
                Code?.Invoke(chtr, counter, i);
                CH(chtr);
            }
        }
    }
    public static async Task ExForEachChilds<T>(this Transform transform, Func<Transform, int, int, Task> Code, WhenToDoFunc<Transform, int, int> whenBreak)
    {
        int counter = 0;
        await CH(transform);
        async Task CH(Transform tr)
        {
            var len = tr.childCount;
            for (int i = 0; i < len; i++)
            {
                var chtr = tr.GetChild(i);
                counter++;
                if (whenBreak.Invoke(tr, counter, i)) { break; }
                await Code?.Invoke(chtr, counter, i);
                await CH(chtr);
            }
        }
    }
    #endregion

    #region Foreach
    public static void ExForEachReverseImmediateChilds(this Transform transform, OnDoAnything<Transform> Code)
    {
        var count = transform.childCount;
        if (count == 0) { return; }
        for (int i = count - 1; i >= 0; i--)
        {
            Code?.Invoke(transform.GetChild(i));
        }
    }
    public static async Task ExForEachReverseImmediateChilds<T>(this Transform transform, Func<Transform, Task> Code)
    {
        var count = transform.childCount;
        if (count == 0) { return; }
        for (int i = count - 1; i >= 0; i--)
        {
            await Code?.Invoke(transform.GetChild(i));
        }
    }
    public static void ExForEachReverseChilds(this Transform transform, OnDoAnything<Transform> Code)
    {
        CH(transform);
        void CH(Transform tr)
        {
            var len = tr.childCount;
            for (int i = len - 1; i >= 0; i--)
            {
                var chtr = tr.GetChild(i);
                Code?.Invoke(chtr);
                CH(chtr);
            }
        }
    }
    public static async Task ExForEachReverseChilds<T>(this Transform transform, Func<Transform, Task> Code)
    {
        await CH(transform);
        async Task CH(Transform tr)
        {
            var len = tr.childCount;
            for (int i = len - 1; i >= 0; i--)
            {
                var chtr = tr.GetChild(i);
                await Code?.Invoke(chtr);
                await CH(chtr);
            }
        }
    }
    #endregion

    #region Foreach Reverse With ID
    public static void ExForEachReverseImmediateChilds<T>(this Transform transform, OnDoAnything<Transform, int> Code)
    {
        var count = transform.childCount;
        if (count == 0) { return; }
        for (int i = count - 1; i >= 0; i--)
        {
            Code?.Invoke(transform.GetChild(i), i);
        }
    }
    public static async Task ExForEachReverseImmediateChilds<T>(this Transform transform, Func<Transform, int, Task> Code)
    {
        var count = transform.childCount;
        if (count == 0) { return; }
        for (int i = count - 1; i >= 0; i--)
        {
            await Code?.Invoke(transform.GetChild(i), i);
        }
    }
    public static void ExForEachReverseChilds(this Transform transform, OnDoAnything<Transform, int, int> Code)
    {
        int counter = 0;
        CH(transform);
        void CH(Transform tr)
        {
            var len = tr.childCount;
            for (int i = len - 1; i >= 0; i--)
            {
                var chtr = tr.GetChild(i);
                counter++;
                Code?.Invoke(chtr, counter, i);
                CH(chtr);
            }
        }
    }
    public static async Task ExForEachReverseChilds<T>(this Transform transform, Func<Transform, int, int, Task> Code)
    {
        int counter = 0;
        await CH(transform);
        async Task CH(Transform tr)
        {
            var len = tr.childCount;
            for (int i = len - 1; i >= 0; i--)
            {
                var chtr = tr.GetChild(i);
                counter++;
                await Code?.Invoke(chtr, counter, i);
                await CH(chtr);
            }
        }
    }
    #endregion

    #region Foreach Reverse with break
    public static void ExForEachReverseImmediateChilds(this Transform transform, OnDoAnything<Transform> Code, WhenToDoFunc<Transform> whenBreak)
    {
        var count = transform.childCount;
        if (count == 0) { return; }
        for (int i = count - 1; i >= 0; i--)
        {
            var t = transform.GetChild(i);
            if (whenBreak.Invoke(t)) { break; }
            Code?.Invoke(t);
        }
    }
    public static async Task ExForEachReverseImmediateChilds<T>(this Transform transform, Func<Transform, Task> Code, WhenToDoFunc<Transform> whenBreak)
    {
        var count = transform.childCount;
        if (count == 0) { return; }
        for (int i = count - 1; i >= 0; i--)
        {
            var t = transform.GetChild(i);
            if (whenBreak.Invoke(t)) { break; }
            await Code?.Invoke(t);
        }
    }
    public static void ExForEachReverseChilds(this Transform transform, OnDoAnything<Transform> Code, WhenToDoFunc<Transform> whenBreak)
    {
        CH(transform);
        void CH(Transform tr)
        {
            var len = tr.childCount;
            for (int i = len - 1; i >= 0; i--)
            {
                var chtr = tr.GetChild(i);
                if (whenBreak.Invoke(chtr)) { break; }
                Code?.Invoke(chtr);
                CH(chtr);
            }
        }
    }
    public static async Task ExForEachReverseChilds<T>(this Transform transform, Func<Transform, Task> Code, WhenToDoFunc<Transform> whenBreak)
    {
        await CH(transform);
        async Task CH(Transform tr)
        {
            var len = tr.childCount;
            for (int i = len - 1; i >= 0; i--)
            {
                var chtr = tr.GetChild(i);
                if (whenBreak.Invoke(chtr)) { break; }
                await Code?.Invoke(chtr);
                await CH(chtr);
            }
        }
    }
    #endregion

    #region Foreach Reverse With ID with break
    public static void ExForEachReverseImmediateChilds<T>(this Transform transform, OnDoAnything<Transform, int> Code, WhenToDoFunc<Transform, int> whenBreak)
    {
        var count = transform.childCount;
        if (count == 0) { return; }
        for (int i = count - 1; i >= 0; i--)
        {
            var tr = transform.GetChild(i);
            if (whenBreak.Invoke(tr, i)) { break; }
            Code?.Invoke(tr, i);
        }
    }
    public static async Task ExForEachReverseImmediateChilds<T>(this Transform transform, Func<Transform, int, Task> Code, WhenToDoFunc<Transform, int> whenBreak)
    {
        var count = transform.childCount;
        if (count == 0) { return; }
        for (int i = count - 1; i >= 0; i--)
        {
            var tr = transform.GetChild(i);
            if (whenBreak.Invoke(tr, i)) { break; }
            await Code?.Invoke(tr, i);
        }
    }
    public static void ExForEachReverseChilds(this Transform transform, OnDoAnything<Transform, int, int> Code, WhenToDoFunc<Transform, int, int> whenBreak)
    {
        int counter = 0;
        CH(transform);
        void CH(Transform tr)
        {
            var len = tr.childCount;
            for (int i = len - 1; i >= 0; i--)
            {
                var chtr = tr.GetChild(i);
                counter++;
                if (whenBreak.Invoke(tr, counter, i)) { break; }
                Code?.Invoke(chtr, counter, i);
                CH(chtr);
            }
        }
    }
    public static async Task ExForEachReverseChilds<T>(this Transform transform, Func<Transform, int, int, Task> Code, WhenToDoFunc<Transform, int, int> whenBreak)
    {
        int counter = 0;
        await CH(transform);
        async Task CH(Transform tr)
        {
            var len = tr.childCount;
            for (int i = len - 1; i >= 0; i--)
            {
                var chtr = tr.GetChild(i);
                counter++;
                if (whenBreak.Invoke(tr, counter, i)) { break; }
                await Code?.Invoke(chtr, counter, i);
                await CH(chtr);
            }
        }
    }
    #endregion
}
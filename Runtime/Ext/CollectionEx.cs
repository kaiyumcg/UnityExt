using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public static class CollectionEx
{
    #region Foreach
    public static void ExForEach<T>(this List<T> list, OnDoAnything<T> Code)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = 0; i < list.Count; i++)
        {
            var l = list[i];
            Code?.Invoke(l);
        }
    }
    public static async Task ExForEach<T>(this List<T> list, Func<T, Task> Code)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = 0; i < list.Count; i++)
        {
            var l = list[i];
            await Code?.Invoke(l);
        }
    }
    public static void ExForEach_NoCheck<T>(this List<T> list, OnDoAnything<T> Code)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var l = list[i];
            Code.Invoke(l);
        }
    }
    public static async Task ExForEach_NoCheck<T>(this List<T> list, Func<T, Task> Code)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var l = list[i];
            await Code.Invoke(l);
        }
    }
    public static void ExForEach_NoCheck<T>(this List<T> list, int len, OnDoAnything<T> Code)
    {
        for (int i = 0; i < len; i++)
        {
            var l = list[i];
            Code.Invoke(l);
        }
    }
    public static async Task ExForEach_NoCheck<T>(this List<T> list, int len, Func<T, Task> Code)
    {
        for (int i = 0; i < len; i++)
        {
            var l = list[i];
            await Code.Invoke(l);
        }
    }
    public static void ExForEach<T>(this T[] list, OnDoAnything<T> Code)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = 0; i < list.Length; i++)
        {
            var l = list[i];
            Code?.Invoke(l);
        }
    }
    public static async Task ExForEach<T>(this T[] list, Func<T, Task> Code)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = 0; i < list.Length; i++)
        {
            var l = list[i];
            await Code?.Invoke(l);
        }
    }
    public static void ExForEach_NoCheck<T>(this T[] list, OnDoAnything<T> Code)
    {
        for (int i = 0; i < list.Length; i++)
        {
            var l = list[i];
            Code.Invoke(l);
        }
    }
    public static async Task ExForEach_NoCheck<T>(this T[] list, Func<T, Task> Code)
    {
        for (int i = 0; i < list.Length; i++)
        {
            var l = list[i];
            await Code.Invoke(l);
        }
    }
    public static void ExForEach_NoCheck<T>(this T[] list, int len, OnDoAnything<T> Code)
    {
        for (int i = 0; i < len; i++)
        {
            var l = list[i];
            Code.Invoke(l);
        }
    }
    public static async Task ExForEach_NoCheck<T>(this T[] list, int len, Func<T, Task> Code)
    {
        for (int i = 0; i < len; i++)
        {
            var l = list[i];
            await Code.Invoke(l);
        }
    }
    #endregion

    #region Foreach With ID
    public static void ExForEach<T>(this List<T> list, OnDoAnything<T, int> Code)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = 0; i < list.Count; i++)
        {
            var l = list[i];
            Code?.Invoke(l, i);
        }
    }
    public static async Task ExForEach<T>(this List<T> list, Func<T, int, Task> Code)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = 0; i < list.Count; i++)
        {
            var l = list[i];
            await Code?.Invoke(l, i);
        }
    }
    public static void ExForEach_NoCheck<T>(this List<T> list, OnDoAnything<T, int> Code)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var l = list[i];
            Code.Invoke(l, i);
        }
    }
    public static async Task ExForEach_NoCheck<T>(this List<T> list, Func<T, int, Task> Code)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var l = list[i];
            await Code.Invoke(l, i);
        }
    }
    public static void ExForEach_NoCheck<T>(this List<T> list, int len, OnDoAnything<T, int> Code)
    {
        for (int i = 0; i < len; i++)
        {
            var l = list[i];
            Code.Invoke(l, i);
        }
    }
    public static async Task ExForEach_NoCheck<T>(this List<T> list, int len, Func<T, int, Task> Code)
    {
        for (int i = 0; i < len; i++)
        {
            var l = list[i];
            await Code.Invoke(l, i);
        }
    }
    public static void ExForEach<T>(this T[] list, OnDoAnything<T, int> Code)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = 0; i < list.Length; i++)
        {
            var l = list[i];
            Code?.Invoke(l, i);
        }
    }
    public static async Task ExForEach<T>(this T[] list, Func<T, int, Task> Code)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = 0; i < list.Length; i++)
        {
            var l = list[i];
            await Code?.Invoke(l, i);
        }
    }
    public static void ExForEach_NoCheck<T>(this T[] list, OnDoAnything<T, int> Code)
    {
        for (int i = 0; i < list.Length; i++)
        {
            var l = list[i];
            Code.Invoke(l, i);
        }
    }
    public static async Task ExForEach_NoCheck<T>(this T[] list, Func<T, int, Task> Code)
    {
        for (int i = 0; i < list.Length; i++)
        {
            var l = list[i];
            await Code.Invoke(l, i);
        }
    }
    public static void ExForEach_NoCheck<T>(this T[] list, int len, OnDoAnything<T, int> Code)
    {
        for (int i = 0; i < len; i++)
        {
            var l = list[i];
            Code.Invoke(l, i);
        }
    }
    public static async Task ExForEach_NoCheck<T>(this T[] list, int len, Func<T, int, Task> Code)
    {
        for (int i = 0; i < len; i++)
        {
            var l = list[i];
            await Code.Invoke(l, i);
        }
    }
    #endregion

    #region Foreach with break
    public static void ExForEach<T>(this List<T> list, OnDoAnything<T> Code, WhenToDoFunc<T> whenBreak)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = 0; i < list.Count; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            Code?.Invoke(l);
        }
    }
    public static async Task ExForEach<T>(this List<T> list, Func<T, Task> Code, WhenToDoFunc<T> whenBreak)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = 0; i < list.Count; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            await Code?.Invoke(l);
        }
    }
    public static void ExForEach_NoCheck<T>(this List<T> list, OnDoAnything<T> Code, WhenToDoFunc<T> whenBreak)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            Code.Invoke(l);
        }
    }
    public static async Task ExForEach_NoCheck<T>(this List<T> list, Func<T, Task> Code, WhenToDoFunc<T> whenBreak)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            await Code.Invoke(l);
        }
    }
    public static void ExForEach_NoCheck<T>(this List<T> list, int len, OnDoAnything<T> Code, WhenToDoFunc<T> whenBreak)
    {
        for (int i = 0; i < len; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            Code.Invoke(l);
        }
    }
    public static async Task ExForEach_NoCheck<T>(this List<T> list, int len, Func<T, Task> Code, WhenToDoFunc<T> whenBreak)
    {
        for (int i = 0; i < len; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            await Code.Invoke(l);
        }
    }
    public static void ExForEach<T>(this T[] list, OnDoAnything<T> Code, WhenToDoFunc<T> whenBreak)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = 0; i < list.Length; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            Code?.Invoke(l);
        }
    }
    public static async Task ExForEach<T>(this T[] list, Func<T, Task> Code, WhenToDoFunc<T> whenBreak)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = 0; i < list.Length; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            await Code?.Invoke(l);
        }
    }
    public static void ExForEach_NoCheck<T>(this T[] list, OnDoAnything<T> Code, WhenToDoFunc<T> whenBreak)
    {
        for (int i = 0; i < list.Length; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            Code.Invoke(l);
        }
    }
    public static async Task ExForEach_NoCheck<T>(this T[] list, Func<T, Task> Code, WhenToDoFunc<T> whenBreak)
    {
        for (int i = 0; i < list.Length; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            await Code.Invoke(l);
        }
    }
    public static void ExForEach_NoCheck<T>(this T[] list, int len, OnDoAnything<T> Code, WhenToDoFunc<T> whenBreak)
    {
        for (int i = 0; i < len; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            Code.Invoke(l);
        }
    }
    public static async Task ExForEach_NoCheck<T>(this T[] list, int len, Func<T, Task> Code, WhenToDoFunc<T> whenBreak)
    {
        for (int i = 0; i < len; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            await Code.Invoke(l);
        }
    }
    #endregion

    #region Foreach With ID
    public static void ExForEach<T>(this List<T> list, OnDoAnything<T, int> Code, WhenToDoFunc<T, int> whenBreak)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = 0; i < list.Count; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            Code?.Invoke(l, i);
        }
    }
    public static async Task ExForEach<T>(this List<T> list, Func<T, int, Task> Code, WhenToDoFunc<T, int> whenBreak)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = 0; i < list.Count; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            await Code?.Invoke(l, i);
        }
    }
    public static void ExForEach_NoCheck<T>(this List<T> list, OnDoAnything<T, int> Code, WhenToDoFunc<T, int> whenBreak)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            Code.Invoke(l, i);
        }
    }
    public static async Task ExForEach_NoCheck<T>(this List<T> list, Func<T, int, Task> Code, WhenToDoFunc<T, int> whenBreak)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            await Code.Invoke(l, i);
        }
    }
    public static void ExForEach_NoCheck<T>(this List<T> list, int len, OnDoAnything<T, int> Code, WhenToDoFunc<T, int> whenBreak)
    {
        for (int i = 0; i < len; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            Code.Invoke(l, i);
        }
    }
    public static async Task ExForEach_NoCheck<T>(this List<T> list, int len, Func<T, int, Task> Code, WhenToDoFunc<T, int> whenBreak)
    {
        for (int i = 0; i < len; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            await Code.Invoke(l, i);
        }
    }
    public static void ExForEach<T>(this T[] list, OnDoAnything<T, int> Code, WhenToDoFunc<T, int> whenBreak)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = 0; i < list.Length; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            Code?.Invoke(l, i);
        }
    }
    public static async Task ExForEach<T>(this T[] list, Func<T, int, Task> Code, WhenToDoFunc<T, int> whenBreak)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = 0; i < list.Length; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            await Code?.Invoke(l, i);
        }
    }
    public static void ExForEach_NoCheck<T>(this T[] list, OnDoAnything<T, int> Code, WhenToDoFunc<T, int> whenBreak)
    {
        for (int i = 0; i < list.Length; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            Code.Invoke(l, i);
        }
    }
    public static async Task ExForEach_NoCheck<T>(this T[] list, Func<T, int, Task> Code, WhenToDoFunc<T, int> whenBreak)
    {
        for (int i = 0; i < list.Length; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            await Code.Invoke(l, i);
        }
    }
    public static void ExForEach_NoCheck<T>(this T[] list, int len, OnDoAnything<T, int> Code, WhenToDoFunc<T, int> whenBreak)
    {
        for (int i = 0; i < len; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            Code.Invoke(l, i);
        }
    }
    public static async Task ExForEach_NoCheck<T>(this T[] list, int len, Func<T, int, Task> Code, WhenToDoFunc<T, int> whenBreak)
    {
        for (int i = 0; i < len; i++)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            await Code.Invoke(l, i);
        }
    }
    #endregion

    #region Foreach Reverse
    public static void ExForEachReverse<T>(this List<T> list, OnDoAnything<T> Code)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = list.Count - 1; i >= 0; i--)
        {
            var l = list[i];
            Code?.Invoke(l);
        }
    }
    public static async Task ExForEachReverse<T>(this List<T> list, Func<T, Task> Code)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = list.Count - 1; i >= 0; i--)
        {
            var l = list[i];
            await Code?.Invoke(l);
        }
    }
    public static void ExForEachReverse_NoCheck<T>(this List<T> list, OnDoAnything<T> Code)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            var l = list[i];
            Code.Invoke(l);
        }
    }
    public static async Task ExForEachReverse_NoCheck<T>(this List<T> list, Func<T, Task> Code)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            var l = list[i];
            await Code.Invoke(l);
        }
    }
    public static void ExForEachReverse_NoCheck<T>(this List<T> list, int len, OnDoAnything<T> Code)
    {
        for (int i = len - 1; i >= 0; i--)
        {
            var l = list[i];
            Code.Invoke(l);
        }
    }
    public static async Task ExForEachReverse_NoCheck<T>(this List<T> list, int len, Func<T, Task> Code)
    {
        for (int i = len - 1; i >= 0; i--)
        {
            var l = list[i];
            await Code.Invoke(l);
        }
    }
    public static void ExForEachReverse<T>(this T[] list, OnDoAnything<T> Code)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = list.Length - 1; i >= 0; i--)
        {
            var l = list[i];
            Code?.Invoke(l);
        }
    }
    public static async Task ExForEachReverse<T>(this T[] list, Func<T, Task> Code)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = list.Length - 1; i >= 0; i--)
        {
            var l = list[i];
            await Code?.Invoke(l);
        }
    }
    public static void ExForEachReverse_NoCheck<T>(this T[] list, OnDoAnything<T> Code)
    {
        for (int i = list.Length - 1; i >= 0; i--)
        {
            var l = list[i];
            Code.Invoke(l);
        }
    }
    public static async Task ExForEachReverse_NoCheck<T>(this T[] list, Func<T, Task> Code)
    {
        for (int i = list.Length - 1; i >= 0; i--)
        {
            var l = list[i];
            await Code.Invoke(l);
        }
    }
    public static void ExForEachReverse_NoCheck<T>(this T[] list, int len, OnDoAnything<T> Code)
    {
        for (int i = len - 1; i >= 0; i--)
        {
            var l = list[i];
            Code.Invoke(l);
        }
    }
    public static async Task ExForEachReverse_NoCheck<T>(this T[] list, int len, Func<T, Task> Code)
    {
        for (int i = len - 1; i >= 0; i--)
        {
            var l = list[i];
            await Code.Invoke(l);
        }
    }
    #endregion

    #region Foreach Reverse With ID
    public static void ExForEachReverse<T>(this List<T> list, OnDoAnything<T, int> Code)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = list.Count - 1; i >= 0; i--)
        {
            var l = list[i];
            Code?.Invoke(l, i);
        }
    }
    public static async Task ExForEachReverse<T>(this List<T> list, Func<T, int, Task> Code)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = list.Count - 1; i >= 0; i--)
        {
            var l = list[i];
            await Code?.Invoke(l, i);
        }
    }
    public static void ExForEachReverse_NoCheck<T>(this List<T> list, OnDoAnything<T, int> Code)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            var l = list[i];
            Code.Invoke(l, i);
        }
    }
    public static async Task ExForEachReverse_NoCheck<T>(this List<T> list, Func<T, int, Task> Code)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            var l = list[i];
            await Code.Invoke(l, i);
        }
    }
    public static void ExForEachReverse_NoCheck<T>(this List<T> list, int len, OnDoAnything<T, int> Code)
    {
        for (int i = len - 1; i >= 0; i--)
        {
            var l = list[i];
            Code.Invoke(l, i);
        }
    }
    public static async Task ExForEachReverse_NoCheck<T>(this List<T> list, int len, Func<T, int, Task> Code)
    {
        for (int i = len - 1; i >= 0; i--)
        {
            var l = list[i];
            await Code.Invoke(l, i);
        }
    }
    public static void ExForEachReverse<T>(this T[] list, OnDoAnything<T, int> Code)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = list.Length - 1; i >= 0; i--)
        {
            var l = list[i];
            Code?.Invoke(l, i);
        }
    }
    public static async Task ExForEachReverse<T>(this T[] list, Func<T, int, Task> Code)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = list.Length - 1; i >= 0; i--)
        {
            var l = list[i];
            await Code?.Invoke(l, i);
        }
    }
    public static void ExForEachReverse_NoCheck<T>(this T[] list, OnDoAnything<T, int> Code)
    {
        for (int i = list.Length - 1; i >= 0; i--)
        {
            var l = list[i];
            Code.Invoke(l, i);
        }
    }
    public static async Task ExForEachReverse_NoCheck<T>(this T[] list, Func<T, int, Task> Code)
    {
        for (int i = list.Length - 1; i >= 0; i--)
        {
            var l = list[i];
            await Code.Invoke(l, i);
        }
    }
    public static void ExForEachReverse_NoCheck<T>(this T[] list, int len, OnDoAnything<T, int> Code)
    {
        for (int i = len - 1; i >= 0; i--)
        {
            var l = list[i];
            Code.Invoke(l, i);
        }
    }
    public static async Task ExForEachReverse_NoCheck<T>(this T[] list, int len, Func<T, int, Task> Code)
    {
        for (int i = len - 1; i >= 0; i--)
        {
            var l = list[i];
            await Code.Invoke(l, i);
        }
    }
    #endregion

    #region Foreach Reverse with break
    public static void ExForEachReverse<T>(this List<T> list, OnDoAnything<T> Code, WhenToDoFunc<T> whenBreak)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = list.Count - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            Code?.Invoke(l);
        }
    }
    public static async Task ExForEachReverse<T>(this List<T> list, Func<T, Task> Code, WhenToDoFunc<T> whenBreak)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = list.Count - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            await Code?.Invoke(l);
        }
    }
    public static void ExForEachReverse_NoCheck<T>(this List<T> list, OnDoAnything<T> Code, WhenToDoFunc<T> whenBreak)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            Code.Invoke(l);
        }
    }
    public static async Task ExForEachReverse_NoCheck<T>(this List<T> list, Func<T, Task> Code, WhenToDoFunc<T> whenBreak)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            await Code.Invoke(l);
        }
    }
    public static void ExForEachReverse_NoCheck<T>(this List<T> list, int len, OnDoAnything<T> Code, WhenToDoFunc<T> whenBreak)
    {
        for (int i = len - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            Code.Invoke(l);
        }
    }
    public static async Task ExForEachReverse_NoCheck<T>(this List<T> list, int len, Func<T, Task> Code, WhenToDoFunc<T> whenBreak)
    {
        for (int i = len - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            await Code.Invoke(l);
        }
    }
    public static void ExForEachReverse<T>(this T[] list, OnDoAnything<T> Code, WhenToDoFunc<T> whenBreak)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = list.Length - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            Code?.Invoke(l);
        }
    }
    public static async Task ExForEachReverse<T>(this T[] list, Func<T, Task> Code, WhenToDoFunc<T> whenBreak)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = list.Length - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            await Code?.Invoke(l);
        }
    }
    public static void ExForEachReverse_NoCheck<T>(this T[] list, OnDoAnything<T> Code, WhenToDoFunc<T> whenBreak)
    {
        for (int i = list.Length - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            Code.Invoke(l);
        }
    }
    public static async Task ExForEachReverse_NoCheck<T>(this T[] list, Func<T, Task> Code, WhenToDoFunc<T> whenBreak)
    {
        for (int i = list.Length - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            await Code.Invoke(l);
        }
    }
    public static void ExForEachReverse_NoCheck<T>(this T[] list, int len, OnDoAnything<T> Code, WhenToDoFunc<T> whenBreak)
    {
        for (int i = len - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            Code.Invoke(l);
        }
    }
    public static async Task ExForEachReverse_NoCheck<T>(this T[] list, int len, Func<T, Task> Code, WhenToDoFunc<T> whenBreak)
    {
        for (int i = len - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l)) { break; }
            await Code.Invoke(l);
        }
    }
    #endregion

    #region Foreach Reverse With ID
    public static void ExForEachReverse<T>(this List<T> list, OnDoAnything<T, int> Code, WhenToDoFunc<T, int> whenBreak)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = list.Count - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            Code?.Invoke(l, i);
        }
    }
    public static async Task ExForEachReverse<T>(this List<T> list, Func<T, int, Task> Code, WhenToDoFunc<T, int> whenBreak)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = list.Count - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            await Code?.Invoke(l, i);
        }
    }
    public static void ExForEachReverse_NoCheck<T>(this List<T> list, OnDoAnything<T, int> Code, WhenToDoFunc<T, int> whenBreak)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            Code.Invoke(l, i);
        }
    }
    public static async Task ExForEachReverse_NoCheck<T>(this List<T> list, Func<T, int, Task> Code, WhenToDoFunc<T, int> whenBreak)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            await Code.Invoke(l, i);
        }
    }
    public static void ExForEachReverse_NoCheck<T>(this List<T> list, int len, OnDoAnything<T, int> Code, WhenToDoFunc<T, int> whenBreak)
    {
        for (int i = len - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            Code.Invoke(l, i);
        }
    }
    public static async Task ExForEachReverse_NoCheck<T>(this List<T> list, int len, Func<T, int, Task> Code, WhenToDoFunc<T, int> whenBreak)
    {
        for (int i = len - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            await Code.Invoke(l, i);
        }
    }
    public static void ExForEachReverse<T>(this T[] list, OnDoAnything<T, int> Code, WhenToDoFunc<T, int> whenBreak)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = list.Length - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            Code?.Invoke(l, i);
        }
    }
    public static async Task ExForEachReverse<T>(this T[] list, Func<T, int, Task> Code, WhenToDoFunc<T, int> whenBreak)
    {
        if (list.ExIsValid() == false) { return; }
        for (int i = list.Length - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            await Code?.Invoke(l, i);
        }
    }
    public static void ExForEachReverse_NoCheck<T>(this T[] list, OnDoAnything<T, int> Code, WhenToDoFunc<T, int> whenBreak)
    {
        for (int i = list.Length - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            Code.Invoke(l, i);
        }
    }
    public static async Task ExForEachReverse_NoCheck<T>(this T[] list, Func<T, int, Task> Code, WhenToDoFunc<T, int> whenBreak)
    {
        for (int i = list.Length - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            await Code.Invoke(l, i);
        }
    }
    public static void ExForEachReverse_NoCheck<T>(this T[] list, int len, OnDoAnything<T, int> Code, WhenToDoFunc<T, int> whenBreak)
    {
        for (int i = len - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            Code.Invoke(l, i);
        }
    }
    public static async Task ExForEachReverse_NoCheck<T>(this T[] list, int len, Func<T, int, Task> Code, WhenToDoFunc<T, int> whenBreak)
    {
        for (int i = len - 1; i >= 0; i--)
        {
            var l = list[i];
            if (whenBreak.Invoke(l, i)) { break; }
            await Code.Invoke(l, i);
        }
    }
    #endregion

    public static bool ExHasAnyNull<T>(this T[] array) where T : class
    { 
        return array.ExContains(null); 
    }
    public static bool ExHasAnyNull<T>(this List<T> list) where T : class
    {
        return list.Contains(null);
    }
    public static int ExNotNullCount<T>(this T[] array) where T : class
    {
        var count = 0;
        array.ExForEach((t) =>
        {
            if (t != null) { count++; }
        });
        return count;
    }
    public static int ExNotNullCount<T>(this List<T> list) where T : class
    {
        var count = 0;
        list.ExForEach((t) =>
        {
            if (t != null) { count++; }
        });
        return count;
    }
    public static int ExNullCount<T>(this T[] array) where T : class
    {
        var count = 0;
        array.ExForEach((t) =>
        {
            if (t == null) { count++; }
        });
        return count;
    }
    public static int ExNullCount<T>(this List<T> list) where T : class
    {
        var count = 0;
        list.ExForEach((t) =>
        {
            if (t == null) { count++; }
        });
        return count;
    }
    public static List<T> ExGetListWithCount<T>(this List<T> list, int count)
    {
        var result = new List<T>();
        for (int i = 0; i < count; i++)
        {
            result.Add(default);
        }
        return result;
    }
    public static bool ExIsValid<T>(this T[] array) { return array != null && array.Length > 0; }
    public static bool ExIsValid<T>(this List<T> list) { return list != null && list.Count > 0; }
    public static List<T> ExRemoveNulls<T>(this List<T> list) where T : class
    {
        var result = new List<T>();
        if (list != null && list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                if (l == null) { continue; }
                result.Add(l);
            }
        }
        return result;
    }
    public static bool ExContains<T>(this T[] array, T item) where T : class
    {
        bool contains = false;
        if (array != null && array.Length > 0)
        {
            for (int i = 0; i < array.Length; i++)
            {
                var it = array[i];
                if (it == item)
                {
                    contains = true;
                    break;
                }
            }
        }
        return contains;
    }
    public static bool ExContainsOptimized<T>(this List<T> lst, T t) where T : class
    {
        var contains = false;
        if (lst != null)
        {
            var len = lst.Count;
            if (len > 0)
            {
                for (int i = 0; i < len; i++)
                {
                    var it = lst[i];
                    if (ReferenceEquals(it, t))
                    {
                        contains = true;
                        break;
                    }
                }
            }
        }
        return contains;
    }
}
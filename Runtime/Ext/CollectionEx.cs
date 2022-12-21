using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

namespace UnityExt
{
    public static class ExCollection
    {
        #region Foreach
        public static void ExForEach<T>(this IList<T> list, OnDoAnything<T> Code) where T : struct
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                Code?.Invoke(l);
            }
        }
        public static async Task ExForEachAsync<T>(this IList<T> list, Func<T, Task> Code) where T : struct
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                await Code?.Invoke(l);
            }
        }
        public static void ExForEachSafe<T>(this IList<T> list, OnDoAnything<T> Code) where T : class
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                if (l == null) { continue; }
                Code?.Invoke(l);
            }
        }
        public static async Task ExForEachAsyncSafe<T>(this IList<T> list, Func<T, Task> Code) where T : class
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                if (l == null) { continue; }
                await Code?.Invoke(l);
            }
        }
        public static void ExForEach_NoCheck<T>(this IList<T> list, OnDoAnything<T> Code)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                Code.Invoke(l);
            }
        }
        public static async Task ExForEach_NoCheckAsync<T>(this IList<T> list, Func<T, Task> Code)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                await Code.Invoke(l);
            }
        }
        public static void ExForEach_NoCheck<T>(this IList<T> list, int len, OnDoAnything<T> Code)
        {
            for (int i = 0; i < len; i++)
            {
                var l = list[i];
                Code.Invoke(l);
            }
        }
        public static async Task ExForEach_NoCheckAsync<T>(this IList<T> list, int len, Func<T, Task> Code)
        {
            for (int i = 0; i < len; i++)
            {
                var l = list[i];
                await Code.Invoke(l);
            }
        }
        #endregion

        #region Foreach With ID
        public static void ExForEach<T>(this IList<T> list, OnDoAnything<T, int> Code) where T : struct
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                Code?.Invoke(l, i);
            }
        }
        public static async Task ExForEachAsync<T>(this IList<T> list, Func<T, int, Task> Code) where T : struct
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                await Code?.Invoke(l, i);
            }
        }
        public static void ExForEachSafe<T>(this IList<T> list, OnDoAnything<T, int> Code) where T : class
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                if (l == null) { continue; }
                Code?.Invoke(l, i);
            }
        }
        public static async Task ExForEachAsyncSafe<T>(this IList<T> list, Func<T, int, Task> Code) where T : class
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                if (l == null) { continue; }
                await Code?.Invoke(l, i);
            }
        }
        public static void ExForEach_NoCheck<T>(this IList<T> list, OnDoAnything<T, int> Code)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                Code.Invoke(l, i);
            }
        }
        public static async Task ExForEach_NoCheckAsync<T>(this IList<T> list, Func<T, int, Task> Code)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                await Code.Invoke(l, i);
            }
        }
        public static void ExForEach_NoCheck<T>(this IList<T> list, int len, OnDoAnything<T, int> Code)
        {
            for (int i = 0; i < len; i++)
            {
                var l = list[i];
                Code.Invoke(l, i);
            }
        }
        public static async Task ExForEach_NoCheckAsync<T>(this IList<T> list, int len, Func<T, int, Task> Code)
        {
            for (int i = 0; i < len; i++)
            {
                var l = list[i];
                await Code.Invoke(l, i);
            }
        }
        #endregion

        #region Foreach with break
        public static void ExForEach<T>(this IList<T> list, OnDoAnything<T> Code, WhenToDoFunc<T> whenBreak) where T : struct
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                if (whenBreak.Invoke(l)) { break; }
                Code?.Invoke(l);
            }
        }
        public static async Task ExForEachAsync<T>(this IList<T> list, Func<T, Task> Code, WhenToDoFunc<T> whenBreak) where T : struct
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                if (whenBreak.Invoke(l)) { break; }
                await Code?.Invoke(l);
            }
        }
        public static void ExForEachSafe<T>(this IList<T> list, OnDoAnything<T> Code, WhenToDoFunc<T> whenBreak) where T : class
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                if (l == null) { continue; }
                if (whenBreak.Invoke(l)) { break; }
                Code?.Invoke(l);
            }
        }
        public static async Task ExForEachAsyncSafe<T>(this IList<T> list, Func<T, Task> Code, WhenToDoFunc<T> whenBreak) where T : class
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                if (l == null) { continue; }
                if (whenBreak.Invoke(l)) { break; }
                await Code?.Invoke(l);
            }
        }
        public static void ExForEach_NoCheck<T>(this IList<T> list, OnDoAnything<T> Code, WhenToDoFunc<T> whenBreak)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                if (whenBreak.Invoke(l)) { break; }
                Code.Invoke(l);
            }
        }
        public static async Task ExForEach_NoCheckAsync<T>(this IList<T> list, Func<T, Task> Code, WhenToDoFunc<T> whenBreak)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                if (whenBreak.Invoke(l)) { break; }
                await Code.Invoke(l);
            }
        }
        public static void ExForEach_NoCheck<T>(this IList<T> list, int len, OnDoAnything<T> Code, WhenToDoFunc<T> whenBreak)
        {
            for (int i = 0; i < len; i++)
            {
                var l = list[i];
                if (whenBreak.Invoke(l)) { break; }
                Code.Invoke(l);
            }
        }
        public static async Task ExForEach_NoCheckAsync<T>(this IList<T> list, int len, Func<T, Task> Code, WhenToDoFunc<T> whenBreak)
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
        public static void ExForEach<T>(this IList<T> list, OnDoAnything<T, int> Code, WhenToDoFunc<T, int> whenBreak) where T : struct
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                if (whenBreak.Invoke(l, i)) { break; }
                Code?.Invoke(l, i);
            }
        }
        public static async Task ExForEachAsync<T>(this IList<T> list, Func<T, int, Task> Code, WhenToDoFunc<T, int> whenBreak) where T : struct
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                if (whenBreak.Invoke(l, i)) { break; }
                await Code?.Invoke(l, i);
            }
        }
        public static void ExForEachSafe<T>(this IList<T> list, OnDoAnything<T, int> Code, WhenToDoFunc<T, int> whenBreak) where T : class
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                if (l == null) { continue; }
                if (whenBreak.Invoke(l, i)) { break; }
                Code?.Invoke(l, i);
            }
        }
        public static async Task ExForEachAsyncSafe<T>(this IList<T> list, Func<T, int, Task> Code, WhenToDoFunc<T, int> whenBreak) where T : class
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                if (l == null) { continue; }
                if (whenBreak.Invoke(l, i)) { break; }
                await Code?.Invoke(l, i);
            }
        }
        public static void ExForEach_NoCheck<T>(this IList<T> list, OnDoAnything<T, int> Code, WhenToDoFunc<T, int> whenBreak)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                if (whenBreak.Invoke(l, i)) { break; }
                Code.Invoke(l, i);
            }
        }
        public static async Task ExForEach_NoCheckAsync<T>(this IList<T> list, Func<T, int, Task> Code, WhenToDoFunc<T, int> whenBreak)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var l = list[i];
                if (whenBreak.Invoke(l, i)) { break; }
                await Code.Invoke(l, i);
            }
        }
        public static void ExForEach_NoCheck<T>(this IList<T> list, int len, OnDoAnything<T, int> Code, WhenToDoFunc<T, int> whenBreak)
        {
            for (int i = 0; i < len; i++)
            {
                var l = list[i];
                if (whenBreak.Invoke(l, i)) { break; }
                Code.Invoke(l, i);
            }
        }
        public static async Task ExForEach_NoCheckAsync<T>(this IList<T> list, int len, Func<T, int, Task> Code, WhenToDoFunc<T, int> whenBreak)
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
        public static void ExForEachReverse<T>(this IList<T> list, OnDoAnything<T> Code) where T : struct
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                Code?.Invoke(l);
            }
        }
        public static async Task ExForEachReverseAsync<T>(this IList<T> list, Func<T, Task> Code) where T : struct
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                await Code?.Invoke(l);
            }
        }
        public static void ExForEachReverseSafe<T>(this IList<T> list, OnDoAnything<T> Code) where T : class
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                if (l == null) { continue; }
                Code?.Invoke(l);
            }
        }
        public static async Task ExForEachReverseAsyncSafe<T>(this IList<T> list, Func<T, Task> Code) where T : class
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                if (l == null) { continue; }
                await Code?.Invoke(l);
            }
        }
        public static void ExForEachReverse_NoCheck<T>(this IList<T> list, OnDoAnything<T> Code)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                Code.Invoke(l);
            }
        }
        public static async Task ExForEachReverse_NoCheckAsync<T>(this IList<T> list, Func<T, Task> Code)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                await Code.Invoke(l);
            }
        }
        public static void ExForEachReverse_NoCheck<T>(this IList<T> list, int len, OnDoAnything<T> Code)
        {
            for (int i = len - 1; i >= 0; i--)
            {
                var l = list[i];
                Code.Invoke(l);
            }
        }
        public static async Task ExForEachReverse_NoCheckAsync<T>(this IList<T> list, int len, Func<T, Task> Code)
        {
            for (int i = len - 1; i >= 0; i--)
            {
                var l = list[i];
                await Code.Invoke(l);
            }
        }
        #endregion

        #region Foreach Reverse With ID
        public static void ExForEachReverse<T>(this IList<T> list, OnDoAnything<T, int> Code) where T : struct
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                Code?.Invoke(l, i);
            }
        }
        public static async Task ExForEachReverseAsync<T>(this IList<T> list, Func<T, int, Task> Code) where T : struct
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                await Code?.Invoke(l, i);
            }
        }
        public static void ExForEachReverseSafe<T>(this IList<T> list, OnDoAnything<T, int> Code) where T : class
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                if (l == null) { continue; }
                Code?.Invoke(l, i);
            }
        }
        public static async Task ExForEachReverseAsyncSafe<T>(this IList<T> list, Func<T, int, Task> Code) where T : class
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                if (l == null) { continue; }
                await Code?.Invoke(l, i);
            }
        }
        public static void ExForEachReverse_NoCheck<T>(this IList<T> list, OnDoAnything<T, int> Code)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                Code.Invoke(l, i);
            }
        }
        public static async Task ExForEachReverse_NoCheckAsync<T>(this IList<T> list, Func<T, int, Task> Code)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                await Code.Invoke(l, i);
            }
        }
        public static void ExForEachReverse_NoCheck<T>(this IList<T> list, int len, OnDoAnything<T, int> Code)
        {
            for (int i = len - 1; i >= 0; i--)
            {
                var l = list[i];
                Code.Invoke(l, i);
            }
        }
        public static async Task ExForEachReverse_NoCheckAsync<T>(this IList<T> list, int len, Func<T, int, Task> Code)
        {
            for (int i = len - 1; i >= 0; i--)
            {
                var l = list[i];
                await Code.Invoke(l, i);
            }
        }
        #endregion

        #region Foreach Reverse with break
        public static void ExForEachReverse<T>(this IList<T> list, OnDoAnything<T> Code, WhenToDoFunc<T> whenBreak) where T : struct
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                if (whenBreak.Invoke(l)) { break; }
                Code?.Invoke(l);
            }
        }
        public static async Task ExForEachReverseAsync<T>(this IList<T> list, Func<T, Task> Code, WhenToDoFunc<T> whenBreak) where T : struct
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                if (whenBreak.Invoke(l)) { break; }
                await Code?.Invoke(l);
            }
        }
        public static void ExForEachReverseSafe<T>(this IList<T> list, OnDoAnything<T> Code, WhenToDoFunc<T> whenBreak) where T : class
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                if (l == null) { continue; }
                if (whenBreak.Invoke(l)) { break; }
                Code?.Invoke(l);
            }
        }
        public static async Task ExForEachReverseAsyncSafe<T>(this IList<T> list, Func<T, Task> Code, WhenToDoFunc<T> whenBreak) where T : class
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                if (l == null) { continue; }
                if (whenBreak.Invoke(l)) { break; }
                await Code?.Invoke(l);
            }
        }
        public static void ExForEachReverse_NoCheck<T>(this IList<T> list, OnDoAnything<T> Code, WhenToDoFunc<T> whenBreak)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                if (whenBreak.Invoke(l)) { break; }
                Code.Invoke(l);
            }
        }
        public static async Task ExForEachReverse_NoCheckAsync<T>(this IList<T> list, Func<T, Task> Code, WhenToDoFunc<T> whenBreak)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                if (whenBreak.Invoke(l)) { break; }
                await Code.Invoke(l);
            }
        }
        public static void ExForEachReverse_NoCheck<T>(this IList<T> list, int len, OnDoAnything<T> Code, WhenToDoFunc<T> whenBreak)
        {
            for (int i = len - 1; i >= 0; i--)
            {
                var l = list[i];
                if (whenBreak.Invoke(l)) { break; }
                Code.Invoke(l);
            }
        }
        public static async Task ExForEachReverse_NoCheckAsync<T>(this IList<T> list, int len, Func<T, Task> Code, WhenToDoFunc<T> whenBreak)
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
        public static void ExForEachReverse<T>(this IList<T> list, OnDoAnything<T, int> Code, WhenToDoFunc<T, int> whenBreak) where T : struct
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                if (whenBreak.Invoke(l, i)) { break; }
                Code?.Invoke(l, i);
            }
        }
        public static async Task ExForEachReverseAsync<T>(this IList<T> list, Func<T, int, Task> Code, WhenToDoFunc<T, int> whenBreak) where T : struct
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                if (whenBreak.Invoke(l, i)) { break; }
                await Code?.Invoke(l, i);
            }
        }
        public static void ExForEachReverseSafe<T>(this IList<T> list, OnDoAnything<T, int> Code, WhenToDoFunc<T, int> whenBreak) where T : class
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                if (l == null) { continue; }
                if (whenBreak.Invoke(l, i)) { break; }
                Code?.Invoke(l, i);
            }
        }
        public static async Task ExForEachReverseAsyncSafe<T>(this IList<T> list, Func<T, int, Task> Code, WhenToDoFunc<T, int> whenBreak) where T : class
        {
            if (list.ExIsValid() == false) { return; }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                if (l == null) { continue; }
                if (whenBreak.Invoke(l, i)) { break; }
                await Code?.Invoke(l, i);
            }
        }
        public static void ExForEachReverse_NoCheck<T>(this IList<T> list, OnDoAnything<T, int> Code, WhenToDoFunc<T, int> whenBreak)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                if (whenBreak.Invoke(l, i)) { break; }
                Code.Invoke(l, i);
            }
        }
        public static async Task ExForEachReverse_NoCheckAsync<T>(this IList<T> list, Func<T, int, Task> Code, WhenToDoFunc<T, int> whenBreak)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var l = list[i];
                if (whenBreak.Invoke(l, i)) { break; }
                await Code.Invoke(l, i);
            }
        }
        public static void ExForEachReverse_NoCheck<T>(this IList<T> list, int len, OnDoAnything<T, int> Code, WhenToDoFunc<T, int> whenBreak)
        {
            for (int i = len - 1; i >= 0; i--)
            {
                var l = list[i];
                if (whenBreak.Invoke(l, i)) { break; }
                Code.Invoke(l, i);
            }
        }
        public static async Task ExForEachReverse_NoCheckAsync<T>(this IList<T> list, int len, Func<T, int, Task> Code, WhenToDoFunc<T, int> whenBreak)
        {
            for (int i = len - 1; i >= 0; i--)
            {
                var l = list[i];
                if (whenBreak.Invoke(l, i)) { break; }
                await Code.Invoke(l, i);
            }
        }
        #endregion
        public static void ExRemoveDuplicates<T>(this IList<T> list)
        {
            HashSet<T> uniqueValues = new HashSet<T>();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (!uniqueValues.Add(list[i]))
                {
                    list.RemoveAt(i);
                }
            }
        }
        static System.Random _random = new System.Random();
        public static void ExShuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
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
            array.ExForEachSafe((t) =>
            {
                if (t != null) { count++; }
            });
            return count;
        }
        public static int ExNotNullCount<T>(this List<T> list) where T : class
        {
            var count = 0;
            list.ExForEachSafe((t) =>
            {
                if (t != null) { count++; }
            });
            return count;
        }
        public static int ExNullCount<T>(this T[] array) where T : class
        {
            var count = 0;
            array.ExForEachSafe((t) =>
            {
                if (t == null) { count++; }
            });
            return count;
        }
        public static int ExNullCount<T>(this List<T> list) where T : class
        {
            var count = 0;
            list.ExForEachSafe((t) =>
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
        public static void ExAddRangeUniquely<T>(this IList<T> list, IList<T> toAdd) where T : class
        {
            toAdd.ExForEachSafe((i) =>
            {
                if (list != null && !list.Contains(i))
                {
                    list.Add(i);
                }
            });
        }
        public static List<T> ExShallowCopy<T>(this List<T> list) where T : class
        {
            var result = new List<T>();
            list.ExForEachSafe((i) => { result.Add(i); });
            return result;
        }
        public static void ExAddDataRangeUniquely<T>(this IList<T> list, IList<T> toAdd) where T : struct
        {
            toAdd.ExForEach((i) =>
            {
                if (list != null && !list.Contains(i))
                {
                    list.Add(i);
                }
            });
        }
        public static List<T> ExShallowCopyData<T>(this List<T> list) where T : struct
        {
            var result = new List<T>();
            list.ExForEach((i) => { result.Add(i); });
            return result;
        }
        public static List<T> ExDeepCopy<T>(this List<T> list) where T : class, ICloneable
        {
            var result = new List<T>();
            list.ExForEachSafe((i) => { result.Add(i.Clone() as T); });
            return result;
        }
        public static T[] ExShallowCopy<T>(this T[] list) where T : class
        {
            var result = new T[list.Length];
            list.ExForEachSafe((i, index) => { result[index] = i; });
            return result;
        }
        public static T[] ExShallowCopyData<T>(this T[] list) where T : struct
        {
            var result = new T[list.Length];
            list.ExForEach((i, index) => { result[index] = i; });
            return result;
        }
        public static T[] ExDeepCopy<T>(this T[] list) where T : class, ICloneable
        {
            var result = new T[list.Length];
            list.ExForEachSafe((i, index) => { result[index] = i.Clone() as T; });
            return result;
        }
        public static bool ExIsValid<T>(this T[] array) { return array != null && array.Length > 0; }
        public static bool ExIsValid<T>(this IList<T> list) { return list != null && list.Count > 0; }
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
}
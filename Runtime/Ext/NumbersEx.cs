using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExt
{
    public static class NumbersEx
    {
        public static bool ExIsBetween(this float a, float min, float max)
        {
            return a >= min && a < max;
        }
        public static bool ExIsAround(this float a, float b, float tolerence = 0.01f)
        {
            return a > b - tolerence && a < b + tolerence;
        }
        public static float ExRemap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
        public static string ExGetCoinUIString(this long coinNum)
        {
            long MarkL_K = 9999L;
            long MarkL_M = 1000L * 100L * 10L - 1;
            long MarkL_B = 1000L * 100L * 10L * 1000L - 1;
            long MarkL_T = 1000L * 100L * 10L * 1000L * 1000L - 1;

            string coinStr = "";
            if (coinNum <= MarkL_K)
            {
                coinStr = "" + coinNum.ToString();
            }
            else if (coinNum > MarkL_K && coinNum <= MarkL_M)
            {
                double ln = ((double)coinNum / (double)1000L);
                coinStr = ln.ToString("0.0") + "K";
            }
            else if (coinNum > MarkL_M && coinNum <= MarkL_B)
            {
                double ln = ((double)coinNum / (double)(MarkL_M + 1));
                coinStr = ln.ToString("0.0") + "M";
            }
            else if (coinNum > MarkL_B && coinNum <= MarkL_T)
            {
                double ln = ((double)coinNum / (double)(MarkL_B + 1));
                coinStr = ln.ToString("0.0") + "B";
            }
            else if (coinNum > MarkL_T)
            {
                double ln = ((double)coinNum / (double)(MarkL_T + 1));
                coinStr = ln.ToString("0.0") + "T";
            }
            return coinStr;
        }
        public static string ExGetCoinUIString(this int coinNum)
        {
            int MarkI_K = 9999;
            int MarkI_M = 1000 * 100 * 10 - 1;
            int MarkI_B = 1000 * 100 * 10 * 1000 - 1;

            string coinStr = "";
            if (coinNum <= MarkI_K)
            {
                coinStr = "" + coinNum.ToString();
            }
            else if (coinNum > MarkI_K && coinNum <= MarkI_M)
            {
                double ln = ((double)coinNum / (double)1000L);
                coinStr = ln.ToString("0.0") + "K";
            }
            else if (coinNum > MarkI_M && coinNum <= MarkI_B)
            {
                double ln = ((double)coinNum / (double)(MarkI_M + 1));
                coinStr = ln.ToString("0.0") + "M";
            }
            else if (coinNum > MarkI_B)
            {
                double ln = ((double)coinNum / (double)(MarkI_B + 1));
                coinStr = ln.ToString("0.0") + "B";
            }
            return coinStr;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UnityExt
{
    public class PlayerStorage<T> where T : struct
    {
        void GetValueFromStringError(string customMsg)
        {
            var txt = "Could not parse data! additional info msg: " + customMsg;
#if DEVELOPMENT_BUILD
		new Exception(txt);
#else
            Debug.LogError(txt);
#endif
        }
        T GetVal(string str)
        {
            T result = default;
            if (typeof(T) == typeof(int))
            {
                int val = 0;
                var success = int.TryParse(str, out val);
                if (success == false)
                {
                    GetValueFromStringError("int->value:" + str);
                }
                result = (T)(object)val;
            }
            else if (typeof(T) == typeof(long))
            {
                long val = 0;
                var success = long.TryParse(str, out val);
                if (success == false)
                {
                    GetValueFromStringError("long->value:" + str);
                }
                result = (T)(object)val;
            }
            else if (typeof(T) == typeof(float))
            {
                float val = 0;
                var success = float.TryParse(str, out val);
                if (success == false)
                {
                    GetValueFromStringError("float->value:" + str);
                }
                result = (T)(object)val;
            }
            else if (typeof(T) == typeof(double))
            {
                double val = 0;
                var success = double.TryParse(str, out val);
                if (success == false)
                {
                    GetValueFromStringError("double->value:" + str);
                }
                result = (T)(object)val;
            }
            else if (typeof(T) == typeof(bool))
            {
                bool val = false;
                var success = bool.TryParse(str, out val);
                if (success == false)
                {
                    GetValueFromStringError("bool->value:" + str);
                }
                result = (T)(object)val;
            }
            else if (typeof(T).IsEnum)
            {
                T val = default;
                var success = Enum.TryParse<T>(str, out val);
                if (success == false)
                {
                    GetValueFromStringError("Enum->value:" + str);
                }
                result = (T)(object)val;
            }
            else if (typeof(T) == typeof(Ray))
            {
                var wrapper = new Ray_Wrapper();
                wrapper = JsonUtility.FromJson<Ray_Wrapper>(str);
                result = (T)(object)(new Ray(wrapper.rayOrigin, wrapper.rayDirection));
            }
            else if (typeof(T) == typeof(Ray2D))
            {
                var wrapper = new Ray2D_Wrapper();
                wrapper = JsonUtility.FromJson<Ray2D_Wrapper>(str);
                result = (T)(object)(new Ray2D(wrapper.rayOrigin, wrapper.rayDirection));
            }
            else if (typeof(T) == typeof(Bounds))
            {
                var wrapper = new Bounds_Wrapper();
                wrapper = JsonUtility.FromJson<Bounds_Wrapper>(str);
                result = (T)(object)(new Bounds(wrapper.center, wrapper.size));
            }
            else if (typeof(T) == typeof(BoundsInt))
            {
                var wrapper = new BoundsInt_Wrapper();
                wrapper = JsonUtility.FromJson<BoundsInt_Wrapper>(str);
                result = (T)(object)(new BoundsInt(wrapper.position, wrapper.size));
            }
            else if (typeof(T) == typeof(Rect))
            {
                var wrapper = new Rect_Wrapper();
                wrapper = JsonUtility.FromJson<Rect_Wrapper>(str);
                result = (T)(object)(new Rect(wrapper.position, wrapper.size));
            }
            else if (typeof(T) == typeof(RectInt))
            {
                var wrapper = new RectInt_Wrapper();
                wrapper = JsonUtility.FromJson<RectInt_Wrapper>(str);
                result = (T)(object)(new RectInt(wrapper.position, wrapper.size));
            }
            else if (typeof(T) == typeof(Vector2Int))
            {
                var wrapper = new Vector2Int_Wrapper();
                wrapper = JsonUtility.FromJson<Vector2Int_Wrapper>(str);
                result = (T)(object)(wrapper.data);
            }
            else if (typeof(T) == typeof(Vector3Int))
            {
                var wrapper = new Vector3Int_Wrapper();
                wrapper = JsonUtility.FromJson<Vector3Int_Wrapper>(str);
                result = (T)(object)(wrapper.data);
            }
            else if (typeof(T) == typeof(Vector2))
            {
                var wrapper = new Vector2_Wrapper();
                wrapper = JsonUtility.FromJson<Vector2_Wrapper>(str);
                result = (T)(object)(wrapper.data);
            }
            else if (typeof(T) == typeof(Vector3))
            {
                var wrapper = new Vector3_Wrapper();
                wrapper = JsonUtility.FromJson<Vector3_Wrapper>(str);
                result = (T)(object)(wrapper.data);
            }
            else
            {
                GetValueFromStringError("Invalid or unsupported type! " + typeof(T) + " You might want to use a more sophisticated save system named 'KSaveDataManager'" +
                    ". Get it from here 'https://github.com/kaiyumcg/KSaveDataManager'");
            }
            return result;
        }

        string GetString(T data)
        {
            string result = "";
            if (typeof(T) == typeof(int))
            {
                result = (int)(object)data + "";
            }
            else if (typeof(T) == typeof(long))
            {
                result = (long)(object)data + "";
            }
            else if (typeof(T) == typeof(float))
            {
                result = (float)(object)data + "";
            }
            else if (typeof(T) == typeof(double))
            {
                result = (double)(object)data + "";
            }
            else if (typeof(T) == typeof(bool))
            {
                result = (bool)(object)data + "";
            }
            else if (typeof(T).IsEnum)
            {
                result = (T)(object)data + "";
            }
            else if (typeof(T) == typeof(Ray))
            {
                var convData = (Ray)(object)data;
                var wrapper = new Ray_Wrapper() { rayOrigin = convData.origin, rayDirection = convData.direction };
                result = JsonUtility.ToJson(wrapper);
            }
            else if (typeof(T) == typeof(Ray2D))
            {
                var convData = (Ray2D)(object)data;
                var wrapper = new Ray2D_Wrapper() { rayOrigin = convData.origin, rayDirection = convData.direction };
                result = JsonUtility.ToJson(wrapper);
            }
            else if (typeof(T) == typeof(Bounds))
            {
                var convData = (Bounds)(object)data;
                var wrapper = new Bounds_Wrapper() { center = convData.center, size = convData.size };
                result = JsonUtility.ToJson(wrapper);
            }
            else if (typeof(T) == typeof(BoundsInt))
            {
                var convData = (BoundsInt)(object)data;
                var wrapper = new BoundsInt_Wrapper() { position = convData.position, size = convData.size };
                result = JsonUtility.ToJson(wrapper);
            }
            else if (typeof(T) == typeof(Rect))
            {
                var convData = (Rect)(object)data;
                var wrapper = new Rect_Wrapper() { position = convData.position, size = convData.size };
                result = JsonUtility.ToJson(wrapper);
            }
            else if (typeof(T) == typeof(RectInt))
            {
                var convData = (RectInt)(object)data;
                var wrapper = new RectInt_Wrapper() { position = convData.position, size = convData.size };
                result = JsonUtility.ToJson(wrapper);
            }
            else if (typeof(T) == typeof(Vector2Int))
            {
                var convData = (Vector2Int)(object)data;
                var wrapper = new Vector2Int_Wrapper() { data = convData };
                result = JsonUtility.ToJson(wrapper);
            }
            else if (typeof(T) == typeof(Vector3Int))
            {
                var convData = (Vector3Int)(object)data;
                var wrapper = new Vector3Int_Wrapper() { data = convData };
                result = JsonUtility.ToJson(wrapper);
            }
            else if (typeof(T) == typeof(Vector2))
            {
                var convData = (Vector2)(object)data;
                var wrapper = new Vector2_Wrapper() { data = convData };
                result = JsonUtility.ToJson(wrapper);
            }
            else if (typeof(T) == typeof(Vector3))
            {
                var convData = (Vector3)(object)data;
                var wrapper = new Vector3_Wrapper() { data = convData };
                result = JsonUtility.ToJson(wrapper);
            }
            else
            {
                GetValueFromStringError("Invalid or unsupported type!" + " You might want to use a more sophisticated save system named 'KSaveDataManager'" +
                    ". Get it from here 'https://github.com/kaiyumcg/KSaveDataManager'");
            }
            return result;
        }

        T vl = default;
        string identifier;
        UnityEngine.Events.UnityEvent<T> onUpdateData;

        public UnityEngine.Events.UnityEvent<T> OnUpdateData
        {
            get
            {
                if (onUpdateData == null) { onUpdateData = new UnityEngine.Events.UnityEvent<T>(); }
                return onUpdateData;
            }
        }
        public PlayerStorage(string identifier, T defaultValue)
        {
            this.identifier = identifier;
            var defaultValueString = GetString(defaultValue);
            var curValue = PlayerPrefs.GetString(this.identifier, defaultValueString);
            vl = GetVal(curValue);
            PlayerPrefs.SetString(this.identifier, curValue);
            PlayerPrefs.Save();
        }
        public T Value
        {
            get
            {
                var str = PlayerPrefs.GetString(identifier);
                vl = GetVal(str);
                return vl;
            }
            set
            {
                vl = value;
                var str = GetString(value);
                PlayerPrefs.SetString(identifier, str);
                PlayerPrefs.Save();
                OnUpdateData?.Invoke(vl);
            }
        }
    }
}
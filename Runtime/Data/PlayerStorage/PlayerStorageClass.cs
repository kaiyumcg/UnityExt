using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExt
{
    public class PlayerStorageClass<T> where T : class
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
            if (typeof(T) == typeof(string))
            {
                result = (T)(object)str;
            }
            else if (typeof(T).IsClass)
            {
                result = JsonUtility.FromJson<T>(str);
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
            if (typeof(T) == typeof(string))
            {
                result = (string)(object)data + "";
            }
            else if(typeof(T).IsClass)
            {
                result = JsonUtility.ToJson((object)data);
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
        public PlayerStorageClass(string identifier, T defaultValue)
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
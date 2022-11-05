using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
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

		void StringConversionError(string customMsg)
		{
			var txt = "Could not convert data into string! additional info msg: " + customMsg;
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
			else if (typeof(T) == typeof(string))
			{
				result = (T)(object)str;
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
			else if (typeof(T) == typeof(Vector3))
			{

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
			else
			{
				GetValueFromStringError("Invalid or unsupported type! " + typeof(T));
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
			else if (typeof(T) == typeof(string))
			{
				result = (string)(object)data + "";
			}
			else if (typeof(T) == typeof(bool))
			{
				result = (bool)(object)data + "";
			}
			else if (typeof(T).IsEnum)
			{
				result = (T)(object)data + "";
			}
			else
			{
				GetValueFromStringError("Invalid or unsupported type!");
			}
			return result;
		}

		T vl = default;
		string identifier;
		UnityEngine.Events.UnityEvent<T> onUpdateData;

		//TODO eta kintu initialization e o call hoy, meta feature e er jonno kono problem hobe ki na
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
			OnUpdateData?.Invoke(vl);
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

	public class BezierPathMinimal
	{
		public List<Vector3> pathPoints;
		private int segments;
		int pointCount;

		public BezierPathMinimal(int ptCount)
		{
			pathPoints = new List<Vector3>();
			pointCount = ptCount;
		}

		public void DeletePath()
		{
			pathPoints.Clear();
		}

		Vector3 BezierPathCalculation(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			float tt = t * t;
			float ttt = t * tt;
			float u = 1.0f - t;
			float uu = u * u;
			float uuu = u * uu;

			Vector3 B = new Vector3();
			B = uuu * p0;
			B += 3.0f * uu * t * p1;
			B += 3.0f * u * tt * p2;
			B += ttt * p3;

			return B;
		}

		public void CreateCurve(List<Vector3> controlPoints, Vector3 lastPt)
		{
			segments = controlPoints.Count / 3;

			for (int s = 0; s < controlPoints.Count - 3; s += 3)
			{
				Vector3 p0 = controlPoints[s];
				Vector3 p1 = controlPoints[s + 1];
				Vector3 p2 = controlPoints[s + 2];
				Vector3 p3 = controlPoints[s + 3];

				if (s == 0)
				{
					pathPoints.Add(BezierPathCalculation(p0, p1, p2, p3, 0.0f));
				}

				for (int p = 0; p < (pointCount / segments); p++)
				{
					float t = (1.0f / (pointCount / segments)) * p;
					Vector3 point = new Vector3();
					point = BezierPathCalculation(p0, p1, p2, p3, t);
					pathPoints.Add(point);
				}
			}
			pathPoints.Add(lastPt);
		}
	}

	[System.Serializable]
	public class RelativeTransform
	{
		[SerializeField] float backward, upward, side;
		[SerializeField] RangedVector3Float rotationOffset;
		public float Backward { get { return backward; } }
		public float Upward { get { return upward; } }
		public float Side { get { return side; } }
		public RangedVector3Float RotationOffset { get { return rotationOffset; } }

		public Vector3 GetRelativePosition(Transform from)
		{
			return from.position + from.forward * -backward + from.up * upward + from.right * side;
		}

		public bool GetLookAtRotation(Transform thisTransform, Transform target, ref Quaternion result)
		{
			var toTarget = target.position - thisTransform.position;
			toTarget = toTarget.normalized + rotationOffset.Get();
			if (Mathf.Approximately(toTarget.magnitude, 0.0f) == false)
			{
				result = Quaternion.LookRotation(toTarget, Vector3.up);
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	[System.Serializable]
	public class RangedVector3Float
	{
		[SerializeField, Range(-1.0f, 1.0f)] float x, y, z;
		public Vector3 Get()
		{
			return new Vector3(x, y, z);
		}
	}

	public delegate void OnDoAnythingFunc2(int level);
	public delegate void OnDoAnythingFunc3(int level, float raise, float totalNow);
	public delegate void OnDoAnythingFunc4(int level, float raise, float totalNow, float totalPrev);
	public delegate bool WhenToDoFunc();
	public delegate bool WhenToDoFunc<T>(T data1);
	public delegate bool WhenToDoFunc<T1, T2>(T1 data1, T2 data2);
	public delegate bool WhenToDoFunc<T1, T2, T3>(T1 data1, T2 data2, T3 data3);

	public delegate void OnDoAnytingPlayerFunc();
	public delegate void OnDoAnything();
	public delegate void OnDoAnything<T>(T data1);
	public delegate void OnDoAnything<T1, T2>(T1 data1, T2 data2);
	public delegate void OnDoAnything<T1, T2, T3>(T1 data1, T2 data2, T3 data3);
}
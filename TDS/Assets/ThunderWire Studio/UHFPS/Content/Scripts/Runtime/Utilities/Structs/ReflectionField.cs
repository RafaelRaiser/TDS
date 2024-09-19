using System;
using System.Reflection;
using UnityEngine;

namespace UHFPS.Runtime
{
    /// <summary>
    /// Reflection field allows you to get or set a bool value for a property, field, or method.
    /// </summary>
    [Serializable]
    public sealed class ReflectionField
    {
        public enum ReflectionType { Field, Property, Method };

        public ReflectionType ReflectType;
        public MonoBehaviour Instance;
        public string ReflectName;
        public bool ReflectDerived;

        public bool IsSet => Instance != null;

        private FieldInfo fieldInfo = null;
        private FieldInfo FieldInfo
        {
            get
            {
                if (fieldInfo == null)
                    fieldInfo = Instance.GetType().GetField(ReflectName, BindingFlags.Public | BindingFlags.Instance);

                return fieldInfo;
            }
        }

        private PropertyInfo propertyInfo = null;
        private PropertyInfo PropertyInfo
        {
            get
            {
                if (propertyInfo == null)
                    propertyInfo = Instance.GetType().GetProperty(ReflectName, BindingFlags.Public | BindingFlags.Instance);

                return propertyInfo;
            }
        }

        private MethodInfo methodInfo = null;
        private MethodInfo MethodInfo
        {
            get
            {
                if (methodInfo == null)
                    methodInfo = Instance.GetType().GetMethod(ReflectName, BindingFlags.Public | BindingFlags.Instance);

                return methodInfo;
            }
        }

        public bool Value
        {
            get
            {
                object value = ReflectType switch
                {
                    ReflectionType.Field => FieldInfo.GetValue(Instance),
                    ReflectionType.Property => PropertyInfo.GetValue(Instance),
                    ReflectionType.Method => MethodInfo.Invoke(Instance, new object[0]),
                    _ => throw new NullReferenceException()
                };

                if (value is bool _value)
                    return _value;

                Debug.LogError($"Reflection Error: The specified reflection type '{ReflectName}' is not a bool type!");
                return false;
            }

            set
            {
                try
                {
                    if (ReflectType == ReflectionType.Field)
                    {
                        FieldInfo.SetValue(Instance, value);
                    }
                    else if (ReflectType == ReflectionType.Property)
                    {
                        PropertyInfo.SetValue(Instance, value);
                    }
                    else
                    {
                        MethodInfo.Invoke(Instance, new object[] { value });
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }
    }
}
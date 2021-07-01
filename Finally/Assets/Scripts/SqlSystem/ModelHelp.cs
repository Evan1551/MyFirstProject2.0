using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace SqlSystem
{
    public class ModelHelp : Attribute
    {
        public bool IsCreated { get; set; }

        public string FieldName { get; set; }

        public string Type { get; set; }

        public bool IsPrimaryKey { get; set; }

        public bool IsCanBeNull { get; set; }

        public ModelHelp(bool isCreated, string fieldName, string type, bool isPrimaryKey,bool isCanBeNull)
        {
            IsCreated = isCreated;
            FieldName = fieldName;
            Type = type;
            IsPrimaryKey = isPrimaryKey;
            IsCanBeNull = isCanBeNull;
        }
    }

}

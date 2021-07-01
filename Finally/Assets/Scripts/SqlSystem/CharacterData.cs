using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.IO;

namespace SqlSystem
{
    
    public class CharacterData : BaseData
    {
        //name
        private string _name;

        [ModelHelp(true, "Name", "text", false, false)]
        public string Name { get { return _name; } set { _name = value; } }

        //height
        private float _height;

        [ModelHelp(true, "Height", "real", false, false)]
        public float Height { get { return _height; } set { _height = value; } }

        //sex
        private bool _sex;

        [ModelHelp(true, "Sex", "integer", false, false)]
        public bool Sex { get { return _sex; } set { _sex = value; } }
    }


    //定义BaseData类，因为id必须存在
    public class BaseData
    {
        //id
        private int _id;

        [ModelHelp(true, "Id", "integer", true, false)]
        public int Id { get { return _id; } set { _id = value; } }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SqlSystem
{
    public class TestDB : MonoBehaviour
    {

        void Start()
        {
            TestDb();
        }


        private void TestDb()
        {
            string dbPath = Application.dataPath + "/../Save/SaveData.db";
            //Debug.Log("ConnectSuccseed");
            //SqlDbConnect sqlDbConnect = new SqlDbConnect(dbPath);
            SqlDbCommand sql = new SqlDbCommand(dbPath);

            CharacterData c1 = new CharacterData()
            {
                Name = "A",
                Height = 1.98f,
                Sex = true
            };


            //CharacterData c2 = new CharacterData()
            //{
            //    Id = 2,
            //    Name = "Mary",
            //    Height = 1.61f,
            //    Sex = false
            //};

            //List<CharacterData> cList = new List<CharacterData>();
            //cList.Add(c1);
            //cList.Add(c2);

            //sql.Insert(c1);

            //sql.DeleteBySql<CharacterData>("Sex = true");

            //string condition1 = "Sex = 1";
            //string condition2 = "Name = 'Jack'";

            //List<string> conditionList = new List<string>();
            //conditionList.Add(condition1);
            //conditionList.Add(condition2);


            ////sql.UpdateByOrder(c1,5);
            //sql.UpdateBySql_Conditions(c1, conditionList, false);

            //²éÑ¯º¯Êý
            List<CharacterData> test = sql.SelectBySql<CharacterData>();
            //Debug.Log(test.Id);
            //Debug.Log(test.Name);
            //Debug.Log(test.Sex);
            //Debug.Log(test.Height);
        }








    }

}

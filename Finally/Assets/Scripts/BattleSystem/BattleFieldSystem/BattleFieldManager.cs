using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleSystem {
    public struct GridVector2
    {
        public int x;
        public int y;
        
        public GridVector2(int x,int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class BattleFieldManager: MonoBehaviour
    {
        public void ShowInfo()
        {
            BattleFieldCheck BFcheck = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<BattleFieldCheck>();
            Debug.Log(new Vector2(BFcheck.x, BFcheck.y));
            Debug.Log(BFcheck.transform.position.x);
        }

        internal Vector3 GridPosToWorld(GridVector2 gridPos)
        {
            Vector3 WorldPos = GridPosConvert(gridPos.x, gridPos.y);
            return WorldPos;
        }

        internal Vector3 GridPosConvert( int x, int y)
        {
            Vector3 WorldPos = new Vector3(0,0,0);

            WorldPos.x = x * 100 - 450;
            WorldPos.y = 0;
            WorldPos.z = y * -100 + 250;
            if (x <= 0 || y <= 0)
            {
                Debug.LogError("The parameter of GridPostion Error!");
            }
          
            return WorldPos;
        }

        internal Vector3 GridTranslationToWorld(GridVector2 gridTranslation)
        {
            Vector3 WorldTranslation = GridTranslationCovert(gridTranslation.x, gridTranslation.y);
            return WorldTranslation;
        }

        internal Vector3 GridTranslationCovert(int x, int y)
        {
            Vector3 WorldTranslation = new Vector3(0, 0, 0);

            WorldTranslation.x = x * 100;
            WorldTranslation.y = 0;
            WorldTranslation.z = y * -100;
            

            return WorldTranslation;
        }
    }
}


using BattleSystem;
using CharacterSystem;
using System.Collections;
using System.Collections.Generic;
using TurnSystem;
using UnityEngine;

public class Enemy2 : EnemyAction
{
    private Dictionary<int, GridVector2> ShortestLine;

    public override void DoAction()
    {

        Debug.Log("����1�ж���ʼ");

        ShortestLine = new Dictionary<int, GridVector2>();

        //��ȡHero1��Vector2����
        int hero_x = Hero_Model.Instance.character.BFC.x;
        int hero_y = Hero_Model.Instance.character.BFC.y;

        //��ȡEnemy��Vector2����
        int enemy_x = GetComponent<Character>().BFC.x;
        int enemy_y = GetComponent<Character>().BFC.y;

        //������˻غϿ�ʼʱ��Hero���ڵ��˹�����Χ�ڣ����˷��𹥻�
        if ((Mathf.Abs(enemy_x - hero_x) == 1 && Mathf.Abs(enemy_y - hero_y) == 0) || (Mathf.Abs(enemy_x - hero_x) == 0 && Mathf.Abs(enemy_y - hero_y) == 1))
        {
            Battle_Model.Instance.Targets.Add(Hero_Model.Instance.character);
            cmdLib.DealDamage(20);

            Character enemy1=GameObject.Find("Character_Ctrller").GetComponent<Character_Ctrller>().CreateCharacter(2, new GridVector2(enemy_x, enemy_y+ 1));
            Character enemy2 = GameObject.Find("Character_Ctrller").GetComponent<Character_Ctrller>().CreateCharacter(3, new GridVector2(enemy_x, enemy_y - 1));

            enemy1.info.Hp = GetComponent<Character>().info.Hp / 2;
            enemy2.info.Hp = GetComponent<Character>().info.Hp / 2;
            Battle_Model.Instance.enemys.Remove(GetComponent<Character>());
            Turn_Model.Instance.ini();
            GameObject.Find("TurnManager").GetComponent<Turn_Ctrller>().Ini();
            Destroy(gameObject,0.2f);
        }
        else
        {
            cmdLib.CharacterMove(Battle_Model.Instance.Current, CompareNumber(enemy_x, enemy_y, hero_x, hero_y));
        }

    }

    /// <summary>
    /// ���������Χ�ĸ��������ĸ���Hero���
    /// </summary>
    /// <param name="x">�������ڵ�x����</param>
    /// <param name="y">�������ڵ�y����</param>
    /// <param name="Hero_x">Hero���ڵ�x����</param>
    /// <param name="Hero_y">Hero���ڵ�y����</param>
    /// <returns></returns>
    public  GridVector2 CompareNumber(int x, int y, int Hero_x, int Hero_y)
    {
        ShortestLine.Clear();

        int line1 = 100;
        int line2 = 100;
        int line3 = 100;
        int line4 = 100;


        //�ҷ����ӵ�Hero�ľ���
        if (x + 1 < 8)
        {
            line1 = Mathf.Abs((x + 1) - Hero_x) + Mathf.Abs(y - Hero_y);
            if (!ShortestLine.ContainsKey(line1))
                ShortestLine.Add(line1, new GridVector2((x + 1), y));
        }

        //�Ϸ����ӵ�Hero�ľ���
        if (y - 1 > 0)
        {
            line2 = Mathf.Abs(x - Hero_x) + Mathf.Abs((y - 1) - Hero_y);
            if (!ShortestLine.ContainsKey(line2))
                ShortestLine.Add(line2, new GridVector2(x, (y - 1)));
        }

        //�󷽸��ӵ�Hero�ľ���
        if (x - 1 > 0)
        {
            line3 = Mathf.Abs((x - 1) - Hero_x) + Mathf.Abs(y - Hero_y);
            if (!ShortestLine.ContainsKey(line3))
                ShortestLine.Add(line3, new GridVector2((x - 1), y));
        }

        //�·����ӵ�Hero�ľ���
        if (y + 1 < 4)
        {
            line4 = Mathf.Abs(x - Hero_x) + Mathf.Abs((y + 1) - Hero_y);
            if (!ShortestLine.ContainsKey(line4))
                ShortestLine.Add(line4, new GridVector2(x, (y + 1)));
        }



        int line_Max = Mathf.Min(Mathf.Min(line1, line3), Mathf.Min(line2, line4));

        if (ShortestLine.ContainsKey(line_Max))
        {
            return ShortestLine[line_Max];
        }
        else
        {
            Debug.Log("line_Max��Ӧ��ֵ������");
            return new GridVector2(-1, -1);
        }

    }
}

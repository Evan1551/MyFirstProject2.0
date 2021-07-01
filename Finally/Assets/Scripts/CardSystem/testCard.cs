using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardSystem
{
    public class testCard : MonoBehaviour
    {
        
       
        // Start is called before the first frame update
        void Start()
        {
            CardInfoManager infoManager = new CardInfoManager();
            Deck deck = new Deck();
            //deck.AddCard(1,cardManager);
            //Debug.Log(deck.CardList[0].Description);
            //deck.CardList[0]=cardManager.GetUpdatedCard(deck.CardList[0]);
            //Debug.Log(deck.CardList[0].Description);
            //Debug.Log(deck.CardList[0].IsUpdated);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}

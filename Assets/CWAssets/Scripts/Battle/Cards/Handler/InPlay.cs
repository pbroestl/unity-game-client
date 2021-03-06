using System;
using UnityEngine;

namespace CW
{
	public class InPlay : AbstractCardHandler
	{
		//player is owner of the card
		public InPlay (AbstractCard card, BattlePlayer player) : base(card, player)
		{
			
		}
		
		public override void affect ()
		{
			BattlePlayer currentPlayer = GameManager.curPlayer;
            
            
			if (currentPlayer.clickedCard == null && player.isActive && card.canAttack()) 
            {
				player.clickedCard = card;
				if (player.player1) {
					Debug.Log ("Player 1 is ready to attack");
				} else {
					Debug.Log ("Player 2 is ready to attack");
				}		
				
			} 
            else if (currentPlayer.clickedCard != null) 
            {
                
                if (currentPlayer.clickedCard.diet == AbstractCard.DIET.FOOD) 
                {
                    if (player.isActive) {
                      
                        currentPlayer.targetCard = card;

                        currentPlayer.applyFoodBuff (currentPlayer.targetCard, 1, 1);

                        //currentPlayer.clickedCard.applyFood(currentPlayer.targetCard, 1, 1);
                        //currentPlayer.getProtocolManager ().sendFoodBuff (currentPlayer.cardID, currentPlayer.targetCard.fieldIndex);
                        currentPlayer.getProtocolManager ().sendFoodBuff (currentPlayer.playerID, currentPlayer.targetCard.fieldIndex);
                        currentPlayer.hand.Remove (currentPlayer.clickedCard.gameObject);
                        GameObject.Destroy (currentPlayer.clickedCard.gameObject);
                        currentPlayer.currentMana -= currentPlayer.clickedCard.getManaCost ();
                        currentPlayer.clickedCard = null;
                        currentPlayer.targetCard = null;
                    } else {
                        currentPlayer.clickedCard = null;
                    }
					
				} 
                
                //else if (currentPlayer != player && currentPlayer.clickedCard.diet != AbstractCard.DIET.HERBIVORE) 
                else if (currentPlayer != player && (currentPlayer.clickedCard.diet == AbstractCard.DIET.CARNIVORE || currentPlayer.clickedCard.diet == AbstractCard.DIET.OMNIVORE))
                {
                    
                    currentPlayer.targetCard = card;	
					bool attackback = false;
					if (currentPlayer.targetCard.diet != AbstractCard.DIET.HERBIVORE) {
						attackback = true;
					}
                    
					currentPlayer.clickedCard.attack (currentPlayer.clickedCard, currentPlayer.targetCard, attackback);
					
					currentPlayer.getProtocolManager ().sendCardAttack (currentPlayer.playerID, currentPlayer.clickedCard.fieldIndex, currentPlayer.targetCard.fieldIndex);
					currentPlayer.clickedCard = null;
					currentPlayer.targetCard = null;
				}
				
			}
			
			if (currentPlayer.clickedCard != null && currentPlayer == player && card != currentPlayer.clickedCard) {
				currentPlayer.clickedCard = null;	
               
			}
			
		}
		
		public override void clicked ()
		{
			
			affect ();
		}
	}
}


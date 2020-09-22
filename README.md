# BlackJack
BlackJack Game Challenge

Features Implemented : 

Single player Blackjack Game. You can set your cash amount, and bet amounts per hand. 
You can Hit or Stand after the dealer has dealt your cards, then dealer will continue hitting until number > 16. 
You can save the current state using Ctrl + S, and load using Ctrl + L. 

Outline :

BlackJackGame - In charge of handling current state of game, deal cards to player and dealer and check win conditions at the end.

BlackJackTableSpot - Meant to be used to create a spot on the table for players or dealer. This handles UI and Card displays. 

BlackJackPlayer - Handles players hand, cash amount and bet amount. 

BlackJackHand - It handles cards dealt to player. Could be used to give player multiple hands for split. 

Card - Card that's spawned and moved in the UI. 

CardDefinition - has the suite and number of the card, as well as the index in the deck. 

CardDeck - Contains a list of 52 cards. Cards are given randomly when dealt. 

#Imports
import random
import os
import time

#Variables
playerOne = ""
playerOneCards = 0
playerOneDeck = []
playerTwo = ""
playerTwoCards = 0
playerTwoDeck = []
turn = 1
currentRound = 1
redDeck = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
blackDeck = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
yellowDeck = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
total = len(redDeck) + len(blackDeck) + len(yellowDeck)

_pOneC = ""
_pOneN = -1
_pTwoC = ""
_pTwoN = -1

#Functions
def clear():
    os.system("cls")


def roundWinner(cardOne, cardTwo):
    if cardOne == "Red" and cardTwo == "Black":
        return 1
    elif cardOne == "Yellow" and cardTwo == "Red":
        return 1
    elif cardOne == "Black" and cardTwo == "Yellow":
        return 1
    else:
        return 2

def deckFromColour(colour):
    if colour == "Red":
        return redDeck
    elif colour == "Black":
        return blackDeck
    elif colour == "Yellow":
        return yellowDeck

def updateNewDeck(colour, deck):
    if colour == "Red":
        global redDeck
        redDeck = deck
    elif colour == "Black":
        global blackDeck
        blackDeck = deck
    elif colour == "Yellow":
        global yellowDeck
        yellowDeck = deck

def updateTotal():
    global total
    total = len(redDeck) + len(blackDeck) + len(yellowDeck)

def getPlayerName(id):
    if id == 1: return playerOne
    elif id == 2: return playerTwo
    else: return None

def cardsFromId(id):
    if id == 1: return playerOneCards
    elif id == 2: return playerTwoCards
    else: return None

def updateTempValue(id, colour, num):
    if id == 1:
        global _pOneC
        global _pOneN
        _pOneC = colour
        _pOneN = num
    elif id == 2:
        global _pTwoC
        global _pTwoN
        _pTwoC = colour
        _pTwoN = num

def updatePlayerDeck(id, colour, num):
    if id == 1:
        global playerOneDeck
        playerOneDeck.append("" + colour + " " + str(num))
    elif id == 2:
        global playerTwoDeck
        playerTwoDeck.append("" + colour + " " + str(num))

def deckFromId(id):
    if id == 1: return playerOneDeck
    elif id == 2: return playerTwoDeck
    else: return None

def clearTempValues():
    global _pOneC
    global _pOneN
    global _pTwoC
    global _pTwoN

    _pOneC = ""
    _pOneN = -1
    _pTwoC = ""
    _pTwoN = -1


#Start up
print("""
That one Card Game: Python Edition!

Welcome! I guess let's just get into the sauce, eh?
First off, I'm gunna need the names of the 2 people playing.
Help me out and enter your names below!
""")
playerOne = input("Player 1's Name > ")
playerTwo = input("Player 2's Name > ")

clear()

print("Hello, " + playerOne + " and " + playerTwo + "! Nice to meet you. Now, let's get started.\n")
print("""
Rules

Here are the rules:
- Both players take a card from the deck.
- If both players have the same colour, the player with the highest number wins the round.
- Else, a winner will be decided based on this table:

Card      Card      Winner
Red       Black     Red
Yellow    Red       Yellow
Black     Yellow    Black

- The winning player of that round will keep both of the cards.
- The game will end when there are no cards left.

(Note that in the 3 decks there are 10 cards each)

Press enter to start!
""")
input("")
clear()

#The game
while total > 0:
    print("Round " + str(currentRound))
    print(getPlayerName(turn) + "'s Turn")
    print("Drawing...")

    drawColour = ""
    drawNumber = -1
    _deck = []

    while len(_deck) == 0:
        _rand = random.randint(1, 3)
        if _rand == 1:
            drawColour = "Red"
        elif _rand == 2:
            drawColour = "Black"
        elif _rand == 3:
            drawColour = "Yellow"

        _deck = deckFromColour(drawColour)

    drawNumber = random.choice(_deck)
    _deck.remove(drawNumber)
    updateNewDeck(drawColour, _deck)
    updateTotal()
    updateTempValue(turn, drawColour, drawNumber)
    updatePlayerDeck(turn, drawColour, drawNumber)

    time.sleep(1)

    print(getPlayerName(turn) + " drew a " + drawColour + " " + str(drawNumber))

    input("\nPress enter to continue...\n")
    if turn == 2: #TODO: tell the player their cards
        clear()
        turn = 1
        _roundWinner = ""
        if _pOneC == _pTwoC:
            if _pOneN == _pTwoN:
                _roundWinner = "Everyone"
                playerOneCards += 1
                playerTwoCards += 1
            elif _pOneN > _pTwoN:
                playerOneCards += 2
                _roundWinner = playerOne
            else:
                playerTwoCards += 2
                _roundWinner = playerTwo
        else:
            _winner = roundWinner(_pOneC, _pTwoC)
            if _winner == 1:
                playerOneCards += 2
                _roundWinner = playerOne
            else:
                playerTwoCards += 2
                _roundWinner = playerTwo

        if total <= 0:
            print("Calculating final scores! One sec...")
            time.sleep(3)
            input("Done. Press enter to see the winner!")
        else:
            print("""
End of Round """ + str(currentRound) + """ Results:
        
""" + _roundWinner + """ has won the round!
        
Scores:
""" + playerOne + """ > """ + str(playerOneCards) + """ cards
""" + playerTwo + """ > """ + str(playerTwoCards) + """ cards

There are """ + str(total) + """ cards left.
""")
            input("\nPress enter to go to the next round!")
            clearTempValues()
            currentRound += 1
            clear()
    else:
        print(" ")
        turn = 2

# Announcing the winner
_globalWinner = ""
_globalWinnerId = -1
_globalWinnerCards = -1
_globalLooserId = -1
_boolDraw = False

if playerOneCards > playerTwoCards:
    _globalWinner = playerOne
    _globalWinnerCards = playerOneCards
    _globalWinnerId = 1
    _globalLooserId = 2
elif playerOneCards < playerTwoCards:
    _globalWinner = playerTwo
    _globalWinnerCards = playerTwoCards
    _globalWinnerId = 2
    _globalLooserId = 1
else:
    _boolDraw = True
    _globalWinnerCards = playerOneCards # they are both the same, so it doesn't matter which one I use

clear()
if _boolDraw == False:
    print("""
Game. OVER!

""" + _globalWinner + """ has won the game!
They had """ + str(_globalWinnerCards) + """ cards.

""" + getPlayerName(_globalLooserId) + """ was """ + str((_globalWinnerCards - cardsFromId(_globalLooserId)) + 1) + """ cards away from winning!
They had """ + str(cardsFromId(_globalLooserId)) + """ cards.

Thanks for playing!
    """)

    _winnerDeckText = ""
    for i in range(len(deckFromId(_globalWinnerId))):
        if _winnerDeckText == "":
            _winnerDeckText += deckFromId(_globalWinnerId)[i]
        else:
            _winnerDeckText += ", " + deckFromId(_globalWinnerId)[i]
    print("Winner's Deck > " + _winnerDeckText)

    input("")
else:
    print("""
    Game. OVER!

    It was a tie! Both player had """ + str(_globalWinnerCards) + """ cards!

    Thanks for playing!
        """)
    input("")

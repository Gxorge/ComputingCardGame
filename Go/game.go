package Go

//Imports
import (
	"fmt"
	"math/rand"
)

//Variables
var playerOne string
var playerOneCards int
var playerOneDeck = []string{}
var playerTwo string
var playerTwoCards int
var playerTwoDeck = []string{}

var turn int
var currentRound int

var redDeck = [10]int{1, 2, 3, 4, 5, 6, 7, 8, 9, 10}
var blackDeck = [10]int{1, 2, 3, 4, 5, 6, 7, 8, 9, 10}
var yellowDeck = [10]int{1, 2, 3, 4, 5, 6, 7, 8, 9, 10}
var total int = len(redDeck) + len(blackDeck) + len(yellowDeck)

var _pOneC string
var _pOneN int
var _pTwoC string
var _pTwoN int

//Functions

func roundWinner(cardOne, cardTwo string) int {
	if cardOne == "Red" && cardTwo == "Black" {
		return 1
	} else if cardOne == "Yellow" && cardTwo == "Red" {
		return 1
	} else if cardOne == "Black" && cardTwo == "Yellow" {
		return 1
	} else {
		return 2
	}
}

func deckFromColour(colour string) [10]int {
	switch colour {
	case "Red":
		return redDeck
		break
	case "Black":
		return blackDeck
		break
	case "Yellow":
		return yellowDeck
		break
	default:
		return [10]int{}
	}
	return [10]int{}
}

func main() {
	
}



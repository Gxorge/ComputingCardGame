using System;
using System.Collections;

namespace Game
{
    internal class Program
    {
        //Variables
        private static Program instance;
        
        public string playerOne;
        public int playerOneCards;
        public ArrayList PlayerOneDeck = new ArrayList();

        public string playerTwo;
        public int playerTwoCards;
        public ArrayList PlayerTwoDeck = new ArrayList();

        private int turn = 1;
        private int currentRound = 1;
        
        public ArrayList RedDeck = new ArrayList();
        public ArrayList BlackDeck = new ArrayList();
        public ArrayList YellowDeck = new ArrayList();
        public int total;

        private string _pOneC = "";
        private int _pOneN = -1;
        private string _pTwoC = "";
        private int _pTwoN = -1;
        
        //Functions
        public static Program GetInstance() { return instance; }
        
        private void RefreshDecks()
        {
            for (int i = 1; i <= 10; i++)
            {
                RedDeck.Add(i);
                BlackDeck.Add(i);
                YellowDeck.Add(i);
            }
            UpdateTotal();
        }

        private int RoundWinner(string cardOne, string cardTwo)
        {
            if (cardOne.Equals("Red") && cardTwo.Equals("Black"))
                return 1;
            else if (cardOne.Equals("Yellow") && cardTwo.Equals("Red"))
                return 1;
            else if (cardOne.Equals("Black") && cardTwo.Equals("Yellow"))
                return 1;
            else
                return 2;    
        }

        private ArrayList DeckFromColour(string colour)
        {
            switch (colour)
            {
                case "Red":
                    return RedDeck;
                case "Black":
                    return BlackDeck;
                case "Yellow":
                    return YellowDeck;
                default:
                    return null;
            }
        }

        private void UpdateDeck(string colour, ArrayList deck)
        {
            switch (colour)
            {
                case "Red":
                    RedDeck = deck;
                    break;
                case "Black":
                    BlackDeck = deck;
                    break;
                case "Yellow":
                    YellowDeck = deck;
                    break;
                default:
                    return;
            }
        }

        private void UpdateTotal()
        {
            total = RedDeck.Count + BlackDeck.Count + YellowDeck.Count;
        }

        private string PlayerNameFromId(int id)
        {
            if (id == 1) return playerOne;
            else if (id == 2) return playerTwo;
            else return null;
        }

        private int CardsFromId(int id)
        {
            switch (id)
            {
                case 1:
                    return playerOneCards;
                case 2:
                    return playerTwoCards;
                default:
                    return -1;
            }
        }

        private void UpdateTempValues(int id, string colour, int amount)
        {
            switch (id)
            {
                case 1:
                    _pOneC = colour;
                    _pOneN = amount;
                    break;
                case 2:
                    _pTwoC = colour;
                    _pTwoN = amount;
                    break;
                default:
                    return;
            }
        }

        private void AddToPlayerDeck(int id, string colour, int amount)
        {
            switch (id)
            {
                case 1:
                    PlayerOneDeck.Add(colour + " " + amount);
                    break;
                case 2:
                    PlayerTwoDeck.Add(colour + " " + amount);
                    break;
                default:
                    return;
            }
        }

        private ArrayList PlayerDeckFromId(int id)
        {
            switch (id)
            {
                case 1:
                    return PlayerOneDeck;
                case 2:
                    return PlayerTwoDeck;
                default:
                    return null;
            }
        }

        private void ClearTempValues()
        {
            _pOneC = "";
            _pOneN = -1;

            _pTwoC = "";
            _pTwoN = -1;
        }

        //This is run automatically. 
        public static void Main(string[] args)
        {
            instance = new Program();
            
            GetInstance().RefreshDecks();
            
            GetInstance().Game(); //Starts the game
        }

        public void Game()
        {
            //Start up
            Console.WriteLine("That one Card Game: C# Addition!\n\n" +
                              "Welcome! I guess let's just get into the sauce, eh?\n" +
                              "First off, I'm gunna need the games of the 2 people playing.\n" +
                              "Help me out and enter your names below!");
            
            Console.WriteLine("\nPlayer 1's Name > ");
            playerOne = Console.ReadLine();
            
            Console.WriteLine("\nPlayer 2's Name > ");
            playerTwo = Console.ReadLine();
            
            Console.Clear();
            
            Console.WriteLine("\nHello, " + playerOne + " and " + playerTwo + "! Nice to meet you, now let's get started.\n");
            Console.WriteLine("Rules\n\n" +
                              "Here are the rules:\n" + 
                              "- Both players take a card from the deck.\n" +
                              "- If both players have the same colour, the player will the highest number wins the round.\n" +
                              "- Else, a winner will be decided based on this table:\n\n" +
                              "Card      Card      Winner\n" + 
                              "Red       Black     Red\n" +
                              "Yellow    Red       Yellow\n" +
                              "Black     Yellow    Black\n\n" +
                              "- The winning player of that round will keep both of the cards.\n" + 
                              "- The game will end when there are no cards left.\n\n" +
                              "(Note that in the 3 decks there are 10 cards each)\n\n" +
                              "Press enter to start!");
            Console.ReadLine();
            Console.Clear();

            //The actual game
            while (total > 0)
            {
                Console.WriteLine("Round " + currentRound);
                Console.WriteLine(PlayerNameFromId(turn) + "'s Turn");
                Console.WriteLine("Drawing...");

                var drawColour = "";
                var randIdx = 0;
                var drawNumber = 0;
                var deck = new ArrayList();
                var rand = new Random();
                
                while (deck.Count == 0)
                {
                    var randInt = rand.Next(1, 4);
                    switch (randInt)
                    {
                        case 1:
                            drawColour = "Red";
                            break;
                        case 2:
                            drawColour = "Black";
                            break;
                        case 3:
                            drawColour = "Yellow";
                            break;
                        default:
                            continue;
                    }
                    
                    deck = DeckFromColour(drawColour);
                }

                randIdx = rand.Next(deck.Count);
                drawNumber = (int) deck[randIdx];
                deck.Remove(drawNumber);
                UpdateDeck(drawColour, deck);
                UpdateTotal();
                UpdateTempValues(turn, drawColour, drawNumber);
                AddToPlayerDeck(turn, drawColour, drawNumber);
                
                System.Threading.Thread.Sleep(1000); //1000ms = 1s
                
                Console.WriteLine(PlayerNameFromId(turn) + " drew a " + drawColour + " " + drawNumber);
                Console.WriteLine("\nPress enter to continue...");
                Console.ReadLine();
                
                if (turn == 2)
                {
                    Console.Clear();
                    turn = 1;
                    var roundWinner = "";

                    if (_pOneC.Equals(_pTwoC))
                    {
                        if (_pOneN == _pTwoN)
                        {
                            roundWinner = "Everyone";
                            playerOneCards++;
                            playerTwoCards++;
                        } 
                        else if (_pOneN > _pTwoN)
                        {
                            playerOneCards += 2;
                            roundWinner = playerOne;
                        }
                        else
                        {
                            playerTwoCards += 2;
                            roundWinner = playerTwo;
                        }
                    }
                    else
                    {
                        var winner = RoundWinner(_pOneC, _pTwoC);
                        if (winner == 1)
                        {
                            playerOneCards += 2;
                            roundWinner = playerOne;
                        }
                        else
                        {
                            playerTwoCards += 2;
                            roundWinner = playerTwo;
                        }
                    }

                    if (total <= 0)
                    {
                        Console.WriteLine("Calculating final scores! One sec...");
                        System.Threading.Thread.Sleep(2000); //2s
                        Console.WriteLine("Done. Press enter to see the winner!");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("End of Round " + currentRound + " Results:\n\n" +
                                          roundWinner + " has won the round!\n\n" +
                                          "Scores:\n" +
                                          playerOne + " > " + playerOneCards + " cards\n" +
                                          playerTwo + " > " + playerTwoCards + " cards\n\n" +
                                          "There are " + total + " cards left.\n\n" + "" +
                                          "Press enter to go to the next round!");
                        Console.ReadLine();
                        ClearTempValues();
                        currentRound++;
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine(" ");
                    turn = 2;
                }
            }
            
            //Announce the winner
            var globalWinner = "";
            var globalWinnerId = -1;
            var globalWinnerCards = -1;
            var globalLooserId = -1;
            var boolDraw = false;

            if (playerOneCards > playerTwoCards)
            {
                globalWinner = playerOne;
                globalWinnerCards = playerOneCards;
                globalWinnerId = 1;
                globalLooserId = 2;
            }
            else if (playerOneCards < playerTwoCards)
            {
                globalWinner = playerTwo;
                globalWinnerCards = playerTwoCards;
                globalWinnerId = 2;
                globalLooserId = 1;
            }
            else
            {
                boolDraw = true;
                globalWinnerCards = playerOneCards; //they are both the same, so it doesn't matter which one i use
            }
            
            Console.Clear();

            if (!boolDraw)
            {
                Console.WriteLine("Game. OVER!\n\n" +
                                  globalWinner + " has won the game!\n" + 
                                  "They had " + globalWinnerCards + " cards.\n\n" +
                                  PlayerNameFromId(globalLooserId) + " was " + ((globalWinnerCards - CardsFromId(globalLooserId)) + 1) + " cards away from winning!\n" +
                                  "They had " + CardsFromId(globalLooserId) + " cards.\n\n" +
                                  "Thanks for playing!");

                var winnerDeckText = "";
                for (int i = 0; i < PlayerDeckFromId(globalWinnerId).Count; i++)
                {
                    if (winnerDeckText.Equals(""))
                        winnerDeckText += PlayerDeckFromId(globalWinnerId)[i];
                    else
                        winnerDeckText += ", " + PlayerDeckFromId(globalWinnerId)[i];
                }
                Console.WriteLine("Winner's Deck > " + winnerDeckText);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Game. OVER!\n\n" +
                                  "It was a tie! Both players had " + globalWinner + " cards!\n\n" +
                                  "Thanks for playing!");
                Console.ReadLine();
            }
        }
    }
}
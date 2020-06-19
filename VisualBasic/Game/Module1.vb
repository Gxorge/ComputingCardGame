Module Module1

    REM Variables
    Public PlayerOne As String = ""
    Public PlayerOneCards As Integer = 0
    Public PlayerOneDeck As ArrayList = New ArrayList()
    
    Public PlayerTwo As String = ""
    Public PlayerTwoCards As Integer = 0
    Public PlayerTwoDeck As ArrayList = New ArrayList()
    
    Public Turn As Integer = 1
    Public CurrentRound As Integer = 1
    
    Public RedDeck As ArrayList = New ArrayList()
    Public BlackDeck As ArrayList = New ArrayList()
    Public YellowDeck As ArrayList = New ArrayList()
    Public Total As Integer = 0
    
    Private _pOneC As String = ""
    Private _pOneN As Integer = -1
    Private _pTwoC As String = ""
    Private _pTwoN As Integer = -1
    
    REM Functions and Subs
    Function RoundWinner(cardOne As String, cardTwo As String) As String
        If cardOne.Equals("Red") AND cardTwo.Equals("Black") Then
            Return 1
        ElseIf cardOne.Equals("Yellow") AND cardTwo.Equals("Red") Then
            Return 1
        ElseIf cardOne.Equals("Black") AND cardTwo.Equals("Yellow") Then
            Return 1
        Else
            Return 2
        End If
    End Function
    
    Function DeckFromColour(colour As String) As ArrayList
        Select Case colour
            Case "Red"
                return RedDeck
            Case "Black"
                return BlackDeck
            Case "Yellow"
                return YellowDeck
            Case Else
                Return Nothing
        End Select
    End Function
    
    Sub UpdateNewDeck(colour As String, deck As ArrayList) 
        Select Case colour
            Case "Red"
                RedDeck = deck
            Case "Black"
                BlackDeck = deck
            Case "Yellow"
                YellowDeck = deck
            Case Else
                Return
        End Select
    End Sub
    
    Sub UpdateTotal()
        Total = RedDeck.Count + BlackDeck.Count + YellowDeck.Count
    End Sub
    
    Function GetPlayerFromId(id As Integer) As String
        Select Case id
            Case 1
                Return PlayerOne
            Case 2
                Return PlayerTwo
            Case Else:
                Return Nothing
        End Select
    End Function
    
    Function GetCardsFromId(id As Integer) As Integer
        Select Case id
            Case 1
                Return PlayerOneCards
            Case 2
                Return PlayerTwoCards
            Case Else
                return Nothing
        End Select
    End Function
    
    Sub UpdateTempValues(id As Integer, colour As String, num As Integer)
        Select Case id
            Case 1
                _pOneC = colour
                _pOneN = num
            Case 2
                _pTwoC = colour
                _pTwoN = num
            Case Else
                Return
        End Select
    End Sub
    
    Sub UpdatePlayerDeck(id As Integer, colour As String, num As Integer)
        Select Case id
            Case 1
                PlayerOneDeck.Add("" & colour & " " & num)
            Case 2
                PlayerTwoDeck.Add("" & colour & " " & num)
            Case Else
                Return
        End Select
    End Sub
    
    Function DeckFromId(id As Integer) As ArrayList
        Select Case id
            Case 1
                Return PlayerOneDeck
            Case 2
                Return PlayerTwoDeck
            Case Else
                Return Nothing
        End Select
    End Function
    
    Sub ClearTempValues()
        _pOneC = ""
        _pOneN = -1
        _pTwoC = ""
        _pTwoN = -1
    End Sub
    
    Sub PopulateDecks()
        For i As Integer = 1 To 10
            RedDeck.Add(i)
            BlackDeck.Add(i)
            YellowDeck.Add(i)
        Next
    End Sub
    
    Sub Main()
        PopulateDecks()
        UpdateTotal()
        Game()
    End Sub
    
    Sub Game()
        Console.WriteLine("That one Card Game: Visual Basic Edition!" & vbNewLine & vbNewLine &
                          "Welcome! I guess let's just get into the sauce, eh?" & vbNewLine &
                          "First off, I'm gunna need the names of the 2 people playing." & vbNewLine &
                          "Help me out and enter your names below!")
        
        Console.WriteLine(vbNewLine & "Player 1's Name > ")
        PlayerOne = Console.ReadLine()
        Console.WriteLine(vbNewLine & "Player 2's Name > ")
        PlayerTwo = Console.ReadLine()
        
        Console.Clear()
        
        Console.WriteLine("Hello, " & PlayerOne & " and " & PlayerTwo & "! Nice to meet you. Now, let's get started." & vbNewLine & vbNewLine & 
                          "Rules" & vbNewLine & vbNewLine &
                          "Here are the rules:" & vbNewLine &
                          "- Both players take a card from the deck." & vbNewLine &
                          "- If both players have the same colour, the player with the highest number wins the round." & vbNewLine &
                          "- Else, a winner will be decided based on this table:" & vbNewLine & vbNewLine &
                          "Card      Card      Winner" & vbNewLine &
                          "Red       Black     Red" & vbNewLine &
                          "Yellow    Red       Yellow" & vbNewLine &
                          "Black     Yellow    Black" & vbNewLine & vbNewLine &
                          "- The winning player of that round will keep both of the cards." & vbNewLine &
                          "- The game will end when there are no cards left." & vbNewLine & vbNewLine &
                          "(Note that in the 3 decks there are 10 cards each)" & vbNewLine & vbNewLine &
                          "Press enter to start!")
        Console.ReadLine()
        Console.Clear()
        
        While Total > 0
            Console.WriteLine("Round " & Turn)
            Console.WriteLine(GetPlayerFromId(Turn) & "'s Turn")
            Console.WriteLine("Drawing...")
            
            Dim drawColour = ""
            Dim drawNumber = -1
            Dim deck = new ArrayList
            
            REM TODO: Fix this code, for some reason it is rigged
            While deck.Count = 0
                Dim randInt = Int((3 * Rnd) + 1)
                Select Case randInt
                    Case 1
                        drawColour = "Red"
                    Case 2
                        drawColour = "Black"
                    Case 3
                        drawColour = "Yellow"
                    Case Else
                        Continue While
                End Select
                
                deck = DeckFromColour(drawColour)
            End While
            
            Dim rand As New Random()
            drawNumber = deck(rand.Next(0, deck.Count - 1))
            
            deck.Remove(drawNumber)
            UpdateNewDeck(drawColour, deck)
            UpdateTotal()
            UpdateTempValues(Turn, drawColour, drawNumber)
            UpdatePlayerDeck(Turn, drawColour, drawNumber)
            
            System.Threading.Thread.Sleep(1000)
            
            Console.WriteLine(GetPlayerFromId(turn) & " drew a " & drawColour & " " & drawNumber)
            Console.WriteLine(vbNewLine & "Press enter to continue..." & vbNewLine)
            Console.ReadLine()
            
            If Turn = 2 Then
                Console.Clear()
                Turn = 1
                Dim winnerRound = ""
                
                If _pOneC.Equals(_pTwoC) Then
                    If _pOneN = _pTwoN Then
                        winnerRound = "Everyone"
                        PlayerOneCards += 1
                        PlayerTwoCards += 1
                    ElseIf _pOneN > _pTwoN Then
                        PlayerOneCards += 2
                        winnerRound = PlayerOne
                    Else
                        PlayerTwoCards += 2
                        winnerRound = PlayerTwo
                    End If
                Else
                    Dim rWinner = RoundWinner(_pOneC, _pTwoC)
                    If rWinner = 1 Then
                        PlayerOneCards += 2
                        winnerRound = PlayerOne
                    Else    
                        PlayerTwoCards += 2
                        winnerRound = PlayerTwo
                    End If
                End If
                
                If total <= 0 Then
                    Console.WriteLine("Calculating final scores! One sec...")
                    System.Threading.Thread.Sleep(3000)
                    Console.WriteLine("Done. Press enter to see the winner!")
                    Console.ReadLine()
                Else    
                    Console.WriteLine("End of Round " & CurrentRound & " Results:" & vbNewLine & vbNewLine &
                                      "" & winnerRound & " has won the round!" & vbNewLine & vbNewLine &
                                      "Scores:" & vbNewLine & 
                                      "" & PlayerOne & " > " & PlayerOneCards & " cards" & vbNewLine & 
                                      "" & PlayerTwo & " > " & PlayerTwoCards & " cards" & vbNewLine & vbNewLine &
                                      "There are " & Total & " cards left." & vbNewLine & vbNewLine &
                                      "Press enter to go to the next round!")
                    Console.ReadLine()
                    ClearTempValues()
                    CurrentRound += 1
                    Console.Clear()
                End If
            Else
                Console.WriteLine(" ")
                turn = 2
            End If
        End While
        
        Dim winner = ""
        Dim winnerCards = -1
        Dim winnerId = -1
        Dim looserId = -1
        Dim draw = False
        
        If PlayerOneCards > PlayerTwoCards Then
            winner = PlayerOne
            winnerCards = PlayerOneCards
            winnerId = 1
            looserId = 2
        ElseIf PlayerOneCards < PlayerTwoCards
            winner = PlayerTwo
            winnerCards = PlayerTwoCards
            winnerId = 2
            looserId = 1
        Else
            draw = True
            winnerCards = PlayerOneCards REM it doesn't matter since they are the same
        End If
        
        Console.Clear()
        If draw = False Then
            Console.WriteLine("Game. OVER!" & vbNewLine & vbNewLine &
                              "" & winner & " has won the game!" & vbNewLine &
                              "They had " & winnerCards & " cards." & vbNewLine & vbNewLine &
                              "" & GetPlayerFromId(looserId) & " was " & ((winnerCards - GetCardsFromId(looserId)) + 1) & " cards away from winning!" & vbNewLine &
                              "They had " & GetCardsFromId(looserId) & " cards." & vbNewLine & vbNewLine &
                              "Thanks for playing!")
            Dim winnerDeck = ""
            For Each card In DeckFromId(winnerId)
                If winnerDeck.Equals("") Then
                    winnerDeck.Append(card.ToString())
                Else    
                    winnerDeck.Append(", " & card.ToString())
                End If
            Next
            Console.WriteLine("Winner's Deck > " & winnerDeck)
            Console.ReadLine()
        Else    
            Console.WriteLine("Game. OVER!" & vbNewLine & vbNewLine &
                              "It was a tie! Both players had " & winnerCards & " cards!" & vbNewLine & vbNewLine &
                              "Thanks for playing!")
            Console.ReadLine()
        End If
    End Sub
End Module

# Variables
$playerone = ""
$playerone_cards = 0
$playerone_deck = []

$playertwo = ""
$playertwo_cards = 0
$playertwo_deck = []

$turn = 1
$currentround = 1

$red_deck = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
$black_deck = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
$yellow_deck = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
$total = $red_deck.size + $black_deck.size + $yellow_deck.size

$_pone_c = ""
$_pone_n = -1
$_ptwo_c = ""
$_ptwo_n = -1

#Functions
def clear
  system("cls")
end

def round_winner(cardone, cardtwo)
  if cardone.eql? "Red" and cardtwo.eql? "Black"
    1
  elsif cardone.eql? "Yellow" and cardtwo.eql? "Red"
    1
  elsif cardone.eql? "Black" and cardtwo.eql? "Yellow"
    1
  else
    2
  end
end

def deck_from_colour(colour)
  case colour
  when "Red" then
    return $red_deck
  when "Black" then
    return $black_deck
  when "Yellow" then
    return $yellow_deck
  else
    return nil
  end
end

def update_new_deck(colour, deck)
  case colour
  when "Red" then
    $red_deck = deck
  when "Black" then
    $black_deck = deck
  when "Yellow" then
    $yellow_deck = deck
  else
    return
  end
end

def update_total()
  $total = $red_deck.size + $black_deck.size + $yellow_deck.size
end

def get_name_from_id(id)
  case id
  when 1 then
    return $playerone
  when 2 then
    return $playertwo
  else
    return nil
  end
end

def get_cards_from_id(id)
  case id
  when 1 then
    return $playerone_cards
  when 2 then
    return $playertwo_cards
  else
    return nil
  end
end

def update_temp_values(id, colour, num)
  case id
  when 1 then
    $_pone_c = colour
    $_pone_n = num
  when 2 then
    $_ptwo_c = colour
    $_ptwo_n = num
  else
    return
  end
end

def update_player_deck(id, colour, num)
  case id
  when 1 then
    $playerone_deck.append("" + colour + " " + num.to_s)
  when 2 then
    $playertwo_deck.append("" + colour + " " + num.to_s)
  else
    return
  end
end

def get_deck_from_id(id)
  case id
  when 1 then
    return $playerone_deck
  when 2 then
    return $playertwo_deck
  else
    return nil
  end
end

def clear_temp_values()
  $_pone_c = ""
  $_pone_n = -1
  $_ptwo_c = ""
  $_ptwo_n = -1
end

#Start up
clear
print "That one Card Game: Ruby Eddition!\n\n" +
          "Welcome! I guess let's just get into the sauce, eh?\n" +
          "First off, I'm gunna need the names of the 2 people playing.\n" +
          "Help me out and enter your names below!\n"

print "\nPlayer 1's Name > "
$playerone = gets.chop
print "\nPlayer 2's Name > "
$playertwo = gets.chop
clear

puts "Hello, " + $playerone + " and " + $playertwo + "! Nice to meet you. Now, let's get started."
puts "\nRules\n\n" +
         "Here are the rules:\n" +
         "- Both players take a card from the deck.\n" +
         "- If both players have the same colour, the player with the highest number wins the round.\n" +
         "- Else, a winner will be deided based on this table:\n\n" +
         "Card      Card      Winner\n" +
         "Red       Black     Red\n" +
         "Yellow    Red       Yellow\n" +
         "Black     Yellow    Black\n\n" +
         "- The winning player of that round will keep both of the cards.\n" +
         "- The game will end when there are no cards left.\n\n" +
         "(Note that in the 3 decks there are 10 cards each)\n\n" +
         "Press enter to start!"
gets.chop
clear

while $total > 0 do
  puts "Round " + $currentround.to_s
  puts get_name_from_id($turn) + "'s Turn"
  puts "Drawing...\n"

  draw_colour = ""
  draw_number = -1
  deck = []

  while deck.size == 0 do
    rand_deck = rand(1..3)

    if rand_deck == 1 then
      draw_colour = "Red"
    elsif rand_deck == 2 then
      draw_colour = "Black"
    elsif rand_deck == 3 then
      draw_colour = "Yellow"
    end

    deck = deck_from_colour(draw_colour)
  end

  draw_number = deck.sample
  deck.delete(draw_number)
  update_new_deck(draw_colour, deck)
  update_total
  update_temp_values($turn, draw_colour, draw_number)
  update_player_deck($turn, draw_colour, draw_number)

  sleep(1)

  puts "\n" + get_name_from_id($turn) + " drew a " + draw_colour + " " + draw_number.to_s

  puts "Press enter to continue..."
  gets.chop

  if $turn == 2
    clear
    $turn = 1

    round_winner = ""

    if $_pone_c == $_ptwo_c
      if $_pone_n == $_ptwo_n
        round_winner = "Everyone"
        $playerone_cards += 1
        $playertwo_cards += 1
      elsif $_pone_n > $_ptwo_n then
        $playerone_cards += 2
        round_winner = $playerone
      else
        $playertwo_cards += 2
        round_winner = $playertwo
      end
    else
      winner = round_winner($_pone_c, $_ptwo_c)
      if winner == 1
        $playerone_cards += 2
        round_winner = $playerone
      else
        $playertwo_cards += 2
        round_winner = $playertwo
      end
    end

    if $total <= 0
      puts "Calculating final scores! One sec..."
      sleep(3)
      puts "Done. Press enter to see the winner!"
      gets.chomp
    else
      puts "End of Round " + $turn.to_s + " Results:\n\n" +
               round_winner + " has won the round!\n\n" +
               "Scores:\n" +
               "" + $playerone + " > " + $playerone_cards.to_s + " cards\n" +
               "" + $playertwo + " > " + $playertwo_cards.to_s + " cards\n\n" +
               "There are " + $total.to_s + " cards left."
      puts "Press enter to go to the next round!"
      gets.chop
      clear_temp_values
      $currentround += 1
      clear
    end
  else
    puts " "
    $turn = 2
  end
end

# Announcing the winner

_winner = ""
_winner_id = -1
_winner_cards = -1
_looser_id = -1
_draw = false

if $playerone_cards > $playertwo_cards
  _winner = $playerone
  _winner_id = 1
  _winner_cards = $playerone_cards
  _looser_id = 2
elsif $playerone_cards < $playertwo_cards
  _winner = $playertwo
  _winner_id = 2
  _winner_cards = $playertwo_cards
  _looser_id = 1
else
  _draw = true
  _winner_cards = $playerone_cards # They are both the same, so it doesn't matter
end

clear
if _draw
  puts "Game. OVER!\n\n" +
           "It was a tie! Both players had " + _winner_cards.to_s + " cards!\n\n" +
           "Thanks for playing!"

  gets.chop
else
  puts "Game. OVER!\n\n" +
           "" + _winner + " has won the game!\n" +
           "They had " + _winner_cards.to_s + " cards.\n\n" +
           "" + get_name_from_id(_looser_id) + " was " + ((_winner_cards - get_cards_from_id(_looser_id)) + 1).to_s + " cards away from winning!\n" +
           "They had " + get_cards_from_id(_looser_id).to_s + " cards.\n\n" +
           "Thanks for playing!"

  _winner_text = ""
  get_deck_from_id(_winner_id).each do |i|
    if _winner_text == ""
      _winner_text += i
    else
      _winner_text += ", " + i
    end
  end
  print "\nWinner's Deck > " + _winner_text + "\n"

  gets.chop
end
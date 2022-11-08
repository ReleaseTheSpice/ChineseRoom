# ChineseRoom
Chinese Room Project Proposal

Chinese Room:
The inside of the room is a black box.
Person on the outside gives an input and receives an output. From their perspective, the Chinese room seems intelligent.
In reality, the person inside is following instructions without understanding the meaning behind the symbols thus not being intelligent.
The Chinese room is like AI in the sense that it appears smart from the outside despite not actually being intelligent or able to understand.
Essence Statement
We will recreate the chinese room thought experiment in a digital video game format, allowing the player to fill out the role of the “translator” in the sealed room.  They will be told which symbols correlate to which others, but will not have the “understanding” to exercise true natural intelligence.
Project Description
The “player” of this game will exist and move around in a 3D environment.  They can enter a room in front of them, causing the door to close and lock behind them.  In the room, they will notice messages appearing on an in-game computer.  Upon interacting with the computer, a terminal will pop up, with a message appearing at the top, written all in emojis.
At the bottom of the terminal, there is a field to input a response to the message, and a list of emojis that can be selected to form that response.
To aid them in formulating the response, they will also be shown a window containing emoji questions and responses, correlating to the questions being asked to the player and the correct answers they are supposed to respond with. When they are done forming their response, they press a button to submit and are then asked a new question.  This is repeated several times, until the “conversation” is done.  When the conversation is done, the player will be kicked out of the computer terminal, the computer will go black, and the door to the room will open.  The player will be free to walk out of the room, and will be presented with a translated script of what they were actually saying in english.

UI Mockup

Key Features
3D space to move in / look around
Computer on table in room, interact with it to start the game
2D UI gameplay
Received message at the top of screen in emojis
Emoji cards in scrollable format
Click on emoji icons to add them to response
Click on question to shortcut to it in the dictionary
Send + backspace buttons
Flippable book at bottom with responses to questions
See translated transcript at the end of the game

Technologies
Development
Unity (Game Engine), ver. 2021.3.11f1 LTS
C# (Programming Language)
GitHub Repository (Version Control)

Project Management
Trello for Task Management
Google Docs for Documentation

Deliverables and Scope
Milestone 1
Github repo
Emoji translation/cipher
1 Conversations + translate it to emoji
5 sentences are given to the player, the player formulates 5 responses
Character movement (3D)
UI windows
2D interactable UI Input system
Scrollable Emoji lists
UI output display
Dictionary for proper response
Environment (Room, Computer)

Milestone 2
Add more conversations and translations
Player instructions
Interaction System (Computer)
Beginning and Ending Conversations

Release
Polishing
Bug fixes

Design Principles
Emojis will be treated like Chinese Characters in that each Emoji has a symbolic meaning.
Emojis’ meanings will be concepts that have no correlation to their meaning to prevent players from understanding them.


Game Mechanics
3D player movement
Emoji selection menu

Game Dynamics
If/Then response book

Game Aesthetics
Computerized

Failure Criteria
If the player can decipher the meaning of the emojis when inside the room, it fails the Chinese Room experiment
Game isn’t functional (bugs)
When translated, the script doesn’t make sense
If the player cannot understand how to play
Visible/Render glitches

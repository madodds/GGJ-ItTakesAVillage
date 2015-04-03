# GGJ-ItTakesAVillage
Code for Global Game Jam 2015 for Twitch chat interaction and the Unity source.

Most of the code for the Twitch chat came from the tutorial http://www.wituz.com/tutorial-make-your-own-twitch-plays-stream.html
After the event, I refactored, cleaned, and debugged the game to make it presentable.

How the game works:

The objective of the game is to keep the miniture dragon "Blobzilla" alive for as long as possible. This is done by having a community of people voting on what should be done to Blobzilla. What action is made is based on which action has the most votes every 15 seconds. Blobzilla will ask for what it wants based on its needs. Its needs are: Food, Affection, and Exercise, which are handled by feeding, petting, and telling it to dance. If Blobzilla's needs are ignored too long, or if it does something so much, Blobzilla will explode. 

The "Owner" of the game will be steaming this game on twitch.tv and will hook up the twitch chat to the game. The twitch chat will then input either "Pet", "Feed", or "Dance" in order to vote for the action they want. The python code interprets these inputs and then causes the streamer's computer to press a button depending on the vote. If the focus is on the Unity game, then the game will interpret the button as a vote. These buttons would be either P, F, or D.

The owner of the game should also configure the stream to display what the commands are, along with the twitch chat and how to "play". 

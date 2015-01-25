# Define the imports
import twitch
import keypresser
import sys
t = twitch.Twitch();
k = keypresser.Keypresser();

lineCount = 0;
#fileName = "textlog.txt"
#textLog = open(fileName, "w")
#textLog.write("");
#textLog.close()

# Enter your twitch username and oauth-key below, and the app connects to twitch with the details.
# Your oauth-key can be generated at http://twitchapps.com/tmi/
# This information is currently getting pulled from the command line arguments.
username = sys.argv[1];
key = sys.argv[2];
t.twitch_connect(username, key);

def SendAction(keypress, uName, action, lc):
    # Hit the key press
    k.key_press(keypress);
    
    # Post the action to the console and log.
    logMessage = str(lc) + ": " + username + ": " + msg
    print(logMessage)
    #textLog = open(fileName, "a")
    #textLog.write(logMessage + "\n")
    #textLog.close()

# The main loop
while True:
    # Check for new messages
    new_messages = t.twitch_recieve_messages();

    if not new_messages:
        # No new messages...
        continue
    else:
        for message in new_messages:
            # Wuhu we got a message. Let's extract some details from it
            msg = message['message'].lower()
            username = message['username']
            lineCount += 1

            # This is where you change the keys that shall be pressed and listened to.
            # Change this to make Twitch fit to your game!
            if msg == "pet": SendAction("p", username, msg, lineCount);
            elif msg == "feed": SendAction("f", username, msg, lineCount);
            elif msg == "dance": SendAction("d", username, msg, lineCount);
            else:
                lineCount -= 1
                #print(username + ": " + msg);
            

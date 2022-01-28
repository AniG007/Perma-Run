
using System.Collections.Generic;
using UnityEngine;

public class Quiz_QA
{
    string []arr = new string [3];
    List<Quiz_Data_Class> quiz = new List<Quiz_Data_Class>();
    string Reply = "";

    public string ReturnQuestion(string characterName, string Level)
    {
        string Question = "";

        if (Level == "Traps")
        {
            if (characterName == "Bee")
                Question = "I installed an app to track my steps. But the app started sending ads as SMS to other bees and " +
                    "I get ads according to my location! I just want to track my steps. Is there a way to stop these things from happening?";

            else if (characterName == "Rabbit")
                Question = "I lost all of my photos! Someone used my phone without my knowledge. They also sent creepy messages to all my contacts. " +
                    "Is there a way to stop this person from accessing my phone?";

            else if (characterName == "GrassHopper")
                Question = "I downloaded some cool apps while browsing for some new apps online. Now my phone is acting weird and keeps spamming me with ads." +
                    "I think it might be because of these new apps. I downloaded these from the Jungle App Store. What to do now?";

            else if (characterName == "Fairy")
                Question = "I signed up for a new social media app to connect with my friends. " +
                    "The app keeps notifying about updates even when I am not using the app. Is there any way I can stop this?"; //Alt: Battery Drain Scenario
        }

        else if (Level == "Level2")
        {
            
            if (characterName == "Hunter")
                Question = "My phone started acting weird when I connected to the public Wifi at the coffee house. " +
                    "It started transmitting data via bluetooth on it's own. I lost all of my savings from my bank when I " +
                    "was trying to transfer money. Police are investigating the case. Do you think this might have something to do with my phone? " +
                    "I use apps in time of need and grant only necessary permissions.";

            else if (characterName == "Fox")
                Question = "I go to the coffee shop because they have a super fast internet connection. " +
                    "Yesterday, my social media account was hacked when I was there. Do you know how to prevent this from happening again?";

            else if (characterName == "Squirrel")
                Question = "I got an SMS from an unknown number saying that my phone number has won $2500000. " +
                    "I clicked on it and provided my social media credentials to confirm my id. Now my social media account has been hacked. " +
                    "I've changed the password now. How do I avoid this?";

            else if (characterName == "Ant")
                Question = "Last week, I connected my phone to a public pc to transfer songs and documents. " +
                    "Since then, my phone has been installing random apps and I keep getting ads in my home screen. How do I resolve this?";
            
            
        }

        else if (Level == "Level3")
        {
            if (characterName == "Pink")
            {
                Question = "My phone was working fine for the past 3 years. All of a sudden it started malfunctioning because of a recent malware. " +
                    "I granted only necessary permissions and I even use anti-virus, VPN. Am I missing something?";
            }

            else if (characterName == "Totem")
                Question = "Yesterday when I went for swimming, I wanted to click some underwater pics. " +
                    "I thought splash proof meant I can use the phone underwater. But water entered my phone and " +
                    "I had to take it to a repair shop to get it fixed. I was locked out of some important contacts and documents for a week. " +
                    "Is there a way to access my data apart from using my phone?";

            else if (characterName == "Astronaut")
                Question = "My friend disposed his phone last week. " +
                    "Later, someone emailed him his private data that were stored in his old phone and threatened to leak them. " +
                    "I do not want to get in such a situation. How to stay safe?";

            else if (characterName == "Frog")
                Question = "I lost all of my data last month when a random guy hit me and took my phone. " +
                    "I lost all my contacts and data. I have a backup but I do not want that guy to access my info. " +
                    "Is there any way to prevent this from happening?";
        }
        return Question;
    }

    public string ReturnPromotionFocus(string characterName, string Level)
    {
        string PromotionFocusText = "";

        if (Level == "Traps")
        {
            if (characterName == "Bee")
                PromotionFocusText = "If you grant unnecessary permissions, your personal data might be misused.";

            else if (characterName == "Rabbit")
                PromotionFocusText = "If you do not setup a lock screen, you would be vulnerable to unauthorised access and data theft.";

            else if (characterName == "GrassHopper")
                PromotionFocusText = "If you install apps from 3rd party app stores, you might be prone to malware, data theft and misuse of data.";

            else if (characterName == "Fairy")
                PromotionFocusText = "If you Install unnecessary apps, malware might infiltrate your phone. " +
                    "If you don't Close apps when not in use, your data might be tracked in background.";
        }

        else if (Level == "Level2")
        {
            if (characterName == "Hunter")
                PromotionFocusText = "If you do not turn off Bluetooth, GPS, Wifi when they are not required, " +
                    "then you would be vulnerable to location tracking, data theft and eavesdropping.";

            else if (characterName == "Fox")
                PromotionFocusText = "If you connect to unknown wireless networks without a VPN, you might be vulnerable to " +
                    "eavesdropping and data theft.";

            else if (characterName == "Squirrel")
                PromotionFocusText = "If you click on unknown links from unknown sources, you might be vulnerable to malware and phishing links";

            else if (characterName == "Ant")
                PromotionFocusText = "If you do not install an anti-virus on your phone, you would be vulnerable to malware and harmful files.";
        }

        else if(Level == "Level3")
        {
            if (characterName == "Pink")
                PromotionFocusText = "If you do not update your Phone's OS regularly, you might be vulnerable to malware attacks due to security pitfalls in the OS.";

            else if (characterName == "Totem")
                PromotionFocusText = "If you do not backup your phone's data regularly, you might be vulnerable to losing your data when you lose your phone.";

            else if (characterName == "Astronaut")
                PromotionFocusText = "If you don't wipe your phone's data before disposing it, it might be misused by perpetrators.";

            else if (characterName == "Frog")
                PromotionFocusText = "If you don't activate remote lock and wipe, you might be vulnerable to data theft and misuse when you lose your phone";
        }

        return PromotionFocusText;
    }

    public List<Quiz_Data_Class> ReturnOptions(string CharacterName, string Level)
    {
        if (Level == "Traps")
        {
            if (CharacterName == "Bee")
            {
                arr[0] = "Only allow Permissions that are needed for the app's functionality/features";
                arr[1] = "You should learn to track your steps manually";
                arr[2] = "Uninstall the App and use another app";

                quiz.Add(new Quiz_Data_Class(arr[0], "c"));
                quiz.Add(new Quiz_Data_Class(arr[1], "w1"));
                quiz.Add(new Quiz_Data_Class(arr[2], "w2"));
            }

            else if (CharacterName == "Rabbit")
            {
                arr[0] = "Always keep your phone to yourself";
                arr[1] = "Setup a Lockscreen";
                arr[2] = "Keep your phone locked away when you are not using it";

                quiz.Add(new Quiz_Data_Class(arr[0], "w1"));
                quiz.Add(new Quiz_Data_Class(arr[1], "c"));
                quiz.Add(new Quiz_Data_Class(arr[2], "w2"));
            }

            else if (CharacterName == "GrassHopper")
            {
                arr[0] = "Turn off notifications for that specific app.";
                arr[1] = "Ignore the notifications. You'll get used to it.";
                arr[2] = "Uninstall the app. Never Install apps from 3rd party app stores.";

                quiz.Add(new Quiz_Data_Class(arr[0], "w1"));
                quiz.Add(new Quiz_Data_Class(arr[1], "w2"));
                quiz.Add(new Quiz_Data_Class(arr[2], "c"));
            }

            else if (CharacterName == "Fairy")
            {
                arr[0] = "Close the app when not in use. If the issue persists, uninstall the app.";
                arr[1] = "Turn Off Notifications for the app";
                arr[2] = "Throw your phone and buy a new one";

                quiz.Add(new Quiz_Data_Class(arr[0], "c"));
                quiz.Add(new Quiz_Data_Class(arr[1], "w1"));
                quiz.Add(new Quiz_Data_Class(arr[2], "w2"));
            }
        }

        else if (Level == "Level2")
        {
            if (CharacterName == "Hunter")
            {
                arr[0] = "Uninstall the app";
                arr[1] = "Do not use banking apps in the public";
                arr[2] = "Turn off GPS, Wifi and Blutooth when they are not needed";

                quiz.Add(new Quiz_Data_Class(arr[0], "w1"));
                quiz.Add(new Quiz_Data_Class(arr[1], "w2"));
                quiz.Add(new Quiz_Data_Class(arr[2], "c"));
            }

            else if (CharacterName == "Fox")
            {
                arr[0] = "Do not use phone for banking purposes or social media";
                arr[1] = "Use VPN while connecting to public Wifi. VPN encrypts data during transmission.";
                arr[2] = "Change the password of the social media account";

                quiz.Add(new Quiz_Data_Class(arr[0], "w1"));
                quiz.Add(new Quiz_Data_Class(arr[1], "c"));
                quiz.Add(new Quiz_Data_Class(arr[2], "w2"));
            }

            else if (CharacterName == "Squirrel")
            {
                arr[0] = "Call the person who sent the message and confirm before opening the message";
                arr[1] = "Avoid clicking on unknown links";
                arr[2] = "Use VPN before clicking on the link";

                quiz.Add(new Quiz_Data_Class(arr[0], "w1"));
                quiz.Add(new Quiz_Data_Class(arr[1], "c"));
                quiz.Add(new Quiz_Data_Class(arr[2], "w2"));
            }

            else if (CharacterName == "Ant")
            {
                arr[0] = "Time to buy a new phone...";
                arr[1] = "Avoid connecting your phone to public PC's";
                arr[2] = "Install an Anti-Virus to detect malaware and remove it";

                quiz.Add(new Quiz_Data_Class(arr[0], "w1"));
                quiz.Add(new Quiz_Data_Class(arr[1], "w2"));
                quiz.Add(new Quiz_Data_Class(arr[2], "c"));
            }
        }

        else if (Level == "Level3")
        {
            if (CharacterName == "Pink")
            {
                arr[0] = "Have you tried updating your Phone's OS?";
                arr[1] = "Buy a new phone";
                arr[2] = "Install an Anti-Virus";

                quiz.Add(new Quiz_Data_Class(arr[0], "c"));
                quiz.Add(new Quiz_Data_Class(arr[1], "w1"));
                quiz.Add(new Quiz_Data_Class(arr[2], "w2"));
            }

            else if (CharacterName == "Totem")
            {
                arr[0] = "Backup your phone's data on a regular basis";
                arr[1] = "Secure all of your data with password";
                arr[2] = "Have 2 phones! If one goes out, you can use the other one!";

                quiz.Add(new Quiz_Data_Class(arr[0], "c"));
                quiz.Add(new Quiz_Data_Class(arr[1], "w1"));
                quiz.Add(new Quiz_Data_Class(arr[2], "w2"));
            }

            else if (CharacterName == "Astronaut")
            {
                arr[0] = "Give it to your close relative or friend";
                arr[1] = "Don't throw your phone. Keep it..";
                arr[2] = "You can wipe your data before throwing away your phone";

                quiz.Add(new Quiz_Data_Class(arr[0], "w1"));
                quiz.Add(new Quiz_Data_Class(arr[1], "w2"));
                quiz.Add(new Quiz_Data_Class(arr[2], "c"));
            }

            else if (CharacterName == "Frog")
            {
                arr[0] = "Lock your phone in a cupboard when it is not needed";
                arr[1] = "Activate Remote Lock & Wipe";
                arr[2] = "Chain your phone to yourself with a wrist band";

                quiz.Add(new Quiz_Data_Class(arr[0], "w1"));
                quiz.Add(new Quiz_Data_Class(arr[1], "c"));
                quiz.Add(new Quiz_Data_Class(arr[2], "w2"));
            }
        }
        return quiz;
    }

    public string ReturnReply(string CharacterName, string answer)
    {
        if(CharacterName == "Bee")
        {
            if (answer == "w1")
                Reply = "That's an age old tradition. The app is neat and keeps track of my step history seamlessly. " +
                    "Other bees do not have any issues with it.";
            else if (answer == "w2")
                Reply = "The other bees also use this app and they don't have any issues." +
                    "This app was developed by the bees, for the bees.";
            else if (answer == "c")
                Reply = "You were right! After denying SMS and Location permission in the settings, the app is working fine now. Thank you!";
        }

        else if (CharacterName == "Rabbit")
        {
            if (answer == "w1")
                Reply = "I can't always keep my phone to myself! The radiation might affect me if I use it for a prolonged period of time.";
            else if (answer == "w2")
                Reply = "I live in a burrow bud! Also, keeping my phone away everytime would be pointless.";
            else if (answer == "c")
                Reply = "That's Awesome! Then only I would be able to unlock my phone. I will make sure that no one peeks while I unlock. " +
                    "Although I look forward to buy a phone with finger print unlock in the near future to avoid this peeking problem.";
        }

        else if (CharacterName == "GrassHopper")
        {
            if (answer == "w1")
                Reply = "That was not helpful. My phone is still acting weird and the screen keeps flickering! Suggest me something else.";
            else if (answer == "w2")
                Reply = "I don't think that is a good idea. What if the app misuses my information. " +
                    "If they don't mind spamming my phone with ads, they might as well steal information.";
            else if (answer == "c")
                Reply = "I thought so. I will install it from the official app store to stay safe. Thanks a lot mate!";
        }

        else if (CharacterName == "Fairy")
        {
            if (answer == "w1")
                Reply = "What if the app updates without notifying me! That does not seem to be a smart solution";
            else if (answer == "w2")
                Reply = "Dude! This is a new phone that my dad bought me last week. Come up with an alternative solution.";
            else if (answer == "c")
                Reply = "Awesome! After closing the app the issue was resolved. Thank you so much!";
        }

        else if (CharacterName == "Hunter")
        {
            if (answer == "w1")
                Reply = "Everyone in the jungle uses it. It is really handy. Nothing like this has happened to me before. " +
                    "There must be another way to prevent this.";
            else if (answer == "w2")
                Reply = "I don't think that would be a solution. I made sure no one was near me while trying to make that transaction. " +
                    "Also, everyone in the jungle uses it.";
            else if (answer == "c")
                Reply = "That makes sense! I was connected to the public Wifi and also had Bluetooth and GPS on when they were not needed. " +
                    "I will make it a habit to check for these on the notification panel. Thanks dude.";
        }

        else if (CharacterName == "Fox")
        {
            if (answer == "w1")
                Reply = "The whole jungle uses those apps and no one has faced this problem before.";
            else if (answer == "w2")
                Reply = "I did, but what if this happens again! I want to prevent this from happening again.";
            else if (answer == "c")
                Reply = "Thank you for your suggestion dillon. I will make sure to use a VPN next time when I connect to a public Wifi Network.";
        }

        else if (CharacterName == "Squirrel")
        {
            if (answer == "w1")
                Reply = "It looks like a generic automated message. I won't be able place a call. " +
                    "I might also lose money if that number happens to be an overseas number.";
            else if (answer == "w2")
                Reply = "So I'd still be accessing a sketchy link and my social media account would be compromised. VPN only encrypts my data so that is not the right solution.";
            else if (answer == "c")
                Reply = "If its too good to be true, it might not be true eh! " + "Sounds like a good thumb rule. I'll keep that in mind.";
        }

        else if (CharacterName == "Ant")
        {
            if (answer == "w1")
                Reply = "Sure! I would be delighted to buy a new one if you can sponsor for it! " +
                    "What if the same problem occurs again? I can't keep buying new phones!";
            else if (answer == "w2")
                Reply = "I cannot afford an internet connection. I am a Worker Ant! So I depend on cheap Public internet browsing places " +
                    "at the time of need.";
            else if (answer == "c")
                Reply = "That sounds like a good idea! I use one for my PC and it works great. I saw there were various free " +
                    "anti-virus softwares available in playstore. I'll try one of them.";
        }

        else if (CharacterName == "Pink")
        {
            if (answer == "w1")
                Reply = "There must be another way to fix this issue. I was guaranteed that this phone would be fine for at least 5 years. " +
                    "My friend has the same phone and it works fine.";
            else if (answer == "w2")
                Reply = "I already said that I use anti-virus software. Please Pay Attention!";
            else if (answer == "c")
                Reply = "Hold on, I'll try that…...That solved the problem! I have never done this before. " +
                    "I thought it was just for some visual updates but never knew about the security updates! Thank you.";
        }

        else if (CharacterName == "Totem")
        {
            if (answer == "w1")
                Reply = "That does not make sense. I'd still be locked out of my phone and the data.";
            else if (answer == "w2")
                Reply = "That does not sound practical. I cannot manually maintain an identical copy of my phone! That's too much of work.";
            else if (answer == "c")
                Reply = "Alright! I'll use google's default sync services. " +
                    "Thank you Dillon, the troll is waiting for you at the end of the jungle. Good luck on your journey!";
        }

        else if (CharacterName == "Astronaut")
        {
            if (answer == "w1")
                Reply = "I can delete some private data and give it to them. But what if they are able to retrieve it!";
            else if (answer == "w2")
                Reply = "Then I'll end up collecting old phones! That would be a mess.";
            else if (answer == "c")
                Reply = "That makes sense! I don't know why my friend did not do that. It is better to be safe than being sorry.";
        }

        else if (CharacterName == "Frog")
        {
            if (answer == "w1")
                Reply = "Ah! The good Ol' days of locking up treasure. Please suggest something for the 21st Century.";
            else if (answer == "w2")
                Reply = "That sounds good but I think that would not be feasible in the long run.";
            else if (answer == "c")
                Reply = "My son Tadpole was talking about it last month. I'll give it a try. Thanks";
        }
        return Reply;
    }
}


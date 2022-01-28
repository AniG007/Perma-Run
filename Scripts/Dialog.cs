using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class Dialog
{
    [TextArea(3,10)]
    public string[] sentences;
    public string[] sentences1;

    /*public string ReturnRandom()
    {
        sentences = new string[13];

        sentences[0] = "Be cautious while charging your phone in public places. Use a USB data protector";
        sentences[1] = "Always download apps from Google Play Store";
        sentences[2] = "Never leave your bluetooth turned on for a long time";
        sentences[3] = "It is good to set up a strong password for the lock screen";
        sentences[4] = "It is wise to use a VPN while connecting to public wifi networks";
        sentences[5] = "Think twice before allowing a specific permission for an app";
        sentences[6] = "If you're in doubt, it is best to decline a permission rather than allowing it";
        sentences[7] = "Never share your password with others";
        sentences[8] = "Never write down passwords. Instead use a password manager";
        sentences[9] = "Never click on ad's spontaneously. Check for authenticity";
        sentences[10] = "Be cautious while being redirected to other webpages";
        sentences[11] = "Check the domain of the website before submitting online forms";
        sentences[12] = "Setting up long alpha-numeric passwords along with a mix of special characters is a good habit to protect your data";

        return sentences[Random.Range(0, sentences.Length)];
    }*/

    public string ReturnSuggestion(string permissionName, string tag)
    {
        string Suggestion = "";
        string level = SceneManager.GetActiveScene().name;
        
        if (permissionName == "camera" || permissionName.Contains("camera"))
        {
            Suggestion = ReturnCamSuggestion(tag, level);
        }

        else if(permissionName == "Storage" || permissionName.Contains("storage"))
        {
            Suggestion = ReturnStorageSuggestion(tag, level);
        }

        else if (permissionName == "Location" || permissionName.Contains("location"))
        {
            Suggestion = ReturnLocationSuggestion(tag, level);
        }

        else if (permissionName == "Mic" || permissionName.Contains("mic"))
        {
            Suggestion = ReturnMicSuggestion(tag, level);
        }

        else if (permissionName == "Phone" || permissionName.Contains("phone"))
        {
            Suggestion = ReturnPhoneSuggestion(tag, level);
        }

        else if (permissionName == "SMS" || permissionName.Contains("sms"))
        {
            Suggestion = ReturnSMSSuggestion(tag, level);
        }

        else if (permissionName == "Activity_Recognition" || permissionName.Contains("activity"))
        {
            Suggestion = ReturnActivitySuggestion(tag, level);
        }

        else if (permissionName == "Sensors" || permissionName.Contains("sensor"))
        {
            Suggestion = ReturnSensorSuggestion(tag, level);
        }

        else if (permissionName == "Contacts" || permissionName.Contains("contact"))
        {
            Suggestion = ReturnContactSuggestion(tag, level);
        }

        else if (permissionName == "Calendar" || permissionName.Contains("calendar"))
        {
            Suggestion = ReturnCalendarSuggestion(tag, level);
        }

        return Suggestion;
    }

    private string ReturnCamSuggestion(string tag, string level)
    {
        string CamSuggestion = "";

        if (level == "Traps")
            CamSuggestion = "Camera permission is required for capturing and sharing pictures with your contacts and for making video calls.";

        else if (level == "Level2")
            CamSuggestion = "Camera permission might be required for capturing and sharing pictures in fitness blogs or groups";

        else if (level == "Level3")
            CamSuggestion = "Camera permission allows an app to take pictures and video from your phone. This is not required for an Event Booking App";

        return CamSuggestion;
    }

    private string ReturnStorageSuggestion(string tag, string level)
    {
        string StorageSuggestion= "";

        if (level == "Traps")
            StorageSuggestion = "Storage Permission is required for downloading files sent by your contacts and for sending files to your contacts.";

        else if (level == "Level2")
            StorageSuggestion = "Storage Permission is required for sharing pictures in fitness blogs";

        else if (level == "Level3")
            StorageSuggestion = "Storage Permission is required for saving event tickets on your phone";
        return StorageSuggestion;
    }

    private string ReturnLocationSuggestion(string tag, string level)
    {
        string LocationSuggestion = "";

        if (level == "Traps")
            LocationSuggestion = "Location permission is used for retrieving your location when you want to share it with your contacts";
        
        else if (level == "Level2")
            LocationSuggestion = "Location permission is required for tracking your steps, calculating distance and for tracking your walking routes";
        
        else if (level == "Level3")
            LocationSuggestion = "Location permission is required for retrieving your Location to suggest events happening near you";

        return LocationSuggestion;
    }

    private string ReturnMicSuggestion(string tag, string level)
    {
        string MicSuggestion = "";

        if (level == "Traps")
            MicSuggestion = "Microphone permission is needed for making video calls and for recording audio snippets";

        else if (level == "Level2")
            MicSuggestion = "Microphone permission is needed for recording audio. This might not be required for a Fitness Tracking app";

        else if (level == "Level3")
            MicSuggestion = "Microphone permission is needed for recording audio. This might not be required for an Event Booking app";


        return MicSuggestion;
    }

    private string ReturnPhoneSuggestion(string tag, string level)
    {
        string PhoneSuggestion = "";

        if (level == "Traps")
            PhoneSuggestion = "The phone dialler feature of the Communication app requires phone permission for Placing calls and retrieving caller id";

        else if (level == "Level2")
            PhoneSuggestion = "The phone permission is used for Placing calls and retrieving caller id. This is not needed for a Fitness Tracking app";

        else if (level == "Level3")
            PhoneSuggestion = "The phone permission is used for Placing calls and retrieving caller id. This is not needed for an Event Booking app.";

        return PhoneSuggestion;
    }

    private string ReturnSMSSuggestion(string tag, string level)
    {
        string SMSSuggestion = "";

        if (level == "Traps")
            SMSSuggestion = "The Communication app needs SMS permission to send SMS to your contacts, receive and read SMS. This might cost you money.";
        else if (level == "Level2")
            SMSSuggestion = "SMS Permission is used for sending and receiving SMS which might cost you money and the app might read your messages. This is not needed for an Activity Tracking app.";

        else if (level == "Level3")
            SMSSuggestion = "SMS Permission is used for sending and receiving SMS which might cost you money and the app might read your messages. This is not needed for an Event Booking app.";
        return SMSSuggestion;
    }

    private string ReturnActivitySuggestion(string tag, string level)
    {
        string ActivitySuggestion = "";

        if (level == "Traps")
            ActivitySuggestion = "Activity Recognition permission is for recognizing activities like running and walking. This might not be required for a Communication app.";

        else if (level == "Level2")
            ActivitySuggestion = "Fitness Tracking app requires Activity Recognition permission for recognizing activities like running and walking.";

        else if (level == "Level3")
            ActivitySuggestion = "Activity Recognition permission is for recognizing activities like running and walking and this is not required for an Event Booking app.";

        return ActivitySuggestion;
    }

    private string ReturnSensorSuggestion(string tag, string level)
    {
        string SensorSuggestion = "";

        if (level == "Traps")
            SensorSuggestion = "Sensor permission is needed for accessing sensors like heart rate sensor to track the user's vitals. This is not needed for a Communication app.";

        else if (level == "Level2")
            SensorSuggestion = "Sensor permission is needed for accessing sensors like heart rate sensor to track the user's vitals.";

        else if (level == "Level3")
            SensorSuggestion = "Sensor permission is needed for accessing sensors like heart rate sensor to track the user's vitals. This might not be required for an Event Booking app.";

        return SensorSuggestion;
    }

    private string ReturnContactSuggestion(string tag, string level)
    {
        string ContactSuggestion = "";

        if (level == "Traps")
            ContactSuggestion = "Contacts permission is needed for accessing contacts saved in the phone. Communication app needs this to access your contacts so that you can send messages to them.";

        else if (level == "Level2")
            ContactSuggestion = "Contacts permission is needed for accessing contacts saved in the phone. This might not be required for a fitness tracking app.";

        if (level == "Level3")
            ContactSuggestion = "Contacts permission is needed for accessing contacts saved in the phone. This might not be required for an Event Booking app.";

        return ContactSuggestion;
    }

    private string ReturnCalendarSuggestion(string tag, string level)
    {
        string CalendarSuggestion = "";

            if (level == "Traps")
                CalendarSuggestion = "Calendar Permission is used for accessing user's calendar and setting reminders or retrieving events. This might not be needed for a Communication app";

            else if (level == "Level2")
                CalendarSuggestion = "Calendar Permission is used for accessing user's calendar and setting reminders or retrieving events. This is not needed for an activity tracker.";

            else if (level == "Level3")
                CalendarSuggestion = "The Calendar Permission is needed for an Events Booking app for saving event reminders.";

        return CalendarSuggestion;
    }


}   
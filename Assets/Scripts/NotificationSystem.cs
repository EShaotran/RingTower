/* Ethan Shaotran 2017
 * in Collaboration with 
 * Purifi Games & Shaotran.com */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LocalNotification = UnityEngine.iOS.LocalNotification;
//using UnityEngine.iOS;

public class NotificationSystem : MonoBehaviour {

	//UnityEngine.iOS.LocalNotification notif;
	LocalNotification newNotif;

	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep; //Stop Dimming
		//	notif = new UnityEngine.iOS.LocalNotification();
		newNotif = new LocalNotification ();




		if (PlayerPrefs.GetInt ("New") == 0) //0 = New
			RegisterForNotifications ();
		//else
		//	PlayerPrefs.SetInt ("New", 1); //1 = Returning

		if (UnityEngine.iOS.NotificationServices.localNotificationCount > 0 || UnityEngine.iOS.NotificationServices.scheduledLocalNotifications.Length > 0) { //If open app & Notification Scheduled
			UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications(); //Clear it
		}

			//Instant Set Notif usually goes here!
		

	}

	void RegisterForNotifications () {
		UnityEngine.iOS.NotificationServices.RegisterForNotifications (UnityEngine.iOS.NotificationType.Alert |
			UnityEngine.iOS.NotificationType.Badge |
			UnityEngine.iOS.NotificationType.Sound);
	}
		
	void SetLongNewNotif () { //2 Days
		newNotif.fireDate = System.DateTime.Now.AddDays (1);

		int NotifMessage = Random.Range (0, 4);
		switch (NotifMessage) {
		case 0: 
			newNotif.alertBody = "Come back! Your colorful rings are waiting for you!";
			break;
		case 1: 
			newNotif.alertBody = "Come back! Your highscore of " + PlayerPrefs.GetInt("HighScore") + " needs a beating!";
			break;
		case 2: 
			newNotif.alertBody = "The tower is waiting for you!";
			break;
		default:
			newNotif.alertBody = "Where you at? We have a tower to climb!";
			break;
		}

		UnityEngine.iOS.NotificationServices.ScheduleLocalNotification (newNotif);
	}

	void OnApplicationQuit () {
		UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications(); //Clear it
		SetLongNewNotif ();
	} 

	void OnApplicationFocus (bool focus) {
		if (!focus) {
			UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications (); //Clear it
			SetLongNewNotif ();
		}
	}


}

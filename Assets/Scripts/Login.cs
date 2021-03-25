using System.Collections;
using Firebase;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {
	IEnumerator Start() {
		FirebaseApp.LogLevel = LogLevel.Info;
		
		// init firebase
		var initTask = FirebaseApp.CheckAndFixDependenciesAsync();
		while (!initTask.IsCompleted) {
			yield return null;
		}
		if (initTask.Exception != null) {
			// TODO: throw new InitException(userTask.Exception);
			Debug.LogException(initTask.Exception);
			yield break;
		}
		
		var auth = FirebaseAuth.DefaultInstance;

		// if the user is authenticated already, just use that sign-in
		if (auth.CurrentUser != null) {
			OnSignInSuccessful(auth.CurrentUser);
			yield break;
		}
		
		var userTask = auth.SignInAnonymouslyAsync();
		while (!userTask.IsCompleted) {
			yield return null;
		}
		if (userTask.Exception != null) {
			// TODO: throw new LoginException(userTask.Exception);
			Debug.LogException(userTask.Exception);
			yield break;
		}

		var user = userTask.Result;
		OnSignInSuccessful(user);
	}

	void OnSignInSuccessful(FirebaseUser user) {
		Debug.Log(Application.persistentDataPath);
		Debug.Log(user.UserId);
		SceneManager.LoadScene("Game");
		//FindObjectOfType<Game>().SignIn(user);
	}
}
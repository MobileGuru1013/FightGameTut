using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// <Summary>
// Splash Screen
// Jose M. Puebla

    [RequireComponent(typeof(AudioSource))]                             // Add AudioSource when attaching the scripts

public class SplashScreen : MonoBehaviour {

    public Texture2D    _splashScreenBackGround;                        // Creates slot in inspector to assign splash screen background image
    public Texture2D    _splashScreenText;                              // Creates slot in inspector to assign splash screen text

    private AudioSource _splashScreenAudio;                             // Defines naming convention for AudioSource component

    public AudioClip    _splashScreenMusic;                             // Creates slot in inspector to assign splash screen music
        
    private float       _splashScreenFadeValue;                         // Defines fade value 
    private float       _splashScreenFadeSpeed = 0.15f;                 //  Defines fade speed

    private SplashScreenController _splashScreenController;             // Defines Naming Convention for SplashScreen controller

    private enum SplashScreenController {                               // Defines states for splash screen

        SplashScreenFadeIn  =   0,                                       
        SplashScreenFadeOut =   1                           
    }

    private void Awake() {

        _splashScreenFadeValue = 0;                                     // Fade value equals zero start up

    }
    // Use this for initialization
    void Start () {

        Cursor.visible = false;                                         // Set the coursor visable state to false
        Cursor.lockState = CursorLockMode.Locked;                       // and lock the cursor
            
        _splashScreenAudio = GetComponent<AudioSource>();               // SplashScreen audio equals the audio source.

        _splashScreenAudio.volume = 0;                                  //  Audio volume equals 0 on start up.
        _splashScreenAudio.clip = _splashScreenMusic;                   // Audioclip eqals the splashScreen music
        _splashScreenAudio.loop = true;                                 // Set audio to loop
        _splashScreenAudio.Play();                                      // Play Audio

        _splashScreenController =                                       // State equals 
            SplashScreen.SplashScreenController.SplashScreenFadeIn;     // fade in on start up

        StartCoroutine("SplashScreenManager");                          // Start SplashScreenManager function
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator SplashScreenManager() {

        while (true) {

            switch (_splashScreenController) {

                case SplashScreenController.SplashScreenFadeIn:
                    SplashScreenFadeIn();
                    break;
            }
            yield return null;
        }
    }
    private void SplashScreenFadeIn() {

        Debug.Log("SplashScreenFadeIn");

        _splashScreenAudio.volume += _splashScreenFadeSpeed * Time.deltaTime;   //  increase volume by fade speed
        _splashScreenFadeValue += _splashScreenFadeSpeed * Time.deltaTime;      //  increase fade value by fade speed

        if (_splashScreenFadeValue > 1)                                         //  if fade value is greater than 1
            _splashScreenFadeValue = 1;                                         // Then set fade value to 1

        if (_splashScreenFadeValue == 1)                                        // If fade value = 1
            _splashScreenController =                                           // Set splash screen controller to =
                SplashScreen.SplashScreenController.SplashScreenFadeOut;        // Splashs screen Fade out

    }

    private void SplashScreenFadeOut() {

        Debug.Log("SplashScreenFadeOut");

        _splashScreenAudio.volume -= _splashScreenFadeSpeed * Time.deltaTime;   //  decrease volume by fade speed
        _splashScreenFadeValue -= _splashScreenFadeSpeed * Time.deltaTime;      //  decrease fade value by fade speed

        if (_splashScreenFadeValue < 0)                                         //  if fade value is greater than 0
            _splashScreenFadeValue = 0;                                         // Then set fade value to 0.

        if (_splashScreenFadeValue == 0)                                        // if fade value = 0 
            SceneManager.LoadScene("ControllerWarning");                        // Load scene ControllerWarning
    }

    private void OnGUI() {

        GUI.DrawTexture(new Rect(0, 0,                                           // Draw texture starting at 0/0
             Screen.width, Screen.height),                                     // by the screen with and height
            _splashScreenBackGround);                                           // and draw the background texture

        GUI.color = new Color(1,1,1, _splashScreenFadeValue);                   // GUI color is equal to true (1,1,1) plus fade value

        GUI.DrawTexture(new Rect(0, 0                                           // Draw texture starting at 0/0
            , Screen.width, Screen.height),                                     // by the screen with and height
            _splashScreenBackGround);                                           // and draw the splash screen  text
    }
}

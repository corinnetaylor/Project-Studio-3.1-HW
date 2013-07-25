using UnityEngine;
using System.Collections;

public class GoalController : MonoBehaviour {	
	
	Color[] colors;
	Color goalColor;
	float theTime = 30; //Change this to control game length
	int theScore = 0;
	int theHighScore = 0;
	public GUIText Timer;
	public GUIText Score;
	public GUIText HighScore;
	public GUIText GameOverText;
	public GUIText IntroText;
	bool inProgress; //tells if the game is currently going on
	bool introPlayed; //tells the game if it should display the intro text before starting the game
	
	//Assign each finger in inspector
	public Transform thumb; //1
	public Transform index; //2
	public Transform middle; //3
	public Transform ring; //4
	public Transform pinkie; //5

	//Initialize all the booleans to determine which fingers are down and which are up
	bool is1Down;
	bool is2Down;
	bool is3Down;
	bool is4Down;
	bool is5Down;
	
	Color currentColor;
	
	void Awake(){
		colors = new Color[4];
		colors[0] = Color.red;
		colors[1] = Color.green;
		colors[2] = Color.blue;
		colors[3] = Color.yellow;
		
		}
	
	// Use this for initialization
	//Hides the game over text, gets the starting color at random, sets the base high score text, and starts the game as in progress
	void Start () {
		
		GameOverText.enabled = false;
		SetNextColor();
		HighScore.text = "High Score: ";
		inProgress = false;
		introPlayed = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!introPlayed){
			IntroText.text = "Welcome to Pose. \nControl the hand with Q, W, E, R, and Space. Try it now!";
			IntroText.text += "\n Each color is tied to a certain pose.";
			IntroText.text += "\nOn the right, each pose in front of its correct color.";
			IntroText.text += "\n The color of the center light shows which pose to make next.";
			IntroText.text += "\n See how many poses you can make before time runs out!";
			IntroText.text += "\n Press Enter when you're ready to start.";
			
			if (Input.GetKeyDown(KeyCode.Return)){
				IntroText.enabled = false;
				introPlayed = true;
				inProgress = true;
				
			}
			
		}
		
		//Central game mechanics are carried out while the game is in progress, when the time runs out, the inProgress state is flipped
		//preventing the color from switching or hand movements from having any effects
		
		if (inProgress){
			//The timer and score keeping Gui text
			theTime -= Time.deltaTime ;		
			if (theTime < 0){
				theTime = 0;//Have some game over text pop up	
				GameOver();
			}
			
			Timer.text = ((int)theTime).ToString();		
			Score.text = theScore.ToString();
			
			//Updates for the positions of the fingers
			is1Down = thumb.GetComponent<FingerControl>().isKeyDown();
			is2Down = index.GetComponent<FingerControl>().isKeyDown();
			is3Down = middle.GetComponent<FingerControl>().isKeyDown();
			is4Down = ring.GetComponent<FingerControl>().isKeyDown();
			is5Down = pinkie.GetComponent<FingerControl>().isKeyDown();
		
			//Controls if the correct position has been achieved for the particular color
			if (currentColor == Color.red){
				Debug.Log ("Color is red");
				
				if (!is1Down && !is2Down && is3Down && is4Down && !is5Down){ //middle and ring finger down
					
					SetNextColor();
					theScore += 1;
					
				}
				
				
			} else if (currentColor == Color.green){
				Debug.Log ("Color is green");
				
				if (is1Down && is2Down && is3Down && is4Down && is5Down){ //all fingers down
					
					SetNextColor();
					theScore += 1;
				}
				
			} else if (currentColor == Color.blue){
				Debug.Log ("Color is blue");
				
				if (!is1Down && is2Down && !is3Down && is4Down && !is5Down){ //index and ring finger down
					
					SetNextColor();
					theScore += 1;
				}
				
				
			} else if (currentColor == Color.yellow){
				Debug.Log ("Color is yellow");
				
				if (is1Down && is2Down && is3Down && is4Down && !is5Down){ //all but thumb down
					
					SetNextColor();
					theScore += 1;
				}
				
			}
			//When a game is over (!inProgress) this allows you to restart the game by pressing enter
		} else if (!inProgress && introPlayed) {
				
				if (Input.GetKeyDown(KeyCode.Return)){
			
				GameOverText.enabled = false;
				theScore = 0;
				theTime = 30;
				SetNextColor();
				inProgress = true;
				
			}
		}
		
		
	}
	
	//Sets the next color randomly, called at the beginning of a game and after each successful pose
	void SetNextColor(){
		
		int i = Random.Range(0,colors.Length);
		light.color = colors[i];
		currentColor = colors[i];
		
	}
	//Game over method shows some GUI text, sets high scores, and allows a new game to be started
	void GameOver(){
		inProgress = false;
		GameOverText.text = "Game Over";
		GameOverText.enabled = true;
		
		if (theScore > theHighScore){
			theHighScore = theScore;
			GameOverText.text += "\n Congratulations! New High Score: " + theHighScore.ToString();
			HighScore.text = "High Score: " + theHighScore.ToString();
		}
		
		GameOverText.text += "\n Press Enter to Play Again";
		
	}
	
	
}

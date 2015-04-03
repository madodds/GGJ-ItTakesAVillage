using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VoteManager : MonoBehaviour {

	public AudioClip aboutToExplode;
	public AudioClip dance;
	public AudioClip eat;
	public AudioClip explode;
	public AudioClip hot;
	public AudioClip pet;
	public AudioClip fail;
	public AudioClip sad;
	public AudioClip tired;
	public AudioSource source;
	public Animator monsterAnim;
	public Animator speechAnim;
	public Meter emotion;
	public Meter hunger;
	public Meter exercise;
	public int petVote;
	public int feedVote;
	public int danceVote;
	public SpriteRenderer sprite;
	public float voteTime;
	public float gameOverTime;
	public float elapsedTime;
	public int increment;
	public int incrementMax;
	public int incrementMin;
	public Text voteTimer;
	public Text petVoteText;
	public Text feedVoteText;
	public Text danceVoteText;
	public int decide;
	public bool exploding;
	public bool sound;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(!exploding)
		{
			if(emotion.value <= hunger.value && emotion.value <= exercise.value)
			{
				print("emotion");
				speechAnim.SetInteger("State", 5);
				if(elapsedTime == voteTime && petVote > 0)
				{
					source.PlayOneShot(pet, 3);
					monsterAnim.SetInteger("State", 6);
				}
			}
			if(hunger.value <= emotion.value && hunger.value <= exercise.value)
			{
				print("hunger");
				speechAnim.SetInteger("State", 3);
				if(elapsedTime == voteTime && feedVote > 0)
				{
					source.PlayOneShot(eat, 3);
					monsterAnim.SetInteger("State", 4);
				}
			}
			if(exercise.value <= hunger.value && exercise.value <= emotion.value)
			{
				print("exercise");
				speechAnim.SetInteger("State", 2);
				if(elapsedTime == voteTime && danceVote > 0)
				{
					source.PlayOneShot(dance, 3);
					monsterAnim.SetInteger("State", 5);
				}
			}
			if(emotion.value > emotion.max)
			{
				speechAnim.SetInteger("State", 4);
				if(!sound)
				{
					sound = true;
					source.PlayOneShot(hot, 3);
				}
			}
			if(hunger.value > hunger.max)
			{
				speechAnim.SetInteger("State", 1);
				if(!sound)
				{
					sound = true;
					source.PlayOneShot(aboutToExplode, 3);
				}
			}
			if(exercise.value > exercise.max)
			{
				speechAnim.SetInteger("State", 0);
				if(!sound)
				{
					sound = true;
					source.PlayOneShot(tired, 3);
				}
			}

			if(emotion.value > emotion.max || hunger.value > hunger.max || exercise.value > exercise.max)
			{
				monsterAnim.SetInteger("State", 1);
			}
			else if(emotion.value < emotion.min || hunger.value < hunger.min || exercise.value < exercise.min)
			{
				monsterAnim.SetInteger("State", 2);
			}
			else
			{
//				monsterAnim.SetInteger("State", 0);
				sound = false;
			}

			if(emotion.value == 0 || hunger.value == 0 || exercise.value == 0)
			{
				monsterAnim.SetInteger("State", 3);
				elapsedTime = 0;
				exploding = true;
				source.PlayOneShot(explode, 3);
			}
			if(emotion.value > emotion.max + 1000 || hunger.value > emotion.max + 1000 || exercise.value > emotion.max + 1000)
			{
				monsterAnim.SetInteger("State", 3);
				elapsedTime = 0;
				exploding = true;
				source.PlayOneShot(explode, 3);
			}

			petVoteText.text = petVote.ToString ();
			feedVoteText.text = feedVote.ToString ();
			danceVoteText.text = danceVote.ToString ();
			voteTimer.text = ((voteTime - elapsedTime)  / 100).ToString ("0.0");

			if(emotion.value > 0)
			{
				emotion.value--;
			}
			if(hunger.value > 0)
			{
				hunger.value--;
			}
			if(exercise.value > 0)
			{
				exercise.value--;
			}
			if(Input.GetButtonDown("Pet"))
			{
				petVote++;
			}
			if(Input.GetButtonDown("Feed"))
			{
				feedVote++;
			}
			if(Input.GetButtonDown("Dance"))
			{
				danceVote++;
			}
			elapsedTime = elapsedTime + 1;
			if(elapsedTime > voteTime)
			{
				increment = Random.Range(incrementMin, incrementMax);
				elapsedTime = 0;

				if(petVote > feedVote && petVote > danceVote)
				{
					emotion.value = emotion.value + increment;
				}
				else if(feedVote > petVote && feedVote > danceVote)
				{
					hunger.value = hunger.value + increment;
				}
				else if(danceVote > feedVote && danceVote > petVote)
				{
					exercise.value = exercise.value + increment;
				}
				else
				{
					if(feedVote == petVote && feedVote == danceVote && feedVote != 0)
					{
						decide = Random.Range(0, 3);
						if(decide == 0)
						{
							hunger.value = hunger.value + increment;
						}
						if(decide == 1)
						{
							emotion.value = emotion.value + increment;
						}
						if(decide == 2)
						{
							exercise.value = exercise.value + increment;
						}
					}
					else if(feedVote != 0)
					{
						if(feedVote == petVote)
						{
							decide = Random.Range(0, 2);
							if(decide == 0)
							{
								hunger.value = hunger.value + increment;
							}
							if(decide == 1)
							{
								emotion.value = emotion.value + increment;
							}
						}

						if(danceVote == petVote)
						{
							decide = Random.Range(0, 2);
							if(decide == 0)
							{
								exercise.value = exercise.value + increment;
							}
							if(decide == 1)
							{
								emotion.value = emotion.value + increment;
							}
						}

						if(feedVote == danceVote)
						{
							decide = Random.Range(0, 2);
							if(decide == 0)
							{
								hunger.value = hunger.value + increment;
							}
							if(decide == 1)
							{
								exercise.value = exercise.value + increment;
							}
						}
					}
				}

				petVote = 0;
				feedVote = 0;
				danceVote = 0;
			}
		}
		else
		{
			elapsedTime = elapsedTime + 1;
			if(elapsedTime > gameOverTime)
			{
				Application.LoadLevel(1);
			}
		}
	}
}

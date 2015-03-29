using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// The different animations for the monster's emotional 
///  state, set to their specified number in unity.
/// </summary>
public enum MonsterAnimationStates { None = 0, Idle = 1, Red = 2, Sad = 3, Exploding = 4 };

/// <summary>
/// The differemt animations for what the monster will do when 
///  voting is done, set to their specified number in unity.
/// </summary>
public enum VoteAnimationStates { None = 0, Petting = 1, Eating = 2, Dancing = 3 };

public class VoteManager : MonoBehaviour
{

	public AudioClip explode;
	public AudioClip fail;
	public AudioSource audioSource;
	public Animator monsterAnim;
	public Meter[] AllMeters;
	public float voteTime;
	public float gameOverTime;
	public Text voteTimer;
	public float explosionVolume;

	private Meter highestVoteMeter;
	private Meter highestMeter;
	private Meter lowestMeter;
	private float elapsedTime;
	private bool exploding;
	private bool failSoundPlayed;

	// Use this for initialization
	void Start()
	{
		failSoundPlayed = false;
	}

	// Update is called once per frame
	void Update()
	{
		// Update the time.
		elapsedTime += Time.deltaTime;

		if (!exploding)
		{
			// Update the UI to show when the next voting time is.
			voteTimer.text = (voteTime - elapsedTime).ToString("0.0");

			// Update the Meter Values.
			float highestVotes = 0;
			foreach (Meter meter in AllMeters)
			{
				// Get the votes from the keyboard inputs.
				if (Input.GetButtonDown(meter.VoteButtonName)) meter.VoteCount++;

				// Deteriorate the meter values.
				meter.Value -= Time.deltaTime;

				if (elapsedTime > voteTime) // Is it time to Vote?
				{
					// Get the meter with the highest votes.
					if (meter.VoteCount > highestVotes)
					{
						highestVoteMeter = meter;
						highestVotes = meter.VoteCount;
					}
					meter.VoteCount = 0; // Reset the votes.
				}
			}

			// Activate what was voted for.
			if (highestVotes > 0) highestVoteMeter.ActivateVotedAction(audioSource, voteTime);

			// Get the meter data for what should be acted on.
			float highestValue = 0;
			float lowestValue = int.MaxValue;
			foreach (Meter meter in AllMeters)
			{
				if (meter.Value > highestValue)
				{
					highestMeter = meter;
					highestValue = meter.Value;
				}
				if (meter.Value < lowestValue)
				{
					lowestMeter = meter;
					lowestValue = meter.Value;
				}
			}

			// Explode the monster if the Meter values get too extreme.
			if ((highestValue > 0 && highestMeter.Value > highestMeter.HighDeathThreshold)
				|| (lowestValue < int.MaxValue && lowestMeter.Value <= 0))
				ExplodeMonster();

			// Warn the players if the Monster is dying (if it isn't dead already).
			else if (lowestMeter.Value < lowestMeter.LowWarningThreshold)
				lowestMeter.ActivateLowWarning(audioSource);

			else if (highestMeter.Value > highestMeter.HighWarningThreshold)
				highestMeter.ActivateHighWarning(audioSource);

			// If everything is OK, make the monster idle.
			else monsterAnim.SetInteger("State", (int)MonsterAnimationStates.Idle);

			// Have the monster ask for what it needs to live.
			if (!exploding) lowestMeter.ActivateAskAction();

			// Reset the time if needed, but only after everything is processed.
			if (elapsedTime > voteTime) elapsedTime = 0;
		}
		else
		{
			// Update the UI to show when the next game starts.
			voteTimer.text = (gameOverTime - elapsedTime).ToString("0.0");
			if (elapsedTime + Time.deltaTime > 5 && !failSoundPlayed)
			{
				audioSource.PlayOneShot(fail, 5);
				failSoundPlayed = true;
			}

			if (elapsedTime > gameOverTime)
			{
				Application.LoadLevel(1);
				failSoundPlayed = false;
			}
		}
	}

	private void ExplodeMonster()
	{
		// Set its animation to explode and to play the sound. Also ends the voting.
		monsterAnim.SetInteger("State", (int)MonsterAnimationStates.Exploding);
		elapsedTime = 0;
		exploding = true;
		audioSource.PlayOneShot(explode, explosionVolume);
	}
}

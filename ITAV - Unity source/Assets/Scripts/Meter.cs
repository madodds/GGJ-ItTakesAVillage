using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Meter : MonoBehaviour
{

	public Text MeterText;
	public string VoteButtonName;
	public float audioVolume;
	public int AskingAnimationState;
	public Animator MonsterAnimator;
	public Animator SpeechAnimator;
	public float endAnimationTime;

	public AudioClip LowWarningSound;
	public int LowWarningAnimationState;
	public float LowWarningThreshold;

	public AudioClip HighWarningSound;
	public int HighWarningAnimationState;
	public float HighWarningThreshold;
	public float HighDeathThreshold;

	public AudioClip VotingSound;
	public int VotingAnimationState;

	public int VoteCount
	{
		get { return _voteCount; }
		set { _voteCount = value; }
	} private int _voteCount = 0;

	public float Value
	{
		get { return _value; }
		set { _value = value; }
	} private float _value = 0;

	private float elapsedAnimationTime;
	
	// Use this for initialization
	void Start()
	{
		Value = (LowWarningThreshold + HighWarningThreshold) * 0.5f;
	}

	// Update is called once per frame
	void Update()
	{
		// Update the UI.
		MeterText.text = VoteCount.ToString();

		if (MonsterAnimator.GetInteger("Vote") == VotingAnimationState)
		{
			// Count the time the animation has been on.
			elapsedAnimationTime += Time.deltaTime;

			// If the action that was voted for completed its animation, then reset the vote animation parameter.
			if (elapsedAnimationTime > endAnimationTime) MonsterAnimator.SetInteger("Vote", (int)VoteAnimationStates.None);
		}
	}

	public void ActivateAskAction()
	{
		SpeechAnimator.SetInteger("State", AskingAnimationState);
	}

	public void ActivateLowWarning(AudioSource aS)
	{
		if (MonsterAnimator.GetInteger("State") != LowWarningAnimationState)
		{
			aS.PlayOneShot(LowWarningSound, audioVolume);
			MonsterAnimator.SetInteger("State", LowWarningAnimationState);
		}
	}

	public void ActivateHighWarning(AudioSource aS)
	{
		if (MonsterAnimator.GetInteger("State") != HighWarningAnimationState)
		{
			aS.PlayOneShot(HighWarningSound, audioVolume);
			MonsterAnimator.SetInteger("State", HighWarningAnimationState);
		}
	}

	public void ActivateVotedAction(AudioSource aS, float voteTime)
	{
		aS.PlayOneShot(VotingSound, audioVolume);
		MonsterAnimator.SetInteger("Vote", VotingAnimationState);
		elapsedAnimationTime = 0;

		print(HighWarningThreshold * Mathf.Pow(.95f, Value) + voteTime * 2.5f);
		// The amount added to the value is increase based on how close the 
		//  value is to 0. There is also a flat increase based on the vote time.
		Value += HighWarningThreshold * Mathf.Pow(.95f, Value) + voteTime * 2.5f;
	}
}

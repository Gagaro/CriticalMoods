/*
 * Created by SharpDevelop.
 * User: Gagaro
 * Date: 24/08/2016
 * Time: 20:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace FairerMood
{
	public class MentalState_AnimalKillingSpree : MentalState
	{
		public override bool ForceHostileTo(Thing t)
		{
			return t is Pawn && ((Pawn)t).RaceProps.Animal;
		}

		public override bool ForceHostileTo(Faction f)
		{
			return true;
		}

		public override RandomSocialMode SocialModeMax()
		{
			return RandomSocialMode.Off;
		}
	}
	
	public class MentalState_LookingForTrouble : MentalState
	{
		public override bool ForceHostileTo(Thing t)
		{
			return t is Pawn && this.pawn.relations.OpinionOf((Pawn) t) < 0;
		}

		public override bool ForceHostileTo(Faction f)
		{
			return true;
		}

		public override RandomSocialMode SocialModeMax()
		{
			return RandomSocialMode.Off;
		}
	}
	
	public class MentalState_BreakingSpree : MentalState
	{
		public override bool ForceHostileTo(Thing t)
		{
			return t is Building;
		}

		public override bool ForceHostileTo(Faction f)
		{
			return true;
		}

		public override RandomSocialMode SocialModeMax()
		{
			return RandomSocialMode.Off;
		}
	}
}
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
using UnityEngine;

namespace CriticalMoods
{
	public class MentalState_AnimalKillingSpree : MentalState
	{
		public override bool ForceHostileTo(Thing t)
		{
			return true;
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
			return true;
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
			return true;
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
	
	// JobGivers
	
	abstract public class JobGiver_AttackingThings : ThinkNode_JobGiver
	{
		protected const float MaxAttackDistance = 30f;

		private const float WaitChance = 0.5f;

		private const int WaitTicks = 90;

		private const int MinMeleeChaseTicks = 420;

		private const int MaxMeleeChaseTicks = 900;

		protected override Job TryGiveJob(Pawn pawn)
		{
			if (Rand.Value < WaitChance)
			{
				return new Job(JobDefOf.WaitCombat)
				{
					expiryInterval = 90
				};
			}
			Thing thing = this.FindTarget(pawn);
			
			if (thing != null)
			{
				return new Job(JobDefOf.AttackMelee, thing)
				{
					maxNumMeleeAttacks = 1,
					expiryInterval = Rand.Range(MinMeleeChaseTicks, MaxMeleeChaseTicks),
					canBash = true
				};
			}
			return null;
		}
		
		abstract protected Thing FindTarget(Pawn pawn);
	}
	
	public class JobGiver_AnimalKillingSpree : JobGiver_AttackingThings
	{
		protected override Thing FindTarget(Pawn pawn)
		{
			return AttackTargetFinder.BestAttackTarget(
				 pawn,
	             TargetScanFlags.NeedReachable | TargetScanFlags.NeedThreat,
	             (Thing x) => x is Pawn && ((Pawn)x).RaceProps.Animal,
	             0f,
	             MaxAttackDistance,
	             default(IntVec3),
	             3.40282347E+38f,
	             true);
		}
	}
	
	public class JobGiver_LookingForTrouble : JobGiver_AttackingThings
	{
		protected override Thing FindTarget(Pawn pawn)
		{
			return AttackTargetFinder.BestAttackTarget(
				 pawn,
	             TargetScanFlags.NeedReachable | TargetScanFlags.NeedThreat,
	             (Thing x) => x is Pawn && pawn.relations.OpinionOf((Pawn) x) < 0,
	             0f,
	             MaxAttackDistance,
	             default(IntVec3),
	             3.40282347E+38f,
	             true);
		}
	}
	
	public class JobGiver_BreakingSpree : JobGiver_AttackingThings
	{
		protected override Thing FindTarget(Pawn pawn)
		{
			Thing searcher = pawn;
			Pawn searcherPawn = pawn;
			const float maxDist = MaxAttackDistance;
			const bool canBash = true;
			Predicate<Thing> predicate = (Thing x) => x is Building;
			const int searchRegionsMax = 40;
			IntVec3 arg_25D_0 = pawn.Position;
			ThingRequest arg_25D_1 = ThingRequest.ForGroup(ThingRequestGroup.BuildingArtificial);
			PathEndMode arg_25D_2 = PathEndMode.Touch;
			
			Thing thing = GenClosest.ClosestThingReachable(arg_25D_0, arg_25D_1, arg_25D_2, TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, canBash), maxDist, predicate, null, searchRegionsMax, false);
			return thing;
		}
	}
}
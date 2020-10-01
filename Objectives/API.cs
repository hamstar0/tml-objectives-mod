using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using HamstarHelpers.Helpers.World;
using Objectives.Definitions;
using Objectives.Logic;


namespace Objectives {
	public class ObjectivesAPI {
		public static bool AreObjectivesLoadedForCurrentPlayer() {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModHelpersException( "Server has no player." );
			}

			if( Main.gameMenu ) {
				return false;
			}

			var myplayer = Main.LocalPlayer.GetModPlayer<ObjectivesPlayer>();
			return myplayer.AreObjectivesLoaded;
		}


		////////////////

		public static bool IsFinishedObjective( string title ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModHelpersException( "Server objectives not allowed." );
			}

			var myplayer = Main.LocalPlayer.GetModPlayer<ObjectivesPlayer>();

			return myplayer.CompletedObjectivesPerWorld.Contains2D(
				WorldHelpers.GetUniqueIdForCurrentWorld( true ),
				title
			);
		}


		////////////////

		public static Objective GetObjective( string title ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModHelpersException( "Server objectives not allowed." );
			}

			var mngr = ModContent.GetInstance<ObjectiveManager>();

			return mngr.Objectives.GetOrDefault( title );
		}


		public static IDictionary<int, Objective> GetObjectives( Func<Objective, int, bool> criteria ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModHelpersException( "Server objectives not allowed." );
			}

			var mngr = ModContent.GetInstance<ObjectiveManager>();
			var matches = new Dictionary<int, Objective>();

			foreach( Objective objective in mngr.Objectives.Values ) {
				int idx = mngr.ObjectiveOrder.IndexOf( objective.Title );

				if( criteria(objective, idx) ) {
					matches[idx] = objective;
				}
			}

			return matches;
		}


		////////////////

		public static bool AddObjective( Objective objective, int order, bool alertPlayer, out string result ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModHelpersException( "Server objectives not allowed." );
			}

			var mngr = ModContent.GetInstance<ObjectiveManager>();

			return mngr.AddObjective( objective, order, alertPlayer, out result );
		}

		////

		public static void RemoveObjective( string title ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModHelpersException( "Server objectives not allowed." );
			}

			var mngr = ModContent.GetInstance<ObjectiveManager>();

			mngr.RemoveObjective( title );
		}

		////

		public static void ClearObjectives() {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModHelpersException( "Server objectives not allowed." );
			}

			var mngr = ModContent.GetInstance<ObjectiveManager>();

			mngr.ClearObjectives();
		}
	}
}

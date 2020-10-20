using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.PlayerData;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
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

			var myplayer = CustomPlayerData.GetPlayerData<ObjectivesCustomPlayer>( Main.myPlayer );
			return myplayer != null;
		}


		////////////////

		/// <summary>
		/// Indicates if an objective is complete. Objective does not need to have been declared for player to remember
		/// having finished it previously.
		/// </summary>
		/// <param name="title"></param>
		/// <returns></returns>
		public static bool IsFinishedObjective( string title ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModHelpersException( "Server objectives not allowed." );
			}

			var myplayer = CustomPlayerData.GetPlayerData<ObjectivesCustomPlayer>( Main.myPlayer );
			return myplayer.IsObjectiveComplete( title );
		}


		////////////////

		public static Objective GetObjective( string title ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModHelpersException( "Server objectives not allowed." );
			}

			var mngr = ModContent.GetInstance<ObjectiveManager>();

			return mngr.CurrentObjectives.GetOrDefault( title );
		}


		public static IDictionary<int, Objective> GetObjectives( Func<Objective, int, bool> criteria ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModHelpersException( "Server objectives not allowed." );
			}

			var mngr = ModContent.GetInstance<ObjectiveManager>();
			var matches = new Dictionary<int, Objective>();

			foreach( Objective objective in mngr.CurrentObjectives.Values ) {
				int idx = mngr.CurrentObjectiveOrder.IndexOf( objective.Title );

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

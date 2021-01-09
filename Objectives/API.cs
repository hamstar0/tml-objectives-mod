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
	public partial class ObjectivesAPI {
		/// <summary>
		/// Indicates the current, local player has their objectives loaded.
		/// </summary>
		/// <returns></returns>
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

		/// <summary></summary>
		/// <param name="title"></param>
		/// <returns></returns>
		public static Objective GetObjective( string title ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModHelpersException( "Server objectives not allowed." );
			}

			var mngr = ModContent.GetInstance<ObjectiveManager>();

			return mngr.CurrentObjectives.GetOrDefault( title );
		}


		/// <summary>
		/// Gets a set of objectives according to a given criteria.
		/// </summary>
		/// <param name="criteria">Accepts an objective to test and its order index as parameters. Returns `true` if valid.</param>
		/// <returns>Objectives mapped to their internal order indices.</returns>
		public static IDictionary<int, Objective> GetObjectives( Func<Objective, int, bool> criteria ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModHelpersException( "Server objectives not allowed." );
			}

			var mngr = ModContent.GetInstance<ObjectiveManager>();
			var matches = new Dictionary<int, Objective>();

			foreach( Objective objective in mngr.CurrentObjectives.Values ) {
				int idx = mngr.CurrentObjectiveOrder.IndexOf( objective.Title );

				if( criteria( objective, idx ) ) {
					matches[ idx ] = objective;
				}
			}

			return matches;
		}


		////////////////

		/// <summary></summary>
		/// <param name="objective"></param>
		/// <param name="order">Priority of objective.</param>
		/// <param name="alertPlayer">Creates an inbox message.</param>
		/// <param name="result">Output message to indicate error type, or else `Success.`</param>
		/// <returns>`true` if objective isn't already defined and is being given a valid order index.</returns>
		public static bool AddObjective( Objective objective, int order, bool alertPlayer, out string result ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModHelpersException( "Server objectives not allowed." );
			}

			var mngr = ModContent.GetInstance<ObjectiveManager>();

			return mngr.AddObjective( objective, order, alertPlayer, out result );
		}

		////

		/// <summary></summary>
		/// <param name="title"></param>
		public static void RemoveObjective( string title ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModHelpersException( "Server objectives not allowed." );
			}

			var mngr = ModContent.GetInstance<ObjectiveManager>();

			mngr.RemoveObjective( title );
		}

		////

		/// <summary></summary>
		public static void ClearObjectives() {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModHelpersException( "Server objectives not allowed." );
			}

			var mngr = ModContent.GetInstance<ObjectiveManager>();

			mngr.ClearObjectives();
		}
	}
}

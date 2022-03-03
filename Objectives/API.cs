using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Classes.PlayerData;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using Objectives.Definitions;
using Objectives.Logic;


namespace Objectives {
	public partial class ObjectivesAPI {
		/// <summary>
		/// Indicates if the current, local player has their objectives loaded.
		/// </summary>
		/// <returns></returns>
		public static bool AreObjectivesLoadedForCurrentPlayer() {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server has no player." );
			}

			if( Main.gameMenu ) {
				return false;
			}

			var myplayer = CustomPlayerData.GetPlayerData<ObjectivesCustomPlayer>( Main.myPlayer );
			return myplayer != null;
		}


		////////////////

		/// <summary>
		/// Indicates if an objective is complete. Objective does not need to be currently declared for player to remember
		/// having finished it previously.
		/// </summary>
		/// <param name="title"></param>
		/// <returns></returns>
		public static bool HasRecordedObjectiveByNameAsFinished( string title ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server objectives not allowed." );
			}

			var myplayer = CustomPlayerData.GetPlayerData<ObjectivesCustomPlayer>( Main.myPlayer );
			return myplayer.IsObjectiveByNameComplete( title );
		}


		////////////////

		/// <summary></summary>
		/// <param name="title"></param>
		/// <returns></returns>
		public static Objective GetObjective( string title ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server objectives not allowed." );
			}

			var mngr = ModContent.GetInstance<ObjectiveManager>();

			return mngr.CurrentObjectives.GetOrDefault( title );
		}


		/// <summary>
		/// Gets a set of objectives according to a given criteria.
		/// </summary>
		/// <param name="criteria">Accepts an objective and its order index as parameters. Returns `true` if valid.</param>
		/// <returns>Objectives mapped to their internal order indices.</returns>
		public static IDictionary<int, Objective> GetObjectives( Func<Objective, int, bool> criteria ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server objectives not allowed." );
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

		/// <summary>Adds an objective to the list.</summary>
		/// <param name="objective"></param>
		/// <param name="order">Priority of objective.</param>
		/// <param name="alertPlayer">Creates an inbox message. Only alerts players for non-completed objectives.</param>
		/// <param name="result">Output message to indicate error type, or else `Success.`</param>
		/// <returns>`true` if objective isn't already defined and is being given a valid order index.</returns>
		public static bool AddObjective( Objective objective, int order, bool alertPlayer, out string result ) {
			if( objective == null ) {
				throw new ModLibsException( "Non-null objectives required." );
			}

			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server objectives not allowed." );
			}

			var mngr = ModContent.GetInstance<ObjectiveManager>();

			return mngr.AddObjective( objective, order, alertPlayer, out result );
		}

		////

		[Obsolete( "use RemoveObjectiveIf", true )]
		public static void RemoveObjective( string title, bool forceIncomplete ) {
			ObjectivesAPI.RemoveObjectiveIf( title, forceIncomplete );
		}

		/// <summary></summary>
		/// <param name="title"></param>
		/// <param name="forceIncomplete"></param>
		/// <returns></returns>
		public static bool RemoveObjectiveIf( string title, bool forceIncomplete ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server objectives not allowed." );
			}

			var mngr = ModContent.GetInstance<ObjectiveManager>();

			return mngr.RemoveObjectiveIf( title, forceIncomplete );
		}

		////

		/// <summary></summary>
		/// <param name="forceIncomplete"></param>
		public static void ClearObjectives( bool forceIncomplete ) {
			if( Main.netMode == NetmodeID.Server ) {
				throw new ModLibsException( "Server objectives not allowed." );
			}

			var mngr = ModContent.GetInstance<ObjectiveManager>();

			mngr.ClearObjectives( forceIncomplete );
		}
	}
}

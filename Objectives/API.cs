using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using Objectives.Definitions;
using Objectives.Logic;


namespace Objectives {
	public class ObjectivesAPI {
		public static Objective GetObjective( string title ) {
			var mngr = ModContent.GetInstance<ObjectiveManager>();

			return mngr.Objectives.GetOrDefault( title );
		}


		public static IDictionary<int, Objective> GetObjectives( Func<Objective, int, bool> criteria ) {
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
			var mngr = ModContent.GetInstance<ObjectiveManager>();

			return mngr.AddObjective( objective, order, alertPlayer, out result );
		}

		////

		public static void RemoveObjective( string title ) {
			var mngr = ModContent.GetInstance<ObjectiveManager>();

			mngr.RemoveObjective( title );
		}
	}
}

using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using Objectives.Logic;


namespace Objectives {
	public partial class ObjectivesAPI {
		public delegate void SubscriptionEvent( string objectiveName, bool isAdded, bool isFinished );



		////////////////

		public static void AddSubscription( string name, SubscriptionEvent subscriber ) {
			var mngr = ModContent.GetInstance<ObjectiveManager>();

			mngr.Subscribers[ name ] = subscriber;
		}
	}
}

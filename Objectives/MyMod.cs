using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsUI.Classes.UI.Theme;
using ModUtilityPanels;
using ModUtilityPanels.Services.UI.UtilityPanels;
using Objectives.UI;
using Objectives.Logic;


namespace Objectives {
	public class ObjectivesMod : Mod {
		public const string UtilityPanelsName = "Objectives";


		////

		public static string GithubUserName => "hamstar0";

		public static string GithubProjectName => "tml-objectives-mod";


		////////////////

		public static ObjectivesMod Instance { get; private set; }



		////////////////

		public ModHotKey UtilityPanelsHotkey { get; private set; }


		////////////////

		internal UIObjectivesTab ObjectivesTabUI;



		////////////////

		public ObjectivesMod() {
			ObjectivesMod.Instance = this;
		}

		////////////////
		
		public override void Load() {
			this.UtilityPanelsHotkey = this.RegisterHotKey( "Toggle Objectives", "OemTilde" );
		}

		public override void PostSetupContent() {
			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				// Add player stats tab
				ModUtilityPanelsMod.Instance.OnUtilityPanelsInitialize += () => {
					this.ObjectivesTabUI = new UIObjectivesTab( UITheme.Vanilla );

					UtilityPanelsTabs.AddTab( ObjectivesMod.UtilityPanelsName, this.ObjectivesTabUI );
				};
			}
		}

		public override void Unload() {
			ObjectivesMod.Instance = null;
		}


		////////////////

		public override void PostUpdateEverything() {
			ModContent.GetInstance<ObjectiveManager>()?.Update_Internal();
		}
	}
}
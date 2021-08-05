using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsUI.Classes.UI.Theme;
using ModControlPanel;
using ModControlPanel.Services.UI.ControlPanel;
using Objectives.UI;
using Objectives.Logic;


namespace Objectives {
	public class ObjectivesMod : Mod {
		public const string ControlPanelName = "Objectives";


		////

		public static string GithubUserName => "hamstar0";

		public static string GithubProjectName => "tml-objectives-mod";


		////////////////

		public static ObjectivesMod Instance { get; private set; }



		////////////////

		public ModHotKey ControlPanelHotkey { get; private set; }


		////////////////

		internal UIObjectivesTab ObjectivesTabUI;



		////////////////

		public ObjectivesMod() {
			ObjectivesMod.Instance = this;
		}

		////////////////
		
		public override void Load() {
			this.ControlPanelHotkey = this.RegisterHotKey( "Toggle Objectives", "OemTilde" );
		}

		public override void PostSetupContent() {
			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				// Add player stats tab
				ModControlPanelMod.Instance.OnControlPanelInitialize += () => {
					this.ObjectivesTabUI = new UIObjectivesTab( UITheme.Vanilla );

					ControlPanelTabs.AddTab( ObjectivesMod.ControlPanelName, this.ObjectivesTabUI );
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
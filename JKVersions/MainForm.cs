using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace JkVersions {
	public partial class MainForm : Form {
		public MainForm() {
			this.InitializeComponent();
			this.PopulateDefaultPaths();
			this.UpdateState();
		}

		private void UpdateState() {
			bool jk10Enabled = this.jk10Enabled.Checked;
			this.jk10Label.Enabled = jk10Enabled;
			this.jk10Path.Enabled = jk10Enabled;
			this.jk10Browse.Enabled = jk10Enabled;
			this.jkUnofficialPatchEnabled.Enabled = jk10Enabled;

			bool jkUnofficialPatchEnabled = jk10Enabled && this.jkUnofficialPatchEnabled.Checked;
			this.jkUnofficialPatchLabel.Enabled = jkUnofficialPatchEnabled;
			this.jkUnofficialPatchPath.Enabled = jkUnofficialPatchEnabled;
			this.jkUnofficialPatchBrowse.Enabled = jkUnofficialPatchEnabled;

			bool jkGameFolder = this.jkGameFolderOutput.Checked;
			this.jkGameFolderPath.Enabled = jkGameFolder;
			this.jkGameFolderBrowse.Enabled = jkGameFolder;

			bool otherFolder = this.otherFolderOutput.Checked;
			this.otherFolderPath.Enabled = otherFolder;
			this.otherFolderBrowse.Enabled = otherFolder;

			this.ValidatePaths();
		}

		private void PopulateDefaultPaths() {
			JkFileSettings settings = Program.Settings.DefaultLocations;
			this.jk101Path.Text = settings.Jk1_01;
			this.jk10Path.Text = settings.Jk1_0;
			this.jkUnofficialPatchPath.Text = settings.JkUnofficialPatch;

			string jkpath = Program.JkPath;
			if (!string.IsNullOrEmpty(jkpath)) {
				this.jkGameFolderPath.Text = jkpath;
			} else {
				this.jkGameFolderOutput.Checked = false;
				this.otherFolderOutput.Checked = true;
			}

			this.otherFolderPath.Text = Program.TempFolder;
		}

		private bool ValidatePath(string path) {
			if (string.IsNullOrEmpty(path)) {
				return false;
			}
			bool isUri = Uri.IsWellFormedUriString(path, UriKind.Absolute);
			return isUri || File.Exists(path);
		}

		private void ValidatePaths() {
			bool valid = true;
			if (!this.ValidatePath(this.jk101Path.Text)) {
				valid = false;
			} else {
				bool jk10Enabled = this.jk10Enabled.Checked;
				if (jk10Enabled) {
					if (!this.ValidatePath(this.jk10Path.Text)) {
						valid = false;
					} else {
						bool jkUnofficialPatchEnabled = this.jkUnofficialPatchEnabled.Checked;
						if (jkUnofficialPatchEnabled) {
							if (!this.ValidatePath(this.jkUnofficialPatchPath.Text)) {
								valid = false;
							}
						}
					}
				}

				bool jkGameFolder = this.jkGameFolderOutput.Checked;
				if (jkGameFolder) {
					if (!File.Exists($@"{this.jkGameFolderPath.Text}\JediKnight.exe")) {
						valid = false;
					}
				}
			}
			this.mainButton.Enabled = valid;
		}

		private void MainForm_HelpButtonClicked(object sender, CancelEventArgs e) {
			MessageBox.Show($"This application will reproduce several different versions of the Jedi Knight game " +
				$"executable using publicly available files. These versions are:{Environment.NewLine}{Environment.NewLine}" +
				$"\t- Jedi Knight v1.00{Environment.NewLine}" +
				$"\t- Jedi Knight v1.01{Environment.NewLine}" +
				$"\t- Jedi Knight Unofficial Patch v2008-01-16{Environment.NewLine}{Environment.NewLine}" +
				$"Files are provided by JKHub.net. Thanks to them for hosting files for the JK community.{Environment.NewLine}{Environment.NewLine}" +
				$"Special thanks to Nikumubeki for always telling me when JKVersions was broke.{Environment.NewLine}{Environment.NewLine}" +
				$"Special thanks to Vertikai for telling me more recently when JKVersions was broke.{Environment.NewLine}{Environment.NewLine}" +
				$"You must be connected to the internet, or already have the necessary files available offline, for this tool to work.",
				"JKVersions", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
			e.Cancel = true;
		}

		private void Jk101Path_TextChanged(object sender, EventArgs e) {
			this.ValidatePaths();
		}

		private void Jk10Path_TextChanged(object sender, EventArgs e) {
			this.ValidatePaths();
		}

		private void JkUnofficialPatchPath_TextChanged(object sender, EventArgs e) {
			this.ValidatePaths();
		}

		private void JkGameFolderPath_TextChanged(object sender, EventArgs e) {
			this.ValidatePaths();
		}

		private void OtherFolderPath_TextChanged(object sender, EventArgs e) {
			this.ValidatePaths();
		}

		private string FileBrowse(string[] files, string path) {
			bool isUrl = !string.IsNullOrEmpty(path) && Uri.IsWellFormedUriString(path, UriKind.Absolute);
			using (OpenFileDialog dialog = new OpenFileDialog() {
				AddExtension = false,
				AutoUpgradeEnabled = true,
				CheckFileExists = true,
				CheckPathExists = true,
				FileName = isUrl ? "" : path,
				Filter = $"Known Names|{string.Join(";", files)}|All Files (*.*)|*.*",
				FilterIndex = 0,
				InitialDirectory = isUrl ? null : Path.GetDirectoryName(path),
				Multiselect = false,
				RestoreDirectory = true,
				ShowHelp = false,
				ShowReadOnly = false,
				SupportMultiDottedExtensions = true,
				Title = $"Select File",
				ValidateNames = true
			}) {
				if (dialog.ShowDialog() != DialogResult.OK) {
					return null;
				}

				return dialog.FileName;
			}
		}

		private void Jk101Browse_Click(object sender, EventArgs e) {
			string path = this.FileBrowse(new[] { "jkupd101.exe" }, this.jk101Path.Text);
			if (!string.IsNullOrEmpty(path)) {
				this.jk101Path.Text = path;
			}
		}

		private void Jk10Enabled_CheckedChanged(object sender, EventArgs e) {
			this.UpdateState();
		}

		private void Jk10Browse_Click(object sender, EventArgs e) {
			string path = this.FileBrowse(new[] { "patch_1.01_to_1.0.zip" }, this.jk10Path.Text);
			if (!string.IsNullOrEmpty(path)) {
				this.jk10Path.Text = path;
			}
		}

		private void JkUnofficialPatchEnabled_CheckedChanged(object sender, EventArgs e) {
			this.UpdateState();
		}

		private void JkUnofficialPatchBrowse_Click(object sender, EventArgs e) {
			string path = this.FileBrowse(new[] { "JKUnofficialPatch_2008-01-16.7z", "JKUnofficialPatch_2008-01-16.zip" }, this.jkUnofficialPatchPath.Text);
			if (!string.IsNullOrEmpty(path)) {
				this.jkUnofficialPatchPath.Text = path;
			}
		}

		private void JkGameFolderOutput_CheckedChanged(object sender, EventArgs e) {
			this.UpdateState();
		}

		private void JkGameFolderBrowse_Click(object sender, EventArgs e) {
			string path = this.FileBrowse(new[] { "JediKnight.exe" }, !string.IsNullOrEmpty(this.jkUnofficialPatchPath.Text) ?
				$@"{this.jkUnofficialPatchPath.Text}\JediKnight.exe" : "");
			if (!string.IsNullOrEmpty(path)) {
				this.jkUnofficialPatchPath.Text = Path.GetDirectoryName(path);
			}
		}

		private void OtherFolderOutput_CheckedChanged(object sender, EventArgs e) {
			this.UpdateState();
		}

		private void OtherFolderBrowse_Click(object sender, EventArgs e) {
			string path = this.otherFolderPath.Text;
			using (FolderBrowserDialog dialog = new FolderBrowserDialog() {
				Description = "Select an output folder",
				SelectedPath = path,
				ShowNewFolderButton = true
			}) {
				if (dialog.ShowDialog() != DialogResult.OK) {
					return;
				}

				this.otherFolderPath.Text = dialog.SelectedPath;
			}
		}

		public string Jk101Path => this.jk101Path.Text;
		public string OutputFolder {
			get {
				if (this.otherFolderOutput.Checked) {
					return this.otherFolderPath.Text;
				} else {
					return $@"{this.jkGameFolderPath.Text}\patches";
				}
			}
		}
		public bool VerifyWithHashes => this.hashValidate.Checked;
		public string Jk10Path => this.jk10Enabled.Checked ? this.jk10Path.Text : null;
		public string JkUnofficialPatchPath => this.jk10Enabled.Checked && this.jkUnofficialPatchEnabled.Checked ? this.jkUnofficialPatchPath.Text : null;
		public bool OutputIsJkGameFolder => !this.otherFolderOutput.Checked;
	}
}

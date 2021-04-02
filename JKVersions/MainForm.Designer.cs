
namespace JkVersions {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Windows.Forms.GroupBox inputFiles;
			System.Windows.Forms.Label jk191label;
			System.Windows.Forms.GroupBox outputPath;
			this.jkUnofficialPatchBrowse = new System.Windows.Forms.Button();
			this.jkUnofficialPatchPath = new System.Windows.Forms.TextBox();
			this.jkUnofficialPatchLabel = new System.Windows.Forms.Label();
			this.jkUnofficialPatchEnabled = new System.Windows.Forms.CheckBox();
			this.jk10Browse = new System.Windows.Forms.Button();
			this.jk10Path = new System.Windows.Forms.TextBox();
			this.jk10Label = new System.Windows.Forms.Label();
			this.jk10Enabled = new System.Windows.Forms.CheckBox();
			this.jk101Browse = new System.Windows.Forms.Button();
			this.jk101Path = new System.Windows.Forms.TextBox();
			this.hashValidate = new System.Windows.Forms.CheckBox();
			this.otherFolderBrowse = new System.Windows.Forms.Button();
			this.otherFolderPath = new System.Windows.Forms.TextBox();
			this.otherFolderOutput = new System.Windows.Forms.RadioButton();
			this.jkGameFolderBrowse = new System.Windows.Forms.Button();
			this.jkGameFolderPath = new System.Windows.Forms.TextBox();
			this.jkGameFolderOutput = new System.Windows.Forms.RadioButton();
			this.mainButton = new System.Windows.Forms.Button();
			inputFiles = new System.Windows.Forms.GroupBox();
			jk191label = new System.Windows.Forms.Label();
			outputPath = new System.Windows.Forms.GroupBox();
			inputFiles.SuspendLayout();
			outputPath.SuspendLayout();
			this.SuspendLayout();
			// 
			// inputFiles
			// 
			inputFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			inputFiles.Controls.Add(this.jkUnofficialPatchBrowse);
			inputFiles.Controls.Add(this.jkUnofficialPatchPath);
			inputFiles.Controls.Add(this.jkUnofficialPatchLabel);
			inputFiles.Controls.Add(this.jkUnofficialPatchEnabled);
			inputFiles.Controls.Add(this.jk10Browse);
			inputFiles.Controls.Add(this.jk10Path);
			inputFiles.Controls.Add(this.jk10Label);
			inputFiles.Controls.Add(this.jk10Enabled);
			inputFiles.Controls.Add(this.jk101Browse);
			inputFiles.Controls.Add(this.jk101Path);
			inputFiles.Controls.Add(jk191label);
			inputFiles.Location = new System.Drawing.Point(12, 12);
			inputFiles.Name = "inputFiles";
			inputFiles.Size = new System.Drawing.Size(662, 151);
			inputFiles.TabIndex = 0;
			inputFiles.TabStop = false;
			inputFiles.Text = "Input Files (Web URI or local path)";
			// 
			// jkUnofficialPatchBrowse
			// 
			this.jkUnofficialPatchBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.jkUnofficialPatchBrowse.Location = new System.Drawing.Point(581, 117);
			this.jkUnofficialPatchBrowse.Name = "jkUnofficialPatchBrowse";
			this.jkUnofficialPatchBrowse.Size = new System.Drawing.Size(75, 23);
			this.jkUnofficialPatchBrowse.TabIndex = 10;
			this.jkUnofficialPatchBrowse.Text = "Browse...";
			this.jkUnofficialPatchBrowse.UseVisualStyleBackColor = true;
			this.jkUnofficialPatchBrowse.Click += new System.EventHandler(this.JkUnofficialPatchBrowse_Click);
			// 
			// jkUnofficialPatchPath
			// 
			this.jkUnofficialPatchPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.jkUnofficialPatchPath.Location = new System.Drawing.Point(179, 119);
			this.jkUnofficialPatchPath.Name = "jkUnofficialPatchPath";
			this.jkUnofficialPatchPath.Size = new System.Drawing.Size(396, 20);
			this.jkUnofficialPatchPath.TabIndex = 9;
			this.jkUnofficialPatchPath.TextChanged += new System.EventHandler(this.JkUnofficialPatchPath_TextChanged);
			// 
			// jkUnofficialPatchLabel
			// 
			this.jkUnofficialPatchLabel.AutoSize = true;
			this.jkUnofficialPatchLabel.Location = new System.Drawing.Point(6, 122);
			this.jkUnofficialPatchLabel.Name = "jkUnofficialPatchLabel";
			this.jkUnofficialPatchLabel.Size = new System.Drawing.Size(167, 13);
			this.jkUnofficialPatchLabel.TabIndex = 8;
			this.jkUnofficialPatchLabel.Text = "JKUnofficialPatch_2008-01-16.zip";
			// 
			// jkUnofficialPatchEnabled
			// 
			this.jkUnofficialPatchEnabled.AutoSize = true;
			this.jkUnofficialPatchEnabled.Checked = true;
			this.jkUnofficialPatchEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
			this.jkUnofficialPatchEnabled.Location = new System.Drawing.Point(6, 96);
			this.jkUnofficialPatchEnabled.Name = "jkUnofficialPatchEnabled";
			this.jkUnofficialPatchEnabled.Size = new System.Drawing.Size(219, 17);
			this.jkUnofficialPatchEnabled.TabIndex = 7;
			this.jkUnofficialPatchEnabled.Text = "Generate JK Unofficial Patch Executable";
			this.jkUnofficialPatchEnabled.UseVisualStyleBackColor = true;
			this.jkUnofficialPatchEnabled.CheckedChanged += new System.EventHandler(this.JkUnofficialPatchEnabled_CheckedChanged);
			// 
			// jk10Browse
			// 
			this.jk10Browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.jk10Browse.Location = new System.Drawing.Point(581, 68);
			this.jk10Browse.Name = "jk10Browse";
			this.jk10Browse.Size = new System.Drawing.Size(75, 23);
			this.jk10Browse.TabIndex = 6;
			this.jk10Browse.Text = "Browse...";
			this.jk10Browse.UseVisualStyleBackColor = true;
			this.jk10Browse.Click += new System.EventHandler(this.Jk10Browse_Click);
			// 
			// jk10Path
			// 
			this.jk10Path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.jk10Path.Location = new System.Drawing.Point(179, 70);
			this.jk10Path.Name = "jk10Path";
			this.jk10Path.Size = new System.Drawing.Size(396, 20);
			this.jk10Path.TabIndex = 5;
			this.jk10Path.TextChanged += new System.EventHandler(this.Jk10Path_TextChanged);
			// 
			// jk10Label
			// 
			this.jk10Label.AutoSize = true;
			this.jk10Label.Location = new System.Drawing.Point(6, 73);
			this.jk10Label.Name = "jk10Label";
			this.jk10Label.Size = new System.Drawing.Size(113, 13);
			this.jk10Label.TabIndex = 4;
			this.jk10Label.Text = "patch_1.01_to_1.0.zip";
			// 
			// jk10Enabled
			// 
			this.jk10Enabled.AutoSize = true;
			this.jk10Enabled.Checked = true;
			this.jk10Enabled.CheckState = System.Windows.Forms.CheckState.Checked;
			this.jk10Enabled.Location = new System.Drawing.Point(6, 47);
			this.jk10Enabled.Name = "jk10Enabled";
			this.jk10Enabled.Size = new System.Drawing.Size(144, 17);
			this.jk10Enabled.TabIndex = 3;
			this.jk10Enabled.Text = "Generate 1.0 Executable";
			this.jk10Enabled.UseVisualStyleBackColor = true;
			this.jk10Enabled.CheckedChanged += new System.EventHandler(this.Jk10Enabled_CheckedChanged);
			// 
			// jk101Browse
			// 
			this.jk101Browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.jk101Browse.Location = new System.Drawing.Point(581, 19);
			this.jk101Browse.Name = "jk101Browse";
			this.jk101Browse.Size = new System.Drawing.Size(75, 23);
			this.jk101Browse.TabIndex = 2;
			this.jk101Browse.Text = "Browse...";
			this.jk101Browse.UseVisualStyleBackColor = true;
			this.jk101Browse.Click += new System.EventHandler(this.Jk101Browse_Click);
			// 
			// jk101Path
			// 
			this.jk101Path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.jk101Path.Location = new System.Drawing.Point(179, 21);
			this.jk101Path.Name = "jk101Path";
			this.jk101Path.Size = new System.Drawing.Size(396, 20);
			this.jk101Path.TabIndex = 1;
			this.jk101Path.TextChanged += new System.EventHandler(this.Jk101Path_TextChanged);
			// 
			// jk191label
			// 
			jk191label.AutoSize = true;
			jk191label.Location = new System.Drawing.Point(6, 24);
			jk191label.Name = "jk191label";
			jk191label.Size = new System.Drawing.Size(71, 13);
			jk191label.TabIndex = 0;
			jk191label.Text = "jkupd101.exe";
			// 
			// outputPath
			// 
			outputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			outputPath.Controls.Add(this.hashValidate);
			outputPath.Controls.Add(this.otherFolderBrowse);
			outputPath.Controls.Add(this.otherFolderPath);
			outputPath.Controls.Add(this.otherFolderOutput);
			outputPath.Controls.Add(this.jkGameFolderBrowse);
			outputPath.Controls.Add(this.jkGameFolderPath);
			outputPath.Controls.Add(this.jkGameFolderOutput);
			outputPath.Location = new System.Drawing.Point(12, 169);
			outputPath.Name = "outputPath";
			outputPath.Size = new System.Drawing.Size(662, 101);
			outputPath.TabIndex = 1;
			outputPath.TabStop = false;
			outputPath.Text = "Output Path";
			// 
			// hashValidate
			// 
			this.hashValidate.AutoSize = true;
			this.hashValidate.Checked = true;
			this.hashValidate.CheckState = System.Windows.Forms.CheckState.Checked;
			this.hashValidate.Location = new System.Drawing.Point(6, 76);
			this.hashValidate.Name = "hashValidate";
			this.hashValidate.Size = new System.Drawing.Size(124, 17);
			this.hashValidate.TabIndex = 11;
			this.hashValidate.Text = "Validate created files";
			this.hashValidate.UseVisualStyleBackColor = true;
			// 
			// otherFolderBrowse
			// 
			this.otherFolderBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.otherFolderBrowse.Location = new System.Drawing.Point(581, 48);
			this.otherFolderBrowse.Name = "otherFolderBrowse";
			this.otherFolderBrowse.Size = new System.Drawing.Size(75, 23);
			this.otherFolderBrowse.TabIndex = 15;
			this.otherFolderBrowse.Text = "Browse...";
			this.otherFolderBrowse.UseVisualStyleBackColor = true;
			this.otherFolderBrowse.Click += new System.EventHandler(this.OtherFolderBrowse_Click);
			// 
			// otherFolderPath
			// 
			this.otherFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.otherFolderPath.Location = new System.Drawing.Point(179, 50);
			this.otherFolderPath.Name = "otherFolderPath";
			this.otherFolderPath.Size = new System.Drawing.Size(396, 20);
			this.otherFolderPath.TabIndex = 14;
			this.otherFolderPath.TextChanged += new System.EventHandler(this.OtherFolderPath_TextChanged);
			// 
			// otherFolderOutput
			// 
			this.otherFolderOutput.AutoSize = true;
			this.otherFolderOutput.Location = new System.Drawing.Point(6, 51);
			this.otherFolderOutput.Name = "otherFolderOutput";
			this.otherFolderOutput.Size = new System.Drawing.Size(80, 17);
			this.otherFolderOutput.TabIndex = 13;
			this.otherFolderOutput.Text = "Other folder";
			this.otherFolderOutput.UseVisualStyleBackColor = true;
			this.otherFolderOutput.CheckedChanged += new System.EventHandler(this.OtherFolderOutput_CheckedChanged);
			// 
			// jkGameFolderBrowse
			// 
			this.jkGameFolderBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.jkGameFolderBrowse.Location = new System.Drawing.Point(581, 19);
			this.jkGameFolderBrowse.Name = "jkGameFolderBrowse";
			this.jkGameFolderBrowse.Size = new System.Drawing.Size(75, 23);
			this.jkGameFolderBrowse.TabIndex = 12;
			this.jkGameFolderBrowse.Text = "Browse...";
			this.jkGameFolderBrowse.UseVisualStyleBackColor = true;
			this.jkGameFolderBrowse.Click += new System.EventHandler(this.JkGameFolderBrowse_Click);
			// 
			// jkGameFolderPath
			// 
			this.jkGameFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.jkGameFolderPath.Location = new System.Drawing.Point(179, 21);
			this.jkGameFolderPath.Name = "jkGameFolderPath";
			this.jkGameFolderPath.Size = new System.Drawing.Size(396, 20);
			this.jkGameFolderPath.TabIndex = 12;
			this.jkGameFolderPath.TextChanged += new System.EventHandler(this.JkGameFolderPath_TextChanged);
			// 
			// jkGameFolderOutput
			// 
			this.jkGameFolderOutput.AutoSize = true;
			this.jkGameFolderOutput.Checked = true;
			this.jkGameFolderOutput.Location = new System.Drawing.Point(6, 22);
			this.jkGameFolderOutput.Name = "jkGameFolderOutput";
			this.jkGameFolderOutput.Size = new System.Drawing.Size(135, 17);
			this.jkGameFolderOutput.TabIndex = 0;
			this.jkGameFolderOutput.TabStop = true;
			this.jkGameFolderOutput.Text = "Jedi Knight game folder";
			this.jkGameFolderOutput.UseVisualStyleBackColor = true;
			this.jkGameFolderOutput.CheckedChanged += new System.EventHandler(this.JkGameFolderOutput_CheckedChanged);
			// 
			// mainButton
			// 
			this.mainButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.mainButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.mainButton.Location = new System.Drawing.Point(599, 282);
			this.mainButton.Name = "mainButton";
			this.mainButton.Size = new System.Drawing.Size(75, 23);
			this.mainButton.TabIndex = 7;
			this.mainButton.Text = "Start";
			this.mainButton.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(686, 317);
			this.Controls.Add(this.mainButton);
			this.Controls.Add(outputPath);
			this.Controls.Add(inputFiles);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.Text = "JKVersions";
			this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.MainForm_HelpButtonClicked);
			inputFiles.ResumeLayout(false);
			inputFiles.PerformLayout();
			outputPath.ResumeLayout(false);
			outputPath.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox jk101Path;
		private System.Windows.Forms.Button jk101Browse;
		private System.Windows.Forms.CheckBox jk10Enabled;
		private System.Windows.Forms.Button jk10Browse;
		private System.Windows.Forms.TextBox jk10Path;
		private System.Windows.Forms.Label jk10Label;
		private System.Windows.Forms.Button jkUnofficialPatchBrowse;
		private System.Windows.Forms.TextBox jkUnofficialPatchPath;
		private System.Windows.Forms.Label jkUnofficialPatchLabel;
		private System.Windows.Forms.CheckBox jkUnofficialPatchEnabled;
		private System.Windows.Forms.CheckBox hashValidate;
		private System.Windows.Forms.Button jkGameFolderBrowse;
		private System.Windows.Forms.TextBox jkGameFolderPath;
		private System.Windows.Forms.RadioButton jkGameFolderOutput;
		private System.Windows.Forms.RadioButton otherFolderOutput;
		private System.Windows.Forms.TextBox otherFolderPath;
		private System.Windows.Forms.Button otherFolderBrowse;
		private System.Windows.Forms.Button mainButton;
	}
}
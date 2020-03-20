using System;
using System.Windows.Forms;

namespace JKVersions {
	public partial class ProgressForm : Form {
		private const int PROGRESS_GRANULARITY = 100000;

		public ProgressForm(int progressItemCount) {
			this.InitializeComponent();

			this.progressItemCount = progressItemCount;
			this.ProgressBar.Maximum = progressItemCount * PROGRESS_GRANULARITY;
		}

		private readonly int progressItemCount;
		private int currentItem = 0;
		private double currentItemProgress = 0;

		public void CompleteItem() {
			this.currentItem++;
			this.currentItemProgress = 0;
			this.UpdateProgress();
		}

		public void SetProgressText(string labelText) => this.StatusLabel.Text = labelText;

		public void SetProgress(double amount) {
			this.currentItemProgress = amount;
			this.UpdateProgress();
		}

		private void UpdateProgress() => this.ProgressBar.Value = (int)((this.currentItem + this.currentItemProgress) * PROGRESS_GRANULARITY);

		public void DisableCancel() => this.Cancel.Enabled = false;

		public void Abort() {
			this.DisableCancel();
			this.SetProgressText("Aborting...");
			this.Cancelled?.Invoke(this, new EventArgs());
		}

		public event EventHandler Cancelled;

		protected override void OnFormClosing(FormClosingEventArgs e) {
			if (e.CloseReason == CloseReason.UserClosing) {
				e.Cancel = true;
				this.Abort();
			}

			base.OnFormClosing(e);
		}

		private void Cancel_Click(object sender, EventArgs e) => this.Abort();
	}
}

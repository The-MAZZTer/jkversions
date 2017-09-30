using Microsoft.Win32;
using MZZT.JKVersions.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JKVersions {
	static class Program {
		private const int PROGRESS_ITEMS = 15;
		private const int BUFFER_SIZE = 4 * 1024;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if (MessageBox.Show($"This application will reproduce several different versions of the Jedi Knight game " +
				$"executable using publicly available files. These versions are:{Environment.NewLine}{Environment.NewLine}" +
				$"\t- Jedi Knight v1.00{Environment.NewLine}" +
				$"\t- Jedi Knight v1.01{Environment.NewLine}" +
				$"\t- Jedi Knight Unofficial Patch v2008-01-16{Environment.NewLine}{Environment.NewLine}" +
				$"Files are provided by JKHub.net. Thanks to them for hosting files for the JK community." +
				$"{Environment.NewLine}{Environment.NewLine}" + 
				$"Special thanks to Nikumubeki for always telling me when JKVersions was broke." +
				$"{Environment.NewLine}{Environment.NewLine}" +
				$"You must be connected to the internet for this tool to work. Do you want to continue?",
				"JKVersions", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
				!= DialogResult.Yes) {

				return;
			}

			form = new ProgressForm(PROGRESS_ITEMS);
			form.Shown += Form_Shown;
			form.Cancelled += (sender, e) => Abort();
			Application.Run(form);
		}

		private static ProgressForm form;
		private static CancellationTokenSource cancellationSource = new CancellationTokenSource();

		private static async void Form_Shown(object sender, EventArgs e) {
			form.SetProgressText("Extracting tools...");

			CreateWorkingDir();
			CheckCancelled();

			await ExtractResourceTools();
			CheckCancelled();

			form.CompleteItem();	
			form.SetProgressText("Downloading Jedi Knight 1.01...");

			string archive = Path.Combine(WorkingDir, "jkupd101.exe");
			await DownloadFile("http://www.jkhub.net/project/get.php?id=1947", archive);
			CheckCancelled();

			form.CompleteItem();
			form.SetProgressText("Extracting Jedi Knight 1.01...");

			await ExtractArchiveFiles(archive, new[] { "jk.exe" }, WorkingDir);
			CheckCancelled();

			DeleteFile(archive);
			string path = Path.Combine(WorkingDir, "jk.1.01.exe");
			if (File.Exists(path)) {
				DeleteFile(path);
			}
			File.Move(Path.Combine(WorkingDir, "jk.exe"), path);

			form.CompleteItem();
			form.SetProgressText("Verifying Jedi Knight 1.01...");

			await VerifyHash(path, Resources.JK_1_0_1_hash);
			CheckCancelled();

			form.CompleteItem();
			form.SetProgressText("Downloading Jedi Knight 1.01 -> 1.0 patch....");

			archive = Path.Combine(WorkingDir, "patch_1.01_to_1.0.zip");
			await DownloadFile("http://www.jkhub.net/project/get.php?id=975", archive);
			CheckCancelled();

			form.CompleteItem();
			form.SetProgressText("Extracting Jedi Knight 1.01 -> 1.0 patch...");

			await ExtractArchiveFiles(archive, new[] { "bspatch.exe", "patch_1.01_to_1.0.dat" }, WorkingDir);
			CheckCancelled();

			DeleteFile(archive);

			form.CompleteItem();
			form.SetProgressText("Patching Jedi Knight 1.01 -> 1.0...");

			path = Path.Combine(WorkingDir, "jk.1.0.exe");
			await PatchFile(Path.Combine(WorkingDir, "jk.1.01.exe"), path, Path.Combine(WorkingDir, "patch_1.01_to_1.0.dat"));
			CheckCancelled();

			form.CompleteItem();
			form.SetProgressText("Verifying Jedi Knight 1.0...");

			await VerifyHash(path, Resources.JK_1_0_hash);
			CheckCancelled();

			form.CompleteItem();
			form.SetProgressText("Downloading Jedi Knight Unofficial Patch 2008-01-16...");

			archive = Path.Combine(WorkingDir, "JKUnofficialPatch_2008-01-16.zip");
			await DownloadFile("http://www.jkhub.net/project/get.php?id=1499", archive);
			CheckCancelled();

			form.CompleteItem();
			form.SetProgressText("Extracting Jedi Knight Unofficial Patch...");

			await ExtractArchiveFiles(archive, new[] { "bspatch.exe", "JK-Extension.dll", "patch.dat" }, WorkingDir);
			CheckCancelled();
			DeleteFile(archive);

			form.CompleteItem();
			form.SetProgressText("Patching Jedi Knight with Unofficial Patch...");

			path = Path.Combine(WorkingDir, "jk.Unofficial.Patch.2008.01.16.exe");
			await PatchFile(Path.Combine(WorkingDir, "jk.1.0.exe"), path, Path.Combine(WorkingDir, "patch.dat"));
			CheckCancelled();

			DeleteFile(Path.Combine(WorkingDir, "bspatch.exe"));
			try {
				Directory.Delete(Path.Combine(WorkingDir, "tools"), true);
			} catch (IOException) {
			} catch (UnauthorizedAccessException) {
			}

			form.CompleteItem();
			form.SetProgressText("Verifying Jedi Knight Unofficial Patch...");

			await VerifyHash(path, Resources.JK_Unofficial_Patch_2008_01_16_hash);
			CheckCancelled();

			form.CompleteItem();
			form.SetProgressText("Locating game folder and installing files...");

			string gamePath = null;
			while (string.IsNullOrEmpty(gamePath) || !Directory.Exists(gamePath)) {
				await Task.Run(() => {
					RegistryKey key = null;
					try {
						key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\LucasArts Entertainment Company\jediknight\1.0");
					} catch (SecurityException) {
					}

					if (key == null) {
						try {
							key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\LucasArts Entertainment Company\jediknight\1.0");
						} catch (SecurityException) {
						}
					}

					if (key != null) {
						using (key) {
							gamePath = key.GetValue("install path", null) as string;
						}
					}
				});
				CheckCancelled();

				if (string.IsNullOrEmpty(gamePath) || !Directory.Exists(gamePath)) {
					if (MessageBox.Show("Could not locate the Jedi Knight game folder. If you are using the Steam version you must run it at least once to finish installing the game. You can do this now and press Retry, or press Cancel to perform the file installation yourself.",
						"JKVersions", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Cancel) {

						Process.Start(WorkingDir);
						Application.Exit();
						return;
					}
				}
			}

			string patchPath = Path.Combine(gamePath, "Patches");
			if (!Directory.Exists(patchPath)) {
				Directory.CreateDirectory(patchPath);
			}

			foreach (string file in new[] { "jk.1.01.exe", "jk.1.0.exe", "jk.Unofficial.Patch.2008.01.16.exe" }) {
				string dest = Path.Combine(patchPath, file);
				if (File.Exists(dest)) {
					try {
						File.Delete(dest);
					} catch (IOException) {
						continue;
					} catch (UnauthorizedAccessException) {
						continue;
					}
				}

				await Task.Run(() => File.Move(Path.Combine(WorkingDir, file), dest));
				CheckCancelled();
			}

			form.CompleteItem();

			path = Path.Combine(gamePath, "JediKnight.exe");
			if (await VerifyHash(path, Resources.JK_Steam_hash, false)) {
				CheckCancelled();

				form.SetProgressText("Backing up Jedi Knight Steam game executable...");

				string dest = Path.Combine(patchPath, "jk.Steam.exe");
				await Task.Run(() => File.Copy(path, dest, true));
			} else {
				form.SetProgressText("Jedi Knight executable doesn't appear to be Steam version, skipping backup.");
			}
			CheckCancelled();

			form.CompleteItem();

			form.DisableCancel();
			form.SetProgressText("Cleaning up...");

			await Task.Run(() => Directory.Delete(WorkingDir, true));

			form.CompleteItem();

			Process.Start(patchPath);
			Application.Exit();
		}

		private static void DeleteFile(string path) {
			try {
				File.Delete(path);
			} catch (IOException) {
			} catch (UnauthorizedAccessException) {
			}
		}

		private static async Task ExtractResource(string path, byte[] data) {
			if (!File.Exists(path)) {
				using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None)) {
					await fs.WriteAsync(data, 0, data.Length);
				}
			}
		}

		private static async Task ExtractResourceTools() {
			string path = Path.Combine(WorkingDir, "tools");
			if (!Directory.Exists(path)) {
				Directory.CreateDirectory(path);
			}

			await ExtractResource(Path.Combine(path, "7za.exe"), Resources._7za);
			CheckCancelled();

			await ExtractResource(Path.Combine(path, "7za_license.txt"), Resources._7za_license);
			CheckCancelled();
		}

		private static async Task ExtractArchiveFiles(string archive, string[] files, string destination) {
			Process process = Process.Start(new ProcessStartInfo() {
				FileName = Path.Combine(WorkingDir, "tools", "7za.exe"),
				Arguments = $"x \"{archive}\" \"{string.Join("\" \"", files)}\" -y -o\"{destination ?? "."}\"",
				CreateNoWindow = true,
				UseShellExecute = false
			});
			await Task.Run(() => process.WaitForExit(), cancellationSource.Token);
		}
		
		private static async Task DownloadFile(string url, string dest) {
			bool retry;
			do {
				retry = false;

				HttpWebRequest http = WebRequest.CreateHttp(url);
				http.Method = WebRequestMethods.Http.Get;
				http.UserAgent = "JKVersions";
				http.Timeout = 15000;
				try {
					using (HttpWebResponse response = await http.GetResponseAsync() as HttpWebResponse) {
						CheckCancelled();
						using (Stream download = response.GetResponseStream()) {
							using (FileStream file = new FileStream(dest, FileMode.Create, FileAccess.Write, FileShare.None)) {
								//await download.CopyToAsync(file, 0, token);

								int readBytes = 0;
								byte[] buffer = new byte[BUFFER_SIZE];
								do {
									readBytes = await download.ReadAsync(buffer, 0, buffer.Length);
									CheckCancelled();

									if (readBytes > 0) {
										await file.WriteAsync(buffer, 0, readBytes);
										CheckCancelled();
									}

									if (response.ContentLength > -1) {
										form.SetProgress((double)file.Position / response.ContentLength);
									}
								} while (readBytes > 0);
								buffer = null;
							}
						}
					}
				} catch (WebException ex) {
					retry = MessageBox.Show($"There was a problem downloading a file:" +
						$"{Environment.NewLine}{Environment.NewLine}{ex.ExceptionToString()}", "JKVersions",
						MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
						== DialogResult.Retry;
					if (!retry) {
						form.Abort();
						return;
					}
				}
			} while (retry);
		}

		private static SHA1Managed sha1 = new SHA1Managed();
		private static async Task<bool> VerifyHash(string filename, byte[] desiredHash, bool fatalFailure = true) {
			byte[] actualHash;
			byte[] buffer;
			using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read)) {
				buffer = new byte[file.Length];
				await file.ReadAsync(buffer, 0, buffer.Length);
			}

			CheckCancelled();

			actualHash = sha1.ComputeHash(buffer);
			buffer = null;

			if (!desiredHash.SequenceEqual(actualHash)) {
				if (fatalFailure) {
					MessageBox.Show(
						"The file could not be verified successfully. You might try starting over again. If this keeps happening one of the downloads may not be functional. :(",
						"JKVersions", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
					form.Abort();
				}
				return false;
			}

			return true;
		}

		private static async Task PatchFile(string inputFile, string outputFile, string patchFile) {
			ProcessStartInfo startInfo = new ProcessStartInfo() {
				FileName = Path.Combine(WorkingDir, "bspatch.exe"),
				Arguments = $"\"{inputFile}\" \"{outputFile}\" \"{patchFile}",
				CreateNoWindow = true,
				UseShellExecute = false
			};
			startInfo.EnvironmentVariables["__COMPAT_LAYER"] = "RunAsInvoker";

			Process process = Process.Start(startInfo);
			await Task.Run(() => process.WaitForExit(), cancellationSource.Token);

			DeleteFile(patchFile);
		}
		
		private static string WorkingDir {
			get {
				return Path.Combine(Path.GetTempPath(), "JKVersions");
			}
		}

		private static void CreateWorkingDir() {
			if (!Directory.Exists(WorkingDir)) {
				Directory.CreateDirectory(WorkingDir);
			}
		}

		private static bool CheckCancelled() {
			if (cancellationSource.Token.IsCancellationRequested) {
				Cleanup();
				return true;
			}
			return false;
		}

		private static void Cleanup() {
			string path = WorkingDir;
			if (Directory.Exists(path)) {
				try {
					Directory.Delete(path, true);
				} catch (IOException) {
				} catch (UnauthorizedAccessException) {
				}
			}

			Environment.Exit(1);
		}

		private static void Abort() {
			cancellationSource.Cancel();
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
			if (Debugger.IsAttached) {
				return;
			}

			Exception ex = e.ExceptionObject as Exception;
			if (ex == null) {
				return;
			}

			string title = "Unknown Error";
			string text = "";
			while (ex != null) {
				text = $"{ex.ExceptionToString()}{Environment.NewLine}{text}";
				title = ex.Message;
				ex = ex.InnerException;
			}

			text = text.Trim(Environment.NewLine.ToCharArray());

			if (MessageBox.Show($"JKVersions encounted an unexpected problem and cannot continue. The problem is:" +
				$"{Environment.NewLine}{Environment.NewLine}{text}{Environment.NewLine}{Environment.NewLine}" +
				$"If you have a GitHub account, please consider filing a bug report about this issue." +
				$"Do you want to open this project's new issue form?", "JKVersions", MessageBoxButtons.YesNo,
				MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {

				Process.Start($"https://github.com/The-MAZZTer/jkversions/issues/new?title={Uri.EscapeDataString(title)}&body={Uri.EscapeDataString(text)}");
			}

			Environment.Exit(1);
		}

		private static string ExceptionToString(this Exception ex) {
			return $"{ex.Source}: {ex.GetType().FullName}: {ex.Message}{Environment.NewLine}{ex.StackTrace}";
		}
	}
}

using JkVersions.Properties;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JkVersions {
	// Form
	// File location jkupd101.exe
	// Patch to 1.0?
	// File location patch_1.01_to_1.0.zip
	// Install JK Unofficial Patch?
	// File location JKUnofficialPatch_2008-01-16.zip
	// Output (autodetect jk folder, browse for folder (default temp folder))
	// Replace Steam EXE

	static class Program {
		private const int PROGRESS_ITEMS = 15;
		private const int BUFFER_SIZE = 4 * 1024;

		public static Settings Settings { get; private set; }

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			string settingsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "settings.json");
			if (File.Exists(settingsPath)) {
				using (FileStream stream = new FileStream(settingsPath, FileMode.Open, FileAccess.Read, FileShare.Read)) {
					Settings = Settings.Load(stream, true);
				}
			}
			if (Settings == null) {
				try {
					Settings.SaveDefaults(settingsPath);
				} catch (IOException) {
				} catch (SecurityException) {
				}
				Settings = new Settings();
			}

			settingsForm = new MainForm();
			if (settingsForm.ShowDialog() != DialogResult.OK) {
				return;
			}

			form = new ProgressForm(PROGRESS_ITEMS);
			form.Shown += Form_Shown;
			form.Cancelled += (sender, e) => Abort();
			Application.Run(form);
		}

		private static MainForm settingsForm;
		private static ProgressForm form;
		private static readonly CancellationTokenSource cancellationSource = new CancellationTokenSource();

		private static async void Form_Shown(object sender, EventArgs e) {
			form.SetProgressText("Extracting tools...");

			CreateTempFolder();
			CheckCancelled();

			await ExtractResourceTools();
			CheckCancelled();
			form.CompleteItem();

			string source = settingsForm.Jk101Path;
			string archive = Path.Combine(TempFolder, "jkupd101.exe");
			if (Uri.IsWellFormedUriString(source, UriKind.Absolute)) {
				form.SetProgressText("Downloading Jedi Knight 1.01...");

				await DownloadFile(source, archive);
			} else {
				archive = source;
			}
			CheckCancelled();
			form.CompleteItem();

			form.SetProgressText("Extracting Jedi Knight 1.01...");

			await ExtractArchiveFiles(archive, new[] { "jk.exe" }, TempFolder);
			if (source != archive) {
				DeleteFile(archive);
			}
			CheckCancelled();

			string destFolder = settingsForm.OutputFolder;
			if (!Directory.Exists(destFolder)) {
				Directory.CreateDirectory(destFolder);
			}
			string path = Path.Combine(destFolder, "jk.1.01.exe");
			if (File.Exists(path)) {
				DeleteFile(path);
			}
			File.Move(Path.Combine(TempFolder, "jk.exe"), path);

			CheckCancelled();
			form.CompleteItem();

			if (settingsForm.VerifyWithHashes) {
				form.SetProgressText("Verifying Jedi Knight 1.01...");

				await VerifyHash(path, Settings.Hashes.Jk1_01);
			}

			CheckCancelled();
			form.CompleteItem();

			source = settingsForm.Jk10Path;
			if (!string.IsNullOrEmpty(source)) {
				archive = Path.Combine(TempFolder, "patch_1.01_to_1.0.zip");
				if (Uri.IsWellFormedUriString(source, UriKind.Absolute)) {
					form.SetProgressText("Downloading Jedi Knight 1.01 -> 1.0 patch....");

					await DownloadFile(source, archive);
				} else {
					archive = source;
				}
				CheckCancelled();
				form.CompleteItem();

				form.SetProgressText("Extracting Jedi Knight 1.01 -> 1.0 patch...");

				await ExtractArchiveFiles(archive, new[] { "bspatch.exe", "patch_1.01_to_1.0.dat" }, TempFolder);
				if (source != archive) {
					DeleteFile(archive);
				}

				CheckCancelled();
				form.CompleteItem();

				form.SetProgressText("Patching Jedi Knight 1.01 -> 1.0...");

				path = Path.Combine(destFolder, "jk.1.0.exe");
				await PatchFile(Path.Combine(destFolder, "jk.1.01.exe"), path, Path.Combine(TempFolder, "patch_1.01_to_1.0.dat"));

				CheckCancelled();
				form.CompleteItem();

				if (settingsForm.VerifyWithHashes) {
					form.SetProgressText("Verifying Jedi Knight 1.0...");

					await VerifyHash(path, Settings.Hashes.Jk1_0);
				}

				CheckCancelled();
				form.CompleteItem();

				source = settingsForm.JkUnofficialPatchPath;
				if (!string.IsNullOrEmpty(source)) {
					archive = Path.Combine(TempFolder, "JKUnofficialPatch_2008-01-16.zip");
					if (Uri.IsWellFormedUriString(source, UriKind.Absolute)) {
						form.SetProgressText("Downloading Jedi Knight Unofficial Patch 2008-01-16...");

						await DownloadFile(source, archive);
					} else {
						archive = source;
					}
					CheckCancelled();
					form.CompleteItem();

					form.SetProgressText("Extracting Jedi Knight Unofficial Patch...");

					await ExtractArchiveFiles(archive, new[] { "bspatch.exe", "JK-Extension.dll", "patch.dat" }, TempFolder);
					if (source != archive) {
						DeleteFile(archive);
					}

					CheckCancelled();
					form.CompleteItem();

					form.SetProgressText("Patching Jedi Knight with Unofficial Patch...");

					path = Path.Combine(destFolder, "jk.Unofficial.Patch.2008.01.16.exe");
					await PatchFile(Path.Combine(destFolder, "jk.1.0.exe"), path, Path.Combine(TempFolder, "patch.dat"));

					DeleteFile(Path.Combine(TempFolder, "bspatch.exe"));
					DeleteFile(Path.Combine(TempFolder, "patch.dat"));
					try {
						Directory.Delete(Path.Combine(TempFolder, "tools"), true);
					} catch (IOException) {
					} catch (UnauthorizedAccessException) {
					}

					CheckCancelled();
					form.CompleteItem();

					if (settingsForm.VerifyWithHashes) {
						form.SetProgressText("Verifying Jedi Knight Unofficial Patch...");

						await VerifyHash(path, Settings.Hashes.JkUnofficialPatch);
					}
					CheckCancelled();
					form.CompleteItem();
				} else {
					form.CompleteItem();
					form.CompleteItem();
					form.CompleteItem();
					form.CompleteItem();
				}
			} else {
				form.CompleteItem();
				form.CompleteItem();
				form.CompleteItem();
				form.CompleteItem();
				form.CompleteItem();
				form.CompleteItem();
				form.CompleteItem();
				form.CompleteItem();
			}

			if (settingsForm.OutputIsJkGameFolder) {
				form.SetProgressText("Installing support files to game folder...");

				string dest = Path.Combine(destFolder, @"..\JK-Extension.dll");
				if (File.Exists(dest)) {
					try {
						File.Delete(dest);
					} catch (IOException) {
					} catch (UnauthorizedAccessException) {
					}
				}

				await Task.Run(() => File.Move(Path.Combine(TempFolder, "JK-Extension.dll"), dest));

				CheckCancelled();
				form.CompleteItem();

				path = Path.Combine(destFolder, @"..\JediKnight.exe");
				if (await VerifyHash(path, Settings.Hashes.JkSteam, false)) {
					CheckCancelled();

					form.SetProgressText("Backing up Jedi Knight Steam game executable...");

					dest = Path.Combine(destFolder, "jk.Steam.exe");
					await Task.Run(() => File.Copy(path, dest, true));
				} else {
					form.SetProgressText("Jedi Knight executable doesn't appear to be Steam version, skipping backup.");
				}
			} else {
				form.CompleteItem();
			}

			form.CompleteItem();

			form.DisableCancel();

			if (destFolder != TempFolder) {
				form.SetProgressText("Cleaning up...");

				await Task.Run(() => Directory.Delete(TempFolder, true));
			}

			form.CompleteItem();

			Process.Start(destFolder);
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
			string path = Path.Combine(TempFolder, "tools");
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
				FileName = Path.Combine(TempFolder, "tools", "7za.exe"),
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

		private static readonly SHA1Managed sha1 = new SHA1Managed();
		private static async Task<bool> VerifyHash(string filename, string hash, bool fatalFailure = true) {
			byte[] actualHash;
			byte[] buffer;
			using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read)) {
				buffer = new byte[file.Length];
				await file.ReadAsync(buffer, 0, buffer.Length);
			}

			CheckCancelled();

			byte[] desiredHash = Enumerable.Range(0, hash.Length / 2)
				.Select(x => Convert.ToByte(hash.Substring(x * 2, 2), 16))
				.ToArray();
			actualHash = sha1.ComputeHash(buffer);

			if (!desiredHash.SequenceEqual(actualHash)) {
				if (fatalFailure) {
					MessageBox.Show(
						$"{filename} could not be verified successfully. You might try starting over again. If this keeps happening you may want to find a better download for this file.",
						"JKVersions", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
					form.Abort();
				}
				return false;
			}

			return true;
		}

		private static async Task PatchFile(string inputFile, string outputFile, string patchFile) {
			ProcessStartInfo startInfo = new ProcessStartInfo() {
				FileName = Path.Combine(TempFolder, "bspatch.exe"),
				Arguments = $"\"{inputFile}\" \"{outputFile}\" \"{patchFile}",
				CreateNoWindow = true,
				UseShellExecute = false
			};
			startInfo.EnvironmentVariables["__COMPAT_LAYER"] = "RunAsInvoker";

			Process process = Process.Start(startInfo);
			await Task.Run(() => process.WaitForExit(), cancellationSource.Token);

			DeleteFile(patchFile);
		}

		private static void CreateTempFolder() {
			if (!Directory.Exists(TempFolder)) {
				Directory.CreateDirectory(TempFolder);
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
			string path = TempFolder;
			if (Directory.Exists(path)) {
				try {
					Directory.Delete(path, true);
				} catch (IOException) {
				} catch (UnauthorizedAccessException) {
				}
			}

			Environment.Exit(1);
		}

		private static void Abort() => cancellationSource.Cancel();

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
			if (Debugger.IsAttached) {
				return;
			}

			if (!(e.ExceptionObject is Exception ex)) {
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

			if (MessageBox.Show($"JKVersions encountered an unexpected problem and cannot continue. The problem is:" +
				$"{Environment.NewLine}{Environment.NewLine}{text}{Environment.NewLine}{Environment.NewLine}" +
				$"If you have a GitHub account, please consider filing a bug report about this issue." +
				$"Do you want to open this project's new issue form?", "JKVersions", MessageBoxButtons.YesNo,
				MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {

				Process.Start($"https://github.com/The-MAZZTer/jkversions/issues/new?title={Uri.EscapeDataString(title)}&body={Uri.EscapeDataString(text)}");
			}

			Environment.Exit(1);
		}

		private static string ExceptionToString(this Exception ex) => $"{ex.Source}: {ex.GetType().FullName}: {ex.Message}{Environment.NewLine}{ex.StackTrace}";

		public static string JkPath {
			get {
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
						return key.GetValue("install path", null) as string;
					}
				}
				return null;
			}
		}

		public static string TempFolder => Path.Combine(Path.GetTempPath(), "JKVersions");
	}
}

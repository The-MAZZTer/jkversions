namespace JkVersions {
	public class Settings : MZZT.Settings.Settings<Settings> {
		public JkFileSettings DefaultLocations { get; set; } = new JkFileSettings() {
			Jk1_01 = "http://www.jkhub.net/project/get.php?id=1947",
			Jk1_0 = "http://www.jkhub.net/project/get.php?id=975",
			JkUnofficialPatch = "http://www.jkhub.net/project/get.php?id=1499"
		};
		public JkPlusSteamFileSettings Hashes { get; set; } = new JkPlusSteamFileSettings() {
			Jk1_01 = "1504212E61B5D8B3B285EC5B3F07D7D86F3B938E",
			Jk1_0 = "4E56EDA47B1FCB67F982C3CC708DCBD03D077CC7",
			JkUnofficialPatch = "7D00908B5D7960AFCA8F9E136C4F749B85873F80",
			JkSteam = "2946E42133D0D9A6A46EEF501D5C48BB4A4E6F99"
		};
	}

	public class JkFileSettings {
		public string Jk1_01 { get; set; }
		public string Jk1_0 { get; set; }
		public string JkUnofficialPatch { get; set; }
	}

	public class JkPlusSteamFileSettings : JkFileSettings {
		public string JkSteam { get; set; }
	}
}

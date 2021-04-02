using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace MZZT.Settings {
	[DataContract]
	public abstract class Settings<T> where T : Settings<T>, new() {
		public static T Load(Stream stream, bool createDefaultOnException = false) {
			DataContractJsonSerializer serializer = new(typeof(T), new DataContractJsonSerializerSettings() {
				UseSimpleDictionaryFormat = true
			});
			T ret;
			try {
				ret = (T)serializer.ReadObject(stream);
			} catch (Exception) {
				if (createDefaultOnException) {
					ret = new T();
				} else {
					throw;
				}
			}
			return ret;
		}

		public static T Load(string path, bool createDefaultOnException = false) {
			T ret;
			try {
				using FileStream stream = new(path, FileMode.Open, FileAccess.Read, FileShare.Read);
				ret = Load(stream, createDefaultOnException);
			} catch (Exception) {
				if (createDefaultOnException) {
					ret = new T();
				} else {
					throw;
				}
			}
			ret.FilePath = path;
			return ret;
		}

		public void Save(Stream stream) {
			DataContractJsonSerializer serializer = new(typeof(T), new DataContractJsonSerializerSettings() {
				UseSimpleDictionaryFormat = true
			});
			serializer.WriteObject(stream, this);
		}

		public void Save(string path = null) {
			if (path == null) {
				path = this.FilePath;
			}

			using FileStream stream = new(path, FileMode.Create, FileAccess.Write, FileShare.None);
			this.Save(stream);

			this.FilePath = path;
		}

		public static void SaveDefaults(string path) {
			DataContractJsonSerializer serializer = new(typeof(T), new DataContractJsonSerializerSettings() {
				UseSimpleDictionaryFormat = true
			});
			using FileStream stream = new(path, FileMode.Create, FileAccess.Write, FileShare.None);
			serializer.WriteObject(stream, new T());
		}

		[IgnoreDataMember, Browsable(false)]
		public string FilePath { get; private set; }
	}
}
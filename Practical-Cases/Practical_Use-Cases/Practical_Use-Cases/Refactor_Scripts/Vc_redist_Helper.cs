using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using Extensions;

namespace Refactor_Scripts
{
	/// <summary>
	/// Microsoft Visual C++ Redistributable Package Versions
	/// </summary>
	public enum RedistributablePackageVersion
	{
		[Description("Microsoft Visual C++ 2005 Redistributable")]
		VC2005x86,
		[Description("Microsoft Visual C++ 2005 Redistributable (x64)")]
		VC2005x64,
		[Description("Microsoft Visual C++ 2008 Redistributable - x86")]
		VC2008x86,
		[Description("Microsoft Visual C++ 2008 Redistributable - x64")]
		VC2008x64,
		[Description("Microsoft Visual C++ 2010  x86 Redistributable")]
		VC2010x86,
		[Description("Microsoft Visual C++ 2010  x64 Redistributable")]
		VC2010x64,
		[Description("Microsoft Visual C++ 2012 Redistributable (x86)")]
		VC2012x86,
		[Description("Microsoft Visual C++ 2012 Redistributable (x64)")]
		VC2012x64,
		[Description("Microsoft Visual C++ 2013")]
		VC2013x86,
		[Description("Microsoft Visual C++ 2013")]
		VC2013x64,
		[Description("Microsoft Visual C++ 2015")]
		VC2015x86,
		[Description("Microsoft Visual C++ 2015")]
		VC2015x64,
		[Description("Microsoft Visual C++ 2017")]
		VC2017x86,
		[Description("Microsoft Visual C++ 2017")]
		VC2017x64,
		[Description("Microsoft Visual C++ 2015-2019 Redistributable (x86)")]
		VC2015to2019x86,
		[Description("Microsoft Visual C++ 2015-2019 Redistributable (x86)")]
		VC2015to2019x64,
	};	

	/// <summary>
	///	Class to detect installed Microsoft Redistributable Packages.
	/// </summary>
	/// <see cref="//https://stackoverflow.com/questions/12206314/detect-if-visual-c-redistributable-for-visual-studio-2012-is-installed"/>
	public class RedistributablePackageHelper
	{
		private static List<string> rechecklst = new List<string>();

		private bool CheckVCRedistInLocalMachine(string subKey, string startNumber, bool writable = false)
        {
			var _readParams = Registry.LocalMachine.OpenSubKey(subKey, writable);

			if (_readParams == null)
				return false;

			var _readVersion = _readParams.GetValue("Version").ToString();

			if (string.IsNullOrEmpty(_readVersion))
				return false;

			return _readVersion.StartsWith(startNumber);
		}

		private bool CheckVCRedistInLocalMachine(string subKey, int minNumber, bool writable = false)
		{
			var _readParams = Registry.LocalMachine.OpenSubKey(subKey, writable);

			if (_readParams == null)
				return false;

			var _readVersion = _readParams.GetValue("Version");

			if (string.IsNullOrEmpty(_readVersion.ToString()))
				return false;

			return (int)_readVersion > minNumber;
		}

		private bool CheckVCRedistInClassesRoot(List<string> inputPaths, string startNumber, bool writable=false)
        {
			foreach (var path in inputPaths)
			{
				var _readParams = Registry.ClassesRoot.OpenSubKey(path, writable);
				if (_readParams == null)
					continue;

				return (bool)_readParams?.GetValue("Version")?.ToString().StartsWith(startNumber);
			}

			return false;
		}

		/// <summary>
		/// Check if a Microsoft Redistributable Package is installed.
		/// </summary>
		/// <param name="redistVersion">The package version to detect.</param>
		/// <returns><c>true</c> if the package is installed, otherwise <c>false</c></returns>
		private bool IsInstalled(RedistributablePackageVersion redistVersion)
		{
			try
			{
				if (redistVersion.Equals(RedistributablePackageVersion.VC2015to2019x86)
					|| redistVersion.Equals(RedistributablePackageVersion.VC2015to2019x64))
                {
					string _subKey = @"SOFTWARE\Microsoft\DevDiv\VC\Servicing\14.0\RuntimeMinimum";
					string _startNumber = "14";
					return CheckVCRedistInLocalMachine(_subKey, _startNumber);
				}
				else if (redistVersion.Equals(RedistributablePackageVersion.VC2017x86))
				{
					var _paths = new List<string>
						{
							@"Installer\Dependencies\,,x86,14.0,bundle",
							@"Installer\Dependencies\VC,redist.x86,x86,14.16,bundle" //changed in 14.16.x
						};
					string _startNumber = "14";

					return CheckVCRedistInClassesRoot(_paths, _startNumber);
				}
				else if (redistVersion.Equals(RedistributablePackageVersion.VC2017x64))
                {
					var _paths = new List<string>
						{
							@"Installer\Dependencies\,,amd64,14.0,bundle",
							@"Installer\Dependencies\VC,redist.x64,amd64,14.16,bundle" //changed in 14.16.x
						};
					string _startNumber = "14";

					return CheckVCRedistInClassesRoot(_paths, _startNumber);
				}
				else if (redistVersion.Equals(RedistributablePackageVersion.VC2015x86))
				{
					string _subKey = @"SOFTWARE\Classes\Installer\Dependencies\{e2803110-78b3-4664-a479-3611a381656a}";
					string _startNumber = "14";
					return CheckVCRedistInLocalMachine(_subKey, _startNumber);
				}
				else if (redistVersion.Equals(RedistributablePackageVersion.VC2015x64))
				{
					string _subKey = @"SOFTWARE\Classes\Installer\Dependencies\{d992c12e-cab2-426f-bde3-fb8c53950b0d}";
					string _startNumber = "14";
					return CheckVCRedistInLocalMachine(_subKey, _startNumber);
				}
				else if (redistVersion.Equals(RedistributablePackageVersion.VC2013x86))
				{
					string _subKey = @"SOFTWARE\Classes\Installer\Dependencies\{f65db027-aff3-4070-886a-0d87064aabb1}";
					string _startNumber = "12";
					return CheckVCRedistInLocalMachine(_subKey, _startNumber);
				}
				else if (redistVersion.Equals(RedistributablePackageVersion.VC2013x64))
				{
					string _subKey = @"SOFTWARE\Classes\Installer\Dependencies\{050d4fc8-5d48-4b8f-8972-47c82c46020f}";
					string _startNumber = "12";
					return CheckVCRedistInLocalMachine(_subKey, _startNumber);
				}
				else if (redistVersion.Equals(RedistributablePackageVersion.VC2012x86))
				{
					string _subKey = @"SOFTWARE\Classes\Installer\Dependencies\{33d1fd90-4274-48a1-9bc1-97e33d9c2d6f}";
					string _startNumber = "11";
					return CheckVCRedistInLocalMachine(_subKey, _startNumber);
				}
				else if (redistVersion.Equals(RedistributablePackageVersion.VC2012x64))
				{
					string _subKey = @"SOFTWARE\Classes\Installer\Dependencies\{ca67548a-5ebe-413a-b50c-4b9ceb6d66c6}";
					string _startNumber = "11";
					return CheckVCRedistInLocalMachine(_subKey, _startNumber);
				}
				else if (redistVersion.Equals(RedistributablePackageVersion.VC2010x86))
				{
					string _subKey = @"SOFTWARE\Classes\Installer\Products\1D5E3C0FEDA1E123187686FED06E995A";
					int _startNumber = 1;
					return CheckVCRedistInLocalMachine(_subKey, _startNumber);
				}
				else if (redistVersion.Equals(RedistributablePackageVersion.VC2010x64))
				{
					string _subKey = @"SOFTWARE\Classes\Installer\Products\1926E8D15D0BCE53481466615F760A7F";
					int _startNumber = 1;
					return CheckVCRedistInLocalMachine(_subKey, _startNumber);
				}
				else if (redistVersion.Equals(RedistributablePackageVersion.VC2008x86))
				{
					string _subKey = @"SOFTWARE\Classes\Installer\Products\6E815EB96CCE9A53884E7857C57002F0";
					int _startNumber = 1;
					return CheckVCRedistInLocalMachine(_subKey, _startNumber);
				}
				else if (redistVersion.Equals(RedistributablePackageVersion.VC2008x64))
				{
					string _subKey = @"SOFTWARE\Classes\Installer\Products\67D6ECF5CD5FBA732B8B22BAC8DE1B4D";
					int _startNumber = 1;
					return CheckVCRedistInLocalMachine(_subKey, _startNumber);
				}
				else if (redistVersion.Equals(RedistributablePackageVersion.VC2005x86))
				{
					string _subKey = @"SOFTWARE\Classes\Installer\Products\c1c4f01781cc94c4c8fb1542c0981a2a";
					int _startNumber = 1;
					return CheckVCRedistInLocalMachine(_subKey, _startNumber);
				}
				else if (redistVersion.Equals(RedistributablePackageVersion.VC2005x64))
				{
					string _subKey = @"SOFTWARE\Classes\Installer\Products\1af2a8da7e60d0b429d7e6453b3d0182";
					int _startNumber = 1;
					return CheckVCRedistInLocalMachine(_subKey, _startNumber);
				}
			}
			catch (Exception ex)
			{
				return false;
			}

			return false;
		}

		private static StringBuilder errormsg = new StringBuilder("Please Install missing RedistributablePackages below to continue:\r\n");
		
		public bool CheckAll()
		{
			bool uptodate = true;
            List<RedistributablePackageVersion> versionsToCheck = new List<RedistributablePackageVersion> 
			{ 
				RedistributablePackageVersion.VC2008x64,
				RedistributablePackageVersion.VC2008x86,
				RedistributablePackageVersion.VC2010x64,
				RedistributablePackageVersion.VC2010x86,
				RedistributablePackageVersion.VC2012x64,
				RedistributablePackageVersion.VC2012x86,
				RedistributablePackageVersion.VC2015to2019x64,
				RedistributablePackageVersion.VC2015to2019x86
			};

			foreach(var version in versionsToCheck)
            {
                bool checkedresult = IsInstalled(version);
                if (!checkedresult)
				{
					rechecklst.Add(version.GetDescription());
				}
			}			

			if (rechecklst.Count > 0)
			{
				uptodate = RecheckByNameList(rechecklst);
			}

			if (!uptodate)
			{
				MessageBox.Show(errormsg.ToString(), "Missing RedistributablePackage", MessageBoxButtons.OK,
						MessageBoxIcon.Error);
			}

			return uptodate;
		}

		private static List<string> _alldisplayName = new List<string>();
		private static bool RecheckByNameList(List<string> listtocheck)
		{
			GetSubkeysValue(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", RegistryHive.LocalMachine, RegistryView.Registry64);
			GetSubkeysValue(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", RegistryHive.LocalMachine, RegistryView.Registry32);
			GetSubkeysValue(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", RegistryHive.LocalMachine, RegistryView.Default);
			List<string> missing = CheckerRed(_alldisplayName, listtocheck);
			bool uptodate = missing.Count == 0;
			if (!uptodate)
			{
				missing.ForEach(s => errormsg.Append($" - {s}\r\n"));
			}
			return uptodate;
		}

		private static List<string> CheckerRed(List<string> items, List<string> requireLst)
		{
			items.Sort();
			List<string> outResult = new List<string>();

			if (items != null && items.Count > 0)
			{
				for (int count = 0; count < requireLst.Count; count++)
				{
					var existItem = from s in items
									where s.Contains(requireLst[count])
									select s;
					if (existItem != null && existItem.Count() > 0)
					{
					}
					else
					{
						outResult.Add(requireLst[count]);
					}
				}
			}
			return outResult;
		}

		private static List<Key> GetSubkeysValue(string path, RegistryHive hive, RegistryView view)
		{
			var result = new List<Key>();
			using (var hiveKey = RegistryKey.OpenBaseKey(hive, view))
			{
				using (var key = hiveKey.OpenSubKey(path))
				{
					var subkeys = key.GetSubKeyNames();

					foreach (var subkey in subkeys)
					{
						var values = GetKeyValue(key, subkey);
						result.Add(values);
					}
				}
				return result;
			}
		}

		private static Key GetKeyValue(RegistryKey hive, string keyName)
		{
			var result = new Key() { KeyName = keyName };
			using (var key = hive.OpenSubKey(keyName))
			{
				foreach (var valueName in key.GetValueNames())
				{
					var val = key.GetValue(valueName);
					if (valueName == "DisplayName")
					{
						if (!_alldisplayName.Contains(val.ToString()))
							_alldisplayName.Add(val.ToString());
					}
					var pair = new KeyValuePair<string, object>(valueName, val);
					result.Values.Add(pair);
				}
			}
			return result;
		}

		class Key
		{
			public string KeyName { get; set; }
			public List<KeyValuePair<string, object>> Values { get; set; } = new List<KeyValuePair<string, object>>();
		}
	}

	public static class RedistHelperTests
    {
		public static void Test1()
        {
			RedistributablePackageHelper redistHelper = new RedistributablePackageHelper();

            if (!redistHelper.CheckAll())
            {
				Console.WriteLine("Something's wrong!");
            }
            else
            {
				Console.WriteLine("All versions are setup");
			}
        }
    }
}

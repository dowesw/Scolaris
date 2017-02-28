using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Security;
using System.Diagnostics;
using System.Threading;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.AccessControl;
using System.Net;

using Microsoft.Win32;

using Scolaris.ENTITE;

using NpgsqlTypes;
using Npgsql;

namespace Scolaris.TOOLS
{
    public class Utils
    {
        static IDictionary mySavedState = new Hashtable();
        static string[] commandLineOptions = new string[1] { "/LogFile=example.log" };

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LogonUser(string lpszUsername,
            string lpszDomain,
            string lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            out IntPtr phToken
            );

        [DllImport("kernel32.dll")]
        public static extern int FormatMessage(int dwFlags, ref IntPtr lpSource, int dwMessageId, int dwLanguageId, ref String lpBuffer, int nSize, ref IntPtr Arguments);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        static int eventId = 0;

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public long dwServiceType;
            public ServiceState dwCurrentState;
            public long dwControlsAccepted;
            public long dwWin32ExitCode;
            public long dwServiceSpecificExitCode;
            public long dwCheckPoint;
            public long dwWaitHint;
        };

        public static string GetErrorMessage(int errorCode)
        {
            int FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x100;
            int FORMAT_MESSAGE_IGNORE_INSERTS = 0x200;
            int FORMAT_MESSAGE_FROM_SYSTEM = 0x1000;

            int msgSize = 255;
            string lpMsgBuf = null;
            int dwFlags = FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS;

            IntPtr lpSource = IntPtr.Zero;
            IntPtr lpArguments = IntPtr.Zero;
            int returnVal = FormatMessage(dwFlags, ref lpSource, errorCode, 0, ref lpMsgBuf, msgSize, ref lpArguments);

            if (returnVal == 0)
            {
                throw new Exception("Failed to format message for error code " + errorCode.ToString() + ". ");
            }
            return lpMsgBuf;

        }
        public static long MILLISECONDS(Object objet)
        {
            DateTime date = objet != null ? Convert.ToDateTime(objet) : DateTime.Now;
            TimeSpan interval = new TimeSpan(date.Day, date.Hour, date.Minute, date.Second, date.Millisecond);
            long nMilliseconds = interval.Days * 24 * 60 * 60 * 1000 +
                                 interval.Hours * 60 * 60 * 1000 +
                                 interval.Minutes * 60 * 1000 +
                                 interval.Seconds * 1000 +
                                 interval.Milliseconds;
            return nMilliseconds;
        }

        public static bool Is64BitOperatingSystem()
        {
            if (Directory.Exists(Chemins.cheminSystem64))
            {
                return true;
            }
            return false;
        }

        public static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }

        // Verify a hash against a string.
        public static bool VerifyMd5Hash(string input, string hash)
        {
            if (hash != null ? !hash.Equals("") : false)
            {
                // Hash the input.
                string hashOfInput = GetMd5Hash(input);
                // Create a StringComparer an compare the hashes.
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                if (0 == comparer.Compare(hashOfInput, hash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public static string GetTime(double valeur)
        {
            string time = Convert.ToString(((Int32)valeur)).Length > 1 ? ((Int32)valeur) + "h" : "0" + ((Int32)valeur) + "h";
            double r = valeur - (Int32)valeur;
            time += Convert.ToString(Math.Round(r * 60)).Length > 1 ? Math.Round(r * 60) + "min" : "0" + Math.Round(r * 60) + "min";
            return time;
        }

        public static DateTime AddTimeInDate(DateTime date, DateTime heure)
        {
            DateTime d = date;
            d = d.AddHours(heure.Hour);
            d = d.AddMinutes(heure.Minute);
            d = d.AddSeconds(heure.Second);
            return d;
        }

        public static DateTime RemoveTimeInDate(DateTime date, DateTime heure)
        {
            DateTime d = date;
            d = d.AddHours(-heure.Hour);
            d = d.AddMinutes(-heure.Minute);
            d = d.AddSeconds(-heure.Second);
            return d;
        }

        public static DateTime TimeStamp(DateTime date, DateTime heure)
        {
            if (date != null)
            {
                if (heure != null)
                {
                    return new DateTime(date.Year, date.Month, date.Day, heure.Hour, heure.Minute, heure.Second);
                }
                return new DateTime(date.Year, date.Month, date.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            }
            return DateTime.Now;
        }

        public static DateTime GetTimeStamp(DateTime date, DateTime heure)
        {
            DateTime d = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
            DateTime f = new DateTime(date.Year, date.Month, date.Day, heure.Hour, heure.Minute, heure.Second);
            if (f <= d)
            {
                d = f;
                d = d.AddDays(1.0);
            }
            else
            {
                d = f;
            }
            return d;
        }

        public static void removeFrom(string name)
        {
            List<string> l = new List<string>();
            for (int i = 0; i < Constantes.FIRST_FORM.Count; i++)
            {
                string s = Constantes.FIRST_FORM[i];
                l.Add(s);
            }
            for (int i = 0; i < l.Count; i++)
            {
                string s = l[i];
                if (s.Trim().Equals(name.Trim(), StringComparison.CurrentCultureIgnoreCase))
                {
                    Constantes.FIRST_FORM.Remove(name);
                }
            }
        }

        public static void addFrom(string name)
        {
            removeFrom(name);
            Constantes.FIRST_FORM.Add(name);
        }

        public static bool verifyParametre()
        {
            Serveur serveur = BLL.ServeurBLL.ReturnServeur();
            if (serveur != null)
            {
                if (!serveur.Adresse.Equals("") && !serveur.Port.Equals(0) && !serveur.Database.Equals("") && !serveur.User.Equals("") && !serveur.Password.Equals(""))
                {
                    return true;
                }
            }
            return false;
        }

        public static double ParsedMaxDouble(String value)
        {
            String d = Double.MaxValue.ToString();
            if (value.Equals(d))
            {
                return Int64.MaxValue;
            }
            return Convert.ToDouble(value);
        }

        public static int GetRowData(DataGridView data, String id)
        {
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i].Cells[0].Value != null)
                {
                    String l = data.Rows[i].Cells[0].Value.ToString();
                    if (l.Equals(id))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public static int GetRowData(DataGridView data, long id)
        {
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i].Cells[0].Value != null)
                {
                    long l = Convert.ToInt32(data.Rows[i].Cells[0].Value);
                    if (l.Equals(id))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public static void Cmd(string[] args)
        {
            if (args != null ? args.Length > 0 : false)
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < args.Length; i++)
                {
                    builder.AppendFormat("{" + i + "} ", args[i]);
                }
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = @"C:\Windows\system32\cmd.exe";
                    process.StartInfo.Arguments = builder.ToString();
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.Start();
                }
            }
        }

        public static void Cmd(string commande)
        {
            Process.Start(@"C:\Windows\system32\cmd.exe", commande);
        }

        public static bool IsAuthenticated(string username, string passwd)
        {
            return IsAuthenticated(username, passwd, Chemins.domainName);
        }

        public static bool IsAuthenticated(string username, string passwd, string domain)
        {
            try
            {
                IntPtr tokenHandle = new IntPtr(0);

                //The MachineName property gets the name of your computer.

                const int LOGON32_PROVIDER_DEFAULT = 0;
                const int LOGON32_LOGON_INTERACTIVE = 2;
                tokenHandle = IntPtr.Zero;

                //Call the LogonUser function to obtain a handle to an access token.
                bool returnValue = LogonUser(username, domain, passwd, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, out tokenHandle);
                if (returnValue == false)
                {
                    //This function returns the error code that the last unmanaged function returned.
                    int ret = Marshal.GetLastWin32Error();
                    string errmsg = GetErrorMessage(ret);
                    Messages.ErrorYesNo(errmsg);
                }
                else
                {
                    //Create the WindowsIdentity object for the Windows user account that is
                    //represented by the tokenHandle token.
                    WindowsIdentity newId = new WindowsIdentity(tokenHandle);
                    WindowsPrincipal userperm = new WindowsPrincipal(newId);

                    //Verify whether the Windows user has administrative credentials.
                    if (newId.IsAuthenticated)
                    {
                        return true;
                    }
                }
                CloseHandle(tokenHandle);
                return false;
            }
            catch (Exception ex)
            {
                Messages.Exception("Utils (IsAuthenticated) ", ex);
                return false;
            }
        }

        public static SecureString GetSecureString(string password)
        {
            SecureString secureString = new SecureString();
            foreach (char ch in password)
            {
                secureString.AppendChar(ch);
            }
            secureString.MakeReadOnly();
            return secureString;
        }

        public static bool IsNumeric(Object valeur)
        {
            try
            {
                if (valeur != null)
                {
                    Decimal d = Convert.ToDecimal(valeur);
                    return true;
                }
                return false;
            }
            catch (InvalidCastException ex)
            {
                return false;
            }
            catch (FormatException ex)
            {
                return false;
            }
        }

        public static String FindWord(string phrase, string mot)
        {
            int idx = phrase.IndexOf(mot);
            if (idx > -1)
            {
                string valeur = phrase.Substring(idx, mot.Length);
                for (int i = idx - 1; i > -1; i--)
                {
                    if (phrase[i] != ' ')
                        valeur = phrase[i] + valeur;
                    else
                        break;
                }
                idx = (phrase.Substring(0, idx) + "" + phrase.Substring(idx, mot.Length)).Length;
                for (int i = idx; i < phrase.Length; i++)
                {
                    if (phrase[i] != ' ' && phrase[i] != '(' && phrase[i] != ')')
                        valeur += phrase[i];
                    else
                        break;
                }
                return valeur;
            }
            return "";
        }

        public static String FindNextWord(string phrase, string mot)
        {
            int idx = phrase.IndexOf(mot);
            if (idx > -1)
            {
                string valeur = "";
                int espace = 0;
                idx = (phrase.Substring(0, idx) + "" + phrase.Substring(idx, mot.Length)).Length;
                for (int i = idx; i < phrase.Length; i++)
                {
                    if (espace == 1 && phrase[i] != ' ' && phrase[i] != '(' && phrase[i] != ')')
                        valeur += phrase[i];
                    if (phrase[i] == ' ' || phrase[i] == '(' || phrase[i] == ')')
                    {
                        espace++;
                        if (espace == 2)
                            break;
                    }
                }
                return valeur + ".";
            }
            return "";
        }

        public static int Occurence(string phrase, char mot)
        {
            int idx = phrase.IndexOf(mot);
            if (idx > -1)
            {
                string[] tab = phrase.Split(mot);
                if (tab != null ? tab.Length > 0 : false)
                    return tab.Length - 1;
            }
            return 0;
        }

        public static RegistrySecurity RegistrySecurity()
        {
            string user = Chemins.domainName + "\\" + Chemins.usersName;
            RegistrySecurity rs = new RegistrySecurity();
            rs.AddAccessRule(new RegistryAccessRule(user, RegistryRights.ReadKey | RegistryRights.Delete, InheritanceFlags.None, PropagationFlags.None, AccessControlType.Allow));
            rs.AddAccessRule(new RegistryAccessRule(user, RegistryRights.WriteKey | RegistryRights.ChangePermissions, InheritanceFlags.None, PropagationFlags.None, AccessControlType.Allow));
            rs.AddAccessRule(new RegistryAccessRule(user, RegistryRights.CreateSubKey | RegistryRights.FullControl, InheritanceFlags.None, PropagationFlags.None, AccessControlType.Allow));
            return rs;
        }
    }
}

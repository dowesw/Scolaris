using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Scolaris.TOOLS
{
    public class Logs
    {

        public static void Exception(string place, Exception ex)
        {
            string file = Chemins.CheminDatabase() + " server.txt";
            string msg = DateTime.Now.ToString()+ " ---- L'erreur suivante a été detectée : " + ex.Message + "----- Place : " + place;
            WriteTxt(file, msg);
        }

        public static void WriteTxt(string fileName, string message)
        {
            if (!File.Exists(fileName))
            {
                SaveTxt(fileName, message);
            }
            else
            {
                UpdateTxt(fileName, message);
            }
        }

        public static void SaveTxt(string fileName, string message)
        {
            using (TxtFileWriter writer = new TxtFileWriter(fileName))
            {
                writer.WriteRow(message);
            }
        }

        public static void UpdateTxt(string fileName, string message)
        {
            using (TxtFileWriter writer = new TxtFileWriter(fileName, true, Encoding.UTF8))
            {
                writer.WriteRow(message);
            }
        }

        public static List<string> ReadTxt(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return new List<string>();
            }

            List<string> l = new List<string>();
            using (TxtFileReader reader = new TxtFileReader(fileName))
            {
                l = reader.ReadRow();
            }
            return l;
        }

        public static List<string> ReadTxt(string fileName, DateTime dateDebut, DateTime dateFin)
        {
            if (!File.Exists(fileName))
            {
                return new List<string>();
            }

            List<string> l = new List<string>();
            using (TxtFileReader reader = new TxtFileReader(fileName))
            {
                l = reader.ReadRow(dateDebut, dateFin);
            }
            return l;
        }
    }
}

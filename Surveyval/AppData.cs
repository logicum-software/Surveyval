using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace Surveyval
{
    [Serializable]
    class AppData
    {
        public List<Fragebogen> appFrageboegen { get; set; }
        public List<Frage> appFragen { get; set; }

        public AppData()
        {
            appFrageboegen = new List<Fragebogen>();
            appFragen = new List<Frage>();
        }

        /*internal void save()
        {
            FileStream fs = new FileStream("udata.dat", FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, appFrageboegen);
            }
            catch (SerializationException ec)
            {
                MessageBox.Show(ec.Message, "Speicherfehler", MessageBoxButton.OK);
                //Console.WriteLine("Failed to serialize. Reason: " + ec.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

            fs = new FileStream("udata2.dat", FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, appFragen);
            }
            catch (SerializationException ec)
            {
                MessageBox.Show(ec.Message, "Speicherfehler", MessageBoxButton.OK);
                //Console.WriteLine("Failed to serialize. Reason: " + ec.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }*/

        internal Boolean isContaining(Fragebogen tmp)
        {
            foreach (Fragebogen item in appFrageboegen)
            {
                if (tmp.strName.Equals(item.strName))
                    return true;
            }
            return false;
        }

        internal void removeFragebogen(Fragebogen tmp)
        {
            foreach (Fragebogen item in appFrageboegen)
            {
                if (item.strName.Equals(tmp.strName))
                    appFrageboegen.Remove(item);
            }
        }
    }
}

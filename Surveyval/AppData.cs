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
            IFormatter formatter = new BinaryFormatter();
            try
            {
                Stream stream = new FileStream("udata1.dat", FileMode.Open, FileAccess.Read, FileShare.Read);
                appFrageboegen = (List<Fragebogen>)formatter.Deserialize(stream);
                stream.Close();

                stream = new FileStream("udata2.dat", FileMode.Open, FileAccess.Read, FileShare.Read);
                appFragen = (List<Frage>)formatter.Deserialize(stream);
                stream.Close();
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.Message, "Dateifehler", MessageBoxButton.OK);
                appFrageboegen = new List<Fragebogen>();
                appFragen = new List<Frage>();
                //throw;
            }
        }

        internal void save()
        {
            FileStream fs = new FileStream("udata1.dat", FileMode.Create);

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
        }

        internal Boolean isContaining(Fragebogen tmp)
        {
            foreach (Fragebogen item in appFrageboegen)
            {
                if (tmp.strName.Equals(item.strName))
                    return true;
            }
            return false;
        }
    }
}

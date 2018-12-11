using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace Surveyval
{
    /// <summary>
    /// Interaktionslogik für Selection.xaml
    /// </summary>
    public partial class Selection : Window
    {
        private AppData appData;

        public Selection()
        {
            InitializeComponent();
            this.Title = strings.SelectionTitle;
            buttonLoad.Content = strings.SelectionButtonLoad;
            buttonCancel.Content = strings.SelectionButtonCancel;
            buttonNew.Content = strings.SelectionButtonNew;
            appData = new AppData();
            listViewSelect.ItemsSource = appData.appFrageboegen;
        }

/*        private void saveData(AppData appData)
        {
            FileStream fs = new FileStream("udata.dat", FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, appData);
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

        private AppData loadData()
        {
            AppData appData;

            IFormatter formatter = new BinaryFormatter();
            try
            {
                Stream stream = new FileStream("udata.dat", FileMode.Open, FileAccess.Read, FileShare.Read);
                appData = (AppData)formatter.Deserialize(stream);
                stream.Close();
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.Message, "Dateifehler", MessageBoxButton.OK);
                appData = new AppData();
                //Application.Current.Shutdown();
                //throw;
            }
            return appData;
        }*/

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ListViewSelect_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            Design dlgDesign = new Design();

            dlgDesign.ShowDialog();
            this.Close();
        }
    }
}

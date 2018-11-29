using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace Surveyval
{
    /// <summary>
    /// Interaktionslogik für Design.xaml
    /// </summary>
    public partial class Design : Window
    {
        private Fragebogen tmpFragebogen = new Fragebogen();
        private List<Frage> tmpFragen = new List<Frage>();

        public Design()
        {
            InitializeComponent();

            // initialize Control labels
            labelName.Content = strings.DesignLabelName;
            labelIncluded.Content = strings.DesignLabelIncluded;
            labelCatalog.Content = strings.DesignLabelCatalog;
            buttonCancel.Content = strings.DesignButtonCancel;
            buttonSave.Content = strings.DesignButtonSave;
            buttonNewQuestion.Content = strings.DesignButtonNewQuestion;
            buttonDelQuestion.Content = strings.DesignButtonDelQuestion;
            buttonAddQuestion.Content = strings.DesignButtonAddQuestion;
            buttonRemoveQuestion.Content = strings.DesignButtonRemoveQuestion;

            // Initialize fields
            textBoxName.Text = strings.DesignNewQuestionnaireText;
            listViewIncluded.ItemsSource = tmpFragebogen.Fragen;
            listViewCatalog.ItemsSource = tmpFragen;
        }

        internal void setFrage(List<Frage> fragen)
        {
            foreach (Frage item in fragen)
                tmpFragen.Add(item);
        }

        internal void setFragebogen(Fragebogen fragebogen)
        {
            tmpFragebogen.strName = fragebogen.strName;

            foreach (Frage item in fragebogen.Fragen)
                tmpFragebogen.Fragen.Add(item);
        }

        private void saveData(AppData appData)
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
        }


        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonNewQuestion_Click(object sender, RoutedEventArgs e)
        {
            NewQuestion dlgNewQuestion = new NewQuestion();

            dlgNewQuestion.ShowDialog();
        }
    }
}

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
        private AppData appData;
        private Fragebogen tmpFragebogen = new Fragebogen();

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

            appData = new AppData();

            // Initialize fields
            textBoxName.Text = strings.DesignNewQuestionnaireText;
            listViewIncluded.ItemsSource = tmpFragebogen.Fragen;
            listViewCatalog.ItemsSource = appData.appFragen;
        }

        private void refreshLists()
        {
            listViewIncluded.Items.Refresh();
            listViewCatalog.Items.Refresh();
            textBoxName.Text = tmpFragebogen.strName;

            // Spaltenbreite neu anpassen
            listViewIncluded.UpdateLayout();
            listViewCatalog.UpdateLayout();
        }


        internal void setFragebogen(Fragebogen fragebogen)
        {
            tmpFragebogen.strName = fragebogen.strName;

            foreach (Frage item in fragebogen.Fragen)
                tmpFragebogen.Fragen.Add(item);
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonNewQuestion_Click(object sender, RoutedEventArgs e)
        {
            NewQuestion dlgNewQuestion = new NewQuestion();

            dlgNewQuestion.ShowDialog();

            if (dlgNewQuestion.DialogResult == true)
            {
                appData.appFragen.Add(dlgNewQuestion.getFrage());
                appData.save();
                MessageBox.Show("Frage gespeichert", "Die eingegebene Frage wurde gespeichert.", MessageBoxButton.OK);
                refreshLists();
            }
        }
    }
}

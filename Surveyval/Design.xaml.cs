using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;

namespace Surveyval
{
    /// <summary>
    /// Interaktionslogik für Design.xaml
    /// </summary>
    public partial class Design : Window
    {
        private AppData appData;
        private int iIndexSelectedQuestionnaire, iIndexSelectedQuestion;

        public Design()
        {
            InitializeComponent();

            // initialize Control labels
            buttonCancel.Content = strings.DesignButtonCancel;
            buttonNewQuestion.Content = strings.DesignButtonNewQuestion;
            buttonDelQuestion.Content = strings.DesignButtonDelQuestion;
            buttonNewQuestionnaire.Content = strings.DesignButtonNewQuestionnaire;
            buttonDelQuestionnaire.Content = strings.DesignButtonDelQuestionnaire;
            groupBoxQuestionnaire.Header = strings.DesignGroupBoxQuestionnaireCatalog;
            groupBoxQuestion.Header = strings.DesignGroupBoxQuestionCatalog;

            appData = new AppData();
            iIndexSelectedQuestionnaire = 0;
            iIndexSelectedQuestion = 0;

            //Daten einlesen aus Datei "udata.dat"
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
                //throw;
            }

            listBoxQuestionnaire.SelectedIndex = 0;
            listBoxQuestionnaire.Focus();

            // Initialize fields
            listBoxQuestionnaire.ItemsSource = appData.appFrageboegen;
            listBoxQuestion.ItemsSource = appData.appFragen;
            refreshLists();
        }

        private void saveData()
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

        private void refreshLists()
        {
            listBoxQuestionnaire.Items.Refresh();

            foreach (Frage item in appData.appFragen)
            {
                if (appData.appFrageboegen[iIndexSelectedQuestionnaire].isContaining(item.strFragetext))
                {
                    item.bInSelected = true;
                }
            }
            listBoxQuestion.Items.Refresh();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonNewQuestion_Click(object sender, RoutedEventArgs e)
        {
            NewQuestion dlgNewQuestion = new NewQuestion();

            dlgNewQuestion.ShowDialog();

            if (dlgNewQuestion.DialogResult.HasValue && dlgNewQuestion.DialogResult.Value == true)
            {
                foreach (Frage item in appData.appFragen)
                {
                    if (String.Compare(item.strFragetext, dlgNewQuestion.getFrage().strFragetext, true) > -1 &&
                        String.Compare(item.strFragetext, dlgNewQuestion.getFrage().strFragetext, true) < 1)
                    {
                        if (MessageBox.Show(strings.NewQuestionExists1 + "\n\n" + item.strFragetext + "\n\n" + strings.NewQuestionExists2,
                            strings.NewQuestionExists3, MessageBoxButton.YesNo) == MessageBoxResult.No)
                            return;
                    }
                }
                appData.appFragen.Add(dlgNewQuestion.getFrage());
                saveData();
                MessageBox.Show(strings.NewQuestionSaved2, strings.NewQuestionSaved1, MessageBoxButton.OK);
                refreshLists();
            }
        }

        private void ButtonDelQuestion_Click(object sender, RoutedEventArgs e)
        {
            /*if (listViewCatalog.SelectedItem != null)
            {
                if (MessageBox.Show(strings.DesignDeleteQuestion2 + "\n\n" +
                    appData.appFragen.ElementAt(listViewCatalog.SelectedIndex).strFragetext +
                    "\n\n" + strings.DesignDeleteQuestion3, strings.DesignDeleteQuestion1, MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
                else
                {
                    appData.appFragen.RemoveAt(listViewCatalog.SelectedIndex);
                    appData.save();
                    refreshLists();
                }
            }*/
        }

        private void ButtonNewQuestionnaire_Click(object sender, RoutedEventArgs e)
        {
            NewQuestionnaire dlgNewQuestionnaire = new NewQuestionnaire();

            dlgNewQuestionnaire.Title = strings.NewQuestionnaireTitle;
            dlgNewQuestionnaire.labelName.Content = strings.NewQuestionnaireLabelName;
            dlgNewQuestionnaire.textBoxName.Text = strings.NewQuestionnaireTextBoxName;
            dlgNewQuestionnaire.buttonSave.Content = strings.NewQuestionnaireButtonSave;
            dlgNewQuestionnaire.buttonCancel.Content = strings.NewQuestionnaireButtonCancel;
            dlgNewQuestionnaire.textBoxName.Focus();
            dlgNewQuestionnaire.textBoxName.SelectAll();

            if (dlgNewQuestionnaire.ShowDialog() == true)
            {
                if (appData.isContaining(new Fragebogen(dlgNewQuestionnaire.textBoxName.Text, new List<Frage>())))
                {
                    MessageBox.Show(strings.NewQuestionnaireExists, strings.NewQuestionnaireExistsTitle, MessageBoxButton.OK);
                    return;
                }
                else
                {
                    appData.appFrageboegen.Add(new Fragebogen(dlgNewQuestionnaire.textBoxName.Text, new List<Frage>()));
                    saveData();
                    refreshLists();
                }
            }
        }

        private void ListBoxQuestionnaire_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            iIndexSelectedQuestionnaire = listBoxQuestionnaire.SelectedIndex;
            /*MessageBox.Show(appData.appFrageboegen[iIndexSelectedQuestionnaire].Fragen.Count.ToString(), "Anzahl Fragen",
                MessageBoxButton.OK);*/
            /*if (listBoxQuestionnaire.SelectedIndex > -1)
            {
                foreach (TmpListViewQuestion item in tmpQuestions)
                {
                    if (appData.appFrageboegen[iIndexSelectedQuestionnaire].isContaining(item.strName))
                        item.IsChecked = true;
                    else
                        item.IsChecked = false;
                }
                MessageBox.Show(iIndexSelectedQuestionnaire.ToString(), "Index", MessageBoxButton.OK);
            }*/
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            iIndexSelectedQuestion = listBoxQuestion.SelectedIndex;
            /*appData.appFrageboegen[iIndexSelectedQuestionnaire].Fragen.Add(appData.appFragen[iIndexSelectedQuestion]);
            saveData();*/
            MessageBox.Show(listBoxQuestion.SelectedIndex.ToString(), "Index checked", MessageBoxButton.OK);
            //MessageBox.Show(strings.DesignAddQuestion1, strings.DesignAddQuestion2, MessageBoxButton.OK);
        }

        private void ListBoxQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(e.AddedItems.IndexOf(e.AddedItems).ToString(), "Index", MessageBoxButton.OK);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            /*iIndexSelectedQuestion = listBoxQuestion.SelectedIndex;
            appData.appFrageboegen[iIndexSelectedQuestionnaire].Fragen.Remove(appData.appFragen[iIndexSelectedQuestion]);
            saveData();
            MessageBox.Show(strings.DesignRemoveQuestion1, strings.DesignRemoveQuestion2, MessageBoxButton.OK);*/
        }

        /*private void ListViewCatalog_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            listViewIncluded.SelectedItem = null;

            if (listViewCatalog.SelectedItem != null)
            {
                buttonDelQuestion.IsEnabled = true;
                buttonAddQuestion.IsEnabled = true;
                buttonRemoveQuestion.IsEnabled = false;
            }
            else
            {
                buttonDelQuestion.IsEnabled = false;
                buttonRemoveQuestion.IsEnabled = false;
            }
        }*/

        /*private void ButtonAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (listViewCatalog.SelectedItem != null)
            {
                tmpFragebogen.Fragen.Add(tmpFragen.ElementAt(listViewCatalog.SelectedIndex));
                MessageBox.Show(strings.DesignAddQuestion1 + "\n\n" + tmpFragen.ElementAt(listViewCatalog.SelectedIndex).strFragetext +
                    "\n\n" + strings.DesignAddQuestion2, strings.DesignAddQuestion3, MessageBoxButton.OK);
                tmpFragen.RemoveAt(listViewCatalog.SelectedIndex);
                refreshLists();
                buttonRemoveQuestion.IsEnabled = false;
                buttonAddQuestion.IsEnabled = false;
            }
        }*/

        /*private void ButtonRemoveQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (listViewIncluded.SelectedItem != null)
            {
                MessageBox.Show(strings.DesignRemoveQuestion1 + "\n\n" + appData.appFrageboegen[listappFragen.ElementAt(listViewIncluded.SelectedIndex).strFragetext +
                    "\n\n" + strings.DesignRemoveQuestion2, strings.DesignButtonRemoveQuestion, MessageBoxButton.OK);
                tmpFragebogen.Fragen.RemoveAt(listViewIncluded.SelectedIndex);
                tmpFragen.Add(appData.appFragen.ElementAt(listViewIncluded.SelectedIndex));
                refreshLists();
            }
        }*/

        /*private void ListViewIncluded_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            listViewCatalog.SelectedItem = null;

            if (listViewIncluded.SelectedItem != null)
            {
                buttonDelQuestion.IsEnabled = false;
                buttonAddQuestion.IsEnabled = false;
                buttonRemoveQuestion.IsEnabled = true;
            }
            else
            {
                buttonDelQuestion.IsEnabled = true;
            }
        }*/
    }
}
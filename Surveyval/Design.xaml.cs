﻿using System;
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
        private List<TmpListViewQuestion> tmpQuestions;

        public Design()
        {
            InitializeComponent();

            // initialize Control labels
            buttonCancel.Content = strings.DesignButtonCancel;
            buttonNewQuestion.Content = strings.DesignButtonNewQuestion;
            buttonDelQuestion.Content = strings.DesignButtonDelQuestion;
            /*buttonAddQuestion.Content = strings.DesignButtonAddQuestion;
            buttonRemoveQuestion.Content = strings.DesignButtonRemoveQuestion;*/
            buttonNewQuestionnaire.Content = strings.DesignButtonNewQuestionnaire;
            buttonDelQuestionnaire.Content = strings.DesignButtonDelQuestionnaire;
            groupBoxQuestionnaire.Header = strings.DesignGroupBoxQuestionnaireCatalog;
            groupBoxQuestion.Header = strings.DesignGroupBoxQuestionCatalog;
            /*((System.Windows.Controls.GridView)listViewIncluded.View).Columns[0].Header = strings.DesignListViewTextLabel;
            ((System.Windows.Controls.GridView)listViewCatalog.View).Columns[0].Header = strings.DesignListViewTextLabel;*/

            appData = new AppData();

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

            // Nachfolgendes muss in "Neuer Fragebogen"
            /*if (tmpFragebogen.strName.Length < 1)
            {
                int i = 1;
                Boolean bFound = false;

                if (appData.appFrageboegen.Count > 0)
                {
                    while (!bFound)
                    {
                        foreach (Fragebogen item in appData.appFrageboegen)
                        {
                            if (item.strName.Equals(strings.DesignNewQuestionnaireText + i))
                            {
                                i++;
                                break;
                            }

                            if (item.strName.Equals(appData.appFrageboegen[appData.appFrageboegen.Count - 1].strName))
                                bFound = true;
                        }
                    }
                }
                tmpFragebogen.strName = strings.DesignNewQuestionnaireText + i;
            }*/
            tmpQuestions = new List<TmpListViewQuestion>();

            foreach (Frage item in appData.appFragen)
                tmpQuestions.Add(new TmpListViewQuestion(item.strFragetext, false));

            // Initialize fields
            listBoxQuestionnaire.ItemsSource = appData.appFrageboegen;
            listBoxQuestion.ItemsSource = tmpQuestions;
            //refreshLists();
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
            listBoxQuestion.Items.Refresh();
            listBoxQuestionnaire.Items.Refresh();
            /*tmpFragen.Clear();

            foreach (Frage item in appData.appFragen)
            {
                if (!tmpFragebogen.isContaining(item))
                    tmpFragen.Add(item);
            }

            listViewIncluded.Items.Refresh();
            listViewCatalog.Items.Refresh();
            textBoxName.Text = tmpFragebogen.strName;*/

            // Spaltenbreite neu anpassen
            /*listViewIncluded.UpdateLayout();
            listViewCatalog.UpdateLayout();*/
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
            foreach (TmpListViewQuestion item in tmpQuestions)
            {
                if (appData.appFrageboegen[listBoxQuestionnaire.SelectedIndex].isContaining(item.strName))
                    item.IsChecked = true;
                else
                    item.IsChecked = false;
            }
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
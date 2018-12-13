﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            appData = new AppData();

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

            // Initialize fields
            textBoxName.Text = strings.DesignNewQuestionnaireText + i;
            tmpFragebogen.strName = textBoxName.Text;
            listViewIncluded.ItemsSource = tmpFragebogen.Fragen;
            listViewCatalog.ItemsSource = appData.appFragen;
            refreshLists();
        }

        private void refreshLists()
        {
            foreach (Frage item in appData.appFragen)
            {
                if (!tmpFragebogen.Fragen.Contains(item))
                    tmpFragen.Add(item);
            }

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
                appData.save();
                MessageBox.Show(strings.NewQuestionSaved2, strings.NewQuestionSaved1, MessageBoxButton.OK);
                refreshLists();
            }
        }

        private void ButtonDelQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (listViewCatalog.SelectedItem != null)
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
            }
        }

        private void ListViewCatalog_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
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
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            // <-- ToDo ÜBERARBEITEN  Was, wenn er vorher schon gespeichert wurde? -->

            if (tmpFragebogen.Fragen.Count < 1)
            {
                if (MessageBox.Show(strings.DesignSaveNoQuestions1 + "\n\n" + tmpFragebogen.strName + "\n\n" +
                    strings.DesignSaveNoQuestions2, strings.DesignSave, MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }

            foreach (Fragebogen item in appData.appFrageboegen)
            {
                if (item.strName.Equals(tmpFragebogen.strName))
                {
                    if (MessageBox.Show(strings.DesignSaveExists, strings.DesignSave, MessageBoxButton.YesNo) == MessageBoxResult.No)
                        return;

                    appData.appFrageboegen.Remove(item);

                    /*item.strName = tmpFragebogen.strName;
                    item.Fragen = tmpFragebogen.Fragen;
                    appData.save();
                    //bChanged = false;
                    MessageBox.Show(strings.DesignSaveNoQuestions1 + "\n\n" + tmpFragebogen.strName + "\n\n" +
                        strings.DesignSaveDone, strings.DesignSave, MessageBoxButton.OK);
                    return;*/
                }
            }

            appData.appFrageboegen.Add(tmpFragebogen);
            appData.save();
            //bChanged = false;
            MessageBox.Show(strings.DesignSaveNoQuestions1 + "\n\n" + tmpFragebogen.strName + "\n\n" +
                strings.DesignSaveDone, strings.DesignSave, MessageBoxButton.OK);
        }

        private void ButtonAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (listViewCatalog.SelectedItem != null)
            {
                tmpFragebogen.Fragen.Add(tmpFragen.ElementAt(listViewCatalog.SelectedIndex));
                MessageBox.Show(strings.DesignAddQuestion1 + "\n\n" + tmpFragen.ElementAt(listViewCatalog.SelectedIndex).strFragetext +
                    "\n\n" + strings.DesignAddQuestion2, strings.DesignAddQuestion3, MessageBoxButton.OK);
                tmpFragen.RemoveAt(listViewCatalog.SelectedIndex);
                refreshLists();
                buttonRemoveQuestion.IsEnabled = false;
            }
        }

        private void ButtonRemoveQuestion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBoxName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            tmpFragebogen.strName = textBoxName.Text;
        }
    }
}
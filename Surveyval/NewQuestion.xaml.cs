using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Surveyval
{
    /// <summary>
    /// Interaktionslogik für NewQuestion.xaml
    /// </summary>
    public partial class NewQuestion : Window
    {
        private Frage tmpFrage;

        public NewQuestion()
        {
            InitializeComponent();

            //Initialize controls
            Title = strings.NewQuestionTitle;
            labelText.Content = strings.NewQuestionLabelText;
            groupBoxAnswer.Header = strings.NewQuestionGroupBoxAnswer;
            radioButtonText.Content = strings.NewQuestionRadioText;
            radioButtonChoice.Content = strings.NewQuestionRadioChoice;
            buttonSave.Content = strings.NewQuestionButtonSave;
            buttonCancel.Content = strings.NewQuestionButtonCancel;

            //TextBox Focus
            textBoxText.Text = strings.NewQuestionTextBoxText;
            textBoxText.Focus();
            textBoxText.SelectAll();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBoxText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buttonSave != null)
            {
                if (textBoxText.Text != "" && textBoxText.Text != strings.NewQuestionTextBoxText)
                    buttonSave.IsEnabled = true;
                else
                    buttonSave.IsEnabled = false;
            }

        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            int i;
            if (radioButtonChoice.IsChecked == true)
                i = 0;
            else
                i = 1;

            tmpFrage = new Frage(textBoxText.Text, i);

            this.DialogResult = true;
            this.Close();
        }

        internal Frage getFrage()
        {
            return tmpFrage;
        }
    }
}

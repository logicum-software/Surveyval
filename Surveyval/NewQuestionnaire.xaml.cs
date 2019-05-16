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
    /// Interaktionslogik für NewQuestionnaire.xaml
    /// </summary>
    public partial class NewQuestionnaire : Window
    {
        public NewQuestionnaire()
        {
            InitializeComponent();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!textBoxName.Text.Equals(strings.NewQuestionnaireTextBoxName) && !textBoxName.Text.Equals(""))
            {
                buttonSave.IsEnabled = true;
            }
            else
                buttonSave.IsEnabled = false;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}

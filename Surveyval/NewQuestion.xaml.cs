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
        public NewQuestion()
        {
            InitializeComponent();
            this.Title = strings.NewQuestionTitle;
            labelText.Content = strings.NewQuestionLabelText;
            groupBoxAnswer.Content = strings.NewQuestionGroupBoxAnswer;
            radioButtonText.Content = strings.NewQuestionRadioText;
            radioButtonChoice.Content = strings.NewQuestionRadioChoice;
            buttonSave.Content = strings.NewQuestionButtonSave;
            buttonCancel.Content = strings.NewQuestionButtonCancel;
        }
    }
}

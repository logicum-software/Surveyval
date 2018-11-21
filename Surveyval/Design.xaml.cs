using System.Windows;

namespace Surveyval
{
    /// <summary>
    /// Interaktionslogik für Design.xaml
    /// </summary>
    public partial class Design : Window
    {
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
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

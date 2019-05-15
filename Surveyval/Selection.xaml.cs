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
            buttonDelete.Content = strings.SelectionButtonDelete;
            appData = new AppData();
            listViewSelect.ItemsSource = appData.appFrageboegen;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ListViewSelect_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            buttonLoad.IsEnabled = true;
            buttonDelete.IsEnabled = true;
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            Design dlgDesign = new Design();

            dlgDesign.Title = strings.DesignTitle;
            dlgDesign.ShowDialog();
            this.Close();
        }

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            Design dlgDesign = new Design();

            dlgDesign.Title = strings.DesignTitle;
            dlgDesign.ShowDialog();
            this.Close();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listViewSelect.SelectedItem != null)
            {
                if (MessageBox.Show(strings.SelectionDeleteText + "\n\n" + appData.appFrageboegen[listViewSelect.SelectedIndex].strName,
                    strings.SelectionDeleteTitle, MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;

                appData.appFrageboegen.RemoveAt(listViewSelect.SelectedIndex);
                appData.save();
                listViewSelect.Items.Refresh();
                MessageBox.Show(strings.SelectionDeleteDeleted, strings.SelectionDeleteTitle, MessageBoxButton.OK);
            }
        }
    }
}

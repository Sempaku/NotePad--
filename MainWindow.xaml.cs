using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
 



namespace LAB1SEM2_NOTEBOOK
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            fntFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            fntSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            appTheme.SelectedItem = thmLight;

            txtEditor.Focus();
        }

        private void CommandBinding_Executed_New(object sender, RoutedEventArgs e)
        {
            txtEditor.SelectAll();
            txtEditor.Selection.Text = "";
        }

        

        private void CommandBinding_Executed_Open(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)| *.txt";
            if(openFileDialog.ShowDialog() == true)
            {
                txtEditor.SelectAll();
                txtEditor.Selection.Text = "";
                txtEditor.AppendText(File.ReadAllText(openFileDialog.FileName));
            }
        }

        private void CommandBinding_Executed_Save(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";

            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, GetText(txtEditor));
            }
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;
        

        private void CommandBinding_CanExecute_1(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;
        

        private void CommandBinding_CanExecute_2(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;

        public void SetFontFamily(RichTextBox target, FontFamily value)
        {
            
            TextRange selectionTextRange = new TextRange(target.Selection.Start, target.Selection.End);
            selectionTextRange.ApplyPropertyValue(TextElement.FontFamilyProperty, value);
            if (fntSize.SelectedItem != null) { 
                double tempsize = (double)fntSize.SelectedItem;
                selectionTextRange.ApplyPropertyValue(TextElement.FontSizeProperty, tempsize);
            }

            target.Focus();
        }

     

        public static string GetText(RichTextBox rtb) //Get text
        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            string text = textRange.Text;
            return text;
        }
        


        private void txtEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = txtEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));

            temp = txtEditor.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));

            temp = txtEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

            temp = txtEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            fntFamily.SelectedItem = temp;
            temp = txtEditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
            fntSize.Text = temp.ToString();

        }

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e) {  //=> SetFontFamily(txtEditor, (FontFamily)fntFamily.SelectedItem);

            
            if(fntFamily.SelectedItem != null  )
            {
                txtEditor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, fntFamily.SelectedItem);

            }
            else if (fntFamily.SelectedItem != null & txtEditor.Selection.IsEmpty)
            {
                TextRange selectionTextRange = new TextRange(txtEditor.Selection.Start, txtEditor.Selection.End);
                selectionTextRange.ApplyPropertyValue(TextElement.FontFamilyProperty, fntFamily.SelectedItem);
            }

            txtEditor.Focus();
        }
        private void cmbFontSize_TextChanged(object sender, TextChangedEventArgs e) {  //=> SetFontSize(txtEditor, (double)fntSize.SelectedItem);

            try
            {
                if (fntSize.SelectedItem != null)
                {
                    txtEditor.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fntSize.Text);
                    
                }
                else if (fntSize.SelectedItem != null & txtEditor.Selection.IsEmpty)
                {
                    TextRange selectionTextRange = new TextRange(txtEditor.Selection.Start, txtEditor.Selection.End);
                    selectionTextRange.ApplyPropertyValue(TextElement.FontSizeProperty, fntSize.SelectedItem);
                }
                else if (fntSize.SelectedItem == null)
                {
                    double curText = Convert.ToDouble(fntSize.Text);
                    if (curText >= 1)
                        txtEditor.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fntSize.Text);
                        

                }
            }
            catch (Exception ex) { 
                
            }
            

        }

        private void mnInfo_Click(object sender, RoutedEventArgs e)
        {
            var infodlg = new Window1();
            infodlg.ShowDialog();
        }

        private void appTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (appTheme.SelectedItem == thmLight)
            {
                txtEditor.Background = new SolidColorBrush(Colors.White);
                txtEditor.Foreground = new SolidColorBrush(Colors.Black);

                menu.Background = new SolidColorBrush(Colors.LightGray);
            }
                
            else if (appTheme.SelectedItem == thmDark)
            {
                txtEditor.Background = new SolidColorBrush(Colors.Black);
                txtEditor.Foreground = new SolidColorBrush(Colors.WhiteSmoke);

                menu.Background = new SolidColorBrush(Colors.SlateGray);
                
            }
        }
        
    }


}

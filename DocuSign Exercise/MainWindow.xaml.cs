using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace DocuSign
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        // Run Button Click Event
        private void RunButtonClick(object sender, RoutedEventArgs e)
        {
            ExcecuteProgram();
        }

        // Input Text Field Enter Listener
        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                ExcecuteProgram();
            }
        }

        // Method that runs program logic
        private void ExcecuteProgram()
        {
            Controller controller = Controller.GetInstance();
            InlineCollection inlines = OutputTextBlock.Inlines;

            string input = InputTextBox.Text.Trim();
           
            InputTextBox.Clear();

            // If nothing on input, do nothing
            if (input.Length < 1)
                return;

            inlines.Add(new Bold(new Run("Input: ")));
            inlines.Add(input);
            
            if (controller.ValidateInput(input))
            {
                string output = controller.ProcessCommands(input);
                inlines.Add("\n");
                inlines.Add(new Bold(new Run("Output: ")));
                inlines.Add(output);

                // Check if Person never left home
                if (Controller.STATUS == BusinessObject.Enums.ControllerStatus.NEVER_LEFT)
                    inlines.Add(new Run(" - Never left house") { Foreground = Brushes.Red });
            }
            else
            {
                inlines.Add(new Run(" - Invalid Input") { Foreground = Brushes.Red });
                inlines.Add("\n");
                inlines.Add(new Bold(new Run("Output: ")));
                inlines.Add("fail");
            }

            inlines.Add("\n____________\n");

            MyScrollViewer.ScrollToEnd();
            InputTextBox.Focus();
        }
    }
}

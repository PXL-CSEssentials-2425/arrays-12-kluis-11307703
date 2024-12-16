using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kluis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly int[] secretCode = { 2, 3, 8, 4, 7, 9 }; 
        private StringBuilder inputCode = new StringBuilder();   
        private int attemptsLeft = 3;                           
        public MainWindow()
        {
            InitializeComponent();
        }


        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (attemptsLeft <= 0)
            {
                MessageBox.Show("Geen pogingen meer over!", "Pogingen Op", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Voeg het cijfer toe aan de ingevoerde code
            string number = ((System.Windows.Controls.Button)sender).Content.ToString();
            inputCode.Append(number);

            // Toon de ingevoerde cijfers met sterretjes
            UpdateInputDisplay();

            // Controleer of de code compleet is
            if (inputCode.Length == 6)
            {
                CheckCode();
            }
        }

        private void UpdateInputDisplay()
        {
            // Toon alleen het laatste ingevoerde cijfer en de rest als sterretjes
            string display = inputCode.ToString();
            if (display.Length > 1)
                display = new string('*', display.Length - 1) + display[^1];

            InputDisplay.Text = display;
        }

        private void CheckCode()
        {
            // Controleer of de ingevoerde code correct is
            bool correct = true;
            for (int i = 0; i < 6; i++)
            {
                if (inputCode[i] - '0' != secretCode[i])
                {
                    correct = false;
                    break;
                }
            }

            if (correct)
            {
                MessageBox.Show("Proficiat, u hebt de kluis geopend!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                ResetGame();
            }
            else
            {
                attemptsLeft--;
                if (attemptsLeft > 0)
                {
                    MessageBox.Show($"Foute ingave, nog {attemptsLeft} resterende pogingen.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Geen pogingen meer over! De kluis blijft gesloten.", "Vergrendeld", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                inputCode.Clear();
                UpdateInputDisplay();
            }
        }

        private void ResetGame()
        {
            inputCode.Clear();
            attemptsLeft = 3;
            UpdateInputDisplay();
        }

    }
}
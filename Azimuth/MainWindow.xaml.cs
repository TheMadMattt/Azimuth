using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Azimuth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AzimuthSystem _azimuthSystem;
        public MainWindow()
        {
            InitializeComponent();
            _azimuthSystem = new AzimuthSystem(); //inicjalizacja zmiennych
            InitControls();
        }

        private void BTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BLabel.Visibility = Visibility.Hidden; //ukrycie komunikatów o błędach
            var isNumber = float.TryParse(BTextBox.Text, out float result); //sprawdź czy podana wartość jest liczbą (próba zamiany na zmienny przecinek)
            _azimuthSystem.B = result; //przypisz liczbę do b

            ShowFloatError(BLabel, isNumber); //pokaż informację o błędzie

            if (string.IsNullOrEmpty(BTextBox.Text)) //jeśli b jest puste b=0
            {
                _azimuthSystem.B = 0;
            }

            if (isNumber) //jeśli b jest liczbą oblicz wszystkie wartości
            {
                CalculateR1();
                CalculateR2();
                CalculateD1();
                CalculateD2();
                CalculateDs();
                CalculateTetaPrim();
                CalculateQs();
            }
        }

        private void DaTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DaLabel.Visibility = Visibility.Hidden; //ukrycie komunikatów o błędach
            var isNumber = float.TryParse(DaTextBox.Text, out float result); //sprawdź czy podana wartość jest liczbą (próba zamiany na zmienny przecinek)
            _azimuthSystem.Da = result; //przypisz liczbę do Da

            ShowFloatError(DaLabel, isNumber);

            if (_azimuthSystem.Da > _azimuthSystem.Db) //jezeli Da jest wieksze
            {
                _azimuthSystem.Dm = _azimuthSystem.Db; //do Dm przypisz Db
            }
            else
            {
                _azimuthSystem.Dm = _azimuthSystem.Da; //jezeli Da jest mniejsze do Dm przypisz Da
            }

            if (isNumber)
            {
                CalculateTetaPrim();
                CalculateDs();
            }
        }

        private void DbTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DbLabel.Visibility = Visibility.Hidden; //ukrycie komunikatów o błędach
            var isNumber = float.TryParse(DbTextBox.Text, out float result); //sprawdź czy podana wartość jest liczbą (próba zamiany na zmienny przecinek)
            _azimuthSystem.Db = result; //przypisz liczbę do Da

            ShowFloatError(DbLabel, isNumber);

            if (_azimuthSystem.Da > _azimuthSystem.Db) //jezeli Da jest wieksze
            {
                _azimuthSystem.Dm = _azimuthSystem.Db; //do Dm przypisz Db
            }
            else
            {
                _azimuthSystem.Dm = _azimuthSystem.Da; //jezeli Da jest mniejsze do Dm przypisz Da
            }

            if (isNumber)
            {
                CalculateTetaPrim();
                CalculateDs();
            }
        }

        private void Teta1TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Teta1Label.Visibility = Visibility.Hidden; //ukrycie komunikatów o błędach
            var isNumber = int.TryParse(Teta1TextBox.Text, out int result); //sprawdź czy podana wartość jest liczbą (próba zamiany na liczbę całkowitą)
            _azimuthSystem.teta1 = result;

            if (_azimuthSystem.teta1 < 30 || _azimuthSystem.teta1 > 150) //sprawdzenie czy teta1 jest z zakresu <30; 150>
            {
                Teta1Label.Visibility = Visibility.Visible; //pokazanie informacji o błędnej liczbie
                Teta1Label.Foreground = Brushes.Red; //informacja w kolorze czerwonym
                Teta1Label.Content = "θ1 musi być z zakresu <30; 150>"; //ustawienie komunikatu
                return; //nie wykonuj dalej 
            }

            if (_azimuthSystem.teta1 >= _azimuthSystem.teta2) //sprawdzenie czy teta1 jest większa lub równa teta2
            {
                Teta1Label.Visibility = Visibility.Visible; //pokazanie informacji o błędnej liczbie
                Teta1Label.Foreground = Brushes.Red; //informacja w kolorze czerwonym
                Teta1Label.Content = "θ1 nie może być większa niż θ2"; //ustawienie komunikatu
                return;  //nie wykonuj dalej 
            }
            _azimuthSystem.teta = _azimuthSystem.teta1; //przypisanie teta1 do teta jezeli podano prawidlowa wartosc

            ShowIntegerError(Teta1Label, isNumber); //wyswietl komunikat o błedzie

            if (string.IsNullOrEmpty(Teta1TextBox.Text))
            {
                _azimuthSystem.teta1 = 0;
            }

            if (isNumber) //jeśli teta1 jest liczbą oblicz wszystkie wartości
            {
                CalculateR1();
                CalculateD1();
                CalculateDs();
                CalculateQs();
            }
        }

        private void Teta2TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Teta2Label.Visibility = Visibility.Hidden;
            var isNumber = int.TryParse(Teta2TextBox.Text, out int result);
            _azimuthSystem.teta2 = result;

            if (_azimuthSystem.teta2 < 30 || _azimuthSystem.teta2 > 150) //sprawdzenie czy teta2 jest z zakresu <30; 150>
            {
                Teta2Label.Visibility = Visibility.Visible; //pokazanie informacji o błędnej liczbie
                Teta2Label.Foreground = Brushes.Red; //informacja w kolorze czerwonym
                Teta2Label.Content = "θ1 musi być z zakresu <30; 150>"; //ustawienie komunikatu
                return;
            }

            if (_azimuthSystem.teta1 >= _azimuthSystem.teta2) //sprawdzenie czy teta1 jest większa lub równa teta2
            {
                Teta2Label.Visibility = Visibility.Visible; //pokazanie informacji o błędnej liczbie
                Teta2Label.Foreground = Brushes.Red; //informacja w kolorze czerwonym
                Teta2Label.Content = "θ2 nie może być mniejsza niż θ1"; //ustawienie komunikatu
                return;
            }

            ShowIntegerError(Teta2Label, isNumber);

            if (string.IsNullOrEmpty(Teta2TextBox.Text)) //sprawdzenie czy teta2 jest puste
            {
                _azimuthSystem.teta2 = 0;
            }

            if (isNumber) //jeśli teta2 jest liczbą oblicz wszystkie wartości
            {
                CalculateR2(); //wywołanie fukncji
                CalculateD2();
                CalculateQs();
            }
        }

        private void ShowIntegerError(Label sender, bool isNumber) //Wyświetlenie informacji o błędnej liczbie
        {
            if (!isNumber)
            {
                sender.Visibility = Visibility.Visible; //pokazanie informacji o błędnej liczbie
                sender.Foreground = Brushes.Red; //informacja w kolorze czerwonym
                sender.Content = "Podaj prawidłową liczbę całkowitą"; //ustawienie komunikatu
            }
        }

        private void ShowFloatError(Label sender, in bool isNumber) //Wyświetlenie informacji o błędnej liczbie
        {
            if (!isNumber)
            {
                sender.Visibility = Visibility.Visible; //pokazanie informacji o błędnej liczbie
                sender.Foreground = Brushes.Red; //informacja w kolorze czerwonym
                sender.Content = "Podaj prawidłową liczbę"; //ustawienie komunikatu
            }
        }

        private void InitControls() //inicjalizacjia kontrolek na widoku
        {
            BTextBox.Text = "" + _azimuthSystem.B;
            DaTextBox.Text = "" + _azimuthSystem.Da;
            DbTextBox.Text = "" + _azimuthSystem.Db;
            Teta1TextBox.Text = "" + _azimuthSystem.teta1;
            Teta2TextBox.Text = "" + _azimuthSystem.teta2;
        }

        private void CalculateR1()
        {
            if (!string.IsNullOrEmpty(Teta1TextBox.Text) && !string.IsNullOrEmpty(BTextBox.Text))
            {
                var sinus = DegreeToRadians(_azimuthSystem.teta1); //zamiana stopni na radiany
                _azimuthSystem.r1 = _azimuthSystem.B / (2 * Math.Sin(sinus)); //obliczenie r1
                R1TextBlock.Text = _azimuthSystem.r1.ToString("#0.00") + " Nm"; //przypisanie wartości do kontrolki na widoku
            }
        }

        private void CalculateR2()
        {
            if (!string.IsNullOrEmpty(Teta2TextBox.Text) && !string.IsNullOrEmpty(BTextBox.Text)) //sprawdzenie czy teta2 i b NIE SĄ puste (wykrzyknik oznacza zaprzeczenie)
            {
                var sinus = DegreeToRadians(_azimuthSystem.teta2); //zamiana stopni na radiany
                _azimuthSystem.r2 = _azimuthSystem.B / (2 * Math.Sin(sinus)); //obliczenie r2
                R2TextBlock.Text = _azimuthSystem.r2.ToString("#0.00") + " Nm"; //przypisanie wartości do kontrolki na widoku
            }
        }

        private void CalculateD1()
        {
            if (!string.IsNullOrEmpty(Teta1TextBox.Text) && !string.IsNullOrEmpty(BTextBox.Text))
            {
                var tan = DegreeToRadians(_azimuthSystem.teta1); //zamiana stopni na radiany
                _azimuthSystem.d1 = _azimuthSystem.B / (2 * Math.Tan(tan)); //obliczenie d1
                D1TextBlock.Text = _azimuthSystem.d1.ToString("#0.00") + " Nm"; //przypisanie wartości do kontrolki na widoku
            }
        }

        private void CalculateD2()
        {
            if (!string.IsNullOrEmpty(Teta2TextBox.Text) && !string.IsNullOrEmpty(BTextBox.Text))
            {
                var tan = DegreeToRadians(_azimuthSystem.teta2); //zamiana stopni na radiany
                _azimuthSystem.d2 = _azimuthSystem.B / (2 * Math.Tan(tan)); //obliczenie d2
                D2TextBlock.Text = _azimuthSystem.d2.ToString("#0.00") + " Nm"; //przypisanie wartości do kontrolki na widoku

            }
        }

        private void CalculateDs()
        {
            if (!string.IsNullOrEmpty(Teta1TextBox.Text) && !string.IsNullOrEmpty(BTextBox.Text))
            {
                double tan;
                if ((_azimuthSystem.tetaPrim / 2) <= (_azimuthSystem.teta / 2))
                {
                    tan = DegreeToRadians(_azimuthSystem.teta / 2); //zamiana stopni na radiany
                }
                else
                {
                    tan = DegreeToRadians(_azimuthSystem.tetaPrim / 2); //zamiana stopni na radiany
                }

                if (_azimuthSystem.Da <= 0) //jezeli Da jest mniejsze lub równe 0, weź teta (nie tetaPrim)
                {
                    tan = DegreeToRadians(_azimuthSystem.teta / 2);
                }

                if (_azimuthSystem.Db <= 0) //jezeli Db jest mniejsze lub równe 0, weź teta (nie tetaPrim)
                {
                    tan = DegreeToRadians(_azimuthSystem.teta / 2);
                }
                _azimuthSystem.Ds = _azimuthSystem.B / (2 * Math.Tan(tan)); //obliczenie d2
                DsTextBlock.Text = _azimuthSystem.Ds.ToString("#0.00") + " Nm"; //przypisanie wartości do kontrolki na widoku
            }
        }

        private void CalculateQs()
        {
            if (!string.IsNullOrEmpty(Teta1TextBox.Text) && !string.IsNullOrEmpty(Teta2TextBox.Text) &&
                _azimuthSystem.teta > 0)
            {
                var first = Math.PI * Math.Pow(_azimuthSystem.r1, 2);
                var second = 0.5 * Math.Pow(_azimuthSystem.r2, 2);
                var sinus = DegreeToRadians(_azimuthSystem.teta * 2);
                var third = (2 * DegreeToRadians(_azimuthSystem.teta)) - Math.Sin(sinus);

                _azimuthSystem.Qs = Math.Abs(first - (second * third));
                QsTextBlock.Text = _azimuthSystem.Qs.ToString("#0.00") + " Nm2";
            }
        }

        private void CalculateTetaPrim()
        {
            if (!string.IsNullOrEmpty(BTextBox.Text) && _azimuthSystem.Dm > 0)
            {
                var temp = RadiansToDegree(Math.Asin(_azimuthSystem.B / (2 * _azimuthSystem.Dm)));
                _azimuthSystem.tetaPrim = 2 * RadiansToDegree(Math.Asin(_azimuthSystem.B / (2 * _azimuthSystem.Dm)));
            }
        }

        private double DegreeToRadians(int angle) //zamiana stopni na radiany
        {
            return (Math.PI * angle) / 180.0;
        }

        private double DegreeToRadians(double angle) //zamiana stopni na radiany
        {
            return (Math.PI * angle) / 180.0;
        }

        private double RadiansToDegree(double radians)
        {
            return (180 / Math.PI) * radians;
        }
    }
}

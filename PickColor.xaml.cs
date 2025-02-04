using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GrafikaProjektMR
{
    public partial class PickColor : Window
    {
        // Właściwość przechowująca wybrany kolor
        public Color SelectedColor { get; private set; }

        public PickColor()
        {
            InitializeComponent();
            UpdateColorPreview(); // Aktualizacja podglądu koloru przy inicjalizacji
        }

        // Obsługa zmiany wartości suwaków
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sender == sliderR)
            {
                txtR.Text = sliderR.Value.ToString();
            }
            else if (sender == sliderG)
            {
                txtG.Text = sliderG.Value.ToString();
            }
            else if (sender == sliderB)
            {
                txtB.Text = sliderB.Value.ToString();
            }
            UpdateColorPreview(); // Aktualizacja podglądu koloru po zmianie wartości suwaka
        }

        // Obsługa zmiany wartości w polu tekstowym dla koloru R
        private void TxtR_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (byte.TryParse(txtR.Text, out byte value))
            {
                sliderR.Value = value;
            }
            else
            {
                txtR.Text = sliderR.Value.ToString();
            }
        }

        // Obsługa zmiany wartości w polu tekstowym dla koloru G
        private void TxtG_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (byte.TryParse(txtG.Text, out byte value))
            {
                sliderG.Value = value;
            }
            else
            {
                txtG.Text = sliderG.Value.ToString();
            }
        }

        // Obsługa zmiany wartości w polu tekstowym dla koloru B
        private void TxtB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (byte.TryParse(txtB.Text, out byte value))
            {
                sliderB.Value = value;
            }
            else
            {
                txtB.Text = sliderB.Value.ToString();
            }
        }

        // Aktualizacja podglądu koloru
        private void UpdateColorPreview()
        {
            SelectedColor = Color.FromRgb((byte)sliderR.Value, (byte)sliderG.Value, (byte)sliderB.Value);
            colorPreview.Fill = new SolidColorBrush(SelectedColor);
            UpdateHSVValues(); // Aktualizacja wartości HSV
        }

        // Aktualizacja wartości HSV
        private void UpdateHSVValues()
        {
            ColorToHSV(SelectedColor, out double h, out double s, out double v);
            txtH.Text = h.ToString("F2");
            txtS.Text = s.ToString("F2");
            txtV.Text = v.ToString("F2");
        }

        // Konwersja koloru RGB na HSV
        private void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        {
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));

            hue = 0;
            if (max == min)
            {
                hue = 0;
            }
            else if (max == r)
            {
                hue = (60 * ((g - b) / (max - min)) + 360) % 360;
            }
            else if (max == g)
            {
                hue = (60 * ((b - r) / (max - min)) + 120) % 360;
            }
            else if (max == b)
            {
                hue = (60 * ((r - g) / (max - min)) + 240) % 360;
            }

            saturation = (max == 0) ? 0 : (1 - (1 * min / max));
            value = max;
        }

        // Obsługa kliknięcia przycisku "OK"
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
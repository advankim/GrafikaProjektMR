using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Win32;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Windows.Media.Imaging;

namespace GrafikaProjektMR
{
    public partial class MainWindow : Window
    {
        // Zmienne do przechowywania stanu aplikacji
        private Point currentPoint = new Point();
        private Line previewLine = null;
        private Polyline currentPolyline = null;

        private int drawMode = 0;
        private bool isDrawing = false;
        private Brush currentBrush = Brushes.Black;
        private string selectedFilePath;
        private bool isDraggingImage = false;
        private Point imageClickPosition;
        private Image selectedImage;

        private bool isEditingLine = false;
        private Line selectedLine = null;
        private bool isMovingStartPoint = false;
        private bool isMovingEndPoint = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Ustawienie koloru pędzla
        public void SetColor(Color color)
        {
            currentBrush = new SolidColorBrush(color);
            whichColor.Fill = currentBrush;
        }

        // Obsługa kliknięcia przycisku "Rysowanie dowolne"
        private void btnDraw_Click(object sender, RoutedEventArgs e)
        {
            drawMode = 1;
        }

        // Obsługa kliknięcia przycisku "Rysuj punkty"
        private void btnPoints_Click(object sender, RoutedEventArgs e)
        {
            drawMode = 2;
        }

        // Obsługa kliknięcia przycisku "Rysuj linie"
        private void btnLines_Click(object sender, RoutedEventArgs e)
        {
            drawMode = 3;
        }

        // Obsługa kliknięcia przycisku "Rysuj wielokąt"
        private void drawPolygon_Click(object sender, RoutedEventArgs e)
        {
            drawMode = 4;
        }

        // Obsługa kliknięcia przycisku "Rysuj prostokąt"
        private void drawRectangle_Click(object sender, RoutedEventArgs e)
        {
            drawMode = 5;
        }

        // Obsługa kliknięcia przycisku "Rysuj koło"
        private void drawCircle_Click(object sender, RoutedEventArgs e)
        {
            drawMode = 6;
        }

        // Obsługa kliknięcia przycisku "Rysuj gwiazdę"
        private void drawStar_Click(object sender, RoutedEventArgs e)
        {
            drawMode = 7;
        }

        // Obsługa kliknięcia przycisku "Rysuj plus"
        private void drawPlus_Click(object sender, RoutedEventArgs e)
        {
            drawMode = 8;
        }

        // Obsługa kliknięcia przycisku "Rysuj trójkąt"
        private void drawTriangle_Click(object sender, RoutedEventArgs e)
        {
            drawMode = 11;
        }

        // Obsługa kliknięcia przycisku "Rysuj linię łamaną"
        private void brokenLine_Click(object sender, RoutedEventArgs e)
        {
            drawMode = 12;
        }

        // Obsługa kliknięcia przycisku "Edytuj linię"
        private void editLine_Click(object sender, RoutedEventArgs e)
        {
            drawMode = 13;
        }

        // Obsługa zdarzenia MouseDown na płótnie
        private void paintSurface_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                currentPoint = e.GetPosition(paintSurface);
            }
        }

        // Obsługa zdarzenia MouseMove na płótnie
        private void paintSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraggingImage)
            {
                Image_MouseMove(sender, e);
                return;
            }

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (drawMode == 1)
                {
                    DrawFreehand(e.GetPosition(paintSurface));
                }
                else if (drawMode == 14)
                {
                    EraseElement(e.GetPosition(paintSurface));
                }
            }
        }

        // Rysowanie dowolne
        private void DrawFreehand(Point position)
        {
            Line line = new Line
            {
                Stroke = currentBrush,
                X1 = currentPoint.X,
                Y1 = currentPoint.Y,
                X2 = position.X,
                Y2 = position.Y
            };

            currentPoint = position;
            paintSurface.Children.Add(line);
        }

        // Obsługa zdarzenia MouseRightButtonDown na płótnie
        private void paintSurface_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (previewLine != null)
            {
                this.MouseMove -= PreviewLine_MouseMove;
                previewLine = null;
            }

            isDrawing = false;
        }

        // Obsługa zdarzenia MouseLeftButtonDown na płótnie
        private void paintSurface_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (drawMode)
            {
                case 2:
                    DrawPoint(e.GetPosition(paintSurface));
                    break;
                case 3:
                    DrawLine(e.GetPosition(paintSurface));
                    break;
                case 4:
                    DrawPolygon(e.GetPosition(paintSurface));
                    break;
                case 5:
                    DrawRectangle(e.GetPosition(paintSurface));
                    break;
                case 6:
                    DrawCircle(e.GetPosition(paintSurface));
                    break;
                case 7:
                    DrawStar(e.GetPosition(paintSurface));
                    break;
                case 8:
                    DrawPlus(e.GetPosition(paintSurface));
                    break;
                case 11:
                    DrawTriangle(e.GetPosition(paintSurface));
                    break;
                case 12:
                    DrawBrokenLine(e.GetPosition(paintSurface));
                    break;
                case 13:
                    EditLine(e.GetPosition(paintSurface));
                    break;
                case 14:
                    EraseElement(e.GetPosition(paintSurface));
                    break;
            }
        }

        // Rysowanie punktu
        private void DrawPoint(Point position)
        {
            System.Windows.Shapes.Ellipse point = new System.Windows.Shapes.Ellipse
            {
                Width = 6,
                Height = 6,
                Fill = currentBrush
            };
            Canvas.SetLeft(point, position.X - point.Width / 2);
            Canvas.SetTop(point, position.Y - point.Height / 2);
            paintSurface.Children.Add(point);
        }

        // Rysowanie linii
        private void DrawLine(Point position)
        {
            if (isDrawing)
            {
                if (previewLine != null)
                {
                    previewLine.X2 = position.X;
                    previewLine.Y2 = position.Y;
                    previewLine = null;
                    this.MouseMove -= PreviewLine_MouseMove;
                }
                isDrawing = false;
            }
            else
            {
                currentPoint = position;
                previewLine = new Line
                {
                    Stroke = currentBrush,
                    X1 = currentPoint.X,
                    Y1 = currentPoint.Y,
                    X2 = currentPoint.X,
                    Y2 = currentPoint.Y
                };
                paintSurface.Children.Add(previewLine);
                this.MouseMove += PreviewLine_MouseMove;
                isDrawing = true;
            }
        }

        // Podgląd rysowanej linii
        private void PreviewLine_MouseMove(object sender, MouseEventArgs e)
        {
            if (previewLine != null)
            {
                Point position = e.GetPosition(paintSurface);
                previewLine.X2 = position.X;
                previewLine.Y2 = position.Y;
            }
        }

        // Rysowanie wielokąta
        private void DrawPolygon(Point position)
        {
            Polygon polygon = new Polygon
            {
                Stroke = currentBrush,
                Points = new System.Windows.Media.PointCollection
                {
                    new Point(position.X - 15, position.Y + 30),
                    new Point(position.X + 15, position.Y + 30),
                    new Point(position.X + 30, position.Y),
                    new Point(position.X + 15, position.Y - 30),
                    new Point(position.X - 15, position.Y - 30),
                    new Point(position.X - 30, position.Y)
                }
            };
            paintSurface.Children.Add(polygon);
        }

        // Rysowanie prostokąta
        private void DrawRectangle(Point position)
        {
            Rectangle rect = new Rectangle
            {
                Width = 30,
                Height = 15,
                Stroke = currentBrush
            };
            Canvas.SetLeft(rect, position.X - rect.Width / 2);
            Canvas.SetTop(rect, position.Y - rect.Height / 2);
            paintSurface.Children.Add(rect);
        }

        // Rysowanie koła
        private void DrawCircle(Point position)
        {
            System.Windows.Shapes.Ellipse circle = new System.Windows.Shapes.Ellipse
            {
                Width = 60,
                Height = 60,
                Stroke = currentBrush
            };
            Canvas.SetLeft(circle, position.X - circle.Width / 2);
            Canvas.SetTop(circle, position.Y - circle.Height / 2);
            paintSurface.Children.Add(circle);
        }

        // Rysowanie gwiazdy
        private void DrawStar(Point position)
        {
            Polygon star = new Polygon
            {
                Stroke = currentBrush,
                Points = new System.Windows.Media.PointCollection()
            };

            double outerRadius = 30;
            double innerRadius = 15;
            int numPoints = 10;

            for (int i = 0; i < numPoints; i++)
            {
                double angle = i * Math.PI / 5;
                double radius = (i % 2 == 0) ? outerRadius : innerRadius;
                double x = position.X + radius * Math.Sin(angle);
                double y = position.Y - radius * Math.Cos(angle);
                star.Points.Add(new Point(x, y));
            }

            paintSurface.Children.Add(star);
        }

        // Rysowanie plusa
        private void DrawPlus(Point position)
        {
            double lineLength = 30;

            Line line1 = new Line
            {
                Stroke = currentBrush,
                X1 = position.X + lineLength,
                Y1 = position.Y,
                X2 = position.X,
                Y2 = position.Y
            };

            Line line2 = new Line
            {
                Stroke = currentBrush,
                X1 = position.X,
                Y1 = position.Y + lineLength,
                X2 = position.X,
                Y2 = position.Y
            };

            Line line3 = new Line
            {
                Stroke = currentBrush,
                X1 = position.X - lineLength,
                Y1 = position.Y,
                X2 = position.X,
                Y2 = position.Y
            };

            Line line4 = new Line
            {
                Stroke = currentBrush,
                X1 = position.X,
                Y1 = position.Y - lineLength,
                X2 = position.X,
                Y2 = position.Y
            };

            paintSurface.Children.Add(line1);
            paintSurface.Children.Add(line2);
            paintSurface.Children.Add(line3);
            paintSurface.Children.Add(line4);
        }

        // Tworzenie trójkąta
        private Polygon CreateTriangle(double x, double y, double width, double height)
        {
            return new Polygon
            {
                Stroke = currentBrush,
                Points = new System.Windows.Media.PointCollection
                {
                    new Point(x, y - height),
                    new Point(x - width / 2, y),
                    new Point(x + width / 2, y)
                }
            };
        }

        // Rysowanie trójkąta
        private void DrawTriangle(Point position)
        {
            Polygon triangle = CreateTriangle(position.X, position.Y, 50, 50);
            paintSurface.Children.Add(triangle);
        }

        // Rysowanie linii łamanej
        private void DrawBrokenLine(Point position)
        {
            if (currentPolyline == null)
            {
                currentPolyline = new Polyline
                {
                    Stroke = currentBrush,
                    StrokeThickness = 1
                };
                paintSurface.Children.Add(currentPolyline);
            }
            currentPolyline.Points.Add(position);
        }

        // Edycja linii
        private void EditLine(Point position)
        {
            foreach (var element in paintSurface.Children)
            {
                if (element is Line line)
                {
                    if (IsPointNearLineStart(line, position))
                    {
                        selectedLine = line;
                        isMovingStartPoint = true;
                        paintSurface.MouseMove += paintSurface_MouseMove_EditLine;
                        paintSurface.MouseLeftButtonUp += paintSurface_MouseLeftButtonUp_EditLine;
                        break;
                    }
                    else if (IsPointNearLineEnd(line, position))
                    {
                        selectedLine = line;
                        isMovingEndPoint = true;
                        paintSurface.MouseMove += paintSurface_MouseMove_EditLine;
                        paintSurface.MouseLeftButtonUp += paintSurface_MouseLeftButtonUp_EditLine;
                        break;
                    }
                }
            }
        }

        // Obsługa zdarzenia MouseMove podczas edycji linii
        private void paintSurface_MouseMove_EditLine(object sender, MouseEventArgs e)
        {
            if (selectedLine != null)
            {
                Point position = e.GetPosition(paintSurface);
                if (isMovingStartPoint)
                {
                    selectedLine.X1 = position.X;
                    selectedLine.Y1 = position.Y;
                }
                else if (isMovingEndPoint)
                {
                    selectedLine.X2 = position.X;
                    selectedLine.Y2 = position.Y;
                }
            }
        }

        // Obsługa zdarzenia MouseLeftButtonUp podczas edycji linii
        private void paintSurface_MouseLeftButtonUp_EditLine(object sender, MouseButtonEventArgs e)
        {
            if (selectedLine != null)
            {
                isMovingStartPoint = false;
                isMovingEndPoint = false;
                paintSurface.MouseMove -= paintSurface_MouseMove_EditLine;
                paintSurface.MouseLeftButtonUp -= paintSurface_MouseLeftButtonUp_EditLine;
                selectedLine = null;
            }
        }

        // Obsługa zdarzenia KeyDown na płótnie
        private void paintSurface_KeyDown(object sender, KeyEventArgs e)
        {
            if (currentPolyline != null && e.Key == Key.Escape)
            {
                currentPolyline = null;
            }
        }

        // Sprawdzenie, czy punkt jest blisko początku linii
        private bool IsPointNearLineStart(Line line, Point point)
        {
            return GetDistance(line.X1, line.Y1, point.X, point.Y) < 10;
        }

        // Sprawdzenie, czy punkt jest blisko końca linii
        private bool IsPointNearLineEnd(Line line, Point point)
        {
            return GetDistance(line.X2, line.Y2, point.X, point.Y) < 10;
        }

        // Obliczanie odległości między dwoma punktami
        private double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        // Wyświetlanie wybranego obecnie koloru
        private void whichColor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PickColor colorPicker = new PickColor();
            if (colorPicker.ShowDialog() == true)
            {
                SetColor(colorPicker.SelectedColor);
            }
        }

        // Obsługa kliknięcia przycisku "Dodaj obraz"
        private void OnAddImageClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Obrazy (*.jpg;*.png)|*.jpg;*.png|Wszystkie pliki (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string filePath = openFileDialog.FileName;
                    BitmapImage bitmap = new BitmapImage(new Uri(filePath));

                    Image imageControl = new Image
                    {
                        Source = bitmap,
                        Width = 200,
                        Height = 200,
                        Tag = filePath
                    };

                    imageControl.MouseLeftButtonDown += OnImageClick;
                    imageControl.MouseLeftButtonDown += Image_MouseLeftButtonDown;
                    imageControl.MouseMove += Image_MouseMove;
                    imageControl.MouseLeftButtonUp += Image_MouseLeftButtonUp;

                    Canvas.SetLeft(imageControl, 50);
                    Canvas.SetTop(imageControl, 50);
                    paintSurface.Children.Add(imageControl);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd podczas wczytywania obrazu: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Obsługa zdarzenia MouseLeftButtonDown na obrazie
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image image)
            {
                isDraggingImage = true;
                selectedImage = image;
                imageClickPosition = e.GetPosition(paintSurface);
                image.CaptureMouse();
                e.Handled = true;
            }
        }

        // Obsługa zdarzenia MouseMove na obrazie
        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraggingImage && selectedImage != null)
            {
                Point currentPosition = e.GetPosition(paintSurface);
                double offsetX = currentPosition.X - imageClickPosition.X;
                double offsetY = currentPosition.Y - imageClickPosition.Y;

                double newLeft = Canvas.GetLeft(selectedImage) + offsetX;
                double newTop = Canvas.GetTop(selectedImage) + offsetY;

                Canvas.SetLeft(selectedImage, newLeft);
                Canvas.SetTop(selectedImage, newTop);

                imageClickPosition = currentPosition;
            }
        }

        // Obsługa zdarzenia MouseLeftButtonUp na obrazie
        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDraggingImage && selectedImage != null)
            {
                isDraggingImage = false;
                selectedImage.ReleaseMouseCapture();
                selectedImage = null;
            }
        }

        // Obsługa kliknięcia na obraz
        private void OnImageClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image clickedImage && clickedImage.Tag is string filePath)
            {
                selectedFilePath = filePath;
            }
        }

        // Obsługa kliknięcia przycisku "Zastosuj filtr dolnoprzepustowy"
        private void OnApplyLowPassFilterClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedFilePath))
            {
                try
                {
                    Image<Bgr, byte> img = new Image<Bgr, byte>(selectedFilePath);

                    // Zastosowanie filtra dolnoprzepustowego
                    Image<Bgr, byte> lowPassImg = img.SmoothBlur(10, 10);

                    CvInvoke.Imshow("Obraz po filtracji dolnoprzepustowej", lowPassImg);
                    CvInvoke.WaitKey(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd podczas przetwarzania obrazu: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Nie wybrano obrazu. Kliknij na obraz przed zastosowaniem filtra.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Obsługa kliknięcia przycisku "Zastosuj macierz filtracji"
        private void OnApplyFilterMatrixClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedFilePath))
            {
                try
                {
                    Image<Bgr, byte> img = new Image<Bgr, byte>(selectedFilePath);

                    // Przykładowa macierz filtracji
                    float[,] kernel = new float[,]
                    {
                        { -1, -1, -1 },
                        { -1, 9, -1 },
                        { -1, -1, -1 }
                    };

                    ConvolutionKernelF convolutionKernel = new ConvolutionKernelF(kernel);
                    Image<Bgr, float> filteredImg = img.Convolution(convolutionKernel);

                    CvInvoke.Imshow("Obraz po filtracji macierzowej", filteredImg);
                    CvInvoke.WaitKey(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd podczas przetwarzania obrazu: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Nie wybrano obrazu. Kliknij na obraz przed zastosowaniem filtra.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Obsługa kliknięcia przycisku "Zapisz jako..."
        private void OnSaveAsClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Obraz PNG|*.png|Obraz JPEG|*.jpg"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                SaveCanvasToFile(filePath);
            }
        }

        // Zapisanie zawartości płótna do pliku
        private void SaveCanvasToFile(string filePath)
        {
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)paintSurface.ActualWidth, (int)paintSurface.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
            renderBitmap.Render(paintSurface);

            BitmapEncoder encoder;
            if (filePath.EndsWith(".png"))
            {
                encoder = new PngBitmapEncoder();
            }
            else
            {
                encoder = new JpegBitmapEncoder();
            }

            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }

        // Obsługa kliknięcia przycisku "Gumka"
        private void OnEraserClick(object sender, RoutedEventArgs e)
        {
            drawMode = 14; // Ustawienie trybu gumki
        }

        // Usuwanie elementów z płótna
        private void EraseElement(Point position)
        {
            double eraserSize = 20; // Rozmiar gumki
            Rect eraserRect = new Rect(position.X - eraserSize / 2, position.Y - eraserSize / 2, eraserSize, eraserSize);

            List<UIElement> elementsToRemove = new List<UIElement>();

            foreach (UIElement element in paintSurface.Children)
            {
                if (element is Line line)
                {
                    if (IsLineIntersectingWithRect(line, eraserRect))
                    {
                        elementsToRemove.Add(element);
                    }
                }
                else if (element is Polyline polyline)
                {
                    if (IsPolylineIntersectingWithRect(polyline, eraserRect))
                    {
                        elementsToRemove.Add(element);
                    }
                }
                else if (element is Shape shape)
                {
                    Rect shapeBounds = new Rect(Canvas.GetLeft(shape), Canvas.GetTop(shape), shape.Width, shape.Height);
                    if (eraserRect.IntersectsWith(shapeBounds))
                    {
                        elementsToRemove.Add(element);
                    }
                }
            }

            foreach (UIElement element in elementsToRemove)
            {
                paintSurface.Children.Remove(element);
            }
        }

        // Sprawdzenie, czy linia przecina prostokąt
        private bool IsLineIntersectingWithRect(Line line, Rect rect)
        {
            Point p1 = new Point(line.X1, line.Y1);
            Point p2 = new Point(line.X2, line.Y2);

            return rect.Contains(p1) || rect.Contains(p2) || LineIntersectsRect(p1, p2, rect);
        }

        // Sprawdzenie, czy linia łamana przecina prostokąt
        private bool IsPolylineIntersectingWithRect(Polyline polyline, Rect rect)
        {
            for (int i = 0; i < polyline.Points.Count - 1; i++)
            {
                Point p1 = polyline.Points[i];
                Point p2 = polyline.Points[i + 1];

                if (rect.Contains(p1) || rect.Contains(p2) || LineIntersectsRect(p1, p2, rect))
                {
                    return true;
                }
            }
            return false;
        }

        // Sprawdzenie, czy linia przecina prostokąt
        private bool LineIntersectsRect(Point p1, Point p2, Rect rect)
        {
            return LineIntersectsLine(p1, p2, new Point(rect.Left, rect.Top), new Point(rect.Right, rect.Top)) ||
                   LineIntersectsLine(p1, p2, new Point(rect.Right, rect.Top), new Point(rect.Right, rect.Bottom)) ||
                   LineIntersectsLine(p1, p2, new Point(rect.Right, rect.Bottom), new Point(rect.Left, rect.Bottom)) ||
                   LineIntersectsLine(p1, p2, new Point(rect.Left, rect.Bottom), new Point(rect.Left, rect.Top));
        }

        // Sprawdzenie, czy dwie linie się przecinają
        private bool LineIntersectsLine(Point p1, Point p2, Point p3, Point p4)
        {
            double denominator = (p4.Y - p3.Y) * (p2.X - p1.X) - (p4.X - p3.X) * (p2.Y - p1.Y);
            if (denominator == 0)
            {
                return false;
            }

            double ua = ((p4.X - p3.X) * (p1.Y - p3.Y) - (p4.Y - p3.Y) * (p1.X - p3.X)) / denominator;
            double ub = ((p2.X - p1.X) * (p1.Y - p3.Y) - (p2.Y - p1.Y) * (p1.X - p3.X)) / denominator;

            return (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1);
        }

        // Obsługa kliknięcia przycisku "Przezroczystość"
        private void OnTransparencyClick(object sender, RoutedEventArgs e)
        {
            if (currentBrush is SolidColorBrush solidColorBrush)
            {
                currentBrush = new SolidColorBrush(Color.FromArgb(128, solidColorBrush.Color.R, solidColorBrush.Color.G, solidColorBrush.Color.B));
                whichColor.Fill = currentBrush;
            }
        }

        // Obsługa zmiany wartości suwaka przezroczystości
        private void OnTransparencySliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (currentBrush is SolidColorBrush solidColorBrush)
            {
                byte alpha = (byte)(255 * (e.NewValue / 100));
                currentBrush = new SolidColorBrush(Color.FromArgb(alpha, solidColorBrush.Color.R, solidColorBrush.Color.G, solidColorBrush.Color.B));
                whichColor.Fill = currentBrush;
            }
        }
    }
}
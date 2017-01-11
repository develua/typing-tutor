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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab_work_2_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rand = new Random();
        string alphabet;
        bool engLengOn = true;
        bool numbersOn = false;
        bool bigLettersOn = false;
        int indexLetters = 0;
        List<StackPanel> keysStackPanel = new List<StackPanel>();
        List<string[]> strKeysLines = new List<string[]>();
        List<int[]> keyCodeList = new List<int[]>();
        DataKeyDownTime dataKeyDownTime = new DataKeyDownTime();

        public MainWindow()
        {
            InitializeComponent();

            strKeysLines.Add(new string[] { "`1234567890-=", "~!@#$%^&*()_+", "ё1234567890-=", "Ё!\"№;%:?*()_+" });
            strKeysLines.Add(new string[] { "qwertyuiop[]\\", "QWERTYUIOP[]\\", "йцукенгшщзхъ\\", "ЙЦУКЕНГШЩЗХЪ\\" });
            strKeysLines.Add(new string[] { "asdfghjkl;'", "ASDFGHJKL;'", "фывапролджэ", "ФЫВАПРОЛДЖЭ" });
            strKeysLines.Add(new string[] { "zxcvbnm,./", "ZXCVBNM,./", "ячсмитьбю.", "ЯЧСМИТЬБЮ." });

            keyCodeList.Add(new int[] { 146, 35, 36, 37, 38, 39, 40, 41, 42, 43, 34, 143, 141, 2 });
            keyCodeList.Add(new int[] { 3, 60, 66, 48, 61, 63, 68, 64, 52, 58, 59, 149, 151, 150 });
            keyCodeList.Add(new int[] { 8, 44, 62, 47, 49, 50, 51, 53, 54, 55, 140, 152, 6 });
            keyCodeList.Add(new int[] { 116, 69, 67, 46, 65, 45, 57, 56, 142, 144, 145, 117 });
            keyCodeList.Add(new int[] { 118, 70, 156, 18, 156, 70, 119, 118 });

            keysStackPanel.Add(KeyLine1);
            keysStackPanel.Add(KeyLine2);
            keysStackPanel.Add(KeyLine3);
            keysStackPanel.Add(KeyLine4);
            keysStackPanel.Add(KeyLine5);

            LoadBgImage();
            LoadLettersInAlphabet();
            LoadKeyboard();
        }

        // Методы...

        private void LoadBgImage()
        {
            BgImage.Source = new BitmapImage(new Uri("Wallpapers/Wallpaper0" + rand.Next(1, 10) + ".jpg", UriKind.Relative));
        }

        private void LoadLettersInAlphabet()
        {
            if(engLengOn)
            {
                alphabet = "qwertyuiopasdfghjklzxcvbnm";

                if (bigLettersOn)
                    alphabet += "QWERTYUIOPASDFGHJKLZXCVBNM";
            }
            else
            {
                alphabet = "йцукенгшщзхъфывапролджэячсмитьбю";

                if (bigLettersOn)
                    alphabet += "ЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ";
            }

            if(numbersOn)
                alphabet += "1234567890";
        }

        private void FillAlphabet()
        {
            int maxLength = (int)(OutputTextBlock.Parent as Border).ActualWidth / 40;

            if (OutputTextBlock.Children.Count != 0)
                maxLength -= OutputTextBlock.Children.Count;

            for (int i = 0; i < maxLength; i++)
            {
                if (rand.Next(0, 4) != 0)
                    AddNewTextBlockElement(OutputTextBlock, Brushes.PapayaWhip, alphabet[rand.Next(0, alphabet.Length)].ToString());
                else
                {
                    if (i >= 1)
                        if ((OutputTextBlock.Children[i - 1] as TextBlock).Text == " ")
                        {
                            i--;
                            continue;
                        }

                    AddNewTextBlockElement(OutputTextBlock, Brushes.PapayaWhip, " ");
                }
            }
        }

        private void LoadOutputTextBlock()
        {
            OutputTextBlock.Children.Clear();
            EnterTextBlock.Children.Clear();
            FillAlphabet();
        }

        private Border NewKeyInBoard(int width, int fontSize, string symbols)
        {
            Border border = new Border();
            border.Background = Brushes.Gray;
            border.Width = width;
            border.Height = 40;
            border.Margin = new Thickness(3);
            border.CornerRadius = new CornerRadius(5);
            Label label = new Label();
            label.Content = symbols;
            label.FontSize = fontSize;
            label.FontFamily = new FontFamily("Lucida");
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Bottom;
            border.Child = label;
            return border;
        }

        private void LoadKeyboard()
        {
            KeyLine2.Children.Add(NewKeyInBoard(70, 28, ((char)8633).ToString()));
            KeyLine3.Children.Add(NewKeyInBoard(95, 18, "Caps Lock"));
            KeyLine4.Children.Add(NewKeyInBoard(120, 20, "Shift"));

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < strKeysLines[i][0].Length; j++)
                    keysStackPanel[i].Children.Add(NewKeyInBoard(44, 24, strKeysLines[i][indexLetters][j].ToString()));

            KeyLine1.Children.Add(NewKeyInBoard(70, 28, ((char)8592).ToString()));
            KeyLine3.Children.Add(NewKeyInBoard(69, 28, ((char)8626).ToString()));
            KeyLine4.Children.Add(NewKeyInBoard(94, 20, "Shift"));

            KeyLine5.Children.Add(NewKeyInBoard(60, 20, "Ctrl"));
            KeyLine5.Children.Add(NewKeyInBoard(60, 20, "Win"));
            KeyLine5.Children.Add(NewKeyInBoard(60, 20, "Alt"));
            KeyLine5.Children.Add(NewKeyInBoard(324, 20, "Space"));
            KeyLine5.Children.Add(NewKeyInBoard(60, 20, "Alt"));
            KeyLine5.Children.Add(NewKeyInBoard(60, 20, "Win"));
            KeyLine5.Children.Add(NewKeyInBoard(60, 20, "Ctrl"));
        }

        private void UpdateKeyboard()
        {
            int plusNum;

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < strKeysLines[i][0].Length; j++)
                {
                    plusNum = (i == 0) ? 0 : 1;
                    Border border = (Border)keysStackPanel[i].Children[j + plusNum];
                    Label label = (Label)border.Child;
                    label.Content = strKeysLines[i][indexLetters][j];
                }
        }

        private void BacklightKey(int keyCode, Brush brush)
        {
            for (int i = 0; i < keyCodeList.Count; i++)
                for (int j = 0; j < keyCodeList[i].Length; j++)
                {
                    if (keyCodeList[i][j] == 0)
                        break;

                    if (keyCode == keyCodeList[i][j])
                    {
                        if (keyCode != 156 && keyCode != 70)
                            (keysStackPanel[i].Children[j] as Border).Background = brush;
                        else
                        {
                            int num1 = (keyCode == 156) ? 2 : 1;
                            int num2 = (keyCode == 156) ? 4 : 5;

                            (keysStackPanel[4].Children[num1] as Border).Background = brush;
                            (keysStackPanel[4].Children[num2] as Border).Background = brush;
                        }

                        return;
                    }
                }
        }

        private void AddNewTextBlockElement(StackPanel stackPanel, Brush brush, string str)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.FontWeight = FontWeights.DemiBold;
            textBlock.Width = 35;
            textBlock.Margin = new Thickness(2);
            textBlock.Background = brush;
            textBlock.FontSize = 32;
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.Padding = new Thickness(0,5,0,0);
            textBlock.FontFamily = new FontFamily("Lucida");
            textBlock.Text = str;
            stackPanel.Children.Add(textBlock);
        }

        private void AddNewSymbolToTextBlock(int keyCode)
        {
            if (OutputTextBlock.Children.Count > EnterTextBlock.Children.Count || (EnterTextBlock.Children[EnterTextBlock.Children.Count - 1] as TextBlock).Background != Brushes.PapayaWhip)
                for (int i = 0; i < keyCodeList.Count; i++)
                    for (int j = 0; j < keyCodeList[i].Length; j++)
                        if (keyCode == keyCodeList[i][j])
                        {
                            Border border = (Border)keysStackPanel[i].Children[j];
                            Label label = (Label)border.Child;
                            string keySymbol = label.Content.ToString();

                            if (keySymbol.Length == 1 && keySymbol != ((char)8633).ToString() && keySymbol != ((char)8592).ToString() && keySymbol != ((char)8626).ToString() || keySymbol == "Space")
                            {
                                if (CountResultBlock.Visibility == Visibility.Visible)
                                    CountResultBlock.Visibility = Visibility.Collapsed;

                                if (keySymbol == "Space")
                                    keySymbol = " ";

                                if (EnterTextBlock.Children.Count > 0)
                                {
                                    TextBlock textBlock = (TextBlock)EnterTextBlock.Children[EnterTextBlock.Children.Count - 1];

                                    if (textBlock.Background == Brushes.IndianRed)
                                    {
                                        textBlock.Text = keySymbol;

                                        if ((OutputTextBlock.Children[EnterTextBlock.Children.Count - 1] as TextBlock).Text == keySymbol)
                                        {
                                            dataKeyDownTime.NewKeyDownInList();
                                            textBlock.Background = Brushes.PapayaWhip;

                                            if (OutputTextBlock.Children.Count == EnterTextBlock.Children.Count)
                                                CountResult();
                                        }
                                        else
                                            dataKeyDownTime.Errors++;

                                        return;
                                    }
                                }

                                Brush brush;

                                if ((OutputTextBlock.Children[EnterTextBlock.Children.Count] as TextBlock).Text == keySymbol)
                                {
                                    brush = Brushes.PapayaWhip;
                                    dataKeyDownTime.NewKeyDownInList();
                                }
                                else
                                {
                                    dataKeyDownTime.Errors++;
                                    brush = Brushes.IndianRed;
                                }

                                AddNewTextBlockElement(EnterTextBlock, brush, keySymbol);

                                if (OutputTextBlock.Children.Count == EnterTextBlock.Children.Count && (EnterTextBlock.Children[EnterTextBlock.Children.Count - 1] as TextBlock).Background == Brushes.PapayaWhip)
                                    CountResult();
                            }

                            return;
                        }
        }

        private void CountResult()
        {
            OutputTextBlock.Children.Clear();
            EnterTextBlock.Children.Clear();
            CountResultBlock.Visibility = Visibility.Visible;

            dataKeyDownTime.CountResult();
            Mastery.Text = dataKeyDownTime.Mastery + " сим./мин.";
            Errors.Text = dataKeyDownTime.Errors + " (" + dataKeyDownTime.ErrorsPercentage + "  %)";
            dataKeyDownTime.Clear();

            LoadBgImage();
            LoadOutputTextBlock();
        }

        // События...

        private void MyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadOutputTextBlock();
        }

        private void EngImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (engLengOn == false)
            {
                engLengOn = true;
                EngImage.Opacity = 1.0;
                RusImage.Opacity = 0.6;
                LoadLettersInAlphabet();
                LoadOutputTextBlock();
                indexLetters = 0;
                UpdateKeyboard();
            }
        }

        private void RusImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (engLengOn == true)
            {
                engLengOn = false;
                EngImage.Opacity = 0.6;
                RusImage.Opacity = 1.0;
                LoadLettersInAlphabet();
                LoadOutputTextBlock();
                indexLetters = 2;
                UpdateKeyboard();
            }
        }

        private void SettingsImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border border = (Border)(e.OriginalSource as Image).Parent;

            if (border.Opacity == 0.6)
                border.Opacity = 1.0;
            else
                border.Opacity = 0.6;

            if ((e.OriginalSource as Image) == LettersImage)
                bigLettersOn = !bigLettersOn;
            else if ((e.OriginalSource as Image) == NumbersImage)
                numbersOn = !numbersOn;

            LoadLettersInAlphabet();
            LoadOutputTextBlock();
        }

        private void MyWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (IsActive)
            {
                int maxLength = (int)(OutputTextBlock.Parent as Border).ActualWidth / 40;

                if (maxLength > OutputTextBlock.Children.Count)
                    FillAlphabet();
                else if (maxLength < OutputTextBlock.Children.Count)
                    OutputTextBlock.Children.RemoveRange(maxLength, OutputTextBlock.Children.Count - maxLength);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;

            if(!e.IsRepeat)
            {
                int keyCode = (int)e.Key;

                if (keyCode == 116 || keyCode == 117)
                {
                    if (indexLetters % 2 == 0)
                        indexLetters += 1;

                    UpdateKeyboard();
                }
                
                AddNewSymbolToTextBlock(keyCode);
                BacklightKey(keyCode, Brushes.GreenYellow);
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            int keyCode = (int)e.Key;
            e.Handled = true;

            if (keyCode == 116 || keyCode == 117)
            {
                if (indexLetters % 2 != 0)
                    indexLetters -= 1;

                UpdateKeyboard();
            }

            BacklightKey((int)e.Key, Brushes.Gray);
        }

    }

    class DataKeyDownTime
    {
        List<DateTime> timeList;
        public int Mastery { get; private set; }
        public int Errors { get; set; }
        public int ErrorsPercentage { get; private set; }

        public DataKeyDownTime()
        {
            timeList = new List<DateTime>();
        }

        public void NewKeyDownInList()
        {
            timeList.Add(DateTime.Now);
        }

        public void CountResult()
        {
            TimeSpan timeSpan = timeList.Last() - timeList[0];
            Mastery = (int)(60 / (double)timeSpan.TotalSeconds) * timeList.Count;

            ErrorsPercentage = (int)(Errors * 100.0) / timeList.Count;
        }

        public void Clear()
        {
            timeList.Clear();
            Errors = 0;
        }
    }
}

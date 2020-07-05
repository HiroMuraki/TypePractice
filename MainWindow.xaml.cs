using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace TypePracticeLite {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        #region 字段
        private bool? enableUpperAlpha {
            get {
                return this.toggleUpperAlpha.IsChecked;
            }
            set {
                this.toggleUpperAlpha.IsChecked = value;
            }
        }
        private bool? enableLowerAlpha {
            get {
                return this.toggleLowerAlpha.IsChecked;
            }
            set {
                this.toggleLowerAlpha.IsChecked = value;
            }
        }
        private bool? enableDigits {
            get {
                return this.toggleDigits.IsChecked;
            }
            set {
                this.toggleDigits.IsChecked = value;
            }
        }
        private bool? enableSymbols {
            get {
                return this.toggleSymbols.IsChecked;
            }
            set {
                this.toggleSymbols.IsChecked = value;
            }
        }
        public int practiceLength {
            get {
                return (int)this.sliderPracticeLength.Value;
            }
            set {
                this.sliderPracticeLength.Value = value;
            }
        }
        private string inputString {
            get {
                return this.txtInputString.Text;
            }
            set {
                this.txtInputString.Text = value;
            }
        }
        private string practiceString {
            get {
                return this.txtPracticeString.Text;
            }
            set {
                this.txtPracticeString.Text = value;
            }
        }
        bool?[] typeSettings;
        private int timerCount = 0;
        private DispatcherTimer timer = new DispatcherTimer();
        private SolidColorBrush colorTypeReady = new SolidColorBrush(Color.FromRgb(0xf9, 0xc5, 0x3a));
        private SolidColorBrush colorTypeStatic = new SolidColorBrush(Color.FromRgb(0x15, 0xb9, 0x79));
        private SolidColorBrush colorTyping = new SolidColorBrush(Color.FromRgb(0xbd, 0x2f, 0x54));
        #endregion
        public MainWindow() {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(Timer_Tick);
            this.practiceLength = 50;
        }
        private void sliderPracticeLength_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            this.lblPracticeLengthCover.Opacity = practiceLength / this.sliderPracticeLength.Maximum;
            GeneralString();
        }
        private void txtInputString_TextChanged(object sender, TextChangedEventArgs e) {
            if (timer.IsEnabled == false) {
                this.lblStatusIndicator.IsChecked = true;
                timer.Start();
            }
            if (inputString.Length >= practiceString.Length && practiceLength > 0) {
                timer.Stop();
                this.txtInputString.IsReadOnly = true;
                this.lblStatusIndicator.IsChecked = false;
                string stars = TypePricatice.GetStars(typeSettings, practiceString, inputString, timerCount);
                this.lblStarsLevel.Content = stars;
                timerCount = 0;
            }
        }
        private void Window_Move(object sender, MouseButtonEventArgs e) {
            this.DragMove();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e) {
            switch (e.Key) {
                case Key.F1:
                    this.enableUpperAlpha = SwitchToggle(enableUpperAlpha);
                    break;
                case Key.F2:
                    this.enableLowerAlpha = SwitchToggle(enableLowerAlpha);
                    break;
                case Key.F3:
                    this.enableDigits = SwitchToggle(enableDigits);
                    break;
                case Key.F4:
                    this.enableSymbols = SwitchToggle(enableSymbols);
                    break;
                case Key.Enter:
                    GeneralString();
                    break;
                case Key.Escape:
                    Application.Current.Shutdown();
                    break;
                default:
                    break;
            }
        }
        private void Window_MouseWheel(object sender, MouseWheelEventArgs e) {
            if (e.Delta > 0) {
                practiceLength += 1;
            } else if (e.Delta < 0) {
                practiceLength -= 1;
            }
        }
    }

    public partial class MainWindow : Window {
        private void Timer_Tick(object sender, EventArgs e) {
            this.lblUsingTime.Content = timerCount;
            ++timerCount;
        }
        private void GeneralString() {
            typeSettings = new bool?[4] {
                enableUpperAlpha,
                enableLowerAlpha,
                enableDigits,
                enableSymbols
            };
            this.practiceString = TypePricatice.GeneratePracticeString(typeSettings, practiceLength);
            ResetStaticstic();
        }
        private bool? SwitchToggle(bool? set) {
            if (set == true) {
                return false;
            } else if (set == false) {
                return true;
            } else {
                return null;
            }
        }
        private void ResetStaticstic() {
            timer.Stop();
            timerCount = 0;
            this.lblUsingTime.Content = 0;
            this.txtInputString.IsReadOnly = false;
            this.lblStatusIndicator.IsChecked = false;
            this.txtInputString.Text = "";
            this.lblStarsLevel.Content = "";
        }
    }
}

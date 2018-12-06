using System;
using System.Collections.ObjectModel;
using System.Text;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Calculatre
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            InitializeFrostedGlass(backboard);
            this.DataContext = this;
        }

        public ObservableCollection<Calculation> History { get; set; }

        Calculate mycalculator = new Calculate();
        StringBuilder my_formula = new StringBuilder("");
        // 用于删除字符时的间接量
        string temp;
        // 用于记录输入
        string[] last = new string[50];
        int count = 0;

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync("Cal-Record.txt");
            string content = await FileIO.ReadTextAsync(file);
            History = JsonHelper.DeserializeJsonToObject<ObservableCollection<Calculation>>(content);
            if (History == null)
                History = new ObservableCollection<Calculation>();
        }

        private void InitializeFrostedGlass(UIElement glassHost)
        {
            Visual hostVisual = ElementCompositionPreview.GetElementVisual(glassHost);
            Compositor compositor = hostVisual.Compositor;
            var backdropBrush = compositor.CreateHostBackdropBrush();
            var glassVisual = compositor.CreateSpriteVisual();
            glassVisual.Brush = backdropBrush;
            ElementCompositionPreview.SetElementChildVisual(glassHost, glassVisual);
            var bindSizeAnimation = compositor.CreateExpressionAnimation("hostVisual.Size");
            bindSizeAnimation.SetReferenceParameter("hostVisual", hostVisual);
            glassVisual.StartAnimation("Size", bindSizeAnimation);
        }

        #region 计算器操作

        private void State_Toggled(object sender, RoutedEventArgs e)
        {
            if (state.IsOn)
            {
                mycalculator.div = 1;
                mycalculator.multis = 1;
                state.IsOn = true;
            }
            else
            {
                mycalculator.div = 180m / (decimal)Math.PI;
                mycalculator.multis = 180m / (decimal)Math.PI;
                state.IsOn = false;
            }
        }

        private void Before(object sender, RoutedEventArgs e)
        {
            my_formula.Append("(");
            result.Text = my_formula.ToString();
            last[count++] = "(";
        }

        private void After(object sender, RoutedEventArgs e)
        {
            my_formula.Append(")");
            result.Text = my_formula.ToString();
            last[count++] = ")";
        }

        private void Sqrt(object sender, RoutedEventArgs e)
        {
            if (count == 0)
            {
                my_formula.Append(mycalculator._result.ToString("G") + "^(1/");
                result.Text = my_formula.ToString();
                last[count++] = mycalculator._result.ToString("G") + "^(1/";
            }
            else
            {
                my_formula.Append("^(1/");
                result.Text = my_formula.ToString();
                last[count++] = "^(1/";
            }
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            my_formula.Clear();
            result.Text = "";
            _result.Text = "";
        }

        private void Division(object sender, RoutedEventArgs e)
        {
            if (count == 0)
            {
                my_formula.Append(mycalculator._result.ToString("G") + "/");
                result.Text = my_formula.ToString();
                last[count++] = mycalculator._result.ToString("G") + "/";
            }
            else
            {
                my_formula.Append("/");
                result.Text = my_formula.ToString();
                last[count++] = "/";
            }
        }

        private void Multi(object sender, RoutedEventArgs e)
        {
            if (count == 0)
            {
                my_formula.Append(mycalculator._result.ToString("G") + "*");
                result.Text = my_formula.ToString();
                last[count++] = mycalculator._result.ToString("G") + "*";
            }
            else
            {
                my_formula.Append("*");
                result.Text = my_formula.ToString();
                last[count++] = "*";
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (result.Text != "")
            {
                count--;
                temp = my_formula.ToString();
                temp = temp.Substring(0, temp.Length - last[count].Length);      //去掉最后一个字符
                my_formula.Clear();
                my_formula.Append(temp);
                result.Text = my_formula.ToString();
            }
        }

        private void Arcsin(object sender, RoutedEventArgs e)
        {
            my_formula.Append("arcsin(");
            result.Text = my_formula.ToString();
            last[count++] = "arcsin(";
        }

        private void Arccos(object sender, RoutedEventArgs e)
        {
            my_formula.Append("arccos(");
            result.Text = my_formula.ToString();
            last[count++] = "arccos(";
        }

        private void Arctan(object sender, RoutedEventArgs e)
        {
            my_formula.Append("arctan(");
            result.Text = my_formula.ToString();
            last[count++] = "arctan(";
        }

        private void _7(object sender, RoutedEventArgs e)
        {
            my_formula.Append("7");
            result.Text = my_formula.ToString();
            last[count++] = "7";
        }

        private void _8(object sender, RoutedEventArgs e)
        {
            my_formula.Append("8");
            result.Text = my_formula.ToString();
            last[count++] = "8";
        }

        private void _9(object sender, RoutedEventArgs e)
        {
            my_formula.Append("9");
            result.Text = my_formula.ToString();
            last[count++] = "9";
        }

        private void Minus(object sender, RoutedEventArgs e)
        {
            if (count == 0)
            {
                my_formula.Append(mycalculator._result.ToString("G") + "-");
                result.Text = my_formula.ToString();
                last[count++] = mycalculator._result.ToString("G") + "-";
            }
            else
            {
                my_formula.Append("-");
                result.Text = my_formula.ToString();
                last[count++] = "-";
            }
        }

        private void Sin(object sender, RoutedEventArgs e)
        {
            my_formula.Append("sin(");
            result.Text = my_formula.ToString();
            last[count++] = "sin(";
        }

        private void Sinh(object sender, RoutedEventArgs e)
        {
            my_formula.Append("sinh(");
            result.Text = my_formula.ToString();
            last[count++] = "sinh(";
        }

        private void Cosh(object sender, RoutedEventArgs e)
        {
            my_formula.Append("cosh(");
            result.Text = my_formula.ToString();
            last[count++] = "csoh(";
        }

        private void Tanh(object sender, RoutedEventArgs e)
        {
            my_formula.Append("tanh(");
            result.Text = my_formula.ToString();
            last[count++] = "tanh(";
        }

        private void Fac(object sender, RoutedEventArgs e)
        {
            if (count == 0)
            {
                my_formula.Append(mycalculator._result.ToString("G") + "!");
                result.Text = my_formula.ToString();
                last[count++] = mycalculator._result.ToString("G") + "!";
            }
            else
            {
                my_formula.Append("!");
                result.Text = my_formula.ToString();
                last[count++] = "!";
            }
        }

        private void Exp(object sender, RoutedEventArgs e)
        {
            my_formula.Append("exp(");
            result.Text = my_formula.ToString();
            last[count++] = "exp(";
        }

        private void Cos(object sender, RoutedEventArgs e)
        {
            my_formula.Append("cos(");
            result.Text = my_formula.ToString();
            last[count++] = "cos(";
        }

        private void Tan(object sender, RoutedEventArgs e)
        {
            my_formula.Append("tan(");
            result.Text = my_formula.ToString();
            last[count++] = "tan(";
        }

        private void _4(object sender, RoutedEventArgs e)
        {
            my_formula.Append("4");
            result.Text = my_formula.ToString();
            last[count++] = "4";
        }

        private void _5(object sender, RoutedEventArgs e)
        {
            my_formula.Append("5");
            result.Text = my_formula.ToString();
            last[count++] = "5";
        }

        private void _6(object sender, RoutedEventArgs e)
        {
            my_formula.Append("6");
            result.Text = my_formula.ToString();
            last[count++] = "6";
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (count == 0)
            {
                my_formula.Append(mycalculator._result.ToString("G") + "+");
                result.Text = my_formula.ToString();
                last[count++] = mycalculator._result.ToString("G") + "+";
            }
            else
            {
                my_formula.Append("+");
                result.Text = my_formula.ToString();
                last[count++] = "+";
            }
        }

        private void Ln(object sender, RoutedEventArgs e)
        {
            my_formula.Append("ln(");
            result.Text = my_formula.ToString();
            last[count++] = "ln(";
        }

        private void Log(object sender, RoutedEventArgs e)
        {
            my_formula.Append("lg(");
            result.Text = my_formula.ToString();
            last[count++] = "lg(";
        }

        private void Exponent(object sender, RoutedEventArgs e)
        {
            if (count == 0)
            {
                my_formula.Append(mycalculator._result.ToString("G") + "^(");
                result.Text = my_formula.ToString();
                last[count++] = mycalculator._result.ToString("G") + "^(";
            }
            else
            {
                my_formula.Append("^(");
                result.Text = my_formula.ToString();
                last[count++] = "^(";
            }
        }

        private void _1(object sender, RoutedEventArgs e)
        {
            my_formula.Append("1");
            result.Text = my_formula.ToString();
            last[count++] = "1";
        }

        private void _2(object sender, RoutedEventArgs e)
        {
            my_formula.Append("2");
            result.Text = my_formula.ToString();
            last[count++] = "2";
        }

        private void _3(object sender, RoutedEventArgs e)
        {
            my_formula.Append("3");
            result.Text = my_formula.ToString();
            last[count++] = "3";
        }

        private void Make(object sender, RoutedEventArgs e)
        {
            if (result.Text != "")
            {
                my_formula.Append("\0");
                mycalculator.formula = my_formula.ToString();
                mycalculator.start();

                // 各种运行的可能性
                switch (mycalculator.out_error)
                {
                    // 为了方便用户的使用，必需给出容错的功能，但是考虑到算法，下面的容错提示不会显示，但也不至于使应用崩溃。
                    // 保留容错提示是为了保持代码的可移植性。
                    case 1: result.Text = "divisor = 0"; break;
                    case 3: result.Text = "unexpected characters"; break;
                    case 4: result.Text = "？"; break;
                    default:
                        _result.Text = result.Text + " = " + mycalculator._result.ToString("G");
                        History.Add(new Calculation { Formulation = result.Text + " = " + mycalculator._result.ToString("G"), Result = mycalculator._result.ToString("G") });
                        SaveLocal();
                        break;
                }
                result.Text = "";
                my_formula.Clear();
                mycalculator.formula = "";
                count = 0;
            }
        }

        private void Ans(object sender, RoutedEventArgs e)
        {
            my_formula.Append(mycalculator._result.ToString("G"));
            result.Text = my_formula.ToString();
            last[count++] = mycalculator._result.ToString("G");
        }

        private void Pi(object sender, RoutedEventArgs e)
        {
            my_formula.Append("pi");
            result.Text = my_formula.ToString();
            last[count++] = "pi";
        }

        private void E(object sender, RoutedEventArgs e)
        {
            my_formula.Append("e");
            result.Text = my_formula.ToString();
            last[count++] = "e";
        }

        private void _0(object sender, RoutedEventArgs e)
        {
            my_formula.Append("0");
            result.Text = my_formula.ToString();
            last[count++] = "0";
        }

        private void Point(object sender, RoutedEventArgs e)
        {
            my_formula.Append(".");
            result.Text = my_formula.ToString();
            last[count++] = ".";
        }

        #endregion

        private void Flyout_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
            => FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);

        private void Record_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 之所以要用Item是因为要读取的数据是Item的成员
            var item = e.ClickedItem as Calculation;
            // 输入计算式
            my_formula.Append(item.Result);
            result.Text = my_formula.ToString();
            last[count++] = item.Result;
        }

        private async void Deleterecord(object sender, RoutedEventArgs e)
        {
            History.Clear();
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("Cal-Record.txt", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, "");
        }

        private async void SaveLocal()
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("Cal-Record.txt", CreationCollisionOption.OpenIfExists);
            string content = JsonHelper.SerializeObject(History);
            await FileIO.WriteTextAsync(file, content);
        }

        private async void Export(object sender, RoutedEventArgs e)
        {
            string data = "";
            for (int i = 0; i < History.Count; i++)
            {
                data += History[i].Formulation + "\t\n";
            }

            FileSavePicker picker = new FileSavePicker();
            picker.FileTypeChoices.Add("Cal-History", new String[] { ".txt" });
            picker.SuggestedStartLocation = PickerLocationId.Desktop;
            picker.SuggestedFileName = "Cal-History" + DateTime.Now.ToString("yyyyMMddHHmmss");
            StorageFile file = await picker.PickSaveFileAsync();
            if (file != null)
            {
                await FileIO.WriteTextAsync(file, data, Windows.Storage.Streams.UnicodeEncoding.Utf8);
            }
        }

    }

    // ListView绑定的数据
    public class Calculation
    {
        public string Formulation { get; set; }    //前台
        public string Result { get; set; }    //后台
    }
}

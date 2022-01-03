using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tsk6WpfApp1
{
    /*Разработать в WPF приложении класс WeatherControl, моделирующий погодную сводку – температуру 
     * (целое число в диапазоне от -50 до +50), направление ветра (строка), скорость ветра (целое число), 
     * наличие осадков (возможные значения: 0 – солнечно, 1 – облачно, 2 – дождь, 3 – снег. 
     * Можно использовать целочисленное значение, либо создать перечисление enum). 
     * Свойство «температура» преобразовать в свойство зависимости.
     */
    class WeatherControl : DependencyObject
    {
        public static readonly DependencyProperty TempProperty;

        public int Temp
        {
            get => (int)GetValue(TempProperty);
            set => SetValue(TempProperty, value);
        }
        public string WindDir { get; set; }
        public int WindSpeed { get; set; }
        
        public enum RainFall
        {
            Sun = 0,
            Cloudy = 1,
            Rainy = 2,
            Snow = 3,
        }
        public WeatherControl(int temp, string windDir, int windSpeed, RainFall rainFall)
        {
            this.Temp = temp;
            this.WindDir = windDir;
            this.WindSpeed = windSpeed;
        } 
        static WeatherControl()
        {
            TempProperty = DependencyProperty.Register(
                nameof(Temp),
                typeof(int),
                typeof(WeatherControl),
                new FrameworkPropertyMetadata(
                    0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null,
                    new CoerceValueCallback(CoerceTemp)),
                new ValidateValueCallback(ValidateTemp));
        }
        private static bool ValidateTemp(object value)
        {
            int v = (int)value;
            if (v >= -100 && v <= 60)
                return true;
            else
                return false;
        }
        private static object CoerceTemp(DependencyObject d, object basevalue)
        {
            int v = (int)basevalue;
            if (v >= -50 && v <= 50)
                return v;
            else if (v > 50)
                return 555555;
            else if (v < -50)
                return -555555;
            else
                return 0;
        }
    }
}

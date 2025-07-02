using System.Windows.Media;

namespace LibraryManagementSystem.Services
{
    public static class ThemeService
    {
        // Primary colors from user specification
        public static readonly Color Cream = Color.FromRgb(247, 240, 222);        // #F7F0DE
        public static readonly Color LightAqua = Color.FromRgb(136, 231, 216);    // #88E7D8
        public static readonly Color Teal = Color.FromRgb(14, 140, 145);          // #0E8C91
        public static readonly Color Navy = Color.FromRgb(4, 28, 38);             // #041C26

        // Additional complementary colors
        public static readonly Color White = Color.FromRgb(255, 255, 255);        // #FFFFFF
        public static readonly Color LightGray = Color.FromRgb(245, 245, 245);    // #F5F5F5
        public static readonly Color Gray = Color.FromRgb(204, 204, 204);         // #CCCCCC
        public static readonly Color DarkGray = Color.FromRgb(102, 102, 102);     // #666666

        // Status colors
        public static readonly Color Success = Color.FromRgb(40, 167, 69);        // #28a745
        public static readonly Color Warning = Color.FromRgb(255, 193, 7);        // #ffc107
        public static readonly Color Error = Color.FromRgb(220, 53, 69);          // #dc3545
        public static readonly Color Info = Color.FromRgb(23, 162, 184);          // #17a2b8

        // Brush properties for easy binding
        public static SolidColorBrush CreamBrush => new(Cream);
        public static SolidColorBrush LightAquaBrush => new(LightAqua);
        public static SolidColorBrush TealBrush => new(Teal);
        public static SolidColorBrush NavyBrush => new(Navy);
        public static SolidColorBrush WhiteBrush => new(White);
        public static SolidColorBrush LightGrayBrush => new(LightGray);
        public static SolidColorBrush GrayBrush => new(Gray);
        public static SolidColorBrush DarkGrayBrush => new(DarkGray);
        public static SolidColorBrush SuccessBrush => new(Success);
        public static SolidColorBrush WarningBrush => new(Warning);
        public static SolidColorBrush ErrorBrush => new(Error);
        public static SolidColorBrush InfoBrush => new(Info);

        // Gradient brushes for modern effects
        public static LinearGradientBrush PrimaryGradient => new()
        {
            StartPoint = new(0, 0),
            EndPoint = new(1, 1),
            GradientStops = new()
            {
                new(LightAqua, 0.0),
                new(Teal, 1.0)
            }
        };

        public static LinearGradientBrush HeaderGradient => new()
        {
            StartPoint = new(0, 0),
            EndPoint = new(1, 0),
            GradientStops = new()
            {
                new(Navy, 0.0),
                new(Teal, 1.0)
            }
        };

        public static LinearGradientBrush CardGradient => new()
        {
            StartPoint = new(0, 0),
            EndPoint = new(0, 1),
            GradientStops = new()
            {
                new(White, 0.0),
                new(Cream, 1.0)
            }
        };
    }
}

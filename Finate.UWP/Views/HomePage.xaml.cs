using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;
using Finate.UWP.Annotations;
using Finate.UWP.ViewModels;
using Syncfusion.UI.Xaml.CellGrid.Helpers;
using Syncfusion.UI.Xaml.Charts;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Finate.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : INotifyPropertyChanged
    {
        public HomePage()
        {
            this.InitializeComponent();
            this.DataContextChanged += this.HomePageDataContextChanged;

            this.SpendingsChart.Loaded += (sender, args) =>
            {
                var splines = FunctionalExtensions.ToList<ChartSeriesPanel>(FindChildren<ChartSeriesPanel>(this.SpendingsChart));
                var currentSpendingSpline = splines.Last();

                var paths = currentSpendingSpline.Children.OfType<Path>();
                var todaysSpendingPath = paths.LastOrDefault();

                todaysSpendingPath.RegisterPropertyChangedCallback(Path.DataProperty, (o, dp) =>
                {
                    var pathGeometry = todaysSpendingPath.Data as PathGeometry;
                    var segment = pathGeometry.Figures[0].Segments[0] as BezierSegment;
                    var positionX =  segment.Point3.X - 3;
                    var positionY = segment.Point3.Y - 3;

                    var ellipse = new Ellipse
                    {
                        Fill = new SolidColorBrush(Colors.DimGray),
                        Width = 6,
                        Height = 6,
                        RenderTransform = new ScaleTransform()
                    };
                    Canvas.SetTop(ellipse, positionY);
                    Canvas.SetLeft(ellipse, positionX);
                    ellipse.Name = "CurrentSpendingsEllipse";

                    currentSpendingSpline.Children.Add(ellipse);

                    Storyboard.SetTarget(this.BoundEasingStoryboard, ellipse);
                    this.BoundEasingStoryboard.Begin();
                });
            };
        }

        private void HomePageDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            this.OnPropertyChanged(nameof(ConcreteDataContext));
        }

        public HomePageViewModel ConcreteDataContext => this.DataContext as HomePageViewModel;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Analyzes both visual and logical tree in order to find all elements
        /// of a given type that are descendants of the <paramref name="source"/>
        /// item.
        /// </summary>
        /// <typeparam name="T">The type of the queried items.</typeparam>
        /// <param name="source">The root element that marks the source of the
        /// search. If the source is already of the requested type, it will not
        /// be included in the result.</param>
        /// <returns>All descendants of <paramref name="source"/> that match the
        /// requested type.</returns>
        public static IEnumerable<T> FindChildren<T>(DependencyObject source)
                                                     where T : DependencyObject
        {
            if (source == null) yield break;
            foreach (var child in GetChildObjects(source))
            {
                // Analyze if children match the requested type
                var typeChild = child as T;
                if (typeChild != null)
                    yield return typeChild;

                // Recurse tree
                foreach (var descendant in FindChildren<T>(child))
                    yield return descendant;
            }
        }

        /// <summary>
        /// This method is an alternative to WPF's
        /// <see cref="VisualTreeHelper.GetChild"/> method, which also
        /// supports content elements. Do note, that for content elements,
        /// this method falls back to the logical tree of the element.
        /// </summary>
        /// <param name="parent">The item to be processed.</param>
        /// <returns>The submitted item's child elements, if available.</returns>
        public static IEnumerable<DependencyObject> GetChildObjects(
                                                    DependencyObject parent)
        {
            if (parent == null) yield break;
            var count = VisualTreeHelper.GetChildrenCount(parent);
            for (var index = 0; index < count; index++)
                yield return VisualTreeHelper.GetChild(parent, index);
        }
    }
}

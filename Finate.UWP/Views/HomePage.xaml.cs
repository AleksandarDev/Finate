﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;
using Finate.UWP.Annotations;
using Finate.UWP.ViewModels;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
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

            // Attach to events
            this.AddTransactionButtonGrid.PointerPressed += this.AddTransactionButtonGridOnPointerPressed;
            this.SpendingsChart.Loaded += this.SpendingsChartOnLoaded;
        }

        private void SpendingsChartOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            this.SpendingsChartAddTodayIndicator();

            this.AnimateAddTransactionToCircle();
        }

        private void SpendingsChartAddTodayIndicator()
        {
            // Retrieve last spline lines collection
            var splines = FunctionalExtensions.ToList<ChartSeriesPanel>(FindChildren<ChartSeriesPanel>(this.SpendingsChart));
            var currentSpendingSpline = splines.Last();

            // Retrieve last path from the collection
            var paths = currentSpendingSpline.Children.OfType<Path>();
            var todaysSpendingPath = paths.LastOrDefault();

            // Attach to path data property changed callback
            todaysSpendingPath.RegisterPropertyChangedCallback(Path.DataProperty, (o, dp) =>
            {
                const int ellipseSize = 6;
                const int ellipseRadius = ellipseSize / 2;

                // Retrieve new data as path geometry
                var pathGeometry = todaysSpendingPath.Data as PathGeometry;
                if (pathGeometry == null)
                    return;

                // Retrieve last segment
                if (!(pathGeometry.Figures?.Any() ?? false) ||
                    !(pathGeometry.Figures[0].Segments?.Any() ?? false))
                    return;
                var segment = pathGeometry.Figures[0].Segments[0] as BezierSegment;
                if (segment == null)
                    return;

                // Calculate position from last point of curve and subtract radius of ellipse
                var positionX = segment.Point3.X - ellipseRadius;
                var positionY = segment.Point3.Y - ellipseRadius;

                // Instantiate new ellipse for indicating path end
                var ellipse = new Ellipse
                {
                    Fill = new SolidColorBrush(Colors.DimGray),
                    Width = ellipseSize,
                    Height = ellipseSize,
                    RenderTransform = new ScaleTransform
                    {
                        CenterX = ellipseRadius,
                        CenterY = ellipseRadius
                    }
                };

                // Position ellipse on the chart
                Canvas.SetTop(ellipse, positionY);
                Canvas.SetLeft(ellipse, positionX);

                // Add ellipse to the chart
                currentSpendingSpline.Children.Add(ellipse);

                // Assing animation target and animate
                Storyboard.SetTarget(this.BoundEasingStoryboard, ellipse);
                this.BoundEasingStoryboard.Begin();
            });
        }

        private void AddTransactionButtonGridOnPointerPressed(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            this.AnimateAddTransactionSlideOut();
        }

        private void AnimateAddTransactionSlideOut()
        {
            var buttonParent = this.AddTransactionButtonGrid.Parent as FrameworkElement;
            if (buttonParent == null)
                return;

            this.SlideOutAddTransactionButtonTranslate.To = buttonParent.ActualHeight;
            this.SlideOutAddTransactionButtonTranslationStoryboard.Begin();
        }

        private void AnimateAddTransactionSlideIn()
        {
            this.SlideInAddTransactionButtonTranslationStoryboard.Begin();
        }

        private void AnimateAddTransactionToCircle()
        {
            // Retrieve button content container's scale transform
            var centerTransform = this.PartAddTransactionButtonCenter.RenderTransform as ScaleTransform;
            if (centerTransform == null)
                return;

            // Set button content container's scale transform 
            centerTransform.CenterX = this.PartAddTransactionButtonCenter.ActualWidth/2;

            // Set button left and right circle's transition animation's parameters
            this.CircleButtonTransitionLeftCircleAnimation.To = this.PartAddTransactionButtonCenter.ActualWidth/2;
            this.CircleButtonTransitionRightCircleAnimation.To = -this.PartAddTransactionButtonCenter.ActualWidth / 2;

            // Set button transition animation destination parameter
            this.CircleButtonTransitionAnimation.To = this.ActualWidth / 2 - this.PartAddTransactionButtonLeftCircle.ActualWidth / 2 - this.AddTransactionButtonGrid.Margin.Bottom;

            // Start the add transaction animation
            this.CircleButtonTransitionStoryboard.Begin();
        }

        private void AnimateAddTransactionToFull()
        {
            this.FullButtonTransitionStoryboard.Begin();
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

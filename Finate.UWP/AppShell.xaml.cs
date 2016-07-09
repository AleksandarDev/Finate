using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Finate.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppShell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppShell"/> class.
        /// </summary>
        public AppShell()
        {
            this.InitializeComponent();
        }


        /// <summary>
        /// Sets the content frame.
        /// </summary>
        /// <param name="frame">The frame.</param>
        public void SetContentFrame(Frame frame)
        {
            rootSplitView.Content = frame;
        }

        /// <summary>
        /// Sets the content of the menu pane.
        /// </summary>
        /// <param name="content">The content.</param>
        public void SetMenuPaneContent(UIElement content)
        {
            rootSplitView.Pane = content;
        }
    }
}

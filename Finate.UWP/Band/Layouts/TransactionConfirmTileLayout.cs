using Microsoft.Band;
using Microsoft.Band.Tiles;
using Microsoft.Band.Tiles.Pages;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Finate.UWP.Band.Layouts
{
	internal class TransactionConfirmTileLayout
	{
		private readonly PageLayout pageLayout;
		private readonly PageLayoutData pageLayoutData;
		
		private readonly FlowPanel panel = new FlowPanel();
		private readonly TextBlock textBlock = new TextBlock();
		private readonly WrappedTextBlock wrappedTextBlock = new WrappedTextBlock();
		
		private readonly TextBlockData textBlockData = new TextBlockData(2, "Transaction");
		private readonly WrappedTextBlockData wrappedTextBlockData = new WrappedTextBlockData(3, "Press Action button to confirm transaction");
		
		public TransactionConfirmTileLayout()
		{
			LoadIconMethod = LoadIcon;
			AdjustUriMethod = (uri) => uri;
			
			panel = new FlowPanel();
			panel.Orientation = FlowPanelOrientation.Vertical;
			panel.Rect = new PageRect(0, 0, 258, 128);
			panel.ElementId = 1;
			panel.Margins = new Margins(0, 0, 0, 0);
			panel.HorizontalAlignment = HorizontalAlignment.Left;
			panel.VerticalAlignment = VerticalAlignment.Top;
			
			textBlock = new TextBlock();
			textBlock.Font = TextBlockFont.Small;
			textBlock.Baseline = 0;
			textBlock.BaselineAlignment = TextBlockBaselineAlignment.Automatic;
			textBlock.AutoWidth = true;
			textBlock.ColorSource = ElementColorSource.BandBase;
			textBlock.Rect = new PageRect(0, 0, 32, 32);
			textBlock.ElementId = 2;
			textBlock.Margins = new Margins(12, 8, 0, 0);
			textBlock.HorizontalAlignment = HorizontalAlignment.Left;
			textBlock.VerticalAlignment = VerticalAlignment.Top;
			
			panel.Elements.Add(textBlock);
			
			wrappedTextBlock = new WrappedTextBlock();
			wrappedTextBlock.Font = WrappedTextBlockFont.Small;
			wrappedTextBlock.AutoHeight = true;
			wrappedTextBlock.ColorSource = ElementColorSource.Custom;
			wrappedTextBlock.Color = new BandColor(255, 255, 255);
			wrappedTextBlock.Rect = new PageRect(0, 0, 246, 32);
			wrappedTextBlock.ElementId = 3;
			wrappedTextBlock.Margins = new Margins(12, 8, 0, 0);
			wrappedTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
			wrappedTextBlock.VerticalAlignment = VerticalAlignment.Top;
			
			panel.Elements.Add(wrappedTextBlock);
			pageLayout = new PageLayout(panel);
			
			PageElementData[] pageElementDataArray = new PageElementData[2];
			pageElementDataArray[0] = textBlockData;
			pageElementDataArray[1] = wrappedTextBlockData;
			
			pageLayoutData = new PageLayoutData(pageElementDataArray);
		}
		
		public PageLayout Layout
		{
			get
			{
				return pageLayout;
			}
		}
		
		public PageLayoutData Data
		{
			get
			{
				return pageLayoutData;
			}
		}
		
		public Func<string, Task<BandIcon>> LoadIconMethod
		{
			get;
			set;
		}
		
		public Func<string, string> AdjustUriMethod
		{
			get;
			set;
		}
		
		private static async Task<BandIcon> LoadIcon(string uri)
		{
			StorageFile imageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uri));
			
			using (IRandomAccessStream fileStream = await imageFile.OpenAsync(FileAccessMode.Read))
			{
				WriteableBitmap bitmap = new WriteableBitmap(1, 1);
				await bitmap.SetSourceAsync(fileStream);
				return bitmap.ToBandIcon();
			}
		}
		
		public async Task LoadIconsAsync(BandTile tile)
		{
			await Task.Run(() => { }); // Dealing with CS1998
		}
		
		public static BandTheme GetBandTheme()
		{
			var theme = new BandTheme();
			theme.Base = new BandColor(51, 102, 204);
			theme.HighContrast = new BandColor(58, 120, 221);
			theme.Highlight = new BandColor(58, 120, 221);
			theme.Lowlight = new BandColor(49, 101, 186);
			theme.Muted = new BandColor(43, 90, 165);
			theme.SecondaryText = new BandColor(137, 151, 171);
			return theme;
		}
		
		public static BandTheme GetTileTheme()
		{
			var theme = new BandTheme();
			theme.Base = new BandColor(51, 102, 204);
			theme.HighContrast = new BandColor(58, 120, 221);
			theme.Highlight = new BandColor(58, 120, 221);
			theme.Lowlight = new BandColor(49, 101, 186);
			theme.Muted = new BandColor(43, 90, 165);
			theme.SecondaryText = new BandColor(137, 151, 171);
			return theme;
		}
		
		public class PageLayoutData
		{
			private readonly PageElementData[] array;
			
			public PageLayoutData(PageElementData[] pageElementDataArray)
			{
				array = pageElementDataArray;
			}
			
			public int Count
			{
				get
				{
					return array.Length;
				}
			}
			
			public T Get<T>(int i) where T : PageElementData
			{
				return (T)array[i];
			}
			
			public T ById<T>(short id) where T:PageElementData
			{
				return (T)array.FirstOrDefault(elm => elm.ElementId == id);
			}
			
			public PageElementData[] All
			{
				get
				{
					return array;
				}
			}
		}
		
	}
}

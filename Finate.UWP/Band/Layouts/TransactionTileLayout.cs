using Microsoft.Band;
using Microsoft.Band.Tiles;
using Microsoft.Band.Tiles.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Finate.UWP.Band.Layouts
{
	internal class TransactionTileLayout
	{
		private readonly PageLayout pageLayout;
		private readonly PageLayoutData pageLayoutData;
		
		private readonly FlowPanel panel = new FlowPanel();
		private readonly TextBlock textBlock = new TextBlock();
		internal TextButton AmountDecimalButton = new TextButton();
		private readonly Icon icon = new Icon();
		internal FilledButton CategoryButton = new FilledButton();
		internal TextButton AmountSingleButton = new TextButton();
		
		private readonly TextBlockData textBlockData = new TextBlockData(2, "Amount");
		internal TextButtonData AmountDecimalButtonData = new TextButtonData(3, "5");
		private readonly IconData iconData = new IconData(5, 1);
		internal FilledButtonData CategoryButtonData = new FilledButtonData(4, new BandColor(32, 32, 32));
		internal TextButtonData AmountSingleButtonData = new TextButtonData(6, "0");
		
		public TransactionTileLayout()
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
			
			AmountDecimalButton = new TextButton();
			AmountDecimalButton.PressedColor = new BandColor(32, 32, 32);
			AmountDecimalButton.Rect = new PageRect(0, 0, 80, 44);
			AmountDecimalButton.ElementId = 3;
			AmountDecimalButton.Margins = new Margins(12, 20, 0, 0);
			AmountDecimalButton.HorizontalAlignment = HorizontalAlignment.Right;
			AmountDecimalButton.VerticalAlignment = VerticalAlignment.Top;
			
			panel.Elements.Add(AmountDecimalButton);
			
			icon = new Icon();
			icon.ColorSource = ElementColorSource.Custom;
			icon.Color = new BandColor(255, 21, 28);
			icon.Rect = new PageRect(0, 0, 32, 32);
			icon.ElementId = 5;
			icon.Margins = new Margins(190, -38, 0, 0);
			icon.HorizontalAlignment = HorizontalAlignment.Center;
			icon.VerticalAlignment = VerticalAlignment.Center;
			
			panel.Elements.Add(icon);
			
			CategoryButton = new FilledButton();
			CategoryButton.BackgroundColor = new BandColor(53, 53, 53);
			CategoryButton.Rect = new PageRect(0, 0, 44, 44);
			CategoryButton.ElementId = 4;
			CategoryButton.Margins = new Margins(184, -40, 0, 0);
			CategoryButton.HorizontalAlignment = HorizontalAlignment.Center;
			CategoryButton.VerticalAlignment = VerticalAlignment.Center;
			
			panel.Elements.Add(CategoryButton);
			
			AmountSingleButton = new TextButton();
			AmountSingleButton.PressedColor = new BandColor(32, 32, 32);
			AmountSingleButton.Rect = new PageRect(0, 0, 80, 44);
			AmountSingleButton.ElementId = 6;
			AmountSingleButton.Margins = new Margins(92, -44, 0, 0);
			AmountSingleButton.HorizontalAlignment = HorizontalAlignment.Left;
			AmountSingleButton.VerticalAlignment = VerticalAlignment.Top;
			
			panel.Elements.Add(AmountSingleButton);
			pageLayout = new PageLayout(panel);
			
			PageElementData[] pageElementDataArray = new PageElementData[5];
			pageElementDataArray[0] = textBlockData;
			pageElementDataArray[1] = AmountDecimalButtonData;
			pageElementDataArray[2] = iconData;
			pageElementDataArray[3] = CategoryButtonData;
			pageElementDataArray[4] = AmountSingleButtonData;
			
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
			int firstIconIndex = tile.AdditionalIcons.Count + 2; // First 2 are used by the Tile itself
			tile.AdditionalIcons.Add(await LoadIconMethod(AdjustUriMethod("ms-appx:///Assets/BandCategoryIcon.png")));
			pageLayoutData.ById<IconData>(5).IconIndex = (ushort)(firstIconIndex + 0);
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

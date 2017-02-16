using System;
using CoreAnimation;
using UIKit;

namespace FlexiTableApp
{
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			headerView.BackgroundColor = UIColor.Orange;
			headerView.Layer.ZPosition = 1;
			//headerView.Layer.AnchorPoint = new CoreGraphics.CGPoint(0.5, 0);

			string[] tableItems = new string[] { "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers" };
			tableView.Source = new TableSource(tableItems, headerHeightConstraint, headerView);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}

	public class TableSource : UITableViewSource
	{

		string[] TableItems;
		string CellIdentifier = "TableCell";
		NSLayoutConstraint headerHeight;
		nfloat baseHeight;
		UIView header;

		public TableSource(string[] items, NSLayoutConstraint headerHeight, UIView header)
		{
			TableItems = items;
			this.headerHeight = headerHeight;
			baseHeight = headerHeight.Constant;
			this.header = header;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return TableItems.Length;
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
			string item = TableItems[indexPath.Row];

			//---- if there are no cells to reuse, create a new one
			if (cell == null)
			{ cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier); }

			cell.TextLabel.Text = item;

			return cell;
		}

		public override void Scrolled(UIScrollView scrollView)
		{
			var offset = scrollView.ContentOffset.Y;
			var headerTransform = CATransform3D.Identity;

			//var height = baseHeight - offset;
			//if (height < 60f)
			//	height = 60f;

			// Pull Down
			if (offset < 0)
			{
				nfloat headerScaleFactor = -(offset) / header.Bounds.Height;
				nfloat headerSizevariation = ((header.Bounds.Height * (1.0f + headerScaleFactor)) - header.Bounds.Height) / 2.0f;

				headerTransform = headerTransform.Translate(0f, headerSizevariation, 0f);
				headerTransform = headerTransform.Scale(1.0f + headerScaleFactor, 1.0f + headerScaleFactor, 0f);

				header.Layer.Transform = headerTransform;
			}

			// Scroll up / down
			else
			{
				// Header
				headerTransform = headerTransform.Translate(0, (nfloat)Math.Max(-60f, -offset), 0);
				header.Layer.Transform = headerTransform;
				headerHeight.Constant = header.Layer.Frame.Height;

				Console.WriteLine("Layer bounds {0}", header.Layer.Bounds);
			}
 		}
	}
}

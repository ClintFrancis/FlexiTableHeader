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

			View.BackgroundColor = UIColor.Blue;

			headerView.BackgroundColor = UIColor.Orange;
			headerView.Layer.ZPosition = 1;

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
			var headerScaleFactor = -(offset) / baseHeight;

			nfloat targetHeight = baseHeight * (1 + headerScaleFactor);

			// Pull Down
			if (offset < 0)
			{
				// TODO Specific pull down logic
				if (targetHeight > 300f)
					targetHeight = 300f;
			}

			// Scroll up / down
			else
			{
				// TODO Specific scroll logic

				if (targetHeight < 60f)
					targetHeight = 60f;
			}

			var frame = header.Frame;
			frame.Height = targetHeight;
			header.Frame = frame;

			//headerHeight.Constant = targetHeight;
			//Console.WriteLine("Layer bounds {0}", header.Layer.Bounds);
		}
	}
}

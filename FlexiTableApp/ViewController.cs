using System;
using CoreAnimation;
using CoreGraphics;
using UIKit;

namespace FlexiTableApp
{
	public partial class ViewController : UIViewController
	{
		nfloat minHeight = 20f;

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var header = headerView as ScrollHeaderView;
			header.BackgroundColor = UIColor.Orange;
			header.Layer.ZPosition = 1;

			tableTopConstraint.Constant = minHeight;

			var tableItems = new string[] { "First", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers","Fruits", "Flower Buds", "Legumes", "Fruits", "Flower Buds", "Legumes", "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers", "Fruits", "Flower Buds", "Legumes", "Fruits", "Flower Buds", "Legumes" };
			tableView.Source = new DemoTableSource(tableItems, this, tableView, headerHeightConstraint, minHeight);

			var offset = headerHeightConstraint.Constant - tableTopConstraint.Constant;
			tableView.ContentInset = new UIEdgeInsets(offset, 0, 0, 0);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}

	public class DemoTableSource:FlexiTableHeaderSource
	{
		string[] TableItems;
		string CellIdentifier = "TableCell";

		public DemoTableSource(string[] items, UIViewController viewController, UITableView tableView, NSLayoutConstraint heightConstraint, nfloat minHeight):base(viewController,tableView,heightConstraint,minHeight)
		{
			TableItems = items;
		}

		// Implement your own height based logic
		protected override nfloat UpdateTargetHeight(nfloat targetHeight)
		{
			// Pull Down
			if (Offset < 0)
			{
				// TODO Specific pull down logic
			}

			// Scroll up / down
			else
			{
				// TODO Specific scroll logic
				if (targetHeight < minHeight)
					targetHeight = minHeight;
			}

			return targetHeight;
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

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return TableItems.Length;
		}
	}
}

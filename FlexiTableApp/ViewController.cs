using System;
using CoreAnimation;
using UIKit;

namespace FlexiTableApp
{
	public partial class ViewController : UIViewController
	{
		nfloat minHeight = 60f;

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			headerView.BackgroundColor = UIColor.Orange;
			headerView.Layer.ZPosition = 1;

			string[] tableItems = new string[] { "First", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers","Fruits", "Flower Buds", "Legumes", "Fruits", "Flower Buds", "Legumes", "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers", "Fruits", "Flower Buds", "Legumes", "Fruits", "Flower Buds", "Legumes" };
			tableView.Source = new TableSource(tableItems, headerHeightConstraint, minHeight);

			//var offset = headerHeightConstraint.Constant - tableView.Frame.Y;
			tableView.ContentInset = new UIEdgeInsets(minHeight, 0, 0, 0);
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
		nfloat minHeight;

		public TableSource(string[] items, NSLayoutConstraint headerHeight, nfloat minHeight)
		{
			TableItems = items;
			this.headerHeight = headerHeight;
			this.minHeight = minHeight;
			baseHeight = headerHeight.Constant;
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
			var offset = (scrollView.ContentOffset.Y + minHeight);
			var headerScaleFactor = -(offset) / baseHeight;
			var targetHeight = baseHeight * (1 + headerScaleFactor);

			// Pull Down
			if (offset < 0)
			{
				// TODO Specific pull down logic
				if (targetHeight > 300f){
					targetHeight = 300f;
				}
			}

			// Scroll up / down
			else
			{
				// TODO Specific scroll logic

				if (targetHeight < minHeight)
					targetHeight = minHeight;
			}

			headerHeight.Constant = targetHeight;
		}
	}
}

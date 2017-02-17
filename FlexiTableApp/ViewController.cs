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

			headerView.BackgroundColor = UIColor.Orange;
			headerView.Layer.ZPosition = 1;
			headerView.AddGestureRecognizer(new CustomPanGesture(headerView, tableView));

			tableTopConstraint.Constant = minHeight;

			var tableItems = new string[] { "First", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers","Fruits", "Flower Buds", "Legumes", "Fruits", "Flower Buds", "Legumes", "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers", "Fruits", "Flower Buds", "Legumes", "Fruits", "Flower Buds", "Legumes" };
			tableView.Source = new TableSource(tableItems, headerHeightConstraint, minHeight);

			var offset = headerHeightConstraint.Constant - tableTopConstraint.Constant;
			tableView.ContentInset = new UIEdgeInsets(offset, 0, 0, 0);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}


	public class CustomPanGesture:UIGestureRecognizer
	{
		UITableView tableView;
		UIView targetView;

		public CustomPanGesture(UIView view, UITableView table)
		{
			targetView = view;
			tableView = table;
		}

		public override void TouchesBegan(Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesBegan(touches, evt);

			if (touches.Count != 1)
				TouchesCancelled(touches, evt);
		}

		public override void TouchesMoved(Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesMoved(touches, evt);

			var touch = touches.AnyObject as UITouch;
			var location = touch.LocationInView(targetView);
			var prevLocation = touch.PreviousLocationInView(targetView);

			tableView.ContentOffset = new CGPoint(0, tableView.ContentOffset.Y - (location.Y - prevLocation.Y));
		}

		public override void TouchesEnded(Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);

			// Reset the table to its original position 
			//tableView.SetContentOffset(new CGPoint(0, -offset), true);
		}

		public override void TouchesCancelled(Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesCancelled(touches, evt);
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
			var offset = scrollView.ContentOffset.Y + (baseHeight - minHeight);
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

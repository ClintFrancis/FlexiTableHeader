using System;
using Foundation;
using UIKit;

namespace FlexiTableApp
{
	public class FlexiTableHeaderSource:UITableViewSource
	{
		protected UIViewController executingController;
		protected NSLayoutConstraint heightConstraint;
		protected nfloat minHeight;
		protected nfloat baseHeight;
		protected UIEdgeInsets scrollBarInsets;

		public nfloat Offset { get; private set; }

		public FlexiTableHeaderSource(UIViewController viewController, UITableView tableView, NSLayoutConstraint heightConstraint, nfloat minHeight)
		{
			executingController = viewController;
			this.heightConstraint = heightConstraint;
			this.minHeight = minHeight;
			baseHeight = heightConstraint.Constant;
		}

		public override void Scrolled(UIScrollView scrollView)
		{
			Offset = scrollView.ContentOffset.Y + (baseHeight - minHeight);
			var headerScaleFactor = -(Offset) / baseHeight;
			var targetHeight = baseHeight * (1 + headerScaleFactor);

			// Scrollbar offset
			scrollBarInsets = scrollView.ScrollIndicatorInsets;
			scrollBarInsets.Top = (Offset < 0f) ?
				scrollBarInsets.Top = -scrollView.ContentOffset.Y :
				scrollBarInsets.Top = -scrollView.ContentOffset.Y - (baseHeight * headerScaleFactor);

			scrollView.ScrollIndicatorInsets = scrollBarInsets;

			// Set the height
			heightConstraint.Constant = UpdateTargetHeight(targetHeight);
		}

		protected virtual nfloat UpdateTargetHeight(nfloat targetHeight)
		{
			return targetHeight;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				executingController = null;
			}

			base.Dispose(disposing);
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			throw new NotImplementedException();
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			throw new NotImplementedException();
		}
	}
}

using System;
using CoreGraphics;
using UIKit;

namespace FlexiTableApp
{
	public class CustomPanGesture : UIGestureRecognizer
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
			//tableView.SetContentOffset(new CGPoint(0, -offset), true)
		}

		public override void TouchesCancelled(Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesCancelled(touches, evt);
		}
	}
}

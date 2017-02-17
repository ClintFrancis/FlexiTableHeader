// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace FlexiTableApp
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.NSLayoutConstraint headerHeightConstraint { get; set; }

		[Outlet]
		UIKit.UIView headerView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint tableTopConstraint { get; set; }

		[Outlet]
		UIKit.UITableView tableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (headerHeightConstraint != null) {
				headerHeightConstraint.Dispose ();
				headerHeightConstraint = null;
			}

			if (tableTopConstraint != null) {
				tableTopConstraint.Dispose ();
				tableTopConstraint = null;
			}

			if (headerView != null) {
				headerView.Dispose ();
				headerView = null;
			}

			if (tableView != null) {
				tableView.Dispose ();
				tableView = null;
			}
		}
	}
}

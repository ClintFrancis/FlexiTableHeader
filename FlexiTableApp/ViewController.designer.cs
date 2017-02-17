// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
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

            if (headerView != null) {
                headerView.Dispose ();
                headerView = null;
            }

            if (tableTopConstraint != null) {
                tableTopConstraint.Dispose ();
                tableTopConstraint = null;
            }

            if (tableView != null) {
                tableView.Dispose ();
                tableView = null;
            }
        }
    }
}
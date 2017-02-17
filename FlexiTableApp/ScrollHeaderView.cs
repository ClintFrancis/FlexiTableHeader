using Foundation;
using System;
using UIKit;
using CoreGraphics;

namespace FlexiTableApp
{
    public partial class ScrollHeaderView : UIView
    {
		public bool Expanded { get; set; }
		public UIView Content { get; set; }

        public ScrollHeaderView (IntPtr handle) : base (handle)
        {
			
        }

		public override bool PointInside(CoreGraphics.CGPoint point, UIEvent uievent)
		{
			return false || (Content != null && Content.Frame.Contains(point)) || Expanded;
		}
    }
}
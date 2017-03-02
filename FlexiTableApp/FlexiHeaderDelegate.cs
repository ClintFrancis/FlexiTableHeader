using System;
using Foundation;
using UIKit;

namespace FlexiTableApp
{
	public class FlexiHeaderDelegate : NSObject, IUITableViewDelegate
	{
		Func<nfloat, nfloat> updateHeightAction;

		protected NSLayoutConstraint heightConstraint;
		protected nfloat minHeight;
		protected nfloat baseHeight;
		protected UIEdgeInsets scrollBarInsets;

		public nfloat Offset { get; private set; }

		public FlexiHeaderDelegate(NSLayoutConstraint heightConstraint, nfloat minHeight)
		{
			this.heightConstraint = heightConstraint;
			this.minHeight = minHeight;
			this.baseHeight = heightConstraint.Constant;
			this.updateHeightAction = updateHeightAction ?? UpdateTargetHeight;
		}

		[Export("scrollViewDidScroll:")]
		public void Scrolled(UIScrollView scrollView)
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
			heightConstraint.Constant = updateHeightAction(targetHeight);
		}

		protected virtual nfloat UpdateTargetHeight(nfloat targetHeight)
		{
			// Pull Down
			if (Offset > 0 && targetHeight < minHeight)
			{
				targetHeight = minHeight;
			}

			return targetHeight;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				heightConstraint = null;
				updateHeightAction = null;
			}

			base.Dispose(disposing);
		}
	}
}

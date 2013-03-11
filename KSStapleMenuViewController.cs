using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace KSStapleMenu
{
	public partial class KSStapleMenuViewController : UIViewController
	{
		static bool UserInterfaceIdiomIsPhone
		{
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public KSStapleMenuViewController ()
			: base (UserInterfaceIdiomIsPhone ? "KSStapleMenuViewController_iPhone" : "KSStapleMenuViewController_iPad", null)
		{
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			// Perform any additional setup after loading the view, typically from a nib.
			UILabel lab = new UILabel ()
			{
				Text = "Hallo World!",
				Font = UIFont.SystemFontOfSize(40f),
				AutoresizingMask = UIViewAutoresizing.All
			};
			lab.SizeToFit ();
			this.View.AddSubview (lab);
			lab.Center = this.View.Center;
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			if (UserInterfaceIdiomIsPhone)
			{
				return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
			}
			else
			{
				return true;
			}
		}
	}
}


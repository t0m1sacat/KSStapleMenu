using System;
using MonoTouch.UIKit;

namespace KSStapleMenu
{
	public class KSMenuItemHostView : UIView
	{
		public KSMenuItemHostView () : base()
		{
		}

		public UIView ActiveElementView
		{
			get;
			set;
		}

		public UIView SubElementsView
		{
			get;
			set;
		}
	}
}


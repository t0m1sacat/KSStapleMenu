using System;
using System.Linq;
using System.Collections.Generic;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;

namespace KSStapleMenu
{
	public class KSStapleMenuItem
	{
		public const string SelectedIndexChangedNotif = "SelectedIndexChanged";
		public const string ElementAddedNotif = "ElementAdded";

		public KSStapleMenuItem (string id, UIImage image, string title = null)
		{
			this.Id = id;
			this.elements = new List<UIButton> ();
			this.AddElement (image, title);
		}

		public string Id
		{
			get;
			private set;
		}

		public UIView GetViewForIndex(int index)
		{
			if(this.elements.Count <= 0 || this.elements.Count < index)
			{
				return null;
			}
			return this.elements[index];
		}

		public int NumberOfElements
		{
			get
			{
				return this.elements == null ? 0 : this.elements.Count;
			}
		}

		public int SelectedIndex
		{
			get
			{
				return this.selectedIndex;
			}
			set
			{
				this.selectedIndex = value;
				NSNotificationCenter.DefaultCenter.PostNotificationName(SelectedIndexChangedNotif, this);
			}
		}
		private int selectedIndex;


		private List<UIButton> elements;

		public int AddElement(UIImage image, string title = null)
		{
			var button = UIButton.FromType (UIButtonType.Custom);
			button.SetImage (image, UIControlState.Normal);
			button.Frame = new RectangleF (new PointF (0, 0), image.Size);
			this.elements.Add (button);

			// Return index of the element.
			var index = this.elements.Count - 1;

			NSNotificationCenter.DefaultCenter.PostNotificationName(SelectedIndexChangedNotif, this);

			return index;
		}

		public float MaxWidth
		{
			get
			{
				if(this.elements == null)
				{
					return 0f;
				}
				return this.elements.Max(elem => elem.Bounds.Width);
			}
		}

		public float MaxHeight
		{
			get
			{
				if(this.elements == null)
				{
					return 0f;
				}
				return this.elements.Max(elem => elem.Bounds.Height);
			}
		}
	}
}


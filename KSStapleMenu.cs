using System;
using System.Linq;
using MonoTouch.UIKit;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace KSStapleMenu
{
	public class KSStapleMenu : UIControl
	{
		public enum STAPLEMENU_MODE
		{
			Left,
			Right,
			Bottom,
			Top
		}

		public KSStapleMenu (STAPLEMENU_MODE mode, float offset) : base()
		{
			this.Mode = mode;
			this.Offset = offset;
			switch(this.Mode)
			{
			case STAPLEMENU_MODE.Right :
				this.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin;
				break;
			case STAPLEMENU_MODE.Left :
				this.AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin;
				break;
			case STAPLEMENU_MODE.Bottom :
				this.AutoresizingMask = UIViewAutoresizing.FlexibleTopMargin;
				break;
			case STAPLEMENU_MODE.Top :
				this.AutoresizingMask = UIViewAutoresizing.FlexibleBottomMargin;
				break;
			}
		}

		public STAPLEMENU_MODE Mode
		{
			get;
			private set;
		}

		public float Offset
		{
			get;
			private set;
		}

		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		public void ExpandItem(string id, bool animated)
		{
			KSStapleMenuItem item = this.items.FirstOrDefault (itm => string.Compare (id, itm.Id, StringComparison.OrdinalIgnoreCase) == 0);
			if(item == null)
			{
				return;
			}


		}

		public void AddItems(params KSStapleMenuItem[] items)
		{
			Debug.Assert (items != null, "Provide valid items for initialization!");

			if (this.items == null)
			{
				this.items = new ObservableCollection<KSStapleMenuItem> ();
			}

			this.items.CollectionChanged -= this.HandleItemsCollectionChanged;
			foreach (var item in items)
			{
				this.items.Add (item);
				this.AddSubview(item.View);
			}
			this.items.CollectionChanged += this.HandleItemsCollectionChanged;
		}

		private void HandleItemsCollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			this.UpdateAlpha (this.Alpha);
			this.SetNeedsLayout ();
		}


		public override void WillMoveToSuperview (UIView newsuper)
		{
			base.WillMoveToSuperview (newsuper);
			float maxSize = this.MaxItemSize;
			if(this.IsVertical)
			{
				this.Frame = new RectangleF (0, 0, maxSize, maxSize * this.ItemCount);
			}
			else
			{
				this.Frame = new RectangleF (0, 0, maxSize * this.ItemCount, maxSize);
			}

			switch(this.Mode)
			{
			case STAPLEMENU_MODE.Right :
				this.Center = new PointF(newsuper.Bounds.Width - this.Bounds.Width / 2f, this.Offset + this.Bounds.Height / 2f);
				break;

			case STAPLEMENU_MODE.Left :
				this.Center = new PointF(this.Bounds.Width / 2f, this.Offset + this.Bounds.Height / 2f);
				break;

			case STAPLEMENU_MODE.Bottom :
				this.Center = new PointF(this.Offset + this.Bounds.Width / 2f, newsuper.Bounds.Height - this.Bounds.Height / 2f);
				break;

			case STAPLEMENU_MODE.Top :
				this.Center = new PointF(this.Offset + this.Bounds.Width / 2f, this.Bounds.Height / 2f);
				break;
			}
#if DEBUG
			this.BackgroundColor = UIColor.Red;
			this.Alpha = 0.5f;
#endif
		}

		public int ItemCount
		{
			get
			{
				return this.items != null ? this.items.Count : 0;
			}
		}

		public bool IsVertical
		{
			get
			{
				return this.Mode == STAPLEMENU_MODE.Left || this.Mode == STAPLEMENU_MODE.Right;
			}
		}

		public float MaxItemSize
		{
			get
			{
				if(this.ItemCount == 0)
				{
					return 0;
				}
				float maxSize = 0;

				if(this.IsVertical)
				{
					maxSize = this.items.Max(item => item.MaxWidth);
				}
				else
				{
					maxSize = this.items.Max(item => item.MaxHeight);
				}
				return maxSize;
			}
		}

		private ObservableCollection<KSStapleMenuItem> items;

		public override void LayoutSubviews ()
		{
			float maxSize = this.MaxItemSize;
			if(this.IsVertical)
			{
				float y = 0;
				foreach(var view in this.Subviews)
				{
					view.Center = new PointF(this.Bounds.Width / 2f, y + maxSize / 2f);
					y += maxSize;
				}
			}
			else
			{
				float x = 0;
				foreach(var view in this.Subviews)
				{
					view.Center = new PointF(x + maxSize / 2f, this.Bounds.Height / 2f);
					x += maxSize;
				}
			}

		}

		public override float Alpha
		{
			get
			{
				return base.Alpha;
			}
			set
			{
				this.UpdateAlpha(value);
				base.Alpha = value;
			}
		}

		private void UpdateAlpha(float alpha)
		{
			if(this.items == null)
			{
				return;
			}
			foreach(var item in this.items)
			{
				item.View.Alpha = alpha;
			}
		}
	}
}


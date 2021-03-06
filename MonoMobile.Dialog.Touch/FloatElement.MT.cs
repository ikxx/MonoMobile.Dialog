﻿//
// Elements.cs: defines the various components of our view
//
// Author:
//   Miguel de Icaza (miguel@gnome.org)
//
// Copyright 2010, Novell, Inc.
//
// Code licensed under the MIT X11 license
//
// TODO: StyledStringElement: merge with multi-line?
// TODO: StyledStringElement: add image scaling features?
// TODO: StyledStringElement: add sizing based on image size?
// TODO: Move image rendering to StyledImageElement, reason to do this: the image loader would only be imported in this case, linked out otherwise
//
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Drawing;
using MonoTouch.Foundation;
using MonoMobile.Dialog.Utilities;
using MonoTouch.CoreAnimation;

namespace MonoMobile.Dialog
{
	/// <summary>
	///  Used to display a slider on the screen.
	/// </summary>
	public partial class FloatElement : Element
	{
		static NSString skey = new NSString("FloatElement");
		UIImage Left, Right;
		UISlider slider;


		# region    Constructors
		//-------------------------------------------------------------------------
		public FloatElement(float value, float min, float max)
			: base(null)
		{
		}
		//-------------------------------------------------------------------------
		public FloatElement(UIImage left, UIImage right, float value)
			: base(null)
		{
			Left = left;
			Right = right;
			MinValue = 0;
			MaxValue = 1;
			Value = value;
		}
		//-------------------------------------------------------------------------
		# endregion Constructors


		protected override NSString CellKey
		{
			get
			{
				return skey;
			}
		}
		public override UITableViewCell GetCell(UITableView tv)
		{
			var cell = tv.DequeueReusableCell(CellKey);
			if (cell == null)
			{
				cell = new UITableViewCell(UITableViewCellStyle.Default, CellKey);
				cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			}
			else
				RemoveTag(cell, 1);

			SizeF captionSize = new SizeF(0, 0);
			if (Caption != null && ShowCaption)
			{
				cell.TextLabel.Text = Caption;
				captionSize = cell.TextLabel.StringSize(Caption, UIFont.FromName(cell.TextLabel.Font.Name, UIFont.LabelFontSize));
				captionSize.Width += 10; // Spacing
			}

			if (slider == null)
			{
				slider = new UISlider(new RectangleF(10f + captionSize.Width, 12f, 280f - captionSize.Width, 7f))
				{
					BackgroundColor = UIColor.Clear,
					MinValue = this.MinValue,
					MaxValue = this.MaxValue,
					Continuous = true,
					Value = this.Value,
					Tag = 1
				};
				slider.ValueChanged += delegate
				{
					Value = slider.Value;
				};
			}
			else
			{
				slider.Value = Value;
			}

			cell.ContentView.AddSubview(slider);
			return cell;
		}


		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (slider != null)
				{
					slider.Dispose();
					slider = null;
				}
			}
		}
	}
}
using Cyvasse.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Cyvasse.Model {
	/// <summary>
	/// A class that models a generic piece on the game board. Will likely later be made into an interface.
	/// </summary>
    public class GamePiece : ObservableObject {
		/// <summary>
		/// This is a BitmapImage and not an image, since this just supplies the scource for the picture in the XAML.
		/// </summary>
		private BitmapImage _image;
		public BitmapImage Image
		{
			get { return _image; }
			set
			{
				if (_image != value)
				{
					_image = value;
					OnPropertyChanged("Image");
				}
			}
		}

		private Point _position;
		public Point Position
		{
			get { return _position; }
			set
			{
				if (Position != value)
				{
					_position = value;
					OnPropertyChanged("Position");
				}
			}
		}

		public Thickness Margin { get { return new Thickness(Position.X, Position.Y, 0, 0); } }

		private bool _isOnTop;
		public bool IsOnTop
		{
			get { return _isOnTop; }
			set
			{
				if (IsOnTop != value)
				{
					_isOnTop = value;
					OnPropertyChanged("IsOnTop");
				}
			}
		}

		public GamePiece(Point p)
		{
			_image = new BitmapImage(new Uri(@"/Resources/apple.jpg", UriKind.Relative));
			Position = p;
		}
	}
}

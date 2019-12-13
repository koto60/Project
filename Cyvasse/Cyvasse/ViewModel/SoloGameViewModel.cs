using Cyvasse.Model;
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

namespace Cyvasse.ViewModel
{
	/// <summary>
	/// The viewmodel for the solegame view.
	/// </summary>
    public class SoloGameViewModel : ObservableObject, IViewModel {
		
		private Board _board;
		public Board Board
		{
			get { return _board; }
			set {
				if (_board != value)
				{
					_board = value;
					OnPropertyChanged("Board");
				}
			}
		}

		private GamePiece _carryPiece;
		public GamePiece CarryPiece
		{
			get { return _carryPiece; }
			set
			{
				if (_carryPiece != value)
				{
					_carryPiece = value;
					OnPropertyChanged("CarryPiece");
				}
			}
		}

		private ICommand _getPieceOnCommand;
		public ICommand GetPieceOnCommand
		{
			get
			{
				if (_getPieceOnCommand == null)
					_getPieceOnCommand = new RelayCommand(p => getPieceOn(p), p => canExecuteGetPieceOn(p));
				return _getPieceOnCommand;
			}
		}

		private ICommand _movePieceCommand;
		public ICommand MovePieceCommand
		{
			get
			{
				if (_movePieceCommand == null)
					_movePieceCommand = new RelayCommand(p => movePiece(p), p => canExecuteMovePiece());
				return _movePieceCommand;
			}
		}

		private ICommand _dropPieceCommand;
		public ICommand DropPieceCommand
		{
			get
			{
				if (_dropPieceCommand == null)
					_dropPieceCommand = new RelayCommand(p => dropPiece(p), p => canExecuteDropPiece());
				return _dropPieceCommand;
			}
		}

		private Point originalPosition;

		public SoloGameViewModel()
		{
			_board = new Board(8, 8);
			Point p = new Point(80, 80);
			Board.GameBoard.Add(new GamePiece(p));
		}

		/// <summary>
		/// Gets a point that is the mouse position recalculated, so that it follows the size of the grid.
		/// </summary>
		/// <param Point="o"></param>
		private void getPieceOn(Object o)
		{
			Point cursorPosition = (Point)o;
			

			foreach (GamePiece p in _board.GameBoard)
			{
				if (p.Position.X == cursorPosition.X && p.Position.Y == cursorPosition.Y)
				{
					//The carrypiece it the piece currently being moved. It is made and then put on top so that it is always drawn on top. Then its original position is saved.
					CarryPiece = p;
					CarryPiece.IsOnTop = true;
					originalPosition = p.Position;
				}
			}
		}

		private bool canExecuteGetPieceOn(Object o)
		{
			Point cursorPosition = (Point)o;

			foreach (GamePiece p in Board.GameBoard)
			{
				if (p.Position.X == cursorPosition.X && p.Position.Y == cursorPosition.Y)
				{
					return true;
				}
			}
			return false;
		}

		private void movePiece(Object o)
		{
			Random rand = new Random();

			int x = rand.Next(7)*40;
			int y = rand.Next(7)*40;

			CarryPiece.Position = new Point(x, y);

			//CarryPiece.Position = (Point)o;
		}

		private bool canExecuteMovePiece()
		{
			if (CarryPiece != null)
			{
				return true;
			}
			return false;
		}

		private void dropPiece(Object o)
		{
			Point cursorPosition = (Point)o;
			bool pieceOnSpot = false;

			//Checks if the spot is occupied
			foreach(GamePiece p in Board.GameBoard)
			{
				if (p.Position.X == cursorPosition.X && p.Position.Y == cursorPosition.Y)
				{
					pieceOnSpot = true;
				}
			}

			//If the spot is occupied: revert to original position. Else: put piece in new spot.
			if (pieceOnSpot)
				CarryPiece.Position = originalPosition;
			 else
				CarryPiece.Position = cursorPosition;

			//Make the piece not on top anymore, and null carrypiece so that we know we are finished dragging.
			CarryPiece.IsOnTop = false;
			CarryPiece = null;
		}

		private bool canExecuteDropPiece()
		{
			if (CarryPiece != null)
			{
				return true;
			}
			return false;
		}
	}
}

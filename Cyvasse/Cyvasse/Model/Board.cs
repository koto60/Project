using Cyvasse.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Cyvasse.Model {
    /// <summary>
    /// A class that models the idea of a game board, with a size and a collection of game pieces.
    /// </summary>
    public class Board : ObservableObject {
        /// <summary>
        /// The GameBoard is an ObservableCollectionEx. This is a custom version of ObservableCollection, that upadtes when properties in its elements are updated.
        /// </summary>
        ObservableCollectionEx<GamePiece> _gameBoard;
        public ObservableCollectionEx<GamePiece> GameBoard
        {
            get 
            { return _gameBoard; }
            set
            {
                if (_gameBoard != value)
                {
                    _gameBoard = value;
                    OnPropertyChanged("GameBoard");
                }
            }
        }


        private readonly int _width;
        public int Width
        {
            get { return _width; }
        }

        private readonly int _height;
        public int Height
        {
            get { return _height; }

        }

        /// <summary>
        /// Contructor that takes size. The size is readonly, since it is not designed to chenge.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Board(int width, int height) {
            _gameBoard = new ObservableCollectionEx<GamePiece>();
            _width = width;
            _height = height;

            //todo delete this test code
            Point p = new Point(160, 160);
            _gameBoard.Add(new GamePiece(p));
        }
    }
}
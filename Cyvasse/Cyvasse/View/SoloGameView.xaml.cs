using Cyvasse.Model;
using Cyvasse.Utility;
using Cyvasse.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cyvasse.View
{
    /// <summary>
    /// Interaction logic for SoloGameView.xaml
    /// </summary>
    public partial class SoloGameView : UserControl
    {
        public SoloGameView()
        {
            InitializeComponent();
        }

        private Point cursorPosition;
        private Point offset;

        //This views assigned viewmodel.
        private SoloGameViewModel viewModel;

        /// <summary>
        /// Standard MouseLeftButtonDown event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            cursorPosition = e.GetPosition(playCanvas);
            Point position = scalePointToBoardPosition(cursorPosition);

            if (viewModel.GetPieceOnCommand.CanExecute(position))
            {
                 viewModel.GetPieceOnCommand.Execute(position);

                //Calculates the offset from the top left corner of the game piece, to the actual point the cursor clicked. This is to make dragging look better.
                offset = new Point(cursorPosition.X - viewModel.CarryPiece.Position.X, cursorPosition.Y - viewModel.CarryPiece.Position.Y);
            }
        }

        /// <summary>
        /// Standard MouseMove event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (viewModel.MovePieceCommand.CanExecute(null))
                {
                    cursorPosition = e.GetPosition(playCanvas);
                    //Applies the offset mentioned before. The values are negated because of the way the offset is calculated.
                    cursorPosition.Offset(-offset.X, -offset.Y);

                    viewModel.MovePieceCommand.Execute(cursorPosition);
                }
            }
        }

        /// <summary>
        /// Standard MouseLeftButtonUp event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (viewModel.DropPieceCommand.CanExecute(null))
            {
                cursorPosition = e.GetPosition(playCanvas);
                Point position = scalePointToBoardPosition(cursorPosition);

                viewModel.DropPieceCommand.Execute(position);
            }
        }

        /// <summary>
        /// DataContextChanged event to get the datacontext. It would be done in the contructor, but the datacontext is set after initialization.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //We cast since we are certain of the object type.
            viewModel = (SoloGameViewModel)DataContext;
        }

        /// <summary>
        /// Calculates the cursors postion on the play board according to size of board and window.
        /// </summary>
        /// <param Cursor point="p"></param>
        /// <returns>Position on play board</returns>
        public Point scalePointToBoardPosition(Point p)
        {
            double x = p.X - (p.X % (playCanvas.ActualWidth / viewModel.Board.Width));
            double y = p.Y - (p.Y % (playCanvas.ActualHeight / viewModel.Board.Height));

            return new Point(x, y);
        }
    }
}
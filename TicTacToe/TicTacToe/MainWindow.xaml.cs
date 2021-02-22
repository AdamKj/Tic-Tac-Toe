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

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Private Members

        // Resultatet för varje box inom spelets gång
        private MarkType[] mResults;


        // Sant om det är spelare 1 tur (X) eller spelare 2 tur (O)
        private bool mPlayer1Turn;

        
        // Sant om spelet är slut
        private bool mGameEnded;



        #endregion


        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion 

        /// Startar ett nytt spel och rensar spelplanen
        private void NewGame()
        {
            // Skapar en ny array av varje tom box
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++) 
               mResults[i] = MarkType.Free;

                // Ser till att spelare 1 startar spelet
                mPlayer1Turn = true;

                
                // Loopar varje knapp i spelet
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    // Ändrar bakgrundsfärgen, färgen på X och O och rensar spelplanen
                    button.Content = string.Empty;
                    button.Background = Brushes.White;
                    button.Foreground = Brushes.Blue;
                });

            // Ser till att spelet inte är över
            mGameEnded = false;
            
        }

        /// <summary>
        /// Hanterar när vi klickar på en box
        /// </summary>
        /// <param name="sender">Boxen som var tryckt</param>
        /// <param name="e">Händelsen av tryckningen</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Startar om spelet med ett knapptryck när spelet är klart
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            // Skickar sender till ett knapptryck
            var button = (Button)sender;

            // Kollar tryckningens position i arrayen
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            // Gör inget om boxen redan har ett värde, alltså X eller O
            if (mResults[index] != MarkType.Free)
                return;

            // Ger boxen ett värde beroende på vilken spelares tur det är
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            // Ger tryckningarna ett värde
            button.Content = mPlayer1Turn ? "X" : "O";

            // Ändrar O till röd färg
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;


            // Kollar vems tur det är
            mPlayer1Turn ^= true;

            // Kollar vem som har vunnit
            CheckForWinner();
        }

        /// <summary>
        /// Kollar om det är någon som har vunnit via 3 i rad
        /// </summary>
        private void CheckForWinner()
        {
            #region Horisontala vinster
            // Kollar om horisontal vinst
            //
            // Rad 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                // Spelet slutar
                mGameEnded = true;

                // Vinnande boxar blir gröna
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            //
            // Rad 1
            //
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                // Spelet slutar
                mGameEnded = true;

                // Vinnande boxar blir gröna
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            //
            // Rad 2
            //
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                // Spelet slutar
                mGameEnded = true;

                // Vinnande boxar blir gröna
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Vertikala vinster
            // Kollar om vertikal vinst
            //
            // Kolumn 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                // Spelet slutar
                mGameEnded = true;

                // Vinnande boxar blir gröna
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            //
            // Kolumn 1
            //
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                // Spelet slutar
                mGameEnded = true;

                // Vinnande boxar blir gröna
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            //
            // Kolumn 2
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                // Spelet slutar
                mGameEnded = true;

                // Vinnande boxar blir gröna
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Diagonala vinster
            // Kollar om diagonal vinst
            //
            // Uppe vänster Nere höger
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                // Spelet slutar
                mGameEnded = true;

                // Vinnande boxar blir gröna
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            //
            // Uppe höger Nere vänster
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                // Spelet slutar
                mGameEnded = true;

                // Vinnande boxar blir gröna
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }


            #endregion

            #region Inga vinster
            // Kollar om ingen vinner och alla boxar är fulla
            if (!mResults.Any(result => result == MarkType.Free))
            {
                // Spelet är klart
                mGameEnded = true;

                // Gör alla boxar till orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    
                    button.Background = Brushes.Orange;
                });
                
            }
            #endregion
        }
    }
}

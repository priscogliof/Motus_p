using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;

using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace Motus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private Grid board;
        private int letters;
        private TextBox inputWord;
        private Jeu game;
        private Button sendButton;
        private Button bRestart;

        public MainWindow()
        //i dont know how to make the grid start from the top left and generate the rest of it dynamically
        //i would have liked to open this window in fullscreen/maximize
        {

            this.MinHeight = 800;
            this.MinWidth = 600;
            
            InitializeComponent();
            this.startGame();
        }

        public void startGame()
        {
            game = new Jeu();
            game.startGame();
            this.Content = createGrillBoard(game.getActualTurn(), game.getNumberCase());
            displayTurn(game.getActualTurn());
        }
        public Grid createGrillBoard(int currentTurn, int wordLetter)
        {
            //i am making and initializing the grid dynamically in order to display words of different sizes
            this.board = new Grid();

            this.board.Width = 80*wordLetter;
            this.board.Height = 80* wordLetter;
            this.board.Background = Brushes.Black;
            
            letters = wordLetter;
            this.board.ShowGridLines = false;
            List<ColumnDefinition> columns = new List<ColumnDefinition>();
            System.Diagnostics.Debug.WriteLine("1");
            List<RowDefinition> rows = new List<RowDefinition>();
            System.Diagnostics.Debug.WriteLine("2");

            //the grid is bigger than the number of letter in a word in order to contain the buttons
            for (int i = 0; i < wordLetter+2 ; i++)
            { 
                
                columns.Add(new ColumnDefinition());
                board.ColumnDefinitions.Add(columns[i]);
                rows.Add(new RowDefinition());
                board.RowDefinitions.Add(rows[i]);
            }

            //I made a button to propose a solution to the game
            System.Windows.Controls.Button btnSend = new System.Windows.Controls.Button();
            btnSend.Name = "Proposer";
            btnSend.Content = "Proposer";
            btnSend.FontSize = 8;
            btnSend.Height = 80;
            btnSend.Width = 80;

            
            Grid.SetColumn(btnSend, 1);
            Grid.SetRow(btnSend, letters + 1);
            board.Children.Add(btnSend);


            //i made a button to restart the game
            this.sendButton = btnSend;
            sendButton.AddHandler(System.Windows.Controls.Button.ClickEvent, new RoutedEventHandler(OnButtonClick));
            System.Windows.Controls.Button btnRestart = new System.Windows.Controls.Button();
            btnRestart.Name = "Recommencer";
            btnRestart.Content = "Recommencer";
            btnRestart.FontSize = 8;
            btnRestart.Height = 80;
            btnRestart.Width = 80;



            btnRestart.AddHandler(System.Windows.Controls.Button.ClickEvent, new RoutedEventHandler(OnButtonClick));
            Grid.SetColumn(btnRestart, letters);
            Grid.SetRow(btnRestart, letters + 1);
            board.Children.Add(btnRestart);

            this.bRestart = btnRestart;

            return board;
        }
        public void displayTurn(int currentTurn)
        ///im displaying a turn of game
        {
            var tb = new TextBox();
            tb.Text = "mot";
            Grid.SetColumn(tb, 0);
            Grid.SetRow(tb, letters + 1);
            board.Children.Add(tb);

            if (game.getVictory())
            {
                TextBlock txtv = new TextBlock();
                txtv.Width = 80;
                txtv.Height = 80;
                txtv.FontSize = 10;


                txtv.Text = $"Félicitation {Environment.NewLine} vous avez {Environment.NewLine} gagné";
                txtv.TextAlignment = TextAlignment.Left;
                txtv.FontWeight = FontWeights.Bold;


                txtv.Background = Brushes.Green;

                Grid.SetColumn(txtv, 3);
                Grid.SetRow(txtv, letters + 1);
                board.Children.Add(txtv);
                this.sendButton.IsEnabled = false;


            }
            if (currentTurn >= game.getNumberCase())
            {
                TextBlock txtv = new TextBlock();
                txtv.Width = 80;
                txtv.Height = 80;
                txtv.FontSize = 10;


                txtv.Text = $"vous avez {Environment.NewLine} perdu";
                txtv.TextAlignment = TextAlignment.Left;
                txtv.FontWeight = FontWeights.Bold;


                txtv.Background = Brushes.Red;

                Grid.SetColumn(txtv, 3);
                Grid.SetRow(txtv, letters + 1);
                board.Children.Add(txtv);
              
                this.sendButton.IsEnabled = false;
            }
            else
            { 
                if (currentTurn == 0)
                {
                    displayGrayLine(0);
                }
                else
                {
                        for (int i = 0; i < game.getNumberCase(); i++)
                        ///In order to display each letter of the solution, i create a textblock for each
                        ///I change the background accordingly to the state of each letter : right, wrong or within the word to find
                        {
                            TextBlock txt = new TextBlock();

                            if (i < inputWord.Text.Length)
                            {
                                txt.Text = inputWord.Text.Substring(i, 1);
                            }


                            txt.Width = 40;
                            txt.Height = 40;
                            txt.FontSize = 20;


                            txt.TextAlignment = TextAlignment.Center;
                            txt.FontWeight = FontWeights.Bold;


                            txt.Background = game.checkLetter(txt.Text, i);
                            txt.Margin = new Thickness(5);
                            Grid.SetColumn(txt, i);
                            Grid.SetRow(txt, currentTurn - 1);
                            board.Children.Add(txt);
                        }
                }
            }
            inputWord = tb;



        }

        private void displayGrayLine(int currentTurn)
        /// i display an empty line
        {
            for (int i = 0; i < game.getNumberCase(); i++)
            {
                TextBlock txt = new TextBlock();
                txt.Text = "";

                txt.FontSize = 20;
                txt.TextAlignment = TextAlignment.Center;
                
                txt.Width = 40;
                txt.Height = 40;
                txt.FontWeight = FontWeights.Bold;
                txt.Background = Brushes.LightGray;
                txt.Margin = new Thickness(5);
                Grid.SetColumn(txt, i);
                Grid.SetRow(txt, currentTurn);
                board.Children.Add(txt);
            }
            
            
        }
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {

            if (sender == sendButton) {
                // i check and modify the state of the game accordingly to the solution proposed
                System.Diagnostics.Debug.WriteLine(inputWord.Text);


                game.checkWord(inputWord.Text);
                game.incrementActualTurn();
                displayTurn(game.getActualTurn());
            }
            if (sender == bRestart)
            {
                startGame();
            }
        }
    }
           
    
}

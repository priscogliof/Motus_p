using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Motus
{
    public class Board 
    {
        private Grid board;

        public Board(){
            board = new Grid();

            board.Width = 250;
            board.Height = 100;
            board.ShowGridLines = true;
            ///displayBoard(0, 5);
    }
    public Grid displayBoard(int currentTurn, int wordLetter)
    {
        List<ColumnDefinition> columns = new List<ColumnDefinition>();
        System.Diagnostics.Debug.WriteLine("1");
        List<RowDefinition> rows = new List<RowDefinition>();
        System.Diagnostics.Debug.WriteLine("2");
        for (int i = 0; i < wordLetter - 1; i++)
        {
            columns.Add(new ColumnDefinition());
            board.ColumnDefinitions.Add(columns[i]);
            rows.Add(new RowDefinition());
            board.RowDefinitions.Add(rows[i]);
        }
        TextBlock txt = new TextBlock();
        txt.Text = "TextBlock";
        txt.FontSize = 20;
        txt.FontWeight = FontWeights.Bold;
        Grid.SetColumn(txt, 0);
        Grid.SetRow(txt, 0);
        board.Children.Add(txt);

            return board;
    }

}
}

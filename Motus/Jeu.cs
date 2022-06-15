using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Motus
{
    internal class Jeu
    {
        private string[] words = new string[7] {"salade","vert","autruche","guitare","autre","chat","ordinateur"};
        private string word;
        int actualTurn = 0;
        int maxRound = 5;
        int numberCase = 5;
        bool victory = false;
        
        public Jeu()
        {
            
            System.Diagnostics.Debug.WriteLine("truc");
            System.Diagnostics.Debug.WriteLine(word);
            
            
        }

         public void startGame()
        {
            word = getNewWord();
            maxRound = word.Length;
            numberCase = maxRound;
        }
        private string getNewWord()
        // finding the word to find using random way
        {
            return words[DateTime.Now.Second % words.Length];
        }

        public int getNumberCase()
        {
            return numberCase;
        }

        public string getWord()
        {
            return word;
        }

        public int getActualTurn()
        {
            return actualTurn;
        }

        public void incrementActualTurn()
        {
            actualTurn++;
        }

        public Brush checkLetter(string text, int i)
        //checking the state of each letter and giving back a background-color accordingly
        //Green : good position within the word
        //Yellow : within the words
        //Red : no such letter in the word
        {
            if(word.Substring(i,1) == text){
                return Brushes.Green;

            }
            else
            {
                if (word.Contains(text))
                {
                    return Brushes.Yellow;
                }
                else
                {
                    return Brushes.Red;
                }
            } 
                
            throw new NotImplementedException();
        }

        public void checkWord(string paramWord)
        {
            if(word == paramWord)
            {
                victory = true; 
            }
        }
        internal bool getVictory()
        {
            return victory;
        }

        public Jeu getJeu()
        {
            return this;
        }
    }
}

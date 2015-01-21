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
using System.Text.RegularExpressions;

namespace MathQuiz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Init variables
        int userAnswer = 0;
        int currentQuestion = 1;
        int numberOne, numberTwo, chooseOperator = 0;
        int correctAnswer = 0;
        int totalCorrect = 0;
        int totalWrong = 0;
        int maxQuestions = 0;
        int highNumber = 0;
        int passRate = 0;
        
        Random rnd = new Random();


        public MainWindow()
        {
       
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // Get difficulty level from radio buttons
            if (rbEasy.IsChecked.Value)
            {
                highNumber = 10;
                passRate = 7;
            }
            if (rbMedium.IsChecked.Value)
            {
                highNumber = 25;
                passRate = 18;
            }
            if (rbHard.IsChecked.Value)
            {
                highNumber = 50;
                passRate = 35;
            }
            // Get number of questions from drop down list 
            maxQuestions = Convert.ToInt16(comboBox1.Text);
            revealQuiz();
            askQuestion();
            hideChoices();

        }

        private void askQuestion() 
        {
            
            numberOne = rnd.Next(1, highNumber);
            numberTwo = rnd.Next(1, highNumber);
            chooseOperator = rnd.Next(1, 1000);
            Keyboard.Focus(tbAnswer);
            lbQuizStatus.Content = "Question " + Convert.ToString(currentQuestion) + " of " + Convert.ToString(maxQuestions);
            if (chooseOperator % 2 == 0) // Even, +
            {
                lblQuestion.Content = Convert.ToString(numberOne) + " + " + Convert.ToString(numberTwo);
                correctAnswer = addition(numberOne, numberTwo);
            }
            else // Odd, *
            {
                lblQuestion.Content = Convert.ToString(numberOne) + " * " + Convert.ToString(numberTwo);
                correctAnswer = multiplication(numberOne, numberTwo);
            }
        }

        private int addition(int num1, int num2)
        {
            int answer = num1 + num2;
            return answer;
        }
        private int multiplication(int num1, int num2)
        {
            int answer = num1 * num2;
            return answer;
        }
        private void btnAnswer_Click(object sender, RoutedEventArgs e)
        {

            if (tbAnswer.Text == "")  

            {
                MessageBox.Show("You must enter a number.");
                tbAnswer.Text = "0";
                
            }
            //check answer and ask next question if needed
            userAnswer = Convert.ToInt16(tbAnswer.Text);
            //int.TryParse(tbAnswer.Text, out userAnswer);
            
            if (userAnswer == correctAnswer)
            {
                totalCorrect += 1;
                lblNumberCorrect.Content = "Correct " + Convert.ToString(totalCorrect);
                if (currentQuestion < maxQuestions)
                {
                    currentQuestion += 1;
                    tbAnswer.Text = "";
                    askQuestion();
                    
                }
                else
                {
                    gameOver();
                }
            }
            else
            {
                totalWrong += 1;
                lblNumberWrong.Content = "Wrong " + Convert.ToString(totalWrong);
                if (currentQuestion < maxQuestions)
                {
                    currentQuestion += 1;
                    tbAnswer.Text = "";
                    askQuestion();
                }
                else
                {
                    gameOver();
                }
            }
            
        }

        private void gameOver()
        {
            if (totalCorrect > passRate)
            {
                PlaySound();
            }
            MessageBox.Show("Game Over!");
            btnPlayAgain.Visibility = System.Windows.Visibility.Visible;
        }

        private void playAgain_Click(object sender, RoutedEventArgs e)
        {
            resetGame();
            
        }

        private void resetGame()
        {
            showChoices();
            userAnswer = 0;
            currentQuestion = 1;
            numberOne = 0;
            numberTwo = 0;
            chooseOperator = 0;
            correctAnswer = 0;
            totalCorrect = 0;
            totalWrong = 0;
            maxQuestions = 0;
            highNumber = 0;
            lblQuestion.Visibility = System.Windows.Visibility.Hidden;
            lbQuizStatus.Visibility = System.Windows.Visibility.Hidden;
            lblNumberCorrect.Visibility = System.Windows.Visibility.Hidden;
            lblNumberWrong.Visibility = System.Windows.Visibility.Hidden;
            tbAnswer.Visibility = System.Windows.Visibility.Hidden;
            btnAnswer.Visibility = System.Windows.Visibility.Hidden;
            btnPlayAgain.Visibility = System.Windows.Visibility.Hidden;
            Random rnd = new Random();
            // Get difficulty level from radio buttons
            if (rbEasy.IsChecked.Value)
            {
                highNumber = 10;
                passRate = 7;
            }
            if (rbMedium.IsChecked.Value)
            {
                highNumber = 25;
                passRate = 18;
            }
            if (rbHard.IsChecked.Value)
            {
                highNumber = 50;
                passRate = 35;
            }
            // Get number of questions from drop down list
            maxQuestions = Convert.ToInt16(comboBox1.Text);
            tbAnswer.Text = "";
            lblNumberCorrect.Content = "Correct 0";
            lblNumberWrong.Content = "Wrong 0";

        }

        private void showChoices()
        {
            groupBox.Visibility = System.Windows.Visibility.Visible;
            rbEasy.Visibility = System.Windows.Visibility.Visible;
            rbMedium.Visibility = System.Windows.Visibility.Visible;
            rbHard.Visibility = System.Windows.Visibility.Visible;
            comboBox1.Visibility = System.Windows.Visibility.Visible;
            btnStart.Visibility = System.Windows.Visibility.Visible;
            lblNumberOfQuestions.Visibility = System.Windows.Visibility.Visible;
        }
        private void hideChoices()
        {
            groupBox.Visibility = System.Windows.Visibility.Hidden;
            rbEasy.Visibility = System.Windows.Visibility.Hidden;
            rbMedium.Visibility = System.Windows.Visibility.Hidden;
            rbHard.Visibility = System.Windows.Visibility.Hidden;
            comboBox1.Visibility = System.Windows.Visibility.Hidden;
            btnStart.Visibility = System.Windows.Visibility.Hidden;
            lblNumberOfQuestions.Visibility = System.Windows.Visibility.Hidden;

        }

        private void revealQuiz()
        {
            lblQuestion.Visibility = System.Windows.Visibility.Visible;
            lbQuizStatus.Visibility = System.Windows.Visibility.Visible;
            lblNumberCorrect.Visibility = System.Windows.Visibility.Visible;
            lblNumberWrong.Visibility = System.Windows.Visibility.Visible;
            tbAnswer.Visibility = System.Windows.Visibility.Visible;
            btnAnswer.Visibility = System.Windows.Visibility.Visible;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void PlaySound()
        {
            //Uri uri = new Uri(@"C:\Users\Garth\Documents\Visual Studio 2013\Projects\MathQuiz\MathQuiz\Audio\Cheer.mp3");
            Uri uri = new Uri(@"..\..\Audio\Cheer.mp3", UriKind.Relative);
            var player = new MediaPlayer();
            player.Open(uri);
            player.Play();
        }
        

            
        

       
} 

    
}

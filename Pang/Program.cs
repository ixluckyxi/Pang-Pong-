using System;
using System.Linq;

namespace Paddeball

{
    class Program
    {
        static void Main(string[] args)
        {
            //Create some constant variables that hold information about the field
            const int fieldLength = 100, fieldWidth = 25;

            //Create the field tiles or wall
            const char fieldWall = '-';
            string line = string.Concat(Enumerable.Repeat(fieldWall, fieldLength)); //this method will repeat this field wall character 100 times 

            //Create variables about the paddles
            const int paddleLength = fieldWidth / 5;
            const char paddle = '|';

            //Create some variable that hold information about paddles and where they are on the field
            int leftPaddleHeight = 0;
            int rightPaddleHeight = 0;

            //Create variables that hold information about the ball
            int ballX = fieldLength / 2;
            int ballY = fieldWidth / 2;
            const char ball = 'O'; //what does the ball look like

            //Create directions of the Ball
            bool isBallGoingDown = true;
            bool isBallGoingRight = true;

            int leftPlayerPoints = 0;
            int rightPlayerPoints = 0;

            int scoreboardX = fieldLength / 2;
            int scoreboardY = fieldWidth + 3;


            //Create the game loop
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(line);

                Console.SetCursorPosition(0, fieldWidth);
                Console.WriteLine(line);


                //Create another loop that starts from zero and continues to paddle length minus one
                for (int i = 0; i < paddleLength; i++)
                {
                    Console.SetCursorPosition(0, i + 1 + leftPaddleHeight); //This allows our cursor to start below the line on top
                    Console.WriteLine(paddle);
                    Console.SetCursorPosition(fieldLength - 1, i + 1 + rightPaddleHeight);
                    Console.WriteLine(paddle);
                }

                //update values by checking if user has pressed any keys - the value will be true when a key is pressed 
                while (!Console.KeyAvailable)
                {
                    //everytime the up key isn't pressed update the ball position
                    Console.SetCursorPosition(ballX, ballY);
                    Console.WriteLine(ball);
                    //Add a timer to give players time to react (also known as ball speed)
                    Thread.Sleep(75);

                    //remove the old position of the ball so it doesn't show
                    Console.SetCursorPosition(ballX, ballY);
                    Console.WriteLine(' ');


                    //update the position depending on where the ball is moving
                    if (isBallGoingDown)
                    {
                        ballY++;

                    }
                    else
                    {
                        ballY--;
                    }
                    if (isBallGoingRight)
                    {
                        ballX++;

                    }
                    else
                    {
                        ballX--;
                    }
                    //check if ball has reached the bottom/top walls
                    if (ballY == 1 || ballY == fieldWidth - 1)
                    {
                        isBallGoingDown = !isBallGoingDown;
                    }

                    //check if ball has reached the left paddle
                    if (ballX == 1)
                    {
                        if (ballY >= leftPaddleHeight + 1 && ballY <= leftPaddleHeight + paddleLength) //check if second coordinate matches second paddle
                        {
                            isBallGoingRight = !isBallGoingRight;
                            
                        }

                        else //if ball misses the paddle and doesn't match the coordinate, increase other player points
                        {
                            rightPlayerPoints++;
                            //reset ball position after points are awarded 
                            ballY = fieldWidth / 2;
                            ballX = fieldLength / 2;

                            //update right player points on scoreboard
                            Console.SetCursorPosition(scoreboardX, scoreboardY);
                            Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");

                            if (rightPlayerPoints == 5)
                            {
                                goto outer;
                            }
                        }
                    }
                    if (ballX == fieldLength - 2) //we set to 2 because our right paddle is set to fieldlength - 1 
                    {
                        if (ballY >= rightPaddleHeight + 1 && ballY <= rightPaddleHeight + paddleLength) //check if second coordinate matches second paddle
                        {
                            isBallGoingRight = !isBallGoingRight;
                        }
                        else //if ball misses the paddle and doesn't match the coordinate, increase other player points
                        {
                            leftPlayerPoints++;
                            //reset ball position after points are awarded 
                            ballY = fieldWidth / 2;
                            ballX = fieldLength / 2;

                            //update left player points on scoreboard
                            Console.SetCursorPosition(scoreboardX, scoreboardY);
                            Console.WriteLine($"{leftPlayerPoints} | {rightPlayerPoints}");

                            if (leftPlayerPoints == 5)
                            {
                                goto outer;
                            }
                        }
                    }
                }

                //When a key is pressed check which key it is
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        if (rightPaddleHeight > 0) //check if right paddle is more than zero, you don't want to go over the field
                        {
                            rightPaddleHeight--; //if its over zero we can decrease the value
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (rightPaddleHeight < fieldWidth - paddleLength - 1) //check if right paddle is more than zero, you don't want to go over the field
                        {
                            rightPaddleHeight++; //if its over zero we can decrease the value
                        }
                        break;
                    case ConsoleKey.W:
                        if (leftPaddleHeight > 0) //check if right paddle is more than zero, you don't want to go over the field
                        {
                            leftPaddleHeight--; //if its over zero we can decrease the value
                        }
                        break;
                    case ConsoleKey.S:
                        if (leftPaddleHeight < fieldWidth - paddleLength - 1) //check if right paddle is more than zero, you don't want to go over the field
                        {
                            leftPaddleHeight++; //if its over zero we can decrease the value
                        }
                        break;
                }
                //delete the entire line everytime we update it
                for (int i = 1; i < fieldWidth; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.WriteLine(" ");
                    Console.SetCursorPosition(fieldLength - 1, i);
                    Console.WriteLine(" ");
                }




            }

        outer:;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            if (rightPlayerPoints == 5)
            {
                Console.WriteLine("Right Player Won!");
            }
            else
            {
                Console.WriteLine("Left Player Won!");
            }






        }

       
    }








}
using System;

namespace OpenGLLearning
{
    class Program
    {
        static void Main(string[] args)
        {
            // Запускаем прогу в отдельном окне, задав параметры окна
            using (Game game = new Game(800,600, "LearnOpenTK"))
            {
                // 60,0 -- частота обновления картинки
                game.Run(60.0);
            }
        }
    }
}
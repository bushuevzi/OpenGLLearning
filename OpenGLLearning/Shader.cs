using System;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace OpenGLLearning
{
    /// <summary>
    /// Шейдерная программа 
    /// </summary>
    public class Shader
    {
        // Дескриптор хранящий в себе "ссылку" на данный объект
        int _handle;

        // Инструкции по сборке простого шейдера
        // Шейдеры написаны на GLSL. исходники данного языка компилируются в runtime,
        // таким образом они могут быть оптимизированы под графическую карту которую использует пользователь
        public Shader(string vertexPath, string fragmentPath)
        {
            //------------------офтоп----------------------------------------------------------------
            // Существуеют несколько типов шейдеров но практически для базового рендеренга используются только 2:
            // vertex shader и fragment shader
            // Vertex shader - отвечает для перемещение вершин и передачи данных в фрагментный шейдер
            // Fragment shader - отвечает за преобразование вершин во "фрагменты" которые представляют из себя все данные
            //     которые необходимы OpenGL для отрисовки
            //---------------------------------------------------------------------------------------
            
            // Загружаем исходный код вершинного шейдера
            string vertexShaderSource;
            using (StreamReader reader = new StreamReader(vertexPath, Encoding.UTF8))
            {
                vertexShaderSource = reader.ReadToEnd();
            }

            // Загружаем исходный код фрагментного шейдера
            string fragmentShaderSource;
            using (StreamReader reader = new StreamReader(fragmentPath, Encoding.UTF8))
            {
                fragmentShaderSource = reader.ReadToEnd();
            }
            
            // создаем переменную для хранения значения которое соответствует - типу "вершинный шейдер" 
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            // Связываем исходный код с типом "вершинный шейдер"
            GL.ShaderSource(vertexShader, vertexShaderSource);
            
            // создаем переменную для хранения значения которое соответствует - типу "фрагментный шейдер" 
            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            // Связываем исходный код с типом "фрагментный шейдер"
            GL.ShaderSource(fragmentShader, fragmentShaderSource);
            
            // Компилируем вершинный шейдер
            GL.CompileShader(vertexShader);
            // Если есть ошибки отсвечиваем их в консоль
            string infoLogVert = GL.GetShaderInfoLog(vertexShader);
            if (!string.IsNullOrEmpty(infoLogVert))
                Console.WriteLine(infoLogVert);

            // Компилируем фрагментный шейдер
            GL.CompileShader(fragmentShader);
            // Если есть ошибки отсвечиваем их в консоль
            string infoLogFrag = GL.GetShaderInfoLog(fragmentShader);
            if (!string.IsNullOrEmpty(infoLogFrag))
                Console.WriteLine(infoLogFrag);
            
            // Эти два шейдера должны быть соединены вместе в шейдерную программу чтобы ее мог использовать OpenGL
            // поэтому создаем программу
            _handle = GL.CreateProgram();

            // Присоединяем к ней оба шейдера
            GL.AttachShader(_handle, vertexShader);
            GL.AttachShader(_handle, fragmentShader);

            // И линкуем шейдеры между собой
            GL.LinkProgram(_handle);
            
            // Cleanup
            // Так как исходный код каждого шейдера КОПИРУЕТСЯ в шейдерную программу, то отдельные сущьности сейчас уже мосор
            // отделяем переменные от программы и удаляем эти переменные 
            GL.DetachShader(_handle, vertexShader);
            GL.DetachShader(_handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);
        }
        
        // Оберточка для функции которая активирует шейдерную программу
        public void Use()
        {
            GL.UseProgram(_handle);
        }

        #region Код для высвобождения ресурсов (интерфейс Disposable)

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(_handle);

                disposedValue = true;
            }
        }

        ~Shader()
        {
            GL.DeleteProgram(_handle);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
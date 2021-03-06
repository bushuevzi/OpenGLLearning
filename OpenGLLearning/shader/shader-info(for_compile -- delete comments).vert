﻿// Вершинный шейдер

// Это строка является директивой и указывает на версию GLSL
#version 330 core

//----------------офтоп--------------------------------
// Язык GLSL похож на C с некоторыми отличиями
// Существуют четыре разных типа переменных: input, output, uniform, internal
// input переменные передаются из буфера, с помощьют метода GL.VertexAttribPointer
// output переменные передаются из текущего шейдера в следующий по цепочке (каковым в большенстве случаев является Фрагментный шейдер)
// Uniform - ?
// internal переменные определяются и используются только внутри текущего шейдера
//-----------------------------------------------------

//--------------------Принцип Вершинного шейдера (Vertex shader)-------------------
// Вершинный шейдер запускается один раз на каждую вершину. Псевдокод на C# выглядел бы примерно так:
// foreach(var vertex in vertices)
//   shader(vertex)

// Здесь оперделяется наша input переменная aPosition
// Строка начинается с "layout(location = 0)" это определяется где эта input переменная будет находится, это будет
// необходимо для метода GL.VertexAttribPointer
// Однако мы можем это опустить и написать просто "in vec3 aPosition", при этом мы должны будем заменить 0 в GL.VertexAttribPointer
// на следующее: GL.GetAttribLocation(shaderHandle, attributeName)
// Далее ключевое слово "in" определяет что это input переменная (если будет "out" -- то это output переменная)
// Далее кючевое слово "vec3" определяется что это вектор с 3 float значениями внутри
layout (location = 0) in vec3 aPosition;

// Здесь мы можем выполнять любые элементарные вычисления для изменения наших вершин, но в данном примере это не требуется
// gl_Position - окончательная позиция вершины, она является vec4 и четвертая компонента вектора называется "w"
// "w" используется для продвинутых функций OpenGL и сейчас она не требуется, 
// поэтому мы конвертирует наш текущий vec3 aPosition в vec4 присваевая последней компоненте просто значение 1.0
void main()
{
    gl_Position = vec4(aPosition, 1.0);
}
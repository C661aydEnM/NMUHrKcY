// 代码生成时间: 2025-10-17 02:53:22
using System;
using System.Collections.Generic;

// 定义一个简单的物理引擎，为了简洁性，这里只实现了基本的物体和力的概念
namespace SimplePhysicsEngine
{
    // 一个基本的物理体类，包含位置、速度和加速度
    public class Rigidbody
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }

        public Rigidbody(Vector2 position, Vector2 velocity)
        {
            Position = position;
            Velocity = velocity;
            Acceleration = Vector2.Zero;
        }

        // 更新物理体的位置和速度
        public void Update(float deltaTime)
        {
            // 计算新的速度
            Velocity += Acceleration * deltaTime;

            // 根据速度和时间更新位置
            Position += Velocity * deltaTime;
        }
    }

    // 一个简单的向量类，用于表示二维向量
    public struct Vector2
    {
        public float X, Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator *(Vector2 a, float b) => new Vector2(a.X * b, a.Y * b);

        public static Vector2 Zero = new Vector2(0, 0);
    }

    // 物理引擎类，管理所有的物理体
    public class PhysicsEngine
    {
        private List<Rigidbody> bodies;

        public PhysicsEngine()
        {
            bodies = new List<Rigidbody>();
        }

        // 添加物理体到引擎中
        public void AddBody(Rigidbody body)
        {
            bodies.Add(body);
        }

        // 更新所有物理体的状态
        public void Update(float deltaTime)
        {
            foreach (var body in bodies)
            {
                body.Update(deltaTime);
            }
        }
    }
}

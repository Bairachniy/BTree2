using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTree
{
    /// <summary>
    /// Розташування вузла відносно батьківського
    /// </summary>
    public enum Side
    {
        Left,
        Right
    }

    /// <summary>
    /// Вузол бінарного дерева
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinaryTreeNode<T> where T : IComparable
    {
        /// <summary>
        /// Дані, що зберігаються у вузлі
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Конструктор класу
        /// </summary>
        /// <param name="data">Дані</param>
        public BinaryTreeNode(T data)
        {
            Data = data;
        }


        /// <summary>
        /// Лівий дочірний елемент
        /// </summary>
        public BinaryTreeNode<T> LeftNode { get; set; }

        /// <summary>
        /// Правий дочірній елемент
        /// </summary>
        public BinaryTreeNode<T> RightNode { get; set; }

        /// <summary>
        /// Батьківський елемент
        /// </summary>
        public BinaryTreeNode<T> ParentNode { get; set; }

        /// <summary>
        /// Розташування вузла відносно батьківського 
        /// </summary>
        public Side? NodeSide =>
            ParentNode == null
            ? (Side?)null
            : ParentNode.LeftNode == this
                ? Side.Left
                : Side.Right;

        /// <summary>
        /// Перетворення екземпляра класу в рядок
        /// </summary>
        /// <returns>Дані вузла дерева</returns>
        public override string ToString() => Data.ToString();
    }
}

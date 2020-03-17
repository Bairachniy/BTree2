﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTree
{
    /// <summary>
    /// Бінарне дерево
    /// </summary>
    /// <typeparam name="T">Тип даних вузлів</typeparam>
    public class BinaryTree<T> where T : IComparable
    {
        public T Summa { get; set; }
        /// <summary>
        /// Корінь бінарного дерева
        /// </summary>
        public BinaryTreeNode<T> RootNode { get; set; }

        /// <summary>
        /// Додавання нового вузла в бінарне дерево
        /// </summary>
        /// <param name="node">Новий вузол</param>
        /// <param name="currentNode">Поточний вузол</param>
        /// <returns>Вузол</returns>
        public BinaryTreeNode<T> Add(BinaryTreeNode<T> node, BinaryTreeNode<T> currentNode = null)
        {
            if (RootNode == null)
            {
                node.ParentNode = null;
                return RootNode = node;
            }

            currentNode = currentNode ?? RootNode;
            node.ParentNode = currentNode;
            int result;
            return (result = node.Data.CompareTo(currentNode.Data)) == 0
                ? currentNode
                : result < 0
                    ? currentNode.LeftNode == null
                        ? (currentNode.LeftNode = node)
                        : Add(node, currentNode.LeftNode)
                    : currentNode.RightNode == null
                        ? (currentNode.RightNode = node)
                        : Add(node, currentNode.RightNode);
        }

        /// <summary>
        /// Додавання даних в бінарне дерево
        /// </summary>
        /// <param name="data">Дані</param>
        /// <returns>Вузол</returns>
        public BinaryTreeNode<T> Add(T data)
        {
            return Add(new BinaryTreeNode<T>(data));
        }

        /// <summary>
        /// Пошук вузла по значенню
        /// </summary>
        /// <param name="data">Шукане значення</param>
        /// <param name="startWithNode">Вузол з якого розпочинається пошук</param>
        /// <returns>Знайдений вузол</returns>
        public BinaryTreeNode<T> FindNode(T data, BinaryTreeNode<T> startWithNode = null)
        {
            startWithNode = startWithNode ?? RootNode;
            int result;
            return (result = data.CompareTo(startWithNode.Data)) == 0
                ? startWithNode
                : result < 0
                    ? startWithNode.LeftNode == null
                        ? null
                        : FindNode(data, startWithNode.LeftNode)
                    : startWithNode.RightNode == null
                        ? null
                        : FindNode(data, startWithNode.RightNode);
        }

        /// <summary>
        /// Видалення вузла бінарного дерева
        /// </summary>
        /// <param name="node">Вузол для видалення</param>
        public void Remove(BinaryTreeNode<T> node)
        {
            if (node == null)
            {
                return;
            }

            var currentNodeSide = node.NodeSide;
            //якщо у вузла немає дочірніх, то можна його видаляти
            if (node.LeftNode == null && node.RightNode == null)
            {
                if (currentNodeSide == Side.Left)
                {
                    node.ParentNode.LeftNode = null;
                }
                else
                {
                    node.ParentNode.RightNode = null;
                }
            }
            //якщо немає лівого, то правий ставимо на місце видаленого
            else if (node.LeftNode == null)
            {
                if (currentNodeSide == Side.Left)
                {
                    node.ParentNode.LeftNode = node.RightNode;
                }
                else
                {
                    node.ParentNode.RightNode = node.RightNode;
                }

                node.RightNode.ParentNode = node.ParentNode;
            }
            //якщо немає правого, то лівий ставимо на місце видаленого
            else if (node.RightNode == null)
            {
                if (currentNodeSide == Side.Left)
                {
                    node.ParentNode.LeftNode = node.LeftNode;
                }
                else
                {
                    node.ParentNode.RightNode = node.LeftNode;
                }

                node.LeftNode.ParentNode = node.ParentNode;
            }
            //якщо наявні обидва дочірні, 
            //то правий ставимо на місце видаленого,
            //а лівий вставляємо в правий
            else
            {
                switch (currentNodeSide)
                {
                    case Side.Left:
                        node.ParentNode.LeftNode = node.RightNode;
                        node.RightNode.ParentNode = node.ParentNode;
                        Add(node.LeftNode, node.RightNode);
                        break;
                    case Side.Right:
                        node.ParentNode.RightNode = node.RightNode;
                        node.RightNode.ParentNode = node.ParentNode;
                        Add(node.LeftNode, node.RightNode);
                        break;
                    default:
                        var bufLeft = node.LeftNode;
                        var bufRightLeft = node.RightNode.LeftNode;
                        var bufRightRight = node.RightNode.RightNode;
                        node.Data = node.RightNode.Data;
                        node.RightNode = bufRightRight;
                        node.LeftNode = bufRightLeft;
                        Add(bufLeft, node);
                        break;
                }
            }
        }

        /// <summary>
        /// Видалення вузла дерева
        /// </summary>
        /// <param name="data">Дані для видалення</param>
        public void Remove(T data)
        {
            var foundNode = FindNode(data);
            Remove(foundNode);
        }

        /// <summary>
        /// Вивід бінарного дерева
        /// </summary>
        public void PrintTree()
        {
            PrintTree(RootNode);
        }

        /// <summary>
        /// Вивід бінарного дерева починаючи з вказаного вузла
        /// </summary>
        /// <param name="startNode">Вузол з якого розпочинається друк дерева</param>
        /// <param name="indent">Відступ</param>
        /// <param name="side">Сторона</param>
        private void PrintTree(BinaryTreeNode<T> startNode, string indent = "", Side? side = null)
        {
            if (startNode != null)
            {
                var nodeSide = side == null ? "+" : side == Side.Left ? "L" : "R";
                Console.WriteLine($"{indent} [{nodeSide}]- {startNode.Data}");
                indent += new string(' ', 3);
                //рекурсивний виклик для лівої та правої гілок
                PrintTree(startNode.LeftNode, indent, Side.Left);
                PrintTree(startNode.RightNode, indent, Side.Right);
            }
        }
        //private void Sum(BinaryTreeNode<T> startNode, Side? side = null)
        //{
        //    if (startNode != null)
        //    {
        //        var nodeSide = side == null ? "+" : side == Side.Left ? "L" : "R";
        //         Summa += startNode.Data;
        //        //рекурсивний виклик для лівої та правої гілок
        //        Sum(startNode.LeftNode, Side.Left);
        //        Sum(startNode.RightNode, Side.Right);
        //    }
        //}
    }


}

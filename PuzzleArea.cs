﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuzzleFifteen
{
    public partial class PuzzleArea : Form
    {
        Random rand = new Random();
        List<Point> InitialLocations = new List<Point>();
        public PuzzleArea()
        {
            InitializeComponent();
            InitializePuzzleArea();
            InitializeBlocks();
            ShuffleBlocks();
        }

        private void InitializePuzzleArea()
        {
            this.BackColor = Color.DarkSlateBlue;
            this.Text = "Puzzle15";
            this.ClientSize = new Size(500, 500);
        }

        private void InitializeBlocks()
        {
            int blockCount = 1;
            PuzzleBlock block;
            for(int row = 1; row < 5; row++)
            {
                for(int col = 1; col < 5; col++)
                {
                    block = new PuzzleBlock
                    {
                        Top = row * 84,
                        Left = col * 84,
                        Text = blockCount.ToString(),
                        Name = "Block" + blockCount.ToString()
                    };

                    InitialLocations.Add(block.Location);

                    //block.Click += new EventHandler(Block_Click);  <----- long version
                    block.Click += Block_Click;                    //<----- short version

                    if (blockCount == 16)
                    {
                        block.Name = "EmptyBlock";
                        block.Text = string.Empty;
                        block.BackColor = Color.DarkSlateBlue;
                        block.FlatAppearance.BorderSize = 0;
                    }
                    blockCount++;
                    this.Controls.Add(block);
                }
            }
        }
        private void Block_Click(object sender, EventArgs e)
        {
            Button block = (Button)sender;
            if (IsAdjacent(block))
            {
                SwapBlocks(block);
            }
            CheckForWin();
        }

        private void SwapBlocks(Button block)
        {
            Button emptyBlock = (Button)this.Controls["EmptyBlock"];
            Point oldLocation = block.Location;
            block.Location = emptyBlock.Location;
            emptyBlock.Location = oldLocation;
        }

        private bool IsAdjacent(Button block)
        {
            double a;
            double b;
            double c;
            Button emptyBlock = (Button)this.Controls["EmptyBlock"];

            a = Math.Abs(emptyBlock.Top - block.Top);
            b = Math.Abs(emptyBlock.Left - block.Left);
            c = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
            if (c < 85)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void ShuffleBlocks()
        {
            string blockName;
            for (int i = 0; i < 100; i++)
            {
                blockName = "Block" + rand.Next(1, 16);
                Button block = (Button)this.Controls[blockName];
                SwapBlocks(block);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShuffleBlocks();
        }
        private void CheckForWin()
        {
            string blockName;
            for (int i = 1; i < 16; i++)
            {
                blockName = "Block" + i;
                if (this.Controls[blockName].Location != InitialLocations[i-1])
                {
                    return;
                }
            }
            PuzzleSolved();
        }
        private void PuzzleSolved()
        {
            MessageBox.Show("U won :D");
        }
    }
}

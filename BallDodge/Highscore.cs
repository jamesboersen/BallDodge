﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BallDodge
{
    public class Highscore
    {
        public string name, level;

        public Highscore(string _name, string _level)
        {
            name = _name;
            level = _level;
        }

        public void save(Highscore hs)
        {
            bool scoreAdded = false;
            //Highscore hs = new Highscore(null, Convert.ToString(Form1.currentScore)); //TODO when highscore screen is finished change null for nameLabel.text (or something like that)

            for (int i = 0; i < Convert.ToInt16(Form1.highscoreList.Count); i++)
            {
                if (Form1.playerScore > Convert.ToInt16(Form1.highscoreList[i].level))
                {
                    Form1.highscoreList.Insert(i, hs);

                    scoreAdded = true;
                    i = Form1.highscoreList.Count;//this is so that it will exit the for loop if this code executes
                }
            }

            if (scoreAdded == false)
            {
                Form1.highscoreList.Add(hs);
            }

        }

        public void saveScores(List<Highscore> _scoreList)
        {
            //Only saves the top 10 highscores
            if (_scoreList.Count > 8)
            {
                for (int i = _scoreList.Count; i > 10; i--)
                {
                    _scoreList.RemoveAt(i - 1);
                }
            }
            //Creates the xml file where highscores are saved
            XmlTextWriter writer = new XmlTextWriter("highscoreDB.xml", null);

            writer.WriteStartElement("highscoreList");

            foreach (Highscore hs in _scoreList)
            {
                //Start "highscore" element
                writer.WriteStartElement("highscore");

                //Write sub-elements
                writer.WriteElementString("name", hs.name);
                writer.WriteElementString("score", hs.level);

                // end the "highscore" element
                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.Close();
        }
    }
}

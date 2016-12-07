using UnityEngine;
using UnityEngine.UI;

using System.Collections;

namespace NarrativePlayDialogue
{
    public class NP_DialogueBlock
    {
        protected string m_script;
        protected ArrayList m_lines;

        protected int m_lineCount;
        protected int m_curLineID;

        public virtual void Initialize(string script)
        {
            m_lines = new ArrayList();

            m_script = script;
            string[] splitedStr = m_script.Split('/');
            foreach (string str in splitedStr)
            {
                str.TrimStart(' ');

                m_lines.Add(str);

                m_lineCount++;
            }
        }

        public string GetNextLine()
        {
            if (m_curLineID < m_lineCount)
            {
                return m_lines[m_curLineID++].ToString();
            }
            else
            {
                // Temp
                return null;
            }
        }


    }
    public class NP_DiaToDiaBlock : NP_DialogueBlock
    {
        private NP_DialogueBlock m_nextDialogueBlock;
        public NP_DialogueBlock GetNextDialogueBlock()
        {
            return m_nextDialogueBlock;
        }

        private void Initialize(string script) { }
        public void Initialize(string script, NP_DialogueBlock nextDialogueBlock)
        {
            base.Initialize(script);
            m_nextDialogueBlock = nextDialogueBlock;
        }
    }
    public class NP_DiaToOpBlock : NP_DialogueBlock
    {
        private NP_PlayerOptionBlock m_nextPlayerOptionBlock;
        public NP_PlayerOptionBlock GetNextPlayerOptionBlock()
        {
            return m_nextPlayerOptionBlock;
        }

        private void Initialize(string script) { }
        public void Initialize(string script, NP_PlayerOptionBlock nextOptionBlock)
        {
            base.Initialize(script);
            m_nextPlayerOptionBlock = nextOptionBlock;
        }
    }



    public class NP_PlayerOptionBlock
    {
        public NP_PlayerOption m_optionA;
        public NP_PlayerOption m_optionB;
        public NP_PlayerOption m_optionC;

        public void Initialize(NP_PlayerOption optA, NP_PlayerOption optB, NP_PlayerOption optC)
        {
            m_optionA = optA;
            m_optionB = optB;
            m_optionC = optC;
        }

        public string[] GetLines()
        {
            string[] lines = new string[] { m_optionA.Line, m_optionB.Line, m_optionC.Line };
            return GetLines();
        }
        public NP_DialogueBlock GetNextNPCDialogue(int option)
        {
            switch (option)
            {
                case 1: return m_optionA.NextDialogueBlock;
                case 2: return m_optionB.NextDialogueBlock;
                case 3: return m_optionC.NextDialogueBlock;
                default: return m_optionA.NextDialogueBlock;
            }
        }
    }
    public class NP_PlayerOption
    {
        private string m_line;
        public string Line
        { get { return m_line; } }

        private NP_DialogueBlock m_nextDialogueBlock;
        public NP_DialogueBlock NextDialogueBlock
        { get { return m_nextDialogueBlock; } }

        private bool m_nextSpeakerIsPlayer = false;
        public bool NextSpeakerIsPlayer
        { get { return m_nextSpeakerIsPlayer; } }

        public void Initialize(string line, NP_DialogueBlock nextNPCDialogue, bool nextDiaIsPlayer = false)
        {
            m_line = line;
            m_nextDialogueBlock = nextNPCDialogue;

            m_nextSpeakerIsPlayer = nextDiaIsPlayer;
        }
    }
}
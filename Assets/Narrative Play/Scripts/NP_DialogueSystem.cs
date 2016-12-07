using UnityEngine;
using UnityEngine.UI;

using System.Collections;


namespace NarrativePlayDialogue
{
    public class NP_DialogueSystem : MonoBehaviour
    {
        public GameObject npcDialoguePanel;
        public GameObject playerDialoguePanel;
        public GameObject playerOptionPanel;

        public Text npcText;
        public Text playerText;
        public Text playerOptAText;
        public Text playerOptBText;
        public Text playerOptCText;

        public enum ConversationID
        {
            SCENE_4,
            SCENE_4A,
            SCENE_5B,
            SCENE_5D,
            SCENE_5C,
            SCENE_5E,
            SCENE_6,
            SCENE_6A,
        };
        private ConversationID m_currentConversationID;
        private int m_choosed;

        public enum Status
        {
            NONE,   // not in conversation
            NPCLine,
            PlayerLine,
            PlayerOption,
        }
        private Status m_currentStatus;

        void Start()
        {
            npcDialoguePanel.SetActive(false);
            playerDialoguePanel.SetActive(false);
            playerOptionPanel.SetActive(false);

            Initialize();

            // For test
            //StartConversation(ConversationID.SCENE_6A);
        }

        void Update()
        {
            if (m_currentStatus != Status.NONE)
            {
                if (Input.GetMouseButtonDown(0) && m_currentStatus != Status.PlayerOption)
                {
                    string nextLine = m_currentDialogueBlock.GetNextLine();

                    if (nextLine != null) // Continue dialogue
                    {
                        if (m_currentStatus == Status.NPCLine)
                        {
                            npcText.text = nextLine;
                        }
                        else
                        {
                            playerText.text = nextLine;
                        }
                    }
                    else // Switch to option panel
                    {
                        if (m_currentDialogueBlock is NP_DiaToDiaBlock)
                        {
                            m_currentDialogueBlock = (m_currentDialogueBlock as NP_DiaToDiaBlock).GetNextDialogueBlock();

                            if (m_currentStatus == Status.NPCLine)
                            {
                                m_currentStatus = Status.PlayerLine;
                            }
                            else
                            {
                                m_currentStatus = Status.NPCLine;
                            }

                            SwitchUIPanel();
                        }
                        else if (m_currentDialogueBlock is NP_DiaToOpBlock)
                        {
                            m_currentStatus = Status.PlayerOption;

                            SwitchUIPanel();
                        }
                        else
                        {
                            EndConversation();
                        }
                        
                    }
                }
            }
        }


        //-----------------------------------------
        private Hashtable m_DialogueDatabase;
        private NP_DialogueBlock m_currentDialogueBlock;
        private NP_PlayerOptionBlock m_currentOptionBlock;

        void Initialize()
        {
            //NPCText.text = "";
            //PlayerText.text = "";
            //PlayerOptAText.text = "";
            //PlayerOptBText.text = "";
            //PlayerOptCText.text = "";

            m_DialogueDatabase = new Hashtable();

            // SCENE 4: HARU’S SPIRIT TREE
            NP_PlayerOptionBlock sn4_plOpBlock = new NP_PlayerOptionBlock();
            NP_PlayerOption sn4_pl_A = new NP_PlayerOption();
            NP_PlayerOption sn4_pl_B = new NP_PlayerOption();
            //NP_PlayerOption sn4_pl_C = new NP_PlayerOption();
            sn4_plOpBlock.m_optionA = sn4_pl_A;
            sn4_plOpBlock.m_optionB = sn4_pl_B;
            //sn4_plOpBlock.m_optionC = sn4_pl_C;


            NP_DiaToOpBlock sn4_sp_01 = new NP_DiaToOpBlock();
            sn4_sp_01.Initialize("You are different./There is form to your spirit./How did you pass through?", sn4_plOpBlock);
            m_DialogueDatabase.Add(ConversationID.SCENE_4, sn4_sp_01);

            NP_DiaToDiaBlock sn4_sp_A_02 = new NP_DiaToDiaBlock();
            NP_DiaToDiaBlock sn4_pl_A_02 = new NP_DiaToDiaBlock();
            NP_DialogueBlock sn4_sp_A_03 = new NP_DialogueBlock();
            sn4_pl_A.Initialize("I’m a human.", sn4_sp_A_02);
            sn4_sp_A_02.Initialize("Limbo is no place for the living./How did you get here?", sn4_pl_A_02);
            sn4_pl_A_02.Initialize("I am temporarily dead./I should return to life in seven days.", sn4_sp_A_03);
            sn4_sp_A_03.Initialize("There is no such notion as time in Limbo./You are here until you are not./You either surrender to your darkness or you do not./All spirits enter Limbo, but not all of them leave./The choice is ours to take hold of ourselves which is to own your truth.");

            NP_DiaToDiaBlock sn4_sp_B_02 = new NP_DiaToDiaBlock();
            NP_DiaToDiaBlock sn4_pl_B_02 = new NP_DiaToDiaBlock();
            NP_DialogueBlock sn4_sp_B_03 = new NP_DialogueBlock();
            sn4_pl_B.Initialize("I don’t understand. What do you mean ‘form’?", sn4_sp_B_02);
            sn4_sp_B_02.Initialize("I am not this tree./I am a spirit./While in Limbo, where spirits have no form, I inhabit this tree until I am free to pass on.", sn4_pl_B_02);
            sn4_pl_B_02.Initialize("What do you mean pass on?", sn4_sp_B_03);
            sn4_sp_B_03.Initialize("Limbo is the place where spirits surrender to their darkness or they do not./All spirits enter Limbo, but not all of them leave./The choice is ours to take hold of ourselves which is to own your truth.");

            //NP_DialogueBlock sn4_sp_C_02 = new NP_DialogueBlock();
            //sn4_pl_C.Initialize("I am temporarily dead. I should return to life in seven days.", sn4_sp_C_02);
            //sn4_sp_C_02.Initialize("There is no such notion as time in Limbo./You are here until you are not./You either surrender to your darkness or you do not./All spirits enter Limbo, but not all of them leave./The choice is ours to take hold of ourselves which is to “own your truth'.");

            // SCENE 4A
            NP_DiaToDiaBlock sn4a_sp_01 = new NP_DiaToDiaBlock();
            NP_DiaToDiaBlock sn4a_pl_01 = new NP_DiaToDiaBlock();
            NP_DialogueBlock sn4a_sp_02 = new NP_DialogueBlock();

            sn4a_sp_01.Initialize("What are you doing here?", sn4a_pl_01);
            m_DialogueDatabase.Add(ConversationID.SCENE_4A, sn4a_sp_01);

            sn4a_pl_01.Initialize("I’m looking for my father.", sn4a_sp_02);
            sn4a_sp_02.Initialize("All spirits are kin./I know enough to know I know what I need to know./Lists and things are not keen to the senses./But… you’re human./There are names on the board back behind the gate you just entered./The Departures Board next to me has not posted yet./I’m here to help until I am not./I sense that helping is my call, but I don’t recall why... ");

            // SCENE 5B: Fire Pit
            NP_PlayerOptionBlock sn5b_plOpBlock = new NP_PlayerOptionBlock();
            NP_PlayerOption sn5b_pl_A = new NP_PlayerOption();
            NP_PlayerOption sn5b_pl_B = new NP_PlayerOption();
            NP_PlayerOption sn5b_pl_C = new NP_PlayerOption();
            sn5b_plOpBlock.m_optionA = sn5b_pl_A;
            sn5b_plOpBlock.m_optionB = sn5b_pl_B;
            sn5b_plOpBlock.m_optionC = sn5b_pl_C;

            NP_DiaToOpBlock sn5b_sp_01 = new NP_DiaToOpBlock();
            sn5b_sp_01.Initialize("I felt angry./I used to remember why./Now? Now there’s only the rage./But you… you’re not like me./But you are. You’re angry, aren’t you?", sn5b_plOpBlock);
            m_DialogueDatabase.Add(ConversationID.SCENE_5B, sn5b_sp_01);

            NP_DialogueBlock sn5b_pl_A_02 = new NP_DialogueBlock();
            sn5b_pl_A.Initialize("I used to be.", sn5b_pl_A_02, true);
            sn5b_pl_A_02.Initialize("My father died suddenly./I’m afraid./The rage… I want to let it all go./But it’s so hard./I need answers.");

            NP_DiaToDiaBlock sn5b_pl_B_02 = new NP_DiaToDiaBlock();
            NP_DialogueBlock sn5b_sp_B_02 = new NP_DialogueBlock();
            sn5b_pl_B.Initialize("Yes, I’m pissed.", sn5b_pl_B_02, true);
            sn5b_pl_B_02.Initialize("I am in this garden./Where is my father? I need answers./Now that I’m here nothing makes sense!", sn5b_sp_B_02);
            sn5b_sp_B_02.Initialize("Sense?!/Nothing ever makes sense./I’m still angry, maybe even more than before./Dying is just new paint on the walls.");

            NP_DiaToDiaBlock sn5b_p1_C_02 = new NP_DiaToDiaBlock();
            NP_DialogueBlock sn5b_sp_C_02 = new NP_DialogueBlock();
            sn5b_pl_C.Initialize("I don’t know.", sn5b_p1_C_02, true);
            sn5b_p1_C_02.Initialize("I don’t know how I feel, or what to feel./Am I feeling at all? Right now, I’m focused on focusing./I need to understand … death.", sn5b_sp_C_02);
            sn5b_sp_C_02.Initialize("But you are not dead…");

            // SCENE 5D: Blue Orchid Bush (01)
            NP_PlayerOptionBlock sn5d_plOpBlock = new NP_PlayerOptionBlock();
            NP_PlayerOption sn5d_pl_A = new NP_PlayerOption();
            NP_PlayerOption sn5d_pl_B = new NP_PlayerOption();
            sn5d_plOpBlock.m_optionA = sn5d_pl_A;
            sn5d_plOpBlock.m_optionB = sn5d_pl_B;

            NP_DiaToOpBlock sn5d_sp_01 = new NP_DiaToOpBlock();
            sn5d_sp_01.Initialize("Flesh!/You have flesh!/I remember life./It was good to me.", sn5d_plOpBlock);
            m_DialogueDatabase.Add(ConversationID.SCENE_5D, sn5d_sp_01);

            NP_DialogueBlock sn5d_sp_A_02 = new NP_DialogueBlock();
            sn5d_pl_A.Initialize("You know what flesh is?", sn5d_sp_A_02);
            sn5d_sp_A_02.Initialize("Yes!/I loved my body, my hair, my skin, good food, fresh air./I hear the longer you stay in the Limbo the more murky your truth becomes./I need to move on./I can’t forget.");

            NP_DialogueBlock sn5d_sp_B_02 = new NP_DialogueBlock();
            sn5d_pl_B.Initialize("Do you know Saul Velasquez, my father?", sn5d_sp_B_02);
            sn5d_sp_B_02.Initialize("A name!/Yes, I saw his name on the arrival board!/It was there with mine!/My name… I used to love my name./I can’t remember it.");

            // SCENE 5E
            NP_DiaToDiaBlock sn5e_pl_01 = new NP_DiaToDiaBlock();
            NP_DialogueBlock sn5e_sp_01 = new NP_DialogueBlock();
            sn5e_pl_01.Initialize("Do you remember your life?", sn5e_sp_01);
            m_DialogueDatabase.Add(ConversationID.SCENE_5E, sn5e_pl_01);
            sn5e_sp_01.Initialize("I don’t recall the facts of life, but I do feel grateful./I feel well with having lived it./This feeling… Free... Truth… Whole.");

            // SCENE 6
            NP_PlayerOptionBlock sn6_plOpBlock = new NP_PlayerOptionBlock();
            NP_PlayerOption sn6_pl_A = new NP_PlayerOption();
            NP_PlayerOption sn6_pl_B = new NP_PlayerOption();
            sn6_plOpBlock.m_optionA = sn6_pl_A;
            sn6_plOpBlock.m_optionB = sn6_pl_B;

            NP_DiaToOpBlock sn6_sp_01 = new NP_DiaToOpBlock();
            sn6_sp_01.Initialize("How do you feel?", sn6_plOpBlock);
            m_DialogueDatabase.Add(ConversationID.SCENE_6, sn6_sp_01);

            NP_DialogueBlock sn6_sp_A_02 = new NP_DialogueBlock();
            sn6_pl_A.Initialize("Why do you ask?", sn6_sp_A_02);
            sn6_sp_A_02.Initialize("Understanding your truth, is that not why you are here?");

            NP_DialogueBlock sn6_sp_B_02 = new NP_DialogueBlock();
            sn6_pl_B.Initialize("I feel even more lost.", sn6_sp_B_02);
            sn6_sp_B_02.Initialize("I remember feeling lost when I first arrived here./Now I just feel helpful./I feel called to help./It’s almost as if I am finally feeling truth.");

            // SCENE 6A
            NP_DiaToDiaBlock sn6a_pl_01 = new NP_DiaToDiaBlock();
            NP_DialogueBlock sn6a_sp_01 = new NP_DialogueBlock();
            sn6a_pl_01.Initialize("I just want to find my father.", sn6a_sp_01);
            m_DialogueDatabase.Add(ConversationID.SCENE_6A, sn6a_pl_01);
            sn6a_sp_01.Initialize("This relationship you have with the spirit you search for…/I wonder what truth you would find beyond them.");
        }

        public void StartConversation(ConversationID currentID)
        {
            m_currentConversationID = currentID;

            NP_DialogueBlock sp_01;
            bool startFromNPC = true;
            switch (m_currentConversationID)
            {
                default:
                case ConversationID.SCENE_4:
                    sp_01 = m_DialogueDatabase[ConversationID.SCENE_4] as NP_DiaToOpBlock;
                    break;

                case ConversationID.SCENE_4A:
                    sp_01 = m_DialogueDatabase[ConversationID.SCENE_4A] as NP_DiaToDiaBlock;
                    break;

                case ConversationID.SCENE_5B:
                    sp_01 = m_DialogueDatabase[ConversationID.SCENE_5B] as NP_DiaToOpBlock;
                    break;

                case ConversationID.SCENE_5D:
                    sp_01 = m_DialogueDatabase[ConversationID.SCENE_5D] as NP_DiaToOpBlock;
                    break;

                case ConversationID.SCENE_5E:
                    sp_01 = m_DialogueDatabase[ConversationID.SCENE_5E] as NP_DiaToDiaBlock;
                    startFromNPC = false;
                    break;

                case ConversationID.SCENE_6:
                    sp_01 = m_DialogueDatabase[ConversationID.SCENE_6] as NP_DiaToOpBlock;
                    break;

                case ConversationID.SCENE_6A:
                    sp_01 = m_DialogueDatabase[ConversationID.SCENE_6A] as NP_DiaToDiaBlock;
                    startFromNPC = false;
                    break;
            }

            m_currentDialogueBlock = sp_01;
            if (startFromNPC)
            {
                npcDialoguePanel.SetActive(true);
                m_currentStatus = Status.NPCLine;
                npcText.text = sp_01.GetNextLine();
            }
            else
            {
                playerDialoguePanel.SetActive(true);
                m_currentStatus = Status.PlayerLine;

                playerText.transform.parent.gameObject.SetActive(true);  // meet bug??
                playerText.text = sp_01.GetNextLine();
            }
        }
        public void EndConversation()
        {
            npcDialoguePanel.SetActive(false);
            playerDialoguePanel.SetActive(false);
            playerOptionPanel.SetActive(false);

            m_currentStatus = Status.NONE;

            // Change Screen Filter
            if (m_currentConversationID == ConversationID.SCENE_5B)
            {
                if (m_choosed == 1)
                {
                    NP_GameManager.instance.ChangeScreenFilter(NP_GameManager.ScreenFilter.redDull);

                    NP_GameManager.instance.bgmManager.SwitchTo(NP_BGMManager.BGMID.Angry);
                    
                }
                if (m_choosed == 2)
                {
                    NP_GameManager.instance.ChangeScreenFilter(NP_GameManager.ScreenFilter.redBright);

                    NP_GameManager.instance.bgmManager.SwitchTo(NP_BGMManager.BGMID.Angry);
                    
                }
            }

            if (m_currentConversationID == ConversationID.SCENE_5D)
            {
                if (m_choosed == 1)
                {
                    NP_GameManager.instance.ChangeScreenFilter(NP_GameManager.ScreenFilter.blueBright);
                }
                else
                {
                    NP_GameManager.instance.ChangeScreenFilter(NP_GameManager.ScreenFilter.blueDull);
                }

                NP_GameManager.instance.bgmManager.SwitchTo(NP_BGMManager.BGMID.Peaceful);
            }

            if (m_currentConversationID == ConversationID.SCENE_6)
            {
                if (m_choosed == 1)
                {
                    NP_GameManager.instance.ChangeScreenFilter(NP_GameManager.ScreenFilter.greenDull);
                }
                else
                {
                    NP_GameManager.instance.ChangeScreenFilter(NP_GameManager.ScreenFilter.greenBright);
                }

                NP_GameManager.instance.bgmManager.SwitchTo(NP_BGMManager.BGMID.Peaceful);

            }

            //--------------------------------
            // Connect SCENE 4 and 4A
            if (m_currentConversationID == ConversationID.SCENE_4)
            {
                StartConversation(ConversationID.SCENE_4A);
                return;
            }

            //--------------------------------
            // Connect SCENE 5D and 5E
            if (m_currentConversationID == ConversationID.SCENE_5D)
            {
                StartConversation(ConversationID.SCENE_5E);
                return;
            }

            //--------------------------------
            // Connect SCENE 6 and 6A
            if (m_currentConversationID == ConversationID.SCENE_6)
            {
                StartConversation(ConversationID.SCENE_6A);
                return;
            }

            //--------------------------------
            NP_GameManager.instance.EndInteracting();
        }


        public void ChooseOption(int btnID)
        {
            NP_PlayerOption clicked;

            switch (btnID)
            {
                case 1:
                    clicked = m_currentOptionBlock.m_optionA;
                    m_choosed = 1;
                    break;

                case 2:
                    clicked = m_currentOptionBlock.m_optionB;
                    m_choosed = 2;
                    break;

                case 3:
                    clicked = m_currentOptionBlock.m_optionC;
                    m_choosed = 3;
                    break;

                default:
                    clicked = m_currentOptionBlock.m_optionA;
                    break;
            }

            m_currentDialogueBlock = clicked.NextDialogueBlock;
            
            if (clicked.NextSpeakerIsPlayer)
            {
                m_currentStatus = Status.PlayerLine;
            }
            else
            {
                m_currentStatus = Status.NPCLine;
            }
            
            SwitchUIPanel();
        }

        private void SwitchUIPanel()
        {
            switch (m_currentStatus)
            {
                case Status.NPCLine:
                    npcDialoguePanel.SetActive(true);
                    playerDialoguePanel.SetActive(false);
                    playerOptionPanel.SetActive(false);

                    npcText.text = m_currentDialogueBlock.GetNextLine();
                    break;

                case Status.PlayerLine:
                    npcDialoguePanel.SetActive(false);
                    playerDialoguePanel.SetActive(true);
                    playerOptionPanel.SetActive(false);

                    playerText.transform.parent.gameObject.SetActive(true);
                    playerText.text = m_currentDialogueBlock.GetNextLine();
                    break;

                case Status.PlayerOption:
                    npcDialoguePanel.SetActive(false);
                    playerDialoguePanel.SetActive(true);
                    playerOptionPanel.SetActive(true);

                    playerText.transform.parent.gameObject.SetActive(false);

                    m_currentOptionBlock = (m_currentDialogueBlock as NP_DiaToOpBlock).GetNextPlayerOptionBlock();
                    Debug.Assert(m_currentOptionBlock != null);

                    playerOptAText.text = m_currentOptionBlock.m_optionA.Line;
                    playerOptBText.text = m_currentOptionBlock.m_optionB.Line;
                    if (m_currentOptionBlock.m_optionC != null) // Maybe there are only two options
                    {
                        playerOptCText.transform.parent.gameObject.SetActive(true);
                        playerOptCText.text = m_currentOptionBlock.m_optionC.Line;
                    }
                    else
                    {
                        playerOptCText.transform.parent.gameObject.SetActive(false);
                    }
                    
                    break;
            }
        }
    }

}
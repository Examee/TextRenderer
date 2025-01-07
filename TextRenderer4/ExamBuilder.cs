using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRenderer4;

namespace TextRenderer3 {

    public class CExamBuilder {
        // The root of the exam
        private CExam m_root;

        // Context variable indicates the current block being built
        CExamBlock m_currentBlock;

        // The scope system is used to manage the symbol tables of exam
        CScopeSystem m_scopeSystem;

        CMacroParser m_macroParser;

        CScope MCurrentScope => m_scopeSystem.MCurrentScope;
        
        public CExamBuilder() {
            m_scopeSystem = CScopeSystem.Instance;
            m_macroParser = CMacroParser.Instance;
        }

        public string AddExam(Action<CScopeSystem> initAction ) {

            // Create the exam block
            CreateExam();

            // ! Create and Enter the exam scope 
            SetExamEnvironment();

            // initialize services for the exam scope
            InitializeServices(initAction);

            // Return the exam ID for future reference
            return "Exam";

            void CreateExam() {
                m_root = new CExam();
                m_currentBlock = m_root;
            }

            void SetExamEnvironment() {
                m_root.MScope = m_scopeSystem.EnterScope("Exam", null);
            }
        }
        
        public string AddQuestion(string questionid, int multiplicity, Action<CScopeSystem> initAction) {
            // Find the Exam block to add the question to
            // includes the block and the scope. The exam scope
            // is used for numbering questions using the serial picker
            var exam = SwitchToExamEnvironment();

            // Render the question ID using the macros of the parent scope
            string symtabId;
            string questionID = m_macroParser.RenderString(MCurrentScope,questionid,out symtabId);

            // Create question and append it to the exam
            CreateQuestion(exam,questionID);

            // Add the question ID to the text block
            CText textBlock = new CText(m_currentBlock as CExamCompositeBlock, 1);
            textBlock.AddText(questionID);

            // Change to question scope 
            SetQuestionEnvironment();

            // Initialize services for the question scope
            InitializeServices(initAction);

            // Return the question ID for future reference
            return questionID;

            void CreateQuestion(CExam exam,string  questionID) {
                CQuestion question = new CQuestion(questionID, exam);
                m_currentBlock = question;
            }

            CExam SwitchToExamEnvironment() {
                CExam exam = m_currentBlock.GetParent<CExam>();
                m_scopeSystem.SetCurrentScope("Exam");
                return exam;
            }

            void SetQuestionEnvironment() {
                m_scopeSystem.EnterScope(symtabId, MCurrentScope);
            }
        }

        public string AddSolution(string questionID, Action<CScopeSystem> initAction) {

            CExam exam = m_currentBlock.GetParent<CExam>();
            m_scopeSystem.SetCurrentScope("Exam");
            
            // Create Solution
            CSolution solution = new CSolution(exam);
            m_currentBlock = solution;

            // Switch to question environment

            m_scopeSystem.SetCurrentScope(questionID);

            // Add the question ID to the text block
            CText textBlock = new CText(m_currentBlock as CExamCompositeBlock, 1);
            textBlock.AddText(questionID);

            // Initialize services for the solution scope
            InitializeServices(initAction);

            return questionID;
        }

        public CText AddText(string text) {

            CText textBlock = 
                new CText(m_currentBlock as CExamCompositeBlock,0);

            string _text = m_macroParser.RenderString(MCurrentScope, text,out _);
            textBlock.AddText(_text);

            return textBlock;
        }

        public void AddNewLine() {
            CNewLine newLine = 
                new CNewLine(m_currentBlock as CExamCompositeBlock, 0);
            return;
        }

        public string AddTextLine(string text) {
            var textBlock = AddText(text);
            AddNewLine();
            return text;
        }
        
        public string RenderExam() {
            return m_root.GetContent();
        }

        private void InitializeServices(Action<CScopeSystem> initAction) {
            if (initAction != null) {
                initAction(m_scopeSystem);
            }
        }
    }
}

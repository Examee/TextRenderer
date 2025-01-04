using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRenderer3 {
    public class CExamBuilder {
        // The root of the exam
        private CExam m_root;

        // Context variable indicates the current block being built
        CExamBlock m_currentBlock;

        // The scope system is used to manage the symbol tables of exam
        CScopeSystem m_scopeSystem;

        CMacroParser m_macroParser;

        
        public CExamBuilder() {
            m_scopeSystem = CScopeSystem.Instance;
            m_macroParser = CMacroParser.Instance;
        }

        public void AddExam(Action<CScopeSystem> initAction ) {
            m_root = new CExam();
            m_currentBlock = m_root;
            // ! Create and Enter the exam scope 
            m_root.MScope = m_scopeSystem.EnterScope("Exam", null);

            // initialize services for the exam scope
            if (initAction != null) {
                initAction(m_scopeSystem);
            }
        }

        public void AddQuestion(string questionid, int multiplicity, Action<CScopeSystem> initAction) {
            // Find the Exam block to add the question to
            CExam exam = m_currentBlock.GetExam();

            string questionID = m_macroParser.RenderString(exam.MScope,questionid);

            // Create question and append it to the exam
            CQuestion question = new CQuestion(questionID, m_currentBlock);
            exam.AddBlock(question, CExam.QUESTION);
            m_currentBlock = question;

            // Change to question scope 
            m_scopeSystem.EnterScope(questionID, m_scopeSystem.MCurrentScope);

            // Initialize services for the question scope
            if (initAction != null) {
                initAction(m_scopeSystem);
            }
        }

        public void AddSolution(string questionID) {

        }


        public void AddLine(string line) {

        }

        public void Render() {

        }
    }
}

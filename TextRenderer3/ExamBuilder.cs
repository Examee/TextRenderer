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

        public void RegisterMacro(string macroID,
            Func<string[], string> action){

        }

        public CExamBuilder() {
            m_scopeSystem = CScopeSystem.Instance;
        }

        public void AddExam() {
            m_root = new CExam();
            m_currentBlock = m_root;
            // ! Create and Enter the exam scope 
            m_scopeSystem.EnterScope("Exam", null);


            SerialPeaker serialPeaker = new SerialPeaker();
            m_scopeSystem.AddMacro("QuestionCounter", (_) =>
                serialPeaker.NextSerial().ToString());

        }

        public void AddQuestion(string questionID, int multiplicity) {
            // Find the Exam block to add the question to
            while (m_currentBlock.MParent is not CExam) {
                m_currentBlock = m_currentBlock.MParent;
            }
            // Create question and append it to the exam
            CQuestion question = new CQuestion(questionID, m_currentBlock);
            m_currentBlock.AddBlock(question, CExam.QUESTION);

            // Change to question scope 
            m_scopeSystem.EnterScope(questionID, m_scopeSystem.MCurrentScope);
        }

        public void AddSolution(string questionID) {

        }


        public void AddLine(string line) {

        }

        public void Render() {

        }
    }
}

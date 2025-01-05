﻿using System;
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
            // includes the block and the scope. The exam scope
            // is used for numbering questions using the serial picker
            var exam = SwitchToExamEnvironment();

            // Render the question ID using the macros of the parent scope
            string questionID = m_macroParser.RenderString(MCurrentScope,questionid);

            // Create question and append it to the exam
            CreateQuestion();

            // Change to question scope 
            SetQuestionEnvironment();

            // Initialize services for the question scope
            if (initAction != null) {
                initAction(m_scopeSystem);
            }
            void CreateQuestion() {
                CQuestion question = new CQuestion(questionID, m_currentBlock);
                exam.AddBlock(question, CExam.QUESTION);
                m_currentBlock = question;
            }

            CExam SwitchToExamEnvironment() {
                CExam exam = m_currentBlock.GetParent<CExam>();
                m_scopeSystem.SetCurrentScope("Exam");
                return exam;
            }

            void SetQuestionEnvironment() {
                m_scopeSystem.EnterScope(questionID, MCurrentScope);
            }
        }
    }
}

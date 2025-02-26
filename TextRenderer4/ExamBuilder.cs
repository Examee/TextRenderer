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
                m_root.MScope = m_scopeSystem.EnterExamScope();
            }
        }
        
        public string AddQuestion(string questionid, int multiplicity, Action<CScopeSystem> initAction) {
            // Find the Exam block to add the question to
            // includes the block and the scope. The exam scope
            // is used for numbering questions using the serial picker
            var exam = SwitchToExamEnvironment();

            // Render the question ID using the macros of the parent scope
            string symtabId;
            (string questionID, string[]? parameters) questionParameters = m_macroParser.RenderString(MCurrentScope,questionid);

            // Create question and append it to the exam
            CreateQuestion(exam,questionParameters.questionID);

            // Add the question ID to the text block
            CText.CreateTextBlock(questionParameters.questionID,
                m_currentBlock,CQuestion.QUESTION_SET);

            // Change to question scope 
            SetQuestionEnvironment(questionParameters.questionID);

            // Initialize services for the question scope
            InitializeServices(initAction);

            // Return the question ID for future reference
            return questionParameters.questionID;

            void CreateQuestion(CExam exam,string  questionID) {
                CQuestion question = new CQuestion(questionID, exam);
                m_currentBlock = question;
            }

            CExam SwitchToExamEnvironment() {
                CExam exam = m_currentBlock.GetParent<CExam>();
                m_scopeSystem.SetCurrentScope("Exam");
                return exam;
            }

            void SetQuestionEnvironment(string symtabId) {
                MCurrentScope.AddValue(questionParameters.parameters[0], symtabId);
                m_scopeSystem.EnterQuestionScope(symtabId, MCurrentScope);
            }
        }

        public string AddSolution(string questionMacroID, Action<CScopeSystem> initAction) {

            CExam exam = m_currentBlock.GetParent<CExam>();
            m_scopeSystem.SetCurrentScope("Exam");
            
            // Create Solution
            CSolution solution = new CSolution(exam);
            m_currentBlock = solution;

            // Switch to question environment
            string questionID = m_macroParser.RenderString(MCurrentScope, questionMacroID).text;
            m_scopeSystem.SetCurrentScope(questionID);

            // Add the question ID to the text block
            CText.CreateTextBlock(questionID, m_currentBlock, CSolution.SOLUTION_SET);
            
            // Initialize services for the solution scope
            InitializeServices(initAction);

            return questionID;
        }

        public CText AddText(string text,int context) {

            (string questionID, string[]? parameters) questionParameters = m_macroParser.RenderString(MCurrentScope, text);

            return CText.CreateTextBlock(questionParameters.questionID, m_currentBlock, context); ;
        }

        public void AddNewLine(int context) {
            CNewLine newLine = 
                new CNewLine(m_currentBlock as CExamCompositeBlock, context);
            return;
        }

        public string AddTextLine(string text,int context) {
            var textBlock = AddText(text,context);
            AddNewLine(context);
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

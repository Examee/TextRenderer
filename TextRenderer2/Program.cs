// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");



ExamBuilder exam = ExamBuilder.Create(/*Initialize services such as Randomizer etc*/).
    Question("A"). // creates a private scope for question body A
            QuestionHeader("Question #s$A").
            QuestionWording("Find the result of the following expressions : ").
            QuestionsBlock(5, "Q1"). // creates a private scope for variables $a, $b and serialnumber
                Question("#SerialNumber$Q1) #RandomInteger$a + #RandomInteger$b").
            EndQuestionsBlock().
            QuestionsBlock(5, "Q2"). // creates a private scope for variables $a, $b and serialnumber
                Question("#SerialNumber$Q2) #RandomInteger$a - #RandomInteger$b").
            EndQuestionsBlock().
    EndQuestion("A").
    Question("B"). // creates a private scope for question body B
            QuestionHeader("Question #s$B").
            QuestionWording("Find the result of the following expressions : ").
            QuestionsBlock(5, "Q1"). // creates a private scope for variables $a, $b and serialnumber
                Question("#SerialNumber$Q1) #RandomInteger$a * #RandomInteger$b").
            EndQuestionsBlock().
            QuestionsBlock(5, "Q2"). // creates a private scope for variables $a, $b and serialnumber
                Question("#SerialNumber$Q2) #RandomInteger$a / #RandomInteger$b").
            EndQuestionsBlock().
    EndQuestion("B").
    Solution("A"). // uses the private scope of question body A
            SolutionHeader("Solution to exercise #SerialNumber$A :").
            SolutionBlock("Q1"). // uses the variables $a, $b and serialnumber from Q1 scope
                Solution("#Result$a$b").
            EndSolutionBlock().
            SolutionBlock("Q2"). // uses the variables $a, $b and serialnumber from Q2 scope
                Solution("#Result$a$b").
            EndSolutionBlock().
    EndSolution("A").
    Solution("B"). // uses the private scope of question body B
            SolutionHeader("Solution to exercise #SerialNumber$B :").
            SolutionBlock("Q1"). // uses the variables $a, $b and serialnumber from Q1 scope
                Solution("#Result$a$b").
            EndSolutionBlock().
            SolutionBlock("Q2"). // uses the variables $a, $b and serialnumber from Q2 scope
                Solution("#Result$a$b").
            EndSolutionBlock().
    EndSolution("B").
    EndExam();

// render 5 separate exams
ExamRenderer examrenderer = new ExamRenderer();
examrenderer.render(exam,5);



// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");



ExamBuilder exam = ExamBuilder.Create(/*Initialize services such as Randomizer etc*/).
    Question("A",// creates a private scope for question body A
            qb => qb.Text("Question #s$A").
            Text("Find the result of the following expressions : ").
            NewLine().
            QuestionSet(5, "Q1", // creates a private scope for variables $a, $b and serialnumber
                qb => qb.Text("#SerialNumber$Q1) #RandomInteger$a + #RandomInteger$b")).
            QuestionSet(5, "Q2", // creates a private scope for variables $a, $b and serialnumber
                qb => qb.Text("#SerialNumber$Q2) #RandomInteger$a + #RandomInteger$b"))).
    Question("B", qb => qb.Text("Question #s$B").
            Text("Find the result of the following expressions : ").
            QuestionSet(5, "Q1", // creates a private scope for variables $a, $b and serialnumber
                qb => qb.Text("#SerialNumber$Q1) #RandomInteger$a * #RandomInteger$b")).
            QuestionSet(5, "Q2", // creates a private scope for variables $a, $b and serialnumber
                qb => qb.Text("#SerialNumber$Q2) #RandomInteger$a / #RandomInteger$b"))).
    Solution("A", // uses the private scope of question body A
            sb => sb.Text("Solution to exercise #SerialNumber$A :").
            SolutionSet("Q1", // uses the variables $a, $b and serialnumber from Q1 scope
                sb => sb.Text("#Result$a$b")).
            SolutionSet("Q2", // uses the variables $a, $b and serialnumber from Q2 scope
                sb => sb.Text("#Result$a$b"))).
    Solution("B", // uses the private scope of question body B
            sb => sb.Text("Solution to exercise #SerialNumber$B :").
            SolutionSet("Q1", // uses the variables $a, $b and serialnumber from Q1 scope
                sb => sb.Text("#Result$a$b")).
            SolutionSet("Q2", // uses the variables $a, $b and serialnumber from Q2 scope
                sb => sb.Text("#Result$a$b")));


// render 5 separate exams
//ExamRenderer examrenderer = new ExamRenderer();
//examrenderer.render(exam,5);



using System;
using System.Collections.Generic;
using TextRenderer2;




public class ExamBuilder  {
    private Exam m_exam; // product
    public Exam MExam => m_exam;

    List<QuestionBuilder> m_questions = new List<QuestionBuilder>();
    List<SolutionBuilder> m_solutions = new List<SolutionBuilder>();

    public ExamBuilder() {
        m_exam = new Exam();
    }

    public static ExamBuilder Create() {
        return new ExamBuilder();
    }
    
    public ExamBuilder Question(string scopename,
        Func<QuestionBuilder,QuestionBuilder> question) {
        var qbuilder = new QuestionBuilder();
        var questionBuilder = question(qbuilder);
        m_questions.Add(questionBuilder);   
        return this;
    }

    public ExamBuilder Solution(string scopename,
        Func<SolutionBuilder, SolutionBuilder> solution) {
        var sbuilder = new SolutionBuilder();
        var solutionBuilder = solution(sbuilder);
        m_solutions.Add(sbuilder);
        return this;
    }
}



public class QuestionBuilder  {
    
    private Question m_question;
    public Question MQuestion => m_question;

    List<QuestionBuilder> m_questions = new List<QuestionBuilder>();

    public QuestionBuilder() {
        
    }

    public QuestionBuilder Text(string hypertext) {

        return this;
    }

    public QuestionBuilder NewLine() {

        return this;
    }

    public QuestionBuilder QuestionSet(UInt32 number, string scopename,
        Func<QuestionBuilder, QuestionBuilder> question) {


        return this;
    }
}


public class SolutionBuilder {

    public SolutionBuilder() { }


    public SolutionBuilder Text(string hypertext) {
        return this;
    }

    public SolutionBuilder NewLine() {
        return this;
    }

    public SolutionBuilder SolutionSet(string scopename,
        Func<SolutionBuilder, SolutionBuilder> solution) {


        return this;
    }
}
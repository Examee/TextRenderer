using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace TextRenderer3 {

   // The scope system is used to manage the symbol tables of the program
    // It is a singleton class that contains a dictionary of scopes
    // The sole object of the class is accessed through the Instance property
    public class CScopeSystem {
        private static CScopeSystem _instance;
        private static readonly object _lock = new object();

        Dictionary<string, CScope> m_scopes;

        // The root scope is the topmost scope in the symbol table
        private CExamScope m_root;
        // The current scope is the scope in which the current object is being built
        private CScope m_currentScope;
        public CScope MCurrentScope => m_currentScope;

        private CScopeSystem() {
            m_scopes = new Dictionary<string, CScope>();
        }

        public static CScopeSystem Instance {
            get {
                if (_instance == null) {
                    lock (_lock) {
                        if (_instance == null) {
                            _instance = new CScopeSystem();
                        }
                    }
                }
                return _instance;
            }
        }

        // Enter a new scope to the Scope System. The scope is identified by a unique ID
        // and has a parent scope from which this method is called. If the scope already
        // exists, the current scope is set to the existing scope. Otherwise, a new scope
        // is created and added to the symbol table
        public CExamScope EnterExamScope() {
            string scopeID = CExamScope.m_scopeID;
            if (m_scopes.ContainsKey(scopeID)) {
                // If the scope already exists, set the current scope to the existing scope
                SetCurrentScope(scopeID);
            }
            else {
                // Create a new scope... 
                CExamScope newScope = new CExamScope();
                // ...and add it to the symbol table
                m_root = newScope;
                m_scopes[scopeID] = newScope;
                // Set the current scope to the new scope
                SetCurrentScope(scopeID);
            }
            return (CExamScope)m_currentScope;
        }

        public CQuestionScope EnterQuestionScope(string scopeID, CScope parent) {
            if (m_scopes.ContainsKey(scopeID)) {
                // If the scope already exists, set the current scope to the existing scope
                SetCurrentScope(scopeID);
            } else {
                // Create a new scope... 
                CQuestionScope newScope = new CQuestionScope(scopeID, parent);
                // ...and add it to the symbol table
                m_scopes[scopeID] = newScope;
                
                // Set the current scope to the new scope
                SetCurrentScope(scopeID);
            }
            return (CQuestionScope)m_currentScope;
        }

        // Set the current scope to the scope identified by the scopeID
        public void SetCurrentScope(string scopeID) {
            if (!m_scopes.ContainsKey(scopeID)) {
                throw new KeyNotFoundException("Scope not found");
            }
            // Set the current scope to the scope identified by the scopeID
            m_currentScope = m_scopes[scopeID];
        }

        // Add an object to the current scope
        public void AddMacroToCurrentScope(string objectName, Func<string[], string> action) {
            m_currentScope.AddMacro(objectName, action);
        }

        // Get an object from the current scope
        public Func<string[], string> GetMacroFromCurrentScope(string objectName) {
            return m_currentScope.GetMacro(objectName);
        }

        public Func<string[], string> GetMacro(string scope,string objectName) {
            return m_scopes[scope].GetMacro(objectName);
        }

    }

    public abstract class CScope{
        // The exam scope contains a dictionary of macros applied on exam scope
        Dictionary<string, Func<string[], string>> m_symbolTableMacros;

        // The scope contains a dictionary for the questions that links question macro name 
        // with the question actual name
        Dictionary<string, string> m_symbolTable;

        // The parent symbol table is used to search for objects in the parent
        // scope if they are not found in the current scope
        private CScope m_parent;

        public CScope MParent => m_parent;

        protected CScope(CScope mParent) {
            m_symbolTableMacros = new Dictionary<string, Func<string[], string>>();
            m_symbolTable = new Dictionary<string, string>();
            m_parent = mParent;
        }
        public void AddMacro(string objectName, Func<string[], string> action) {
            m_symbolTableMacros[objectName] = action;
        }
        public Func<string[], string> GetMacro(string objectName)
        {
            if (m_symbolTableMacros.ContainsKey(objectName))
            {
                return m_symbolTableMacros[objectName];
            }
            if (m_parent != null)
            {
                return m_parent.GetMacro(objectName);
            }
            throw new KeyNotFoundException($"Macro '{objectName}' not found.");
        }
        public void AddValue(string key, string value) {
            m_symbolTable[key] = value;
        }
        public string GetValue(string key)
        {
            if (m_symbolTable.ContainsKey(key))
            {
                return m_symbolTable[key];
            }
            throw new KeyNotFoundException($"Object '{key}' not found in scope {this}.");
        }
    }
    
    public class CExamScope :CScope{
        
        public const string m_scopeID="Exam";
        public string ScopeID => m_scopeID;

        public CExamScope(): base(null) {
            
        }
    }

    public class CQuestionScope : CScope {
        string m_scopeID;
        public string ScopeID => m_scopeID;
        public CQuestionScope(string scopeID,CScope parent) : base(parent) {
            m_scopeID = scopeID;
        }
    }
}

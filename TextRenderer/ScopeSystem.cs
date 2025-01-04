using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRenderer {
    
    public class CScopeSystem {
        CScope m_rootScope;

        public CScopeSystem() {
            m_rootScope = new CScope(null);
        }
        
        public void AddScope(CScope parentScope, CScope childScope) {
            parentScope.AddChildScope(childScope);
        }
    }

    public class CScope {
        private List<CScope> m_childScopes;
        private CScope m_parentScope;
        Dictionary<string,int> m_variables;

        public CScope(CScope parentScope) {
            m_parentScope = parentScope;
            m_childScopes = new List<CScope>();
        }

        public void AddChildScope(CScope childScope) {
            m_childScopes.Add(childScope);
            childScope.m_parentScope = this;
        }


    }
}


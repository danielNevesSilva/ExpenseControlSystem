import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import { 
  AppBar, 
  Toolbar, 
  Typography, 
  Button, 
  Container,
  Box,
  CircularProgress,
  Alert
} from '@mui/material';
import { Home, People, Category, Receipt } from '@mui/icons-material';
import HomePage from './pages/Home';
import PersonsPage from './pages/PersonsPage';
import CategoriesPage from './pages/CategoriesPage';
import TransactionsPage from './pages/TransactionsPage';
import PersonTransactionsReport from './pages/PersonTransactionsReport';
import { personService } from './services/personService';
import './App.css';

function App() {
  const [backendStatus, setBackendStatus] = useState<'checking' | 'online' | 'offline'>('checking');
  const [, setTestData] = useState<any>(null);

  // Testa conexão com backend
  useEffect(() => {
    const testConnection = async () => {
      try {
        const data = await personService.getAll();
        setBackendStatus('online');
        setTestData(data);
      } catch (error) {
        setBackendStatus('offline');
      }
    };

    testConnection();
  }, []);

  return (
    <Router>
      <div className="App">
        <AppBar position="static">
          <Toolbar>
            <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
              Controle de Despesas
            </Typography>
            
            <Box sx={{ mr: 2, display: 'flex', alignItems: 'center' }}>
              {backendStatus === 'checking' && (
                <CircularProgress size={20} color="inherit" sx={{ mr: 1 }} />
              )}
              {backendStatus === 'online' && (
                <Box sx={{ display: 'flex', alignItems: 'center', color: 'lightgreen' }}>
                  ● Online
                </Box>
              )}
              {backendStatus === 'offline' && (
                <Box sx={{ display: 'flex', alignItems: 'center', color: 'lightcoral' }}>
                  ● Offline
                </Box>
              )}
            </Box>

            {}
            <Button color="inherit" component={Link} to="/" startIcon={<Home />}>
              Home
            </Button>
            <Button color="inherit" component={Link} to="/persons" startIcon={<People />}>
              Pessoas
            </Button>
            <Button color="inherit" component={Link} to="/categories" startIcon={<Category />}>
              Categorias
            </Button>
            <Button color="inherit" component={Link} to="/transactions" startIcon={<Receipt />}>
              Transações
            </Button>
            <Button color="inherit" component={Link} to="/person-transactions" startIcon={<Receipt />}>
              Relatório por Pessoa
            </Button>
             
          </Toolbar>
        </AppBar>

        <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
          {/* Alerta se backend offline */}
          {backendStatus === 'offline' && (
            <Alert severity="error" sx={{ mb: 3 }}>
              offline!
            </Alert>
          )}

          {/* Rotas */}
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/persons" element={<PersonsPage />} />
            <Route path="/categories" element={<CategoriesPage />} />
            <Route path="/transactions" element={<TransactionsPage />} />
            <Route path="/person-transactions" element={<PersonTransactionsReport />} />
          </Routes>
        </Container>

        {/* Footer */}
        <Box component="footer" sx={{ py: 3, textAlign: 'center', color: 'text.secondary' }}>
          <Typography variant="body2">
            Sistema de Controle de Despesas • {new Date().getFullYear()}
          </Typography>
        </Box>
      </div>
    </Router>
  );
}

export default App;